
namespace PoGoEncTool
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
            this.LB_Species = new System.Windows.Forms.ListBox();
            this.CB_Species = new System.Windows.Forms.ComboBox();
            this.CB_Form = new System.Windows.Forms.ComboBox();
            this.pogoRow1 = new PoGoEncTool.PogoRow();
            this.LB_Appearances = new System.Windows.Forms.ListBox();
            this.PB_Poke = new System.Windows.Forms.PictureBox();
            this.B_AddNew = new System.Windows.Forms.Button();
            this.B_DeleteSelected = new System.Windows.Forms.Button();
            this.B_Save = new System.Windows.Forms.Button();
            this.L_Serebii = new System.Windows.Forms.LinkLabel();
            this.L_PGFandom = new System.Windows.Forms.LinkLabel();
            this.B_CopyTo = new System.Windows.Forms.Button();
            this.CHK_Available = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Poke)).BeginInit();
            this.SuspendLayout();
            // 
            // LB_Species
            // 
            this.LB_Species.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LB_Species.FormattingEnabled = true;
            this.LB_Species.ItemHeight = 15;
            this.LB_Species.Location = new System.Drawing.Point(12, 42);
            this.LB_Species.Name = "LB_Species";
            this.LB_Species.Size = new System.Drawing.Size(131, 604);
            this.LB_Species.TabIndex = 1;
            this.LB_Species.SelectedIndexChanged += new System.EventHandler(this.LB_Species_SelectedIndexChanged);
            // 
            // CB_Species
            // 
            this.CB_Species.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CB_Species.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CB_Species.FormattingEnabled = true;
            this.CB_Species.Location = new System.Drawing.Point(12, 13);
            this.CB_Species.Name = "CB_Species";
            this.CB_Species.Size = new System.Drawing.Size(131, 23);
            this.CB_Species.TabIndex = 2;
            this.CB_Species.SelectedValueChanged += new System.EventHandler(this.CB_Species_SelectedValueChanged);
            // 
            // CB_Form
            // 
            this.CB_Form.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Form.FormattingEnabled = true;
            this.CB_Form.Location = new System.Drawing.Point(149, 12);
            this.CB_Form.Name = "CB_Form";
            this.CB_Form.Size = new System.Drawing.Size(68, 23);
            this.CB_Form.TabIndex = 3;
            this.CB_Form.SelectedIndexChanged += new System.EventHandler(this.CB_Form_SelectedIndexChanged);
            // 
            // pogoRow1
            // 
            this.pogoRow1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pogoRow1.Location = new System.Drawing.Point(223, 12);
            this.pogoRow1.Name = "pogoRow1";
            this.pogoRow1.Size = new System.Drawing.Size(673, 84);
            this.pogoRow1.TabIndex = 4;
            // 
            // LB_Appearances
            // 
            this.LB_Appearances.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LB_Appearances.FormattingEnabled = true;
            this.LB_Appearances.ItemHeight = 15;
            this.LB_Appearances.Location = new System.Drawing.Point(223, 102);
            this.LB_Appearances.Name = "LB_Appearances";
            this.LB_Appearances.Size = new System.Drawing.Size(737, 544);
            this.LB_Appearances.TabIndex = 5;
            this.LB_Appearances.SelectedIndexChanged += new System.EventHandler(this.LB_Appearances_SelectedIndexChanged);
            // 
            // PB_Poke
            // 
            this.PB_Poke.Location = new System.Drawing.Point(149, 35);
            this.PB_Poke.Name = "PB_Poke";
            this.PB_Poke.Size = new System.Drawing.Size(68, 56);
            this.PB_Poke.TabIndex = 6;
            this.PB_Poke.TabStop = false;
            // 
            // B_AddNew
            // 
            this.B_AddNew.Location = new System.Drawing.Point(148, 147);
            this.B_AddNew.Name = "B_AddNew";
            this.B_AddNew.Size = new System.Drawing.Size(70, 23);
            this.B_AddNew.TabIndex = 7;
            this.B_AddNew.Text = "Add Entry";
            this.B_AddNew.UseVisualStyleBackColor = true;
            this.B_AddNew.Click += new System.EventHandler(this.B_AddNew_Click);
            // 
            // B_DeleteSelected
            // 
            this.B_DeleteSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.B_DeleteSelected.Location = new System.Drawing.Point(149, 624);
            this.B_DeleteSelected.Name = "B_DeleteSelected";
            this.B_DeleteSelected.Size = new System.Drawing.Size(68, 23);
            this.B_DeleteSelected.TabIndex = 10;
            this.B_DeleteSelected.Text = "Delete";
            this.B_DeleteSelected.UseVisualStyleBackColor = true;
            this.B_DeleteSelected.Click += new System.EventHandler(this.B_DeleteSelected_Click);
            // 
            // B_Save
            // 
            this.B_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.B_Save.Location = new System.Drawing.Point(902, 12);
            this.B_Save.Name = "B_Save";
            this.B_Save.Size = new System.Drawing.Size(58, 44);
            this.B_Save.TabIndex = 13;
            this.B_Save.Text = "Save All";
            this.B_Save.UseVisualStyleBackColor = true;
            this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
            // 
            // L_Serebii
            // 
            this.L_Serebii.AutoSize = true;
            this.L_Serebii.Location = new System.Drawing.Point(149, 180);
            this.L_Serebii.Name = "L_Serebii";
            this.L_Serebii.Size = new System.Drawing.Size(42, 15);
            this.L_Serebii.TabIndex = 14;
            this.L_Serebii.TabStop = true;
            this.L_Serebii.Text = "Serebii";
            this.L_Serebii.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.L_Serebii_LinkClicked);
            // 
            // L_PGFandom
            // 
            this.L_PGFandom.AutoSize = true;
            this.L_PGFandom.Location = new System.Drawing.Point(149, 199);
            this.L_PGFandom.Name = "L_PGFandom";
            this.L_PGFandom.Size = new System.Drawing.Size(66, 15);
            this.L_PGFandom.TabIndex = 15;
            this.L_PGFandom.TabStop = true;
            this.L_PGFandom.Text = "PGFandom";
            this.L_PGFandom.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.L_PGFandom_LinkClicked);
            // 
            // B_CopyTo
            // 
            this.B_CopyTo.Location = new System.Drawing.Point(148, 251);
            this.B_CopyTo.Name = "B_CopyTo";
            this.B_CopyTo.Size = new System.Drawing.Size(70, 47);
            this.B_CopyTo.TabIndex = 16;
            this.B_CopyTo.Text = "Copy to\r\nEvolutions";
            this.B_CopyTo.UseVisualStyleBackColor = true;
            this.B_CopyTo.Click += new System.EventHandler(this.B_CopyTo_Click);
            // 
            // CHK_Available
            // 
            this.CHK_Available.AutoSize = true;
            this.CHK_Available.Location = new System.Drawing.Point(149, 98);
            this.CHK_Available.Name = "CHK_Available";
            this.CHK_Available.Size = new System.Drawing.Size(74, 19);
            this.CHK_Available.TabIndex = 17;
            this.CHK_Available.Text = "Available";
            this.CHK_Available.UseVisualStyleBackColor = true;
            this.CHK_Available.CheckedChanged += new System.EventHandler(this.CHK_Available_CheckedChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 662);
            this.Controls.Add(this.CHK_Available);
            this.Controls.Add(this.B_CopyTo);
            this.Controls.Add(this.L_PGFandom);
            this.Controls.Add(this.L_Serebii);
            this.Controls.Add(this.B_Save);
            this.Controls.Add(this.B_DeleteSelected);
            this.Controls.Add(this.B_AddNew);
            this.Controls.Add(this.PB_Poke);
            this.Controls.Add(this.LB_Appearances);
            this.Controls.Add(this.pogoRow1);
            this.Controls.Add(this.CB_Form);
            this.Controls.Add(this.CB_Species);
            this.Controls.Add(this.LB_Species);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PGET";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.PB_Poke)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Button B_CopyTo;
        private System.Windows.Forms.CheckBox CHK_Available;
    }
}

