namespace NAROU_downloader
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.Linkbox = new System.Windows.Forms.TextBox();
            this.Button_getlist = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Title_text = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.auther = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Wasuu_text = new System.Windows.Forms.TextBox();
            this.datasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.datasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "リンク";
            // 
            // Linkbox
            // 
            this.Linkbox.Location = new System.Drawing.Point(60, 47);
            this.Linkbox.Name = "Linkbox";
            this.Linkbox.Size = new System.Drawing.Size(468, 19);
            this.Linkbox.TabIndex = 1;
            // 
            // Button_getlist
            // 
            this.Button_getlist.Location = new System.Drawing.Point(534, 45);
            this.Button_getlist.Name = "Button_getlist";
            this.Button_getlist.Size = new System.Drawing.Size(75, 23);
            this.Button_getlist.TabIndex = 2;
            this.Button_getlist.Text = "リスト取得";
            this.Button_getlist.UseVisualStyleBackColor = true;
            this.Button_getlist.Click += new System.EventHandler(this.Button_getlist_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "タイトル";
            // 
            // Title_text
            // 
            this.Title_text.Location = new System.Drawing.Point(60, 77);
            this.Title_text.Name = "Title_text";
            this.Title_text.Size = new System.Drawing.Size(549, 19);
            this.Title_text.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "作者";
            // 
            // auther
            // 
            this.auther.Location = new System.Drawing.Point(60, 107);
            this.auther.Name = "auther";
            this.auther.Size = new System.Drawing.Size(549, 19);
            this.auther.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(534, 132);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "クリア";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "話数";
            // 
            // Wasuu_text
            // 
            this.Wasuu_text.Location = new System.Drawing.Point(60, 134);
            this.Wasuu_text.Name = "Wasuu_text";
            this.Wasuu_text.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.Wasuu_text.Size = new System.Drawing.Size(468, 19);
            this.Wasuu_text.TabIndex = 9;
            // 
            // datasBindingSource
            // 
            this.datasBindingSource.DataSource = typeof(NAROU_downloader.Form1.Datas);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(60, 161);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(547, 264);
            this.dataGridView1.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 535);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Wasuu_text);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.auther);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Title_text);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Button_getlist);
            this.Controls.Add(this.Linkbox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Linkbox;
        private System.Windows.Forms.Button Button_getlist;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Title_text;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox auther;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Wasuu_text;
        private System.Windows.Forms.BindingSource datasBindingSource;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

