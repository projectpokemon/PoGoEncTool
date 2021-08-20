using System;
using System.Linq;
using System.Windows.Forms;
using PoGoEncTool.Core;

namespace PoGoEncTool.WinForms
{
    public sealed partial class PogoRow : UserControl
    {
        public PogoRow()
        {
            InitializeComponent();

            var names = Enum.GetNames<PogoType>();
            var values = Enum.GetValues<PogoType>();
            var ds = names.Select((z, i) => new ComboItem(z, (int) values[i]));
            CB_Type.DisplayMember = nameof(ComboItem.Text);
            CB_Type.ValueMember = nameof(ComboItem.Value);
            CB_Type.DataSource = new BindingSource(ds, null);
        }

        /// <summary>
        /// Key Value pair for a displayed <see cref="T:System.String" /> and underlying <see cref="T:System.Int32" /> value.
        /// </summary>
        public record ComboItem(string Text, int Value);

        public void LoadEntry(PogoEntry entry)
        {
            CHK_Shiny.CheckState = entry.Shiny switch
            {
                PogoShiny.Always => CheckState.Checked,
                PogoShiny.Random => CheckState.Indeterminate,
                _ => CheckState.Unchecked,
            };

            CHK_MaleOnly.Checked = entry.Gender == PogoGender.MaleOnly;
            CHK_FemaleOnly.Checked = entry.Gender == PogoGender.FemaleOnly;

            DT_Start.Value = PogoDate.GetDateTime(entry.Start);
            DT_Start.Checked = entry.Start != null;
            DT_End.Value = PogoDate.GetDateTime(entry.End);
            DT_End.Checked = entry.End != null;

            CHK_Localized.Checked = entry.LocalizedStart;
            CHK_EndTolerance.Checked = !entry.NoEndTolerance;

            CB_Type.SelectedValue = (int)entry.Type;

            TB_Comment.Text = entry.Comment;
        }

        public void SaveEntry(PogoEntry entry)
        {
            entry.Shiny = CHK_Shiny.CheckState switch
            {
                CheckState.Checked => PogoShiny.Always,
                CheckState.Indeterminate => PogoShiny.Random,
                _ => PogoShiny.Never,
            };

            entry.Gender = CHK_MaleOnly.Checked ? PogoGender.MaleOnly : CHK_FemaleOnly.Checked ? PogoGender.FemaleOnly : PogoGender.Random;

            entry.Start = !DT_Start.Checked ? null : new PogoDate(DT_Start.Value);
            entry.End = !DT_End.Checked ? null : new PogoDate(DT_End.Value);

            entry.Type = (PogoType)(int)CB_Type.SelectedValue;

            entry.LocalizedStart = CHK_Localized.Checked;
            entry.NoEndTolerance = !CHK_EndTolerance.Checked;

            entry.Comment = TB_Comment.Text;
        }
    }
}
