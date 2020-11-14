
namespace PoGoEncTool
{
    partial class PogoRow
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DT_End = new System.Windows.Forms.DateTimePicker();
            this.DT_Start = new System.Windows.Forms.DateTimePicker();
            this.L_Start = new System.Windows.Forms.Label();
            this.FLP_Parent = new System.Windows.Forms.FlowLayoutPanel();
            this.L_End = new System.Windows.Forms.Label();
            this.CB_Type = new System.Windows.Forms.ComboBox();
            this.CHK_Shiny = new System.Windows.Forms.CheckBox();
            this.TB_Comment = new System.Windows.Forms.TextBox();
            this.FLP_Parent.SuspendLayout();
            this.SuspendLayout();
            // 
            // DT_End
            // 
            this.DT_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DT_End.Location = new System.Drawing.Point(187, 3);
            this.DT_End.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.DT_End.Name = "DT_End";
            this.DT_End.ShowCheckBox = true;
            this.DT_End.Size = new System.Drawing.Size(102, 23);
            this.DT_End.TabIndex = 0;
            // 
            // DT_Start
            // 
            this.DT_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DT_Start.Location = new System.Drawing.Point(43, 3);
            this.DT_Start.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.DT_Start.Name = "DT_Start";
            this.DT_Start.ShowCheckBox = true;
            this.DT_Start.Size = new System.Drawing.Size(102, 23);
            this.DT_Start.TabIndex = 1;
            // 
            // L_Start
            // 
            this.L_Start.AutoSize = true;
            this.L_Start.Location = new System.Drawing.Point(3, 7);
            this.L_Start.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.L_Start.Name = "L_Start";
            this.L_Start.Size = new System.Drawing.Size(34, 15);
            this.L_Start.TabIndex = 2;
            this.L_Start.Text = "Start:";
            // 
            // FLP_Parent
            // 
            this.FLP_Parent.Controls.Add(this.L_Start);
            this.FLP_Parent.Controls.Add(this.DT_Start);
            this.FLP_Parent.Controls.Add(this.L_End);
            this.FLP_Parent.Controls.Add(this.DT_End);
            this.FLP_Parent.Controls.Add(this.CB_Type);
            this.FLP_Parent.Controls.Add(this.CHK_Shiny);
            this.FLP_Parent.Controls.Add(this.TB_Comment);
            this.FLP_Parent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FLP_Parent.Location = new System.Drawing.Point(0, 0);
            this.FLP_Parent.Name = "FLP_Parent";
            this.FLP_Parent.Size = new System.Drawing.Size(465, 61);
            this.FLP_Parent.TabIndex = 3;
            // 
            // L_End
            // 
            this.L_End.AutoSize = true;
            this.L_End.Location = new System.Drawing.Point(151, 7);
            this.L_End.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.L_End.Name = "L_End";
            this.L_End.Size = new System.Drawing.Size(30, 15);
            this.L_End.TabIndex = 3;
            this.L_End.Text = "End:";
            // 
            // CB_Type
            // 
            this.CB_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Type.FormattingEnabled = true;
            this.CB_Type.Location = new System.Drawing.Point(295, 3);
            this.CB_Type.Name = "CB_Type";
            this.CB_Type.Size = new System.Drawing.Size(86, 23);
            this.CB_Type.TabIndex = 5;
            // 
            // CHK_Shiny
            // 
            this.CHK_Shiny.AutoSize = true;
            this.CHK_Shiny.Location = new System.Drawing.Point(387, 5);
            this.CHK_Shiny.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.CHK_Shiny.Name = "CHK_Shiny";
            this.CHK_Shiny.Size = new System.Drawing.Size(55, 19);
            this.CHK_Shiny.TabIndex = 6;
            this.CHK_Shiny.Text = "Shiny";
            this.CHK_Shiny.UseVisualStyleBackColor = true;
            // 
            // TB_Comment
            // 
            this.TB_Comment.Location = new System.Drawing.Point(3, 32);
            this.TB_Comment.Name = "TB_Comment";
            this.TB_Comment.Size = new System.Drawing.Size(439, 23);
            this.TB_Comment.TabIndex = 7;
            // 
            // PogoRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FLP_Parent);
            this.Name = "PogoRow";
            this.Size = new System.Drawing.Size(465, 61);
            this.FLP_Parent.ResumeLayout(false);
            this.FLP_Parent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker DT_End;
        private System.Windows.Forms.DateTimePicker DT_Start;
        private System.Windows.Forms.Label L_Start;
        private System.Windows.Forms.FlowLayoutPanel FLP_Parent;
        private System.Windows.Forms.Label L_End;
        private System.Windows.Forms.ComboBox CB_Type;
        private System.Windows.Forms.CheckBox CHK_Shiny;
        private System.Windows.Forms.TextBox TB_Comment;
    }
}
