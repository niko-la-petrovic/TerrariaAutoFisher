namespace TerrariaAutoFisher
{
    partial class ControlForm
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
            this.picturePreview = new System.Windows.Forms.PictureBox();
            this.numericDelay = new System.Windows.Forms.NumericUpDown();
            this.numericDuration = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericRed = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericRefishDelay = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picturePreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRefishDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // picturePreview
            // 
            this.picturePreview.Location = new System.Drawing.Point(21, 22);
            this.picturePreview.Name = "picturePreview";
            this.picturePreview.Size = new System.Drawing.Size(60, 57);
            this.picturePreview.TabIndex = 0;
            this.picturePreview.TabStop = false;
            // 
            // numericDelay
            // 
            this.numericDelay.Location = new System.Drawing.Point(255, 36);
            this.numericDelay.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numericDelay.Name = "numericDelay";
            this.numericDelay.Size = new System.Drawing.Size(120, 20);
            this.numericDelay.TabIndex = 1;
            this.numericDelay.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericDelay.ValueChanged += new System.EventHandler(this.NumericShootingDelay_ValueChanged);
            // 
            // numericDuration
            // 
            this.numericDuration.Location = new System.Drawing.Point(255, 89);
            this.numericDuration.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numericDuration.Name = "numericDuration";
            this.numericDuration.Size = new System.Drawing.Size(120, 20);
            this.numericDuration.TabIndex = 1;
            this.numericDuration.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericDuration.ValueChanged += new System.EventHandler(this.NumericShootingDuration_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(255, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Shooting Delay";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Duration Held";
            // 
            // numericRed
            // 
            this.numericRed.Location = new System.Drawing.Point(117, 36);
            this.numericRed.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericRed.Name = "numericRed";
            this.numericRed.Size = new System.Drawing.Size(120, 20);
            this.numericRed.TabIndex = 1;
            this.numericRed.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericRed.ValueChanged += new System.EventHandler(this.NumericShootingDelay_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(114, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Red Difference";
            // 
            // numericRefishDelay
            // 
            this.numericRefishDelay.Location = new System.Drawing.Point(117, 89);
            this.numericRefishDelay.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericRefishDelay.Name = "numericRefishDelay";
            this.numericRefishDelay.Size = new System.Drawing.Size(120, 20);
            this.numericRefishDelay.TabIndex = 1;
            this.numericRefishDelay.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericRefishDelay.ValueChanged += new System.EventHandler(this.NumericShootingDelay_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(114, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Re-fish delay";
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 141);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericDuration);
            this.Controls.Add(this.numericRefishDelay);
            this.Controls.Add(this.numericRed);
            this.Controls.Add(this.numericDelay);
            this.Controls.Add(this.picturePreview);
            this.Name = "ControlForm";
            this.Text = "Niko\'s Terraria Automator";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturePreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRefishDelay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picturePreview;
        private System.Windows.Forms.NumericUpDown numericDelay;
        private System.Windows.Forms.NumericUpDown numericDuration;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericRed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericRefishDelay;
        private System.Windows.Forms.Label label4;
    }
}

