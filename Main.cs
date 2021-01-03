using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using PKHeX.Core;
using PKHeX.Drawing;

namespace PoGoEncTool
{
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

            Entries = DataLoader.GetData(Application.StartupPath, out Settings);
            // Entries = new PogoEncounterList(EncountersGO.CreateSeed());
            // Entries.ModifyAll(e => e.Comment.Contains("Purified"), e => e.Type = PogoType.Shadow);
            // Entries.ModifyAll(_ => true, e => e.Available = e.Data.Count != 0);
            // Entries.ReapplyDuplicates();

            LoadEntries();
            InitializeDataSources();
        }

        private void InitializeDataSources()
        {
            var gi = GameInfo.SpeciesDataSource;
            CB_Species.DisplayMember = nameof(ComboItem.Text);
            CB_Species.ValueMember = nameof(ComboItem.Value);
            CB_Species.DataSource = new BindingSource(gi, null);
            ChangingFields = false;
            CB_Species.SelectedValue = (int)Species.Bulbasaur;
        }

        private void LoadEntries()
        {
            var species = GameInfo.Strings.Species;
            var entries = species.Select((z, i) => $"{i:000} - {z}").ToArray();
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

            DataLoader.SaveData(Application.StartupPath, Entries, Settings.DataPath);
            System.Media.SystemSounds.Asterisk.Play();
            LastSaved = DateTime.Now;
        }

        private void CB_Species_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ChangingFields)
                return;
            ChangingFields = true;
            var species = (int)CB_Species.SelectedValue;
            LB_Species.SelectedIndex = species;
            ChangingFields = false;
            LoadSpecies(species);
        }

        private void LB_Species_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ChangingFields)
                return;
            ChangingFields = true;
            var species = LB_Species.SelectedIndex;
            CB_Species.SelectedValue = species;
            ChangingFields = false;
            LoadSpecies(species);
        }

        private void LoadSpecies(in int species)
        {
            var forms = FormConverter.GetFormList(species, GameInfo.Strings.Types, GameInfo.Strings.forms, GameInfo.GenderSymbolASCII, PKX.Generation);
            CB_Form.Items.Clear();
            CB_Form.Items.AddRange(forms);
            CB_Form.SelectedIndex = 0;
            CB_Form.Enabled = forms.Length > 1;
            CurrentSpecies = species;
        }

        private void CB_Form_SelectedIndexChanged(object sender, EventArgs e)
        {
            var species = (int)CB_Species.SelectedValue;
            var form = CB_Form.SelectedIndex;
            if (form < 0)
                return;

            LoadPoke(species, form);
        }

        private void LoadPoke(int species, int form)
        {
            var entry = Entries.GetDetails(species, form);
            LoadPoke(entry);
            PB_Poke.Image = SpriteUtil.GetSprite(species, form, 0, 0, 0, false, false);
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

        private void LoadEntry(PogoEntry entry) => pogoRow1.LoadEntry(CurrentEntry = entry);
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
                entry.Type = PogoType.None;
                CurrentPoke.Data.Remove(entry);
                LB_Appearances.Items.Remove(entry);
            }
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
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private string GetSpeciesNameURL()
        {
            var species = GameInfo.Strings.Species;
            var current = species[CurrentSpecies];
            return current.Replace("’", "\'"); // Farfetch’d and Sirfetch’d
        }

        private void B_CopyTo_Click(object sender, EventArgs e)
        {
            var species = CurrentSpecies;
            var form = CB_Form.SelectedIndex;

            var evos = EvoUtil.GetEvoSpecForms(species, form)
                .Select(z => new { Species = z & 0x7FF, Form = z >> 11 })
                .Where(z => EvoUtil.IsAllowedEvolution(species, form, z.Species, z.Form)
                            && Entries.GetDetails(z.Species, z.Form).Available)
                .ToArray();
            if (evos.Length == 0)
            {
                WinFormsUtil.Alert("The current Pokémon cannot evolve into anything; no results found to copy to.");
                return;
            }

            var names = evos.Select(z => $"{SpeciesName.GetSpeciesName(z.Species, 2)}{(z.Form == 0 ? "" : $"-{z.Form}")}");
            var prompt = string.Join(Environment.NewLine, names);

            var result = WinFormsUtil.Prompt(MessageBoxButtons.YesNo, "Copy the current edit-fields to the following entries as a NEW ENTRY?", prompt);
            if (result != DialogResult.Yes)
                return;

            foreach (var evo in evos)
            {
                var detail = PogoEntry.CreateNew();
                pogoRow1.SaveEntry(detail);

                var parent = Entries.GetDetails(evo.Species, evo.Form);
                parent.Add(detail);
            }
            System.Media.SystemSounds.Asterisk.Play();
        }
    }
}
