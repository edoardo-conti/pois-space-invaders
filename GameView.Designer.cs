namespace SpaceInvaders
{
    partial class GameView
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.ModeOne = new System.Windows.Forms.RadioButton();
            this.ModeTwo = new System.Windows.Forms.RadioButton();
            this.StartButton = new System.Windows.Forms.Button();
            this.PlayerTimerShots = new System.Windows.Forms.Timer(this.components);
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(122, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(356, 46);
            this.label1.TabIndex = 2;
            this.label1.Tag = "Title";
            this.label1.Text = "SPACE INVADERS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 31);
            this.label2.TabIndex = 3;
            this.label2.Tag = "Score";
            this.label2.Text = "SCORE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(350, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 31);
            this.label3.TabIndex = 4;
            this.label3.Tag = "LivesLabel";
            this.label3.Text = "LIFES";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.LawnGreen;
            this.label4.Location = new System.Drawing.Point(120, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 31);
            this.label4.TabIndex = 5;
            this.label4.Tag = "Score_Number";
            this.label4.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(115, 186);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(382, 31);
            this.label5.TabIndex = 6;
            this.label5.Tag = "subtitle";
            this.label5.Text = "CHOOSE A MODE TO START ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer
            // 
            this.timer.Interval = 20;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // ModeOne
            // 
            this.ModeOne.AutoSize = true;
            this.ModeOne.Font = new System.Drawing.Font("Courier New", 15.15F, System.Drawing.FontStyle.Bold);
            this.ModeOne.Location = new System.Drawing.Point(143, 247);
            this.ModeOne.Name = "ModeOne";
            this.ModeOne.Size = new System.Drawing.Size(314, 27);
            this.ModeOne.TabIndex = 12;
            this.ModeOne.TabStop = true;
            this.ModeOne.Tag = "RadioButton";
            this.ModeOne.Text = "1) Push-based Movement";
            this.ModeOne.UseVisualStyleBackColor = true;
            // 
            // ModeTwo
            // 
            this.ModeTwo.AutoSize = true;
            this.ModeTwo.Font = new System.Drawing.Font("Courier New", 15.15F, System.Drawing.FontStyle.Bold);
            this.ModeTwo.Location = new System.Drawing.Point(143, 280);
            this.ModeTwo.Name = "ModeTwo";
            this.ModeTwo.Size = new System.Drawing.Size(314, 27);
            this.ModeTwo.TabIndex = 13;
            this.ModeTwo.TabStop = true;
            this.ModeTwo.Tag = "RadioButton";
            this.ModeTwo.Text = "2) Continuous Movement";
            this.ModeTwo.UseVisualStyleBackColor = true;
            // 
            // StartButton
            // 
            this.StartButton.Font = new System.Drawing.Font("Courier New", 15.15F, System.Drawing.FontStyle.Bold);
            this.StartButton.ForeColor = System.Drawing.Color.Black;
            this.StartButton.Location = new System.Drawing.Point(230, 374);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(140, 50);
            this.StartButton.TabIndex = 14;
            this.StartButton.Tag = "StartButton";
            this.StartButton.Text = "START";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // PlayerTimerShots
            // 
            this.PlayerTimerShots.Interval = 500;
            this.PlayerTimerShots.Tick += new System.EventHandler(this.PlayerTimerShots_Tick);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::SpaceInvaders.Properties.Resources.player;
            this.pictureBox3.Location = new System.Drawing.Point(483, 35);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(30, 18);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 9;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Tag = "Live";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::SpaceInvaders.Properties.Resources.player;
            this.pictureBox2.Location = new System.Drawing.Point(519, 35);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(30, 18);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Tag = "Live";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SpaceInvaders.Properties.Resources.player;
            this.pictureBox1.Location = new System.Drawing.Point(447, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 18);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Tag = "Live";
            // 
            // Activity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(584, 501);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.ModeTwo);
            this.Controls.Add(this.ModeOne);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Activity";
            this.Text = "Space Invaders";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsPressed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.RadioButton ModeOne;
        private System.Windows.Forms.RadioButton ModeTwo;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Timer PlayerTimerShots;
    }
}

