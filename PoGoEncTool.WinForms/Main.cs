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
        InitializeComponent();
        SpriteUtil.ChangeMode(SpriteBuilderMode.SpritesArtwork5668);

        Entries = DataLoader.GetData(Application.StartupPath, out Settings);
        // Entries.ModifyAll(e => e.Comment.Contains("Purified"), e => e.Type = Core.PogoType.Shadow);
        // BulkActions.AddBossEncounters(Entries);
        // BulkActions.AddNewShadows(Entries);
        // BulkActions.AddMonthlyRaidBosses(Entries);

        LoadEntries();
        InitializeDataSources();
    }

    private void InitializeDataSources()
    {
        var gi = GameInfo.SpeciesDataSource;
        CB_Species.DisplayMember = nameof(ComboItem.Text);
        CB_Species.ValueMember = nameof(ComboItem.Value);
        CB_Species.DataSource = new BindingSource(gi, null!);
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
        var prompt = WinFormsUtil.Prompt(MessageBoxButtons.YesNo, $"Changes were last saved {delta:g} ago. Are you sure you want to exit?");
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
            (ushort)Species.Gimmighoul => 1,
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
        LoadEntry(CurrentEntry);
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

    private void LoadEntry(PogoEntry entry)
    {
        var species = Convert.ToUInt16(CB_Species.SelectedValue);
        var comment = entry.Comment;
        byte form = comment switch
        {
            _ when comment.Contains("Mega Charizard Y") || comment.Contains("Mega Mewtwo Y") => 2,
            _ when comment.Contains("Mega Raid Boss") || comment.Contains("Primal Raid Boss") || comment.Contains("Elite Raid: Mega") => 1,
            _ => (byte)CB_Form.SelectedIndex,
        };

        if (!pogoRow1.Visible)
            form = (byte)CB_Form.SelectedIndex;

        var gender = (byte)(entry.Gender - 1);
        var shiny = entry.Shiny == PogoShiny.Always ? Shiny.Always : Shiny.Never;
        PB_Poke.Image = SpriteUtil.GetSprite(species, form, gender, 0, 0, false, shiny);

        pogoRow1.LoadEntry(CurrentEntry = entry);
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
        LoadEntry(CurrentPoke[index]);

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

                var form = f == 0 ? string.Empty : $"-{f} ({forms[f]})";
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
                        3 => "Strange Egg",
                        10 or 11 or 12 => "Raid",
                        13 => "Shadow Raid",
                        >= 20 and <= 39 or 200 or 201 => "Research",
                        40 or 41 => "GO Battle League",
                        42 => "GO Battle Day",
                        50 => "Shadow",
                        >= 60 and <= 69 => "Max Battle",
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
        System.Media.SystemSounds.Asterisk.Play();
    }
}
