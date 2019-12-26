namespace MyCloudStoreClient
{
    partial class DownloadForm
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
            this.lbFiles = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnLocation = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFolder = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cbRemove = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lbFiles
            // 
            this.lbFiles.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lbFiles.FormattingEnabled = true;
            this.lbFiles.ItemHeight = 16;
            this.lbFiles.Location = new System.Drawing.Point(12, 33);
            this.lbFiles.Name = "lbFiles";
            this.lbFiles.Size = new System.Drawing.Size(584, 244);
            this.lbFiles.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select file to download:";
            // 
            // btnDownload
            // 
            this.btnDownload.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnDownload.Location = new System.Drawing.Point(182, 314);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(251, 27);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnLocation
            // 
            this.btnLocation.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnLocation.Location = new System.Drawing.Point(12, 419);
            this.btnLocation.Name = "btnLocation";
            this.btnLocation.Size = new System.Drawing.Size(241, 28);
            this.btnLocation.TabIndex = 3;
            this.btnLocation.Text = "Change download location";
            this.btnLocation.UseVisualStyleBackColor = true;
            this.btnLocation.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(12, 389);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Download folder: ";
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(136, 389);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(46, 17);
            this.lblFolder.TabIndex = 5;
            this.lblFolder.Text = "label3";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 453);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(241, 30);
            this.button1.TabIndex = 6;
            this.button1.Text = "<-Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbRemove
            // 
            this.cbRemove.AutoSize = true;
            this.cbRemove.Location = new System.Drawing.Point(221, 287);
            this.cbRemove.Name = "cbRemove";
            this.cbRemove.Size = new System.Drawing.Size(152, 21);
            this.cbRemove.TabIndex = 7;
            this.cbRemove.Text = "Remove from cloud";
            this.cbRemove.UseVisualStyleBackColor = true;
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 495);
            this.Controls.Add(this.cbRemove);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLocation);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbFiles);
            this.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Name = "DownloadForm";
            this.Text = "MyCloudStore";
            this.Load += new System.EventHandler(this.DownloadForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbFiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnLocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox cbRemove;
    }
}