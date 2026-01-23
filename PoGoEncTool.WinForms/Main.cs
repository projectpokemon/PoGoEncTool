using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using PKHeX.Core;
using PKHeX.Drawing.PokeSprite;
using PoGoEncTool.Core;

namespace PoGoEncTool.WinForms;

public partial class Main : Form
{
    private readonly ProgramSettings Settings;
    private readonly PogoEncounterList Entries;

    private PogoPoke CurrentPoke = PogoPoke.CreateNew(1, 0);
    private PogoEntry CurrentEntry = PogoEntry.CreateNew();

    private bool ChangingFields = true;
    private DateTime LastSaved = DateTime.Now;
    private int CurrentSpecies = 1;

    public Main()
    {
        Entries = DataLoader.GetData(Application.StartupPath, out Settings);
        if (Settings.IsDarkMode)
            Application.SetColorMode(SystemColorMode.Dark);

        InitializeComponent();
        SpriteUtil.ChangeMode(SpriteBuilderMode.SpritesArtwork5668);

        if (Application.IsDarkModeEnabled)
            ReformatDark(Controls);

        // Entries.ModifyAll(e => e.Comment.Contains("Purified"), e => e.Type = Core.PogoType.Shadow);
        // BulkActions.AddBossEncounters(Entries);
        // BulkActions.AddNewShadows(Entries);
        // BulkActions.AddMonthlyRaidBosses(Entries);

        LoadEntries();
        InitializeDataSources();
    }

    private static void ReformatDark(Control.ControlCollection controls)
    {
        foreach (Control control in controls)
        {
            ReformatDark(control.Controls);

            if (control is ListBox lb)
                lb.BorderStyle = BorderStyle.None;
            else if (control is Button b)
                b.FlatStyle = FlatStyle.Popup;
            else if (control is TextBoxBase t)
                t.BorderStyle = BorderStyle.None;
        }
    }

    private void InitializeDataSources()
    {
        var gi = GameInfo.Sources;
        CB_Species.DisplayMember = nameof(ComboItem.Text);
        CB_Species.ValueMember = nameof(ComboItem.Value);
        CB_Species.DataSource = new BindingSource(gi.SpeciesDataSource, null!);
        ChangingFields = false;
        CB_Species.SelectedValue = 1;
    }

    private void LoadEntries()
    {
        var species = GameInfo.Strings.Species;
        var entries = species.Select((z, i) => $"{i:0000} - {z}").ToArray();
        LB_Species.Items.AddRange(entries);
    }

    private void Main_FormClosing(object sender, FormClosingEventArgs e)
    {
        var now = DateTime.Now;
        var delta = now.Subtract(LastSaved);
        if (delta <= TimeSpan.FromSeconds(10))
            return;

        System.Media.SystemSounds.Asterisk.Play();
        var formatted = string.Format("{0:%h}h {0:%m}m {0:%s}.{0:ff}s", delta);
        var prompt = WinFormsUtil.Prompt(MessageBoxButtons.YesNo, $"Changes were last saved {formatted} ago. Are you sure you want to exit?");
        if (prompt != DialogResult.Yes)
            e.Cancel = true;
    }

    private void B_Save_Click(object sender, EventArgs e)
    {
        SaveEntry(CurrentEntry);

        var errors = Entries.SanityCheck().ToList();
        if (errors.Count > 0)
        {
            WinFormsUtil.Alert("Errors found, canceling save request.", string.Join(Environment.NewLine, errors));
            return;
        }

        DataLoader.SaveAllData(Application.StartupPath, Entries, Settings.DataPath);
        System.Media.SystemSounds.Asterisk.Play();
        LastSaved = DateTime.Now;
    }

    private void CB_Species_SelectedValueChanged(object sender, EventArgs e)
    {
        if (ChangingFields)
            return;
        ChangingFields = true;
        var species = (int?)CB_Species.SelectedValue ?? 0;
        LB_Species.SelectedIndex = species;
        ChangingFields = false;
        LoadSpecies((ushort)species);
    }

    private void LB_Species_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ChangingFields)
            return;
        ChangingFields = true;
        var species = LB_Species.SelectedIndex;
        CB_Species.SelectedValue = species;
        ChangingFields = false;
        LoadSpecies((ushort)species);
    }

    private void LoadSpecies(in ushort species)
    {
        var forms = FormConverter.GetFormList(species, GameInfo.Strings.Types, GameInfo.Strings.forms, GameInfo.GenderSymbolASCII, Latest.Context);
        var prefer = species switch
        {
          //(ushort)Species.Minior => 7,
            (ushort)Species.Gimmighoul => 1, // GO is always Roaming Form
            _ => 0,
        };
        CB_Form.Items.Clear();
        CB_Form.Items.AddRange(forms);
        CB_Form.SelectedIndex = prefer;
        CB_Form.Enabled = forms.Length > 1;
        CurrentSpecies = species;
    }

    private void CB_Form_SelectedIndexChanged(object sender, EventArgs e)
    {
        var species = (int?)CB_Species.SelectedValue ?? 0;
        var form = CB_Form.SelectedIndex;
        if (form < 0)
            return;

        LoadPoke((ushort)species, (byte)form);
    }

    private void LoadPoke(ushort species, byte form)
    {
        var entry = Entries.GetDetails(species, form);
        LoadPoke(entry);
        LoadEntry(CurrentEntry, species, form);
    }

    private void LoadPoke(PogoPoke poke)
    {
        LB_Appearances.Items.Clear();
        LB_Appearances.Items.AddRange(poke.Data.ToArray());
        CurrentPoke = poke;
        CHK_Available.Checked = poke.Available;
        ChangeRowCount(0);
    }

    private void CHK_Available_CheckedChanged(object sender, EventArgs e) => CurrentPoke.Available = CHK_Available.Checked;

    private void ChangeRowCount(int i)
    {
        if (LB_Appearances.Items.Count == 0)
        {
            pogoRow1.Visible = false;
        }
        else
        {
            int index = Math.Min(LB_Appearances.Items.Count - 1, i);
            LB_Appearances.SelectedIndex = index;
        }
    }

    private void LoadEntry(PogoEntry entry, ushort species, byte form)
    {
        var comment = entry.Comment;
        if (comment.StartsWith("Mega Raid Boss") || comment.StartsWith("Primal Raid Boss") || comment.Contains("Elite Raid: Mega"))
            form = GetMegaFormIndex(comment, species, form);

        if (!pogoRow1.Visible)
            form = (byte)CB_Form.SelectedIndex;

        var gender = (byte)(entry.Gender - 1);
        var shiny = entry.Shiny == PogoShiny.Always ? Shiny.Always : Shiny.Never;
        PB_Poke.Image = SpriteUtil.GetSprite(species, form, gender, 0, 0, false, shiny, Latest.Context);

        pogoRow1.LoadEntry(CurrentEntry = entry);
    }

    private static byte GetMegaFormIndex(string comment, ushort species, byte form)
    {
        // Mega X, Mega Y
        if (species is (int)Species.Charizard or (int)Species.Raichu or (int)Species.Mewtwo)
        {
            var shift = species is (int)Species.Raichu ? 1 : 0;
            return comment.Contains($"Mega {(Species)species} Y") ? (byte)(shift + 2) : (byte)(shift + 1);
        }

        // Mega Z
        if (species is (int)Species.Absol or (int)Species.Garchomp or (int)Species.Lucario)
        {
            return comment.Contains($"Mega {(Species)species} Z") ? (byte)2 : (byte)1;
        }

        return species switch
        {
            (int)Species.Greninja => 3,
            (int)Species.Floette => 6,
            (int)Species.Meowstic => (byte)(form + 2),
            (int)Species.Zygarde => 5,
            (int)Species.Magearna => (byte)(form + 2),
            (int)Species.Tatsugiri => (byte)(form + 3),
            _ => 1,
        };
    }

    private void SaveEntry(PogoEntry entry) => pogoRow1.SaveEntry(entry);

    private void B_AddNew_Click(object sender, EventArgs e)
    {
        var entry = PogoEntry.CreateNew();
        CurrentPoke.Add(entry);
        LB_Appearances.Items.Add(entry);
        LB_Appearances.SelectedIndex = LB_Appearances.Items.Count - 1;
    }

    private void B_DeleteSelected_Click(object sender, EventArgs e)
    {
        var selected = LB_Appearances.SelectedItems.Cast<PogoEntry>().ToArray();
        foreach (var entry in selected)
        {
            CurrentPoke.Data.Remove(entry);
            LB_Appearances.Items.Remove(entry);
            entry.Clear();
        }
    }

    private void B_DeleteAll_Click(object sender, EventArgs e)
    {
        if (CurrentPoke.Data.Count == 0)
            return;
        if (WinFormsUtil.Prompt(MessageBoxButtons.YesNo, "Delete all entries for this Pokémon?", "Cannot be undone.") != DialogResult.Yes)
            return;
        CurrentPoke.Data.Clear();
        LB_Appearances.Items.Clear();
        System.Media.SystemSounds.Asterisk.Play();
    }

    private void LB_Appearances_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadNewAppearance(LB_Appearances.SelectedIndex);
    }

    private void LoadNewAppearance(int index)
    {
        if (index < 0)
        {
            pogoRow1.Visible = false;
            return;
        }

        pogoRow1.Visible = true;
        SaveEntry(CurrentEntry);
        LoadEntry(CurrentPoke[index], CurrentPoke.Species, CurrentPoke.Form);

        RefreshAppearanceText();
    }

    private void RefreshAppearanceText()
    {
        LB_Appearances.DrawMode = DrawMode.OwnerDrawFixed;
        LB_Appearances.DrawMode = DrawMode.Normal;
    }

    private void L_Serebii_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenWebpage($"https://www.serebii.net/pokemongo/pokemon/{CurrentSpecies:000}.shtml");
    private void L_PGFandom_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenWebpage($"https://pokemongo.fandom.com/wiki/{GetSpeciesNameURL()}#Availability");

    private static void OpenWebpage(string url)
    {
        var psi = new ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true,
        };
        Process.Start(psi);
    }

    private string GetSpeciesNameURL()
    {
        var species = GameInfo.Strings.Species;
        var current = species[CurrentSpecies];
        return current.Replace("’", "\'"); // Farfetch’d and Sirfetch’d
    }

    private void B_MarkEvosAvailable_Click(object sender, EventArgs e)
    {
        var species = (ushort)CurrentSpecies;
        var form = (byte)CB_Form.SelectedIndex;

        var evos = EvoUtil.GetEvoSpecForms(species, form)
            .Where(z => EvoUtil.IsAllowedEvolution(species, form, z.Species, z.Form)).ToArray();

        if (evos.Length == 0)
        {
            WinFormsUtil.Alert("The current Pokémon cannot evolve into anything; no results found to modify.");
            return;
        }

        var names = evos.Select(z => $"{SpeciesName.GetSpeciesName(z.Species, 2)}{(z.Form == 0 ? "" : $"-{z.Form}")}");
        var prompt = string.Join(Environment.NewLine, names);

        var result = WinFormsUtil.Prompt(MessageBoxButtons.YesNo, "Mark the following Pokémon as available?", prompt);
        if (result != DialogResult.Yes)
            return;

        foreach (var evo in evos)
        {
            var parent = Entries.GetDetails(evo.Species, evo.Form);
            parent.Available = true;
        }
        System.Media.SystemSounds.Asterisk.Play();
    }

    private void B_CopyToForms_Click(object sender, EventArgs e)
    {
        if (CB_Form.Items.Count <= 1)
        {
            WinFormsUtil.Alert("The current Pokémon does not have other forms; no results found to copy to.");
            return;
        }

        var species = (ushort)CurrentSpecies;
        var form = (byte)CB_Form.SelectedIndex;
        List<ComboItem> entries = [];
        for (int f = 0; f < CB_Form.Items.Count; f++)
        {
            if (f == form)
                continue;
            var entry = CB_Form.Items[f]!;
            var combo = new ComboItem((string)entry, f);
            entries.Add(combo);
        }

        var names = entries.Select(z => $"{z.Value}-{z.Text}");
        var prompt = string.Join(Environment.NewLine, names);
        var result = WinFormsUtil.Prompt(MessageBoxButtons.YesNo, "Copy the current edit-fields to the following entries as a NEW ENTRY?", prompt);
        if (result != DialogResult.Yes)
            return;

        foreach (var formCombo in entries)
        {
            var detail = PogoEntry.CreateNew();
            pogoRow1.SaveEntry(detail);

            var parent = Entries.GetDetails(species, (byte)formCombo.Value);
            if (!parent.Available)
                continue;
            parent.Add(detail);
        }
        System.Media.SystemSounds.Asterisk.Play();
    }

    private void B_DumpAll_Click(object sender, EventArgs e)
    {
        var names = GameInfo.Strings.Species;
        var species = CB_Species.Items;
        var list = new List<string>();
        const string filter = "";

        for (int i = 1; i < species.Count; i++)
        {
            var forms = FormConverter.GetFormList((ushort)i, GameInfo.Strings.Types, GameInfo.Strings.forms, GameInfo.GenderSymbolASCII, Latest.Context);
            for (int f = 0; f <= forms.Length; f++)
            {
                var current = Entries.GetDetails((ushort)i, (byte)f);
                if (current.Data.Count == 0)
                    continue;
                if (!current.Data.Any(z => z.Comment.Contains(filter)))
                    continue;

                var form = forms.Length == 1 ? string.Empty : f == 0 ? $" ({forms[f]})" : $"-{f} ({forms[f]})";
                var name = $"{i:0000} {names[current.Species]}{form}";
                list.Add(name);

                foreach (var enc in current.Data)
                {
                    if (!enc.Comment.Contains(filter))
                        continue;

                    var start = enc.Start?.ToString();
                    var end = enc.End == null ? "XXXX.XX.XX" : enc.End.ToString();

                    var shiny = enc.Shiny switch
                    {
                        PogoShiny.Random => " (Shiny: Random)",
                        PogoShiny.Always => " (Shiny: Always)",
                        _ => " (Shiny:  Never)",
                    };

                    var gender = enc.Gender switch
                    {
                        PogoGender.MaleOnly => " (Gender: ♂)",
                        PogoGender.FemaleOnly => " (Gender: ♀)",
                        _ => string.Empty,
                    };

                    var method = (int)enc.Type switch
                    {
                        1 => "Wild",
                        2 => "Egg",
                        3 => "12 km Egg",
                        10 or 11 or 12 or 15 or 16 or 17 => "Raid",
                        13 or 14 or 18 or 19 => "Shadow Raid",
                        (>= 20 and <= 89) or 254 or 255 => "Research",
                        >= 90 and <= 99 => "GO Battle League",
                        >= 100 and <= 109 => "Shadow",
                        >= 110 and <= 119 => "Max Battle",
                        _ => throw new Exception("Invalid PogoType"),
                    };

                    var type = $" ({method}) ".PadRight(20, ' ');
                    var line = $"{start} to {end}{type}{shiny}{gender} - {enc.Comment}";
                    list.Add(line);
                }

                list.Add("");
            }
        }

        var path = Path.Combine(Directory.GetCurrentDirectory(), "encounter_dump.txt");
        File.WriteAllLines(path, list);

        // start explorer process
        var psi = new ProcessStartInfo
        {
            FileName = "explorer.exe",
            Arguments = $"/select,\"{path}\"",
            UseShellExecute = true,
        };
        Process.Start(psi);

        System.Media.SystemSounds.Asterisk.Play();
    }
}
