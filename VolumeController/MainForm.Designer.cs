namespace VolumeController
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.txtMax = new System.Windows.Forms.TextBox();
            this.txtSpeed = new System.Windows.Forms.TextBox();
            this.lbMax = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.cbDevice = new System.Windows.Forms.ComboBox();
            this.lbDevice = new System.Windows.Forms.Label();
            this.lbDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(206, 48);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(75, 23);
            this.btnUp.TabIndex = 0;
            this.btnUp.Text = "set";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(206, 86);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(75, 23);
            this.btnDown.TabIndex = 1;
            this.btnDown.Text = "down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // txtMax
            // 
            this.txtMax.Location = new System.Drawing.Point(79, 48);
            this.txtMax.Name = "txtMax";
            this.txtMax.Size = new System.Drawing.Size(100, 21);
            this.txtMax.TabIndex = 2;
            this.txtMax.Text = "50";
            // 
            // txtSpeed
            // 
            this.txtSpeed.Location = new System.Drawing.Point(79, 86);
            this.txtSpeed.Name = "txtSpeed";
            this.txtSpeed.Size = new System.Drawing.Size(100, 21);
            this.txtSpeed.TabIndex = 3;
            this.txtSpeed.Text = "50";
            // 
            // lbMax
            // 
            this.lbMax.AutoSize = true;
            this.lbMax.Location = new System.Drawing.Point(14, 51);
            this.lbMax.Name = "lbMax";
            this.lbMax.Size = new System.Drawing.Size(30, 12);
            this.lbMax.TabIndex = 4;
            this.lbMax.Text = "Max";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Speed";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(16, 178);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(265, 196);
            this.listBox1.TabIndex = 6;
            // 
            // cbDevice
            // 
            this.cbDevice.FormattingEnabled = true;
            this.cbDevice.Location = new System.Drawing.Point(79, 12);
            this.cbDevice.Name = "cbDevice";
            this.cbDevice.Size = new System.Drawing.Size(202, 20);
            this.cbDevice.TabIndex = 7;
            this.cbDevice.SelectedIndexChanged += new System.EventHandler(this.cbDevice_SelectedIndexChanged);
            // 
            // lbDevice
            // 
            this.lbDevice.AutoSize = true;
            this.lbDevice.Location = new System.Drawing.Point(14, 15);
            this.lbDevice.Name = "lbDevice";
            this.lbDevice.Size = new System.Drawing.Size(43, 12);
            this.lbDevice.TabIndex = 8;
            this.lbDevice.Text = "Device";
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Location = new System.Drawing.Point(14, 118);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(283, 48);
            this.lbDescription.TabIndex = 9;
            this.lbDescription.Text = "Volume Up : Ctrl + Shift + Up Arrow\r\nVolume Down : Ctrl + Shift + Down Arrow\r\nVol" +
    "ume Set : Ctrl + Shift + Alt+ Up Arrow\r\nVolume Fade Out  : Ctrl + Shift + Alt+ D" +
    "own Arrow";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 174);
            this.Controls.Add(this.lbDescription);
            this.Controls.Add(this.lbDevice);
            this.Controls.Add(this.cbDevice);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbMax);
            this.Controls.Add(this.txtSpeed);
            this.Controls.Add(this.txtMax);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.MaximumSize = new System.Drawing.Size(319, 425);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Volume";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.TextBox txtMax;
        private System.Windows.Forms.TextBox txtSpeed;
        private System.Windows.Forms.Label lbMax;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ComboBox cbDevice;
        private System.Windows.Forms.Label lbDevice;
        private System.Windows.Forms.Label lbDescription;
    }
}

