using System;
using System.Linq;
using System.Windows.Forms;
using PKHeX.Core;

namespace PoGoEncTool
{
    public partial class Main : Form
    {
        private readonly ProgramSettings Settings;
        private readonly PogoEncounterList Entries;

        private PogoPoke CurrentPoke = PogoPoke.CreateNew(1, 0);
        private PogoEntry CurrentEntry = PogoEntry.CreateNew();

        private bool ChangingFields = true;

        public Main()
        {
            InitializeComponent();

            Entries = DataLoader.GetData(Application.StartupPath, out Settings);
            // Entries = new PogoEncounterList(EncountersGO.CreateSeed());
            // Entries.ModifyAll(e => e.Comment.Contains("Purified"), e => e.Type = PogoType.Shadow);

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

        private void B_Save_Click(object sender, EventArgs e)
        {
            SaveEntry(CurrentEntry);
            SaveData(Entries, Settings.DataPath);
        }

        private static void SaveData(PogoEncounterList entries, string path)
        {
            DataLoader.SaveData(Application.StartupPath, entries, path);
            System.Media.SystemSounds.Asterisk.Play();
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
        }

        private void LoadPoke(PogoPoke poke)
        {
            LB_Appearances.Items.Clear();
            LB_Appearances.Items.AddRange(poke.Data.ToArray());
            CurrentPoke = poke;
            ChangeRowCount(0);
        }

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
    }
}
