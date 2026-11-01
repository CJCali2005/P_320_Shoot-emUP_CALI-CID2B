namespace P_320_POO_CALI.View
{
    partial class GameOverForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameOverForm));
            lblScore = new Label();
            lblPerdu = new Label();
            SuspendLayout();
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.BackColor = Color.Transparent;
            lblScore.Font = new Font("Arial", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblScore.ForeColor = Color.Red;
            lblScore.Location = new Point(313, 346);
            lblScore.Margin = new Padding(14, 0, 14, 0);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(200, 70);
            lblScore.TabIndex = 0;
            lblScore.Text = "label1";
            lblScore.Click += label1_Click;
            // 
            // lblPerdu
            // 
            
            lblPerdu.AutoSize = true;
            lblPerdu.BackColor = Color.Transparent;
            lblPerdu.Location = new Point(313, 235);
            lblPerdu.Margin = new Padding(14, 0, 14, 0);
            lblPerdu.Name = "lblPerdu";
            lblPerdu.Size = new Size(595, 70);
            lblPerdu.TabIndex = 1;
            lblPerdu.Text = "VOUS AVEZ PERDU";
            lblPerdu.Click += label1_Click_1;
            // 
            // GameOverForm
            // 
            AutoScaleDimensions = new SizeF(37F, 70F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1182, 553);
            Controls.Add(lblPerdu);
            Controls.Add(lblScore);
            DoubleBuffered = true;
            Font = new Font("Arial", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ForeColor = Color.Red;
            Margin = new Padding(14, 10, 14, 10);
            Name = "GameOverForm";
            Text = "Form1";
            Load += GameOverForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblScore;
        private Label lblPerdu;
    }
}