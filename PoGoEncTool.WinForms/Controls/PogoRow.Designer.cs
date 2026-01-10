namespace PoGoEncTool.WinForms
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
            DT_End = new System.Windows.Forms.DateTimePicker();
            DT_Start = new System.Windows.Forms.DateTimePicker();
            L_Start = new System.Windows.Forms.Label();
            FLP_Parent = new System.Windows.Forms.FlowLayoutPanel();
            CHK_Localized = new System.Windows.Forms.CheckBox();
            L_End = new System.Windows.Forms.Label();
            CHK_EndTolerance = new System.Windows.Forms.CheckBox();
            CB_Type = new System.Windows.Forms.ComboBox();
            CHK_Shiny = new System.Windows.Forms.CheckBox();
            CHK_MaleOnly = new System.Windows.Forms.CheckBox();
            CHK_FemaleOnly = new System.Windows.Forms.CheckBox();
            TB_Comment = new System.Windows.Forms.TextBox();
            FLP_Parent.SuspendLayout();
            SuspendLayout();
            // 
            // DT_End
            // 
            DT_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            DT_End.Location = new System.Drawing.Point(258, 3);
            DT_End.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            DT_End.Name = "DT_End";
            DT_End.ShowCheckBox = true;
            DT_End.Size = new System.Drawing.Size(121, 25);
            DT_End.TabIndex = 0;
            // 
            // DT_Start
            // 
            DT_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            DT_Start.Location = new System.Drawing.Point(47, 3);
            DT_Start.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            DT_Start.Name = "DT_Start";
            DT_Start.ShowCheckBox = true;
            DT_Start.Size = new System.Drawing.Size(121, 25);
            DT_Start.TabIndex = 1;
            // 
            // L_Start
            // 
            L_Start.AutoSize = true;
            L_Start.Location = new System.Drawing.Point(3, 8);
            L_Start.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            L_Start.Name = "L_Start";
            L_Start.Size = new System.Drawing.Size(38, 17);
            L_Start.TabIndex = 2;
            L_Start.Text = "Start:";
            // 
            // FLP_Parent
            // 
            FLP_Parent.Controls.Add(L_Start);
            FLP_Parent.Controls.Add(DT_Start);
            FLP_Parent.Controls.Add(CHK_Localized);
            FLP_Parent.Controls.Add(L_End);
            FLP_Parent.Controls.Add(DT_End);
            FLP_Parent.Controls.Add(CHK_EndTolerance);
            FLP_Parent.Controls.Add(CB_Type);
            FLP_Parent.Controls.Add(CHK_Shiny);
            FLP_Parent.Controls.Add(CHK_MaleOnly);
            FLP_Parent.Controls.Add(CHK_FemaleOnly);
            FLP_Parent.Controls.Add(TB_Comment);
            FLP_Parent.Dock = System.Windows.Forms.DockStyle.Fill;
            FLP_Parent.Location = new System.Drawing.Point(0, 0);
            FLP_Parent.Name = "FLP_Parent";
            FLP_Parent.Size = new System.Drawing.Size(625, 101);
            FLP_Parent.TabIndex = 3;
            // 
            // CHK_Localized
            // 
            CHK_Localized.AutoSize = true;
            CHK_Localized.Location = new System.Drawing.Point(174, 6);
            CHK_Localized.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            CHK_Localized.Name = "CHK_Localized";
            CHK_Localized.Size = new System.Drawing.Size(39, 21);
            CHK_Localized.TabIndex = 8;
            CHK_Localized.Text = "-1";
            CHK_Localized.UseVisualStyleBackColor = true;
            // 
            // L_End
            // 
            L_End.AutoSize = true;
            L_End.Location = new System.Drawing.Point(219, 8);
            L_End.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            L_End.Name = "L_End";
            L_End.Size = new System.Drawing.Size(33, 17);
            L_End.TabIndex = 3;
            L_End.Text = "End:";
            // 
            // CHK_EndTolerance
            // 
            CHK_EndTolerance.AutoSize = true;
            CHK_EndTolerance.Location = new System.Drawing.Point(385, 6);
            CHK_EndTolerance.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            CHK_EndTolerance.Name = "CHK_EndTolerance";
            CHK_EndTolerance.Size = new System.Drawing.Size(43, 21);
            CHK_EndTolerance.TabIndex = 9;
            CHK_EndTolerance.Text = "+1";
            CHK_EndTolerance.UseVisualStyleBackColor = true;
            // 
            // CB_Type
            // 
            CB_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            CB_Type.FormattingEnabled = true;
            CB_Type.Location = new System.Drawing.Point(3, 34);
            CB_Type.Name = "CB_Type";
            CB_Type.Size = new System.Drawing.Size(220, 25);
            CB_Type.TabIndex = 5;
            // 
            // CHK_Shiny
            // 
            CHK_Shiny.AutoSize = true;
            CHK_Shiny.Location = new System.Drawing.Point(229, 37);
            CHK_Shiny.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            CHK_Shiny.Name = "CHK_Shiny";
            CHK_Shiny.Size = new System.Drawing.Size(57, 21);
            CHK_Shiny.TabIndex = 6;
            CHK_Shiny.Text = "Shiny";
            CHK_Shiny.ThreeState = true;
            CHK_Shiny.UseVisualStyleBackColor = true;
            // 
            // CHK_MaleOnly
            // 
            CHK_MaleOnly.AutoSize = true;
            CHK_MaleOnly.Location = new System.Drawing.Point(292, 37);
            CHK_MaleOnly.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            CHK_MaleOnly.Name = "CHK_MaleOnly";
            CHK_MaleOnly.Size = new System.Drawing.Size(86, 21);
            CHK_MaleOnly.TabIndex = 10;
            CHK_MaleOnly.Text = "Only Male";
            CHK_MaleOnly.UseVisualStyleBackColor = true;
            // 
            // CHK_FemaleOnly
            // 
            CHK_FemaleOnly.AutoSize = true;
            CHK_FemaleOnly.Location = new System.Drawing.Point(384, 37);
            CHK_FemaleOnly.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            CHK_FemaleOnly.Name = "CHK_FemaleOnly";
            CHK_FemaleOnly.Size = new System.Drawing.Size(98, 21);
            CHK_FemaleOnly.TabIndex = 11;
            CHK_FemaleOnly.Text = "Only Female";
            CHK_FemaleOnly.UseVisualStyleBackColor = true;
            // 
            // TB_Comment
            // 
            TB_Comment.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TB_Comment.Location = new System.Drawing.Point(3, 65);
            TB_Comment.Name = "TB_Comment";
            TB_Comment.Size = new System.Drawing.Size(613, 25);
            TB_Comment.TabIndex = 7;
            // 
            // PogoRow
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(FLP_Parent);
            Name = "PogoRow";
            Size = new System.Drawing.Size(625, 101);
            FLP_Parent.ResumeLayout(false);
            FLP_Parent.PerformLayout();
            ResumeLayout(false);
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
        private System.Windows.Forms.CheckBox CHK_Localized;
        private System.Windows.Forms.CheckBox CHK_EndTolerance;
        private System.Windows.Forms.CheckBox CHK_MaleOnly;
        private System.Windows.Forms.CheckBox CHK_FemaleOnly;
    }
}
