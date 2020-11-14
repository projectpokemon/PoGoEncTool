
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
            this.B_Down = new System.Windows.Forms.Button();
            this.B_Up = new System.Windows.Forms.Button();
            this.B_Save = new System.Windows.Forms.Button();
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
            this.LB_Species.Size = new System.Drawing.Size(124, 289);
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
            this.CB_Species.Size = new System.Drawing.Size(124, 23);
            this.CB_Species.TabIndex = 2;
            this.CB_Species.SelectedValueChanged += new System.EventHandler(this.CB_Species_SelectedValueChanged);
            // 
            // CB_Form
            // 
            this.CB_Form.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Form.FormattingEnabled = true;
            this.CB_Form.Location = new System.Drawing.Point(142, 12);
            this.CB_Form.Name = "CB_Form";
            this.CB_Form.Size = new System.Drawing.Size(68, 23);
            this.CB_Form.TabIndex = 3;
            this.CB_Form.SelectedIndexChanged += new System.EventHandler(this.CB_Form_SelectedIndexChanged);
            // 
            // pogoRow1
            // 
            this.pogoRow1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pogoRow1.Location = new System.Drawing.Point(216, 12);
            this.pogoRow1.Name = "pogoRow1";
            this.pogoRow1.Size = new System.Drawing.Size(517, 61);
            this.pogoRow1.TabIndex = 4;
            // 
            // LB_Appearances
            // 
            this.LB_Appearances.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LB_Appearances.FormattingEnabled = true;
            this.LB_Appearances.ItemHeight = 15;
            this.LB_Appearances.Location = new System.Drawing.Point(216, 72);
            this.LB_Appearances.Name = "LB_Appearances";
            this.LB_Appearances.Size = new System.Drawing.Size(517, 259);
            this.LB_Appearances.TabIndex = 5;
            this.LB_Appearances.SelectedIndexChanged += new System.EventHandler(this.LB_Appearances_SelectedIndexChanged);
            // 
            // PB_Poke
            // 
            this.PB_Poke.Location = new System.Drawing.Point(142, 42);
            this.PB_Poke.Name = "PB_Poke";
            this.PB_Poke.Size = new System.Drawing.Size(68, 56);
            this.PB_Poke.TabIndex = 6;
            this.PB_Poke.TabStop = false;
            // 
            // B_AddNew
            // 
            this.B_AddNew.Location = new System.Drawing.Point(142, 113);
            this.B_AddNew.Name = "B_AddNew";
            this.B_AddNew.Size = new System.Drawing.Size(57, 23);
            this.B_AddNew.TabIndex = 7;
            this.B_AddNew.Text = "Add";
            this.B_AddNew.UseVisualStyleBackColor = true;
            this.B_AddNew.Click += new System.EventHandler(this.B_AddNew_Click);
            // 
            // B_DeleteSelected
            // 
            this.B_DeleteSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.B_DeleteSelected.Location = new System.Drawing.Point(142, 308);
            this.B_DeleteSelected.Name = "B_DeleteSelected";
            this.B_DeleteSelected.Size = new System.Drawing.Size(57, 23);
            this.B_DeleteSelected.TabIndex = 10;
            this.B_DeleteSelected.Text = "Delete";
            this.B_DeleteSelected.UseVisualStyleBackColor = true;
            this.B_DeleteSelected.Click += new System.EventHandler(this.B_DeleteSelected_Click);
            // 
            // B_Down
            // 
            this.B_Down.Enabled = false;
            this.B_Down.Location = new System.Drawing.Point(142, 207);
            this.B_Down.Name = "B_Down";
            this.B_Down.Size = new System.Drawing.Size(57, 23);
            this.B_Down.TabIndex = 11;
            this.B_Down.Text = "Down";
            this.B_Down.UseVisualStyleBackColor = true;
            // 
            // B_Up
            // 
            this.B_Up.Enabled = false;
            this.B_Up.Location = new System.Drawing.Point(142, 178);
            this.B_Up.Name = "B_Up";
            this.B_Up.Size = new System.Drawing.Size(57, 23);
            this.B_Up.TabIndex = 12;
            this.B_Up.Text = "Up";
            this.B_Up.UseVisualStyleBackColor = true;
            // 
            // B_Save
            // 
            this.B_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.B_Save.Location = new System.Drawing.Point(675, 12);
            this.B_Save.Name = "B_Save";
            this.B_Save.Size = new System.Drawing.Size(58, 44);
            this.B_Save.TabIndex = 13;
            this.B_Save.Text = "Save All";
            this.B_Save.UseVisualStyleBackColor = true;
            this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 346);
            this.Controls.Add(this.B_Save);
            this.Controls.Add(this.B_Up);
            this.Controls.Add(this.B_Down);
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
        private System.Windows.Forms.Button B_Down;
        private System.Windows.Forms.Button B_Up;
        private System.Windows.Forms.Button B_Save;
    }
}

