namespace KutuphaneOtomasyonu
{
    partial class Panelislemleri
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Panelislemleri));
            this.label1 = new System.Windows.Forms.Label();
            this.KiralamaPaneliButonu = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.kiraliklarDataGrid = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.CalisanPaneliButonu = new System.Windows.Forms.Button();
            this.UyePaneliButonu = new System.Windows.Forms.Button();
            this.KitapPaneliButonu = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.kiraliklarDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(510, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sayın ";
            // 
            // KiralamaPaneliButonu
            // 
            this.KiralamaPaneliButonu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.KiralamaPaneliButonu.FlatAppearance.BorderSize = 0;
            this.KiralamaPaneliButonu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KiralamaPaneliButonu.Image = global::KutuphaneOtomasyonu.Properties.Resources.kirala;
            this.KiralamaPaneliButonu.Location = new System.Drawing.Point(45, 682);
            this.KiralamaPaneliButonu.Name = "KiralamaPaneliButonu";
            this.KiralamaPaneliButonu.Size = new System.Drawing.Size(176, 176);
            this.KiralamaPaneliButonu.TabIndex = 1;
            this.KiralamaPaneliButonu.UseVisualStyleBackColor = true;
            this.KiralamaPaneliButonu.Click += new System.EventHandler(this.KiralamaPaneliButonu_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Perpetua", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(260, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 120);
            this.label2.TabIndex = 2;
            this.label2.Text = "Çalısan Paneli";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Perpetua", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(260, 261);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 120);
            this.label3.TabIndex = 2;
            this.label3.Text = "Uye Paneli";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Perpetua", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(260, 479);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(176, 120);
            this.label4.TabIndex = 2;
            this.label4.Text = "Kitap Paneli";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Perpetua", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(260, 703);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(213, 120);
            this.label5.TabIndex = 2;
            this.label5.Text = "Kiralama Paneli";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kiraliklarDataGrid
            // 
            this.kiraliklarDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kiraliklarDataGrid.Location = new System.Drawing.Point(508, 150);
            this.kiraliklarDataGrid.Name = "kiraliklarDataGrid";
            this.kiraliklarDataGrid.Size = new System.Drawing.Size(645, 448);
            this.kiraliklarDataGrid.TabIndex = 3;
            this.kiraliklarDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.kiraliklarDataGrid_CellContentClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Black;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(510, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 39);
            this.label6.TabIndex = 4;
            this.label6.Text = "Kiralıklar";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Perpetua", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(523, 640);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(206, 36);
            this.label7.TabIndex = 5;
            this.label7.Text = "Kiralayıcı Adı :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Perpetua", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(575, 720);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(154, 36);
            this.label8.TabIndex = 5;
            this.label8.Text = "Kitap Adı :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Perpetua", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(561, 800);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(168, 36);
            this.label9.TabIndex = 5;
            this.label9.Text = "Kalan Gün :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Perpetua", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(750, 649);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 24);
            this.label10.TabIndex = 5;
            this.label10.Text = "10";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Perpetua", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(750, 732);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 24);
            this.label11.TabIndex = 5;
            this.label11.Text = "11";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Perpetua", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(750, 809);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(30, 24);
            this.label12.TabIndex = 5;
            this.label12.Text = "12";
            // 
            // CalisanPaneliButonu
            // 
            this.CalisanPaneliButonu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CalisanPaneliButonu.FlatAppearance.BorderSize = 0;
            this.CalisanPaneliButonu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CalisanPaneliButonu.Image = global::KutuphaneOtomasyonu.Properties.Resources.calisan2;
            this.CalisanPaneliButonu.Location = new System.Drawing.Point(45, 22);
            this.CalisanPaneliButonu.Name = "CalisanPaneliButonu";
            this.CalisanPaneliButonu.Size = new System.Drawing.Size(176, 176);
            this.CalisanPaneliButonu.TabIndex = 1;
            this.CalisanPaneliButonu.UseVisualStyleBackColor = true;
            this.CalisanPaneliButonu.Click += new System.EventHandler(this.CalisanPaneliButonu_Click);
            // 
            // UyePaneliButonu
            // 
            this.UyePaneliButonu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UyePaneliButonu.FlatAppearance.BorderSize = 0;
            this.UyePaneliButonu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UyePaneliButonu.Image = global::KutuphaneOtomasyonu.Properties.Resources.uye2;
            this.UyePaneliButonu.Location = new System.Drawing.Point(45, 240);
            this.UyePaneliButonu.Name = "UyePaneliButonu";
            this.UyePaneliButonu.Size = new System.Drawing.Size(176, 176);
            this.UyePaneliButonu.TabIndex = 1;
            this.UyePaneliButonu.UseVisualStyleBackColor = true;
            this.UyePaneliButonu.Click += new System.EventHandler(this.UyePaneliButonu_Click);
            // 
            // KitapPaneliButonu
            // 
            this.KitapPaneliButonu.BackColor = System.Drawing.Color.Black;
            this.KitapPaneliButonu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.KitapPaneliButonu.FlatAppearance.BorderSize = 0;
            this.KitapPaneliButonu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KitapPaneliButonu.ForeColor = System.Drawing.Color.Transparent;
            this.KitapPaneliButonu.Image = global::KutuphaneOtomasyonu.Properties.Resources.book;
            this.KitapPaneliButonu.Location = new System.Drawing.Point(45, 458);
            this.KitapPaneliButonu.Name = "KitapPaneliButonu";
            this.KitapPaneliButonu.Size = new System.Drawing.Size(176, 176);
            this.KitapPaneliButonu.TabIndex = 1;
            this.KitapPaneliButonu.UseVisualStyleBackColor = false;
            this.KitapPaneliButonu.Click += new System.EventHandler(this.KitapPaneliButonu_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Perpetua", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.Color.White;
            this.checkBox1.Location = new System.Drawing.Point(961, 104);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(192, 35);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Günü Geçenler";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Panelislemleri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1184, 886);
            this.Controls.Add(this.CalisanPaneliButonu);
            this.Controls.Add(this.UyePaneliButonu);
            this.Controls.Add(this.KitapPaneliButonu);
            this.Controls.Add(this.KiralamaPaneliButonu);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.kiraliklarDataGrid);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1000, 670);
            this.Name = "Panelislemleri";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Panelislemleri";
            this.Load += new System.EventHandler(this.Panelislemleri_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kiraliklarDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button KitapPaneliButonu;
        private System.Windows.Forms.Button UyePaneliButonu;
        private System.Windows.Forms.Button CalisanPaneliButonu;
        private System.Windows.Forms.Button KiralamaPaneliButonu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView kiraliklarDataGrid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}