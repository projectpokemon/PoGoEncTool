namespace PoGoEncTool.WinForms
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            LB_Species = new System.Windows.Forms.ListBox();
            CB_Species = new System.Windows.Forms.ComboBox();
            CB_Form = new System.Windows.Forms.ComboBox();
            pogoRow1 = new PogoRow();
            LB_Appearances = new System.Windows.Forms.ListBox();
            PB_Poke = new System.Windows.Forms.PictureBox();
            B_AddNew = new System.Windows.Forms.Button();
            B_DeleteSelected = new System.Windows.Forms.Button();
            B_Save = new System.Windows.Forms.Button();
            L_Serebii = new System.Windows.Forms.LinkLabel();
            L_PGFandom = new System.Windows.Forms.LinkLabel();
            B_MarkEvosAvailable = new System.Windows.Forms.Button();
            CHK_Available = new System.Windows.Forms.CheckBox();
            B_CopyToForms = new System.Windows.Forms.Button();
            B_DeleteAll = new System.Windows.Forms.Button();
            B_DumpAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)PB_Poke).BeginInit();
            SuspendLayout();
            // 
            // LB_Species
            // 
            LB_Species.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            LB_Species.FormattingEnabled = true;
            LB_Species.Location = new System.Drawing.Point(12, 71);
            LB_Species.Name = "LB_Species";
            LB_Species.Size = new System.Drawing.Size(140, 454);
            LB_Species.TabIndex = 1;
            LB_Species.SelectedIndexChanged += LB_Species_SelectedIndexChanged;
            // 
            // CB_Species
            // 
            CB_Species.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            CB_Species.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            CB_Species.FormattingEnabled = true;
            CB_Species.Location = new System.Drawing.Point(11, 13);
            CB_Species.Name = "CB_Species";
            CB_Species.Size = new System.Drawing.Size(140, 23);
            CB_Species.TabIndex = 2;
            CB_Species.SelectedValueChanged += CB_Species_SelectedValueChanged;
            // 
            // CB_Form
            // 
            CB_Form.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            CB_Form.FormattingEnabled = true;
            CB_Form.Location = new System.Drawing.Point(11, 43);
            CB_Form.Name = "CB_Form";
            CB_Form.Size = new System.Drawing.Size(140, 23);
            CB_Form.TabIndex = 3;
            CB_Form.SelectedIndexChanged += CB_Form_SelectedIndexChanged;
            // 
            // pogoRow1
            // 
            pogoRow1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pogoRow1.Location = new System.Drawing.Point(287, 11);
            pogoRow1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            pogoRow1.Name = "pogoRow1";
            pogoRow1.Size = new System.Drawing.Size(937, 58);
            pogoRow1.TabIndex = 4;
            // 
            // LB_Appearances
            // 
            LB_Appearances.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            LB_Appearances.FormattingEnabled = true;
            LB_Appearances.Location = new System.Drawing.Point(287, 71);
            LB_Appearances.Name = "LB_Appearances";
            LB_Appearances.Size = new System.Drawing.Size(991, 454);
            LB_Appearances.TabIndex = 5;
            LB_Appearances.SelectedIndexChanged += LB_Appearances_SelectedIndexChanged;
            // 
            // PB_Poke
            // 
            PB_Poke.Location = new System.Drawing.Point(160, 13);
            PB_Poke.Name = "PB_Poke";
            PB_Poke.Size = new System.Drawing.Size(70, 58);
            PB_Poke.TabIndex = 6;
            PB_Poke.TabStop = false;
            // 
            // B_AddNew
            // 
            B_AddNew.Location = new System.Drawing.Point(159, 102);
            B_AddNew.Name = "B_AddNew";
            B_AddNew.Size = new System.Drawing.Size(120, 42);
            B_AddNew.TabIndex = 7;
            B_AddNew.Text = "Add Entry";
            B_AddNew.UseVisualStyleBackColor = true;
            B_AddNew.Click += B_AddNew_Click;
            // 
            // B_DeleteSelected
            // 
            B_DeleteSelected.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            B_DeleteSelected.Location = new System.Drawing.Point(160, 435);
            B_DeleteSelected.Name = "B_DeleteSelected";
            B_DeleteSelected.Size = new System.Drawing.Size(120, 42);
            B_DeleteSelected.TabIndex = 10;
            B_DeleteSelected.Text = "Delete One Row";
            B_DeleteSelected.UseVisualStyleBackColor = true;
            B_DeleteSelected.Click += B_DeleteSelected_Click;
            // 
            // B_Save
            // 
            B_Save.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            B_Save.Location = new System.Drawing.Point(1208, 13);
            B_Save.Name = "B_Save";
            B_Save.Size = new System.Drawing.Size(70, 45);
            B_Save.TabIndex = 13;
            B_Save.Text = "Save All";
            B_Save.UseVisualStyleBackColor = true;
            B_Save.Click += B_Save_Click;
            // 
            // L_Serebii
            // 
            L_Serebii.AutoSize = true;
            L_Serebii.Location = new System.Drawing.Point(159, 150);
            L_Serebii.Name = "L_Serebii";
            L_Serebii.Size = new System.Drawing.Size(62, 15);
            L_Serebii.TabIndex = 14;
            L_Serebii.TabStop = true;
            L_Serebii.Text = "Serebii.net";
            L_Serebii.LinkClicked += L_Serebii_LinkClicked;
            // 
            // L_PGFandom
            // 
            L_PGFandom.AutoSize = true;
            L_PGFandom.Location = new System.Drawing.Point(159, 169);
            L_PGFandom.Name = "L_PGFandom";
            L_PGFandom.Size = new System.Drawing.Size(104, 15);
            L_PGFandom.TabIndex = 15;
            L_PGFandom.TabStop = true;
            L_PGFandom.Text = "Pokémon GO Wiki";
            L_PGFandom.LinkClicked += L_PGFandom_LinkClicked;
            // 
            // B_MarkEvosAvailable
            // 
            B_MarkEvosAvailable.Location = new System.Drawing.Point(160, 266);
            B_MarkEvosAvailable.Name = "B_MarkEvosAvailable";
            B_MarkEvosAvailable.Size = new System.Drawing.Size(120, 42);
            B_MarkEvosAvailable.TabIndex = 16;
            B_MarkEvosAvailable.Text = "Mark Evolutions\nas Available";
            B_MarkEvosAvailable.UseVisualStyleBackColor = true;
            B_MarkEvosAvailable.Click += B_MarkEvosAvailable_Click;
            // 
            // CHK_Available
            // 
            CHK_Available.AutoSize = true;
            CHK_Available.Location = new System.Drawing.Point(160, 77);
            CHK_Available.Name = "CHK_Available";
            CHK_Available.Size = new System.Drawing.Size(74, 19);
            CHK_Available.TabIndex = 17;
            CHK_Available.Text = "Available";
            CHK_Available.UseVisualStyleBackColor = true;
            CHK_Available.CheckedChanged += CHK_Available_CheckedChanged;
            // 
            // B_CopyToForms
            // 
            B_CopyToForms.Location = new System.Drawing.Point(159, 313);
            B_CopyToForms.Name = "B_CopyToForms";
            B_CopyToForms.Size = new System.Drawing.Size(120, 42);
            B_CopyToForms.TabIndex = 18;
            B_CopyToForms.Text = "Copy Current Row to All Forms";
            B_CopyToForms.UseVisualStyleBackColor = true;
            B_CopyToForms.Click += B_CopyToForms_Click;
            // 
            // B_DeleteAll
            // 
            B_DeleteAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            B_DeleteAll.Location = new System.Drawing.Point(159, 483);
            B_DeleteAll.Name = "B_DeleteAll";
            B_DeleteAll.Size = new System.Drawing.Size(120, 42);
            B_DeleteAll.TabIndex = 19;
            B_DeleteAll.Text = "Delete All Rows";
            B_DeleteAll.UseVisualStyleBackColor = true;
            B_DeleteAll.Click += B_DeleteAll_Click;
            // 
            // B_DumpAll
            // 
            B_DumpAll.Location = new System.Drawing.Point(159, 373);
            B_DumpAll.Name = "B_DumpAll";
            B_DumpAll.Size = new System.Drawing.Size(120, 42);
            B_DumpAll.TabIndex = 20;
            B_DumpAll.Text = "Dump All";
            B_DumpAll.UseVisualStyleBackColor = true;
            B_DumpAll.Click += B_DumpAll_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1288, 536);
            Controls.Add(B_DumpAll);
            Controls.Add(B_DeleteAll);
            Controls.Add(B_CopyToForms);
            Controls.Add(CHK_Available);
            Controls.Add(B_MarkEvosAvailable);
            Controls.Add(L_PGFandom);
            Controls.Add(L_Serebii);
            Controls.Add(B_Save);
            Controls.Add(B_DeleteSelected);
            Controls.Add(B_AddNew);
            Controls.Add(LB_Appearances);
            Controls.Add(pogoRow1);
            Controls.Add(CB_Form);
            Controls.Add(CB_Species);
            Controls.Add(LB_Species);
            Controls.Add(PB_Poke);
            MinimumSize = new System.Drawing.Size(800, 400);
            Name = "Main";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "PoGoEncTool";
            FormClosing += Main_FormClosing;
            ((System.ComponentModel.ISupportInitialize)PB_Poke).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.ListBox LB_Species;
        private System.Windows.Forms.ComboBox CB_Species;
        private System.Windows.Forms.ComboBox CB_Form;
        private PogoRow pogoRow1;
        private System.Windows.Forms.ListBox LB_Appearances;
        private System.Windows.Forms.PictureBox PB_Poke;
        private System.Windows.Forms.Button B_AddNew;
        private System.Windows.Forms.Button B_DeleteSelected;
        private System.Windows.Forms.Button B_Save;
        private System.Windows.Forms.LinkLabel L_Serebii;
        private System.Windows.Forms.LinkLabel L_PGFandom;
        private System.Windows.Forms.Button B_MarkEvosAvailable;
        private System.Windows.Forms.CheckBox CHK_Available;
        private System.Windows.Forms.Button B_CopyToForms;
        private System.Windows.Forms.Button B_DeleteAll;
        private System.Windows.Forms.Button B_DumpAll;
    }
}

