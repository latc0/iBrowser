namespace iphone
{
    partial class ItemProp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemProp));
            this.name = new System.Windows.Forms.Label();
            this.type = new System.Windows.Forms.Label();
            this.size = new System.Windows.Forms.Label();
            this.dateMod = new System.Windows.Forms.Label();
            this.dateBirth = new System.Windows.Forms.Label();
            this.location = new System.Windows.Forms.Label();
            this.contents = new System.Windows.Forms.Label();
            this.split = new System.Windows.Forms.Label();
            this.split2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.Location = new System.Drawing.Point(12, 10);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(49, 20);
            this.name.TabIndex = 0;
            this.name.Text = "name";
            // 
            // type
            // 
            this.type.AutoSize = true;
            this.type.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.type.Location = new System.Drawing.Point(12, 40);
            this.type.Name = "type";
            this.type.Size = new System.Drawing.Size(39, 20);
            this.type.TabIndex = 1;
            this.type.Text = "type";
            // 
            // size
            // 
            this.size.AutoSize = true;
            this.size.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.size.Location = new System.Drawing.Point(12, 70);
            this.size.Name = "size";
            this.size.Size = new System.Drawing.Size(37, 20);
            this.size.TabIndex = 2;
            this.size.Text = "size";
            // 
            // dateMod
            // 
            this.dateMod.AutoSize = true;
            this.dateMod.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateMod.Location = new System.Drawing.Point(12, 162);
            this.dateMod.Name = "dateMod";
            this.dateMod.Size = new System.Drawing.Size(40, 20);
            this.dateMod.TabIndex = 3;
            this.dateMod.Text = "mod";
            // 
            // dateBirth
            // 
            this.dateBirth.AutoSize = true;
            this.dateBirth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateBirth.Location = new System.Drawing.Point(12, 192);
            this.dateBirth.Name = "dateBirth";
            this.dateBirth.Size = new System.Drawing.Size(40, 20);
            this.dateBirth.TabIndex = 4;
            this.dateBirth.Text = "birth";
            // 
            // location
            // 
            this.location.AutoSize = true;
            this.location.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.location.Location = new System.Drawing.Point(12, 102);
            this.location.Name = "location";
            this.location.Size = new System.Drawing.Size(29, 20);
            this.location.TabIndex = 5;
            this.location.Text = "loc";
            // 
            // contents
            // 
            this.contents.AutoSize = true;
            this.contents.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contents.Location = new System.Drawing.Point(12, 131);
            this.contents.Name = "contents";
            this.contents.Size = new System.Drawing.Size(69, 20);
            this.contents.TabIndex = 6;
            this.contents.Text = "contains";
            // 
            // split
            // 
            this.split.AutoSize = true;
            this.split.Location = new System.Drawing.Point(12, 149);
            this.split.Name = "split";
            this.split.Size = new System.Drawing.Size(35, 13);
            this.split.TabIndex = 7;
            this.split.Text = "label1";
            // 
            // split2
            // 
            this.split2.AutoSize = true;
            this.split2.Location = new System.Drawing.Point(12, 30);
            this.split2.Name = "split2";
            this.split2.Size = new System.Drawing.Size(35, 13);
            this.split2.TabIndex = 8;
            this.split2.Text = "label1";
            // 
            // ItemProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 217);
            this.Controls.Add(this.split2);
            this.Controls.Add(this.split);
            this.Controls.Add(this.contents);
            this.Controls.Add(this.location);
            this.Controls.Add(this.dateBirth);
            this.Controls.Add(this.dateMod);
            this.Controls.Add(this.size);
            this.Controls.Add(this.type);
            this.Controls.Add(this.name);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ItemProp";
            this.Text = "Properties";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label type;
        private System.Windows.Forms.Label size;
        private System.Windows.Forms.Label dateMod;
        private System.Windows.Forms.Label dateBirth;
        private System.Windows.Forms.Label location;
        private System.Windows.Forms.Label contents;
        private System.Windows.Forms.Label split;
        private System.Windows.Forms.Label split2;
    }
}