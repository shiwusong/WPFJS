﻿namespace WinjsLib
{
    partial class WebForm
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
            this.SuspendLayout();
            // 
            // WebForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "WebForm";
            this.Text = "WebForm";
            this.Activated += new System.EventHandler(this.WebForm_Activated);
            this.Deactivate += new System.EventHandler(this.WebForm_Deactivate);
            this.Load += new System.EventHandler(this.WebForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WebForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
    }
}