using System;
using System.Linq;
using System.Windows.Forms;
using PKHeX.Core;

namespace PoGoEncTool
{
    public partial class PogoRow : UserControl
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

        public void LoadEntry(PogoEntry entry)
        {
            CHK_Shiny.Checked = entry.Shiny;

            DT_Start.Value = GetDateTime(entry.Start);
            DT_Start.Checked = entry.Start != null;
            DT_End.Value = GetDateTime(entry.End);
            DT_End.Checked = entry.End != null;

            CB_Type.SelectedValue = (int)entry.Type;

            TB_Comment.Text = entry.Comment;
        }

        private static DateTime GetDateTime(PogoDate? date) => date == null ? DateTime.Now : new DateTime(date.Y, date.M, date.D);

        public void SaveEntry(PogoEntry entry)
        {
            entry.Shiny = CHK_Shiny.Checked;

            entry.Start = !DT_Start.Checked ? null : new PogoDate(DT_Start.Value);
            entry.End = !DT_End.Checked ? null : new PogoDate(DT_End.Value);

            entry.Type = (PogoType)(int)CB_Type.SelectedValue;

            entry.Comment = TB_Comment.Text;
        }
    }
}
