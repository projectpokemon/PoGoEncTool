using System;
using System.Windows.Forms;

namespace PoGoEncTool
{
    public partial class PogoRow : UserControl
    {
        public PogoRow()
        {
            InitializeComponent();
            CB_Type.Items.AddRange(Enum.GetNames<PogoType>());
        }

        public void LoadEntry(PogoEntry entry)
        {
            CHK_Shiny.Checked = entry.Shiny;

            DT_Start.Value = GetDateTime(entry.Start);
            DT_Start.Checked = entry.Start != null;
            DT_End.Value = GetDateTime(entry.End);
            DT_End.Checked = entry.End != null;

            CB_Type.SelectedIndex = (int)entry.Type;

            TB_Comment.Text = entry.Comment;
        }

        private static DateTime GetDateTime(PogoDate? date) => date == null ? DateTime.Now : new DateTime(date.Y, date.M, date.D);

        public void SaveEntry(PogoEntry entry)
        {
            entry.Shiny = CHK_Shiny.Checked;

            entry.Start = !DT_Start.Checked ? null : new PogoDate(DT_Start.Value);
            entry.End = !DT_End.Checked ? null : new PogoDate(DT_End.Value);

            entry.Type = (PogoType)CB_Type.SelectedIndex;

            entry.Comment = TB_Comment.Text;
        }
    }
}
