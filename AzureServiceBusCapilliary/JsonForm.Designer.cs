
namespace AzureServiceBusCapilliary
{
    partial class JsonForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.JsonObjectTxtBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.JsontypeDDL = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SaveButtonClick = new System.Windows.Forms.Button();
            this.CancelButtonClick = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // JsonObjectTxtBox
            // 
            this.JsonObjectTxtBox.Location = new System.Drawing.Point(64, 92);
            this.JsonObjectTxtBox.Name = "JsonObjectTxtBox";
            this.JsonObjectTxtBox.Size = new System.Drawing.Size(682, 235);
            this.JsonObjectTxtBox.TabIndex = 0;
            this.JsonObjectTxtBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(185, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "Manual Insert";
            // 
            // JsontypeDDL
            // 
            this.JsontypeDDL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.JsontypeDDL.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.JsontypeDDL.FormattingEnabled = true;
            this.JsontypeDDL.Items.AddRange(new object[] {
            "Select",
            "ORDER",
            "ARTICLE",
            "RETURN"});
            this.JsontypeDDL.Location = new System.Drawing.Point(586, 36);
            this.JsontypeDDL.Name = "JsontypeDDL";
            this.JsontypeDDL.Size = new System.Drawing.Size(160, 33);
            this.JsontypeDDL.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(423, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "JSON Type : ";
            // 
            // SaveButtonClick
            // 
            this.SaveButtonClick.BackColor = System.Drawing.Color.Green;
            this.SaveButtonClick.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SaveButtonClick.Location = new System.Drawing.Point(265, 344);
            this.SaveButtonClick.Name = "SaveButtonClick";
            this.SaveButtonClick.Size = new System.Drawing.Size(100, 42);
            this.SaveButtonClick.TabIndex = 4;
            this.SaveButtonClick.Text = "Save";
            this.SaveButtonClick.UseVisualStyleBackColor = false;
            this.SaveButtonClick.Click += new System.EventHandler(this.SaveButtonClick_Click);
            // 
            // CancelButtonClick
            // 
            this.CancelButtonClick.BackColor = System.Drawing.Color.Brown;
            this.CancelButtonClick.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CancelButtonClick.Location = new System.Drawing.Point(396, 344);
            this.CancelButtonClick.Name = "CancelButtonClick";
            this.CancelButtonClick.Size = new System.Drawing.Size(100, 42);
            this.CancelButtonClick.TabIndex = 4;
            this.CancelButtonClick.Text = "Cancel";
            this.CancelButtonClick.UseVisualStyleBackColor = false;
            this.CancelButtonClick.Click += new System.EventHandler(this.CancelButtonClick_Click);
            // 
            // JsonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CancelButtonClick);
            this.Controls.Add(this.SaveButtonClick);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.JsontypeDDL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.JsonObjectTxtBox);
            this.Name = "JsonForm";
            this.Text = "JsonForm";
            this.Load += new System.EventHandler(this.JsonForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox JsonObjectTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox JsontypeDDL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SaveButtonClick;
        private System.Windows.Forms.Button CancelButtonClick;
    }
}