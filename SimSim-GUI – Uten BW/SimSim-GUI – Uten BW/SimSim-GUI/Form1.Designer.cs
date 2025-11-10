namespace SimSim_GUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtTemp = new TextBox();
            lbLogg = new ListBox();
            label1 = new Label();
            btnStart = new Button();
            btnStopp = new Button();
            label2 = new Label();
            cbPorter = new ComboBox();
            SuspendLayout();
            // 
            // txtTemp
            // 
            txtTemp.Location = new Point(559, 236);
            txtTemp.Name = "txtTemp";
            txtTemp.ReadOnly = true;
            txtTemp.Size = new Size(151, 27);
            txtTemp.TabIndex = 0;
            txtTemp.TextAlign = HorizontalAlignment.Right;
            // 
            // lbLogg
            // 
            lbLogg.FormattingEnabled = true;
            lbLogg.Location = new Point(12, 12);
            lbLogg.Name = "lbLogg";
            lbLogg.Size = new Size(523, 384);
            lbLogg.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(559, 190);
            label1.Name = "label1";
            label1.Size = new Size(88, 20);
            label1.TabIndex = 2;
            label1.Text = "Temperatur:";
            // 
            // btnStart
            // 
            btnStart.Location = new Point(559, 296);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(151, 37);
            btnStart.TabIndex = 3;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnStopp
            // 
            btnStopp.Enabled = false;
            btnStopp.Location = new Point(559, 359);
            btnStopp.Name = "btnStopp";
            btnStopp.Size = new Size(151, 37);
            btnStopp.TabIndex = 4;
            btnStopp.Text = "Stopp";
            btnStopp.UseVisualStyleBackColor = true;
            btnStopp.Click += btnStopp_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(559, 12);
            label2.Name = "label2";
            label2.Size = new Size(144, 20);
            label2.TabIndex = 5;
            label2.Text = "Tilgjengelige porter:";
            // 
            // cbPorter
            // 
            cbPorter.FormattingEnabled = true;
            cbPorter.Location = new Point(559, 51);
            cbPorter.Name = "cbPorter";
            cbPorter.Size = new Size(151, 28);
            cbPorter.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(728, 421);
            Controls.Add(cbPorter);
            Controls.Add(label2);
            Controls.Add(btnStopp);
            Controls.Add(btnStart);
            Controls.Add(label1);
            Controls.Add(lbLogg);
            Controls.Add(txtTemp);
            Name = "Form1";
            Text = "SimSim - GUI";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtTemp;
        private ListBox lbLogg;
        private Label label1;
        private Button btnStart;
        private Button btnStopp;
        private Label label2;
        private ComboBox cbPorter;
    }
}
