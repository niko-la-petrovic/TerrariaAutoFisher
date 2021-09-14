namespace TerrariaAutoFisher
{
    partial class OverlayForm
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
            this.components = new System.ComponentModel.Container();
            this.fastTimer = new System.Windows.Forms.Timer(this.components);
            this.labelStartRed = new System.Windows.Forms.Label();
            this.labelCurrentRed = new System.Windows.Forms.Label();
            this.labelAverageRed = new System.Windows.Forms.Label();
            this.labelRedDiff = new System.Windows.Forms.Label();
            this.slowTimer = new System.Windows.Forms.Timer(this.components);
            this.labelError = new System.Windows.Forms.Label();
            this.labelState = new System.Windows.Forms.Label();
            this.labelFishing = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // fastTimer
            // 
            this.fastTimer.Tick += new System.EventHandler(this.FastTimer_Tick);
            // 
            // labelStartRed
            // 
            this.labelStartRed.AutoSize = true;
            this.labelStartRed.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelStartRed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStartRed.ForeColor = System.Drawing.Color.Black;
            this.labelStartRed.Location = new System.Drawing.Point(14, 13);
            this.labelStartRed.Name = "labelStartRed";
            this.labelStartRed.Size = new System.Drawing.Size(55, 13);
            this.labelStartRed.TabIndex = 0;
            this.labelStartRed.Text = "Start Red:";
            // 
            // labelCurrentRed
            // 
            this.labelCurrentRed.AutoSize = true;
            this.labelCurrentRed.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelCurrentRed.Location = new System.Drawing.Point(14, 39);
            this.labelCurrentRed.Name = "labelCurrentRed";
            this.labelCurrentRed.Size = new System.Drawing.Size(67, 13);
            this.labelCurrentRed.TabIndex = 0;
            this.labelCurrentRed.Text = "Current Red:";
            // 
            // labelAverageRed
            // 
            this.labelAverageRed.AutoSize = true;
            this.labelAverageRed.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelAverageRed.Location = new System.Drawing.Point(14, 52);
            this.labelAverageRed.Name = "labelAverageRed";
            this.labelAverageRed.Size = new System.Drawing.Size(73, 13);
            this.labelAverageRed.TabIndex = 0;
            this.labelAverageRed.Text = "Average Red:";
            // 
            // labelRedDiff
            // 
            this.labelRedDiff.AutoSize = true;
            this.labelRedDiff.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelRedDiff.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelRedDiff.Location = new System.Drawing.Point(14, 26);
            this.labelRedDiff.Name = "labelRedDiff";
            this.labelRedDiff.Size = new System.Drawing.Size(49, 13);
            this.labelRedDiff.TabIndex = 0;
            this.labelRedDiff.Text = "Red Diff:";
            // 
            // slowTimer
            // 
            this.slowTimer.Interval = 1000;
            this.slowTimer.Tick += new System.EventHandler(this.SlowTimer_Tick);
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelError.ForeColor = System.Drawing.Color.DarkRed;
            this.labelError.Location = new System.Drawing.Point(14, 110);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(12, 16);
            this.labelError.TabIndex = 0;
            this.labelError.Text = "-";
            // 
            // labelState
            // 
            this.labelState.AutoSize = true;
            this.labelState.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelState.ForeColor = System.Drawing.Color.DarkRed;
            this.labelState.Location = new System.Drawing.Point(14, 84);
            this.labelState.Name = "labelState";
            this.labelState.Size = new System.Drawing.Size(102, 13);
            this.labelState.TabIndex = 0;
            this.labelState.Text = "Deactivated - Num4";
            // 
            // labelFishing
            // 
            this.labelFishing.AutoSize = true;
            this.labelFishing.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelFishing.ForeColor = System.Drawing.Color.DarkRed;
            this.labelFishing.Location = new System.Drawing.Point(14, 97);
            this.labelFishing.Name = "labelFishing";
            this.labelFishing.Size = new System.Drawing.Size(75, 13);
            this.labelFishing.TabIndex = 0;
            this.labelFishing.Text = "Not Fishing - F";
            // 
            // OverlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelRedDiff);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.labelFishing);
            this.Controls.Add(this.labelState);
            this.Controls.Add(this.labelAverageRed);
            this.Controls.Add(this.labelCurrentRed);
            this.Controls.Add(this.labelStartRed);
            this.Name = "OverlayForm";
            this.Text = "OverlayForm";
            this.Load += new System.EventHandler(this.OverlayForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer fastTimer;
        private System.Windows.Forms.Label labelStartRed;
        private System.Windows.Forms.Label labelCurrentRed;
        private System.Windows.Forms.Label labelAverageRed;
        private System.Windows.Forms.Label labelRedDiff;
        private System.Windows.Forms.Timer slowTimer;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.Label labelState;
        private System.Windows.Forms.Label labelFishing;
    }
}