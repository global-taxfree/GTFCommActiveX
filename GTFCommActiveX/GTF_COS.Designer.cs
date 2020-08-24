namespace GTFCommActiveX
{
    partial class GTF_COS
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbPassportNm = new System.Windows.Forms.Label();
            this.lbResidence = new System.Windows.Forms.Label();
            this.lbPassportEtc = new System.Windows.Forms.Label();
            this.lbBirthday = new System.Windows.Forms.Label();
            this.lbPassportNo = new System.Windows.Forms.Label();
            this.lbGender = new System.Windows.Forms.Label();
            this.lbLandingPort = new System.Windows.Forms.Label();
            this.lbNationality = new System.Windows.Forms.Label();
            this.lbExpireDate = new System.Windows.Forms.Label();
            this.lbLandingDate = new System.Windows.Forms.Label();
            this.txtPassportNm = new System.Windows.Forms.TextBox();
            this.txtGender = new System.Windows.Forms.TextBox();
            this.txtPassportNo = new System.Windows.Forms.TextBox();
            this.txtBirthday = new System.Windows.Forms.TextBox();
            this.txtNationality = new System.Windows.Forms.TextBox();
            this.txtExpireDate = new System.Windows.Forms.TextBox();
            this.comboResidence = new System.Windows.Forms.ComboBox();
            this.comboPassportEtc = new System.Windows.Forms.ComboBox();
            this.comboLandingPort = new System.Windows.Forms.ComboBox();
            this.txtLandingDate = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbPassportNm
            // 
            this.lbPassportNm.AutoSize = true;
            this.lbPassportNm.Font = new System.Drawing.Font("굴림", 8.5F);
            this.lbPassportNm.Location = new System.Drawing.Point(34, 20);
            this.lbPassportNm.Name = "lbPassportNm";
            this.lbPassportNm.Size = new System.Drawing.Size(65, 12);
            this.lbPassportNm.TabIndex = 0;
            this.lbPassportNm.Text = "購入者氏名";
            // 
            // lbResidence
            // 
            this.lbResidence.AutoSize = true;
            this.lbResidence.Font = new System.Drawing.Font("굴림", 8.5F);
            this.lbResidence.Location = new System.Drawing.Point(34, 78);
            this.lbResidence.Name = "lbResidence";
            this.lbResidence.Size = new System.Drawing.Size(53, 12);
            this.lbResidence.TabIndex = 2;
            this.lbResidence.Text = "在留資格";
            // 
            // lbPassportEtc
            // 
            this.lbPassportEtc.AutoSize = true;
            this.lbPassportEtc.Font = new System.Drawing.Font("굴림", 8.5F);
            this.lbPassportEtc.Location = new System.Drawing.Point(34, 107);
            this.lbPassportEtc.Name = "lbPassportEtc";
            this.lbPassportEtc.Size = new System.Drawing.Size(77, 12);
            this.lbPassportEtc.TabIndex = 3;
            this.lbPassportEtc.Text = "旅券等の種類";
            // 
            // lbBirthday
            // 
            this.lbBirthday.AutoSize = true;
            this.lbBirthday.Font = new System.Drawing.Font("굴림", 8.5F);
            this.lbBirthday.Location = new System.Drawing.Point(347, 49);
            this.lbBirthday.Name = "lbBirthday";
            this.lbBirthday.Size = new System.Drawing.Size(53, 12);
            this.lbBirthday.TabIndex = 5;
            this.lbBirthday.Text = "生年月日";
            // 
            // lbPassportNo
            // 
            this.lbPassportNo.AutoSize = true;
            this.lbPassportNo.Font = new System.Drawing.Font("굴림", 8.5F);
            this.lbPassportNo.Location = new System.Drawing.Point(347, 20);
            this.lbPassportNo.Name = "lbPassportNo";
            this.lbPassportNo.Size = new System.Drawing.Size(87, 12);
            this.lbPassportNo.TabIndex = 4;
            this.lbPassportNo.Text = "パスポート番号";
            // 
            // lbGender
            // 
            this.lbGender.AutoSize = true;
            this.lbGender.Font = new System.Drawing.Font("굴림", 8.5F);
            this.lbGender.Location = new System.Drawing.Point(34, 49);
            this.lbGender.Name = "lbGender";
            this.lbGender.Size = new System.Drawing.Size(29, 12);
            this.lbGender.TabIndex = 1;
            this.lbGender.Text = "性別";
            // 
            // lbLandingPort
            // 
            this.lbLandingPort.AutoSize = true;
            this.lbLandingPort.Font = new System.Drawing.Font("굴림", 8.5F);
            this.lbLandingPort.Location = new System.Drawing.Point(347, 78);
            this.lbLandingPort.Name = "lbLandingPort";
            this.lbLandingPort.Size = new System.Drawing.Size(41, 12);
            this.lbLandingPort.TabIndex = 6;
            this.lbLandingPort.Text = "上陸地";
            // 
            // lbNationality
            // 
            this.lbNationality.AutoSize = true;
            this.lbNationality.Font = new System.Drawing.Font("굴림", 8.5F);
            this.lbNationality.Location = new System.Drawing.Point(607, 20);
            this.lbNationality.Name = "lbNationality";
            this.lbNationality.Size = new System.Drawing.Size(29, 12);
            this.lbNationality.TabIndex = 7;
            this.lbNationality.Text = "国籍";
            // 
            // lbExpireDate
            // 
            this.lbExpireDate.AutoSize = true;
            this.lbExpireDate.Font = new System.Drawing.Font("굴림", 8.5F);
            this.lbExpireDate.Location = new System.Drawing.Point(607, 49);
            this.lbExpireDate.Name = "lbExpireDate";
            this.lbExpireDate.Size = new System.Drawing.Size(53, 12);
            this.lbExpireDate.TabIndex = 8;
            this.lbExpireDate.Text = "有効期限";
            // 
            // lbLandingDate
            // 
            this.lbLandingDate.AutoSize = true;
            this.lbLandingDate.Font = new System.Drawing.Font("굴림", 8.5F);
            this.lbLandingDate.Location = new System.Drawing.Point(607, 78);
            this.lbLandingDate.Name = "lbLandingDate";
            this.lbLandingDate.Size = new System.Drawing.Size(65, 12);
            this.lbLandingDate.TabIndex = 9;
            this.lbLandingDate.Text = "上陸年月日";
            // 
            // txtPassportNm
            // 
            this.txtPassportNm.Location = new System.Drawing.Point(128, 15);
            this.txtPassportNm.Name = "txtPassportNm";
            this.txtPassportNm.ReadOnly = true;
            this.txtPassportNm.Size = new System.Drawing.Size(192, 21);
            this.txtPassportNm.TabIndex = 10;
            // 
            // txtGender
            // 
            this.txtGender.Location = new System.Drawing.Point(128, 44);
            this.txtGender.Name = "txtGender";
            this.txtGender.ReadOnly = true;
            this.txtGender.Size = new System.Drawing.Size(192, 21);
            this.txtGender.TabIndex = 11;
            // 
            // txtPassportNo
            // 
            this.txtPassportNo.Location = new System.Drawing.Point(440, 15);
            this.txtPassportNo.MaxLength = 9;
            this.txtPassportNo.Name = "txtPassportNo";
            this.txtPassportNo.ReadOnly = true;
            this.txtPassportNo.Size = new System.Drawing.Size(140, 21);
            this.txtPassportNo.TabIndex = 14;
            // 
            // txtBirthday
            // 
            this.txtBirthday.Location = new System.Drawing.Point(440, 43);
            this.txtBirthday.Name = "txtBirthday";
            this.txtBirthday.ReadOnly = true;
            this.txtBirthday.Size = new System.Drawing.Size(140, 21);
            this.txtBirthday.TabIndex = 15;
            // 
            // txtNationality
            // 
            this.txtNationality.Location = new System.Drawing.Point(686, 16);
            this.txtNationality.Name = "txtNationality";
            this.txtNationality.ReadOnly = true;
            this.txtNationality.Size = new System.Drawing.Size(140, 21);
            this.txtNationality.TabIndex = 17;
            // 
            // txtExpireDate
            // 
            this.txtExpireDate.Location = new System.Drawing.Point(686, 43);
            this.txtExpireDate.Name = "txtExpireDate";
            this.txtExpireDate.ReadOnly = true;
            this.txtExpireDate.Size = new System.Drawing.Size(140, 21);
            this.txtExpireDate.TabIndex = 18;
            // 
            // comboResidence
            // 
            this.comboResidence.FormattingEnabled = true;
            this.comboResidence.Location = new System.Drawing.Point(128, 72);
            this.comboResidence.Name = "comboResidence";
            this.comboResidence.Size = new System.Drawing.Size(192, 20);
            this.comboResidence.TabIndex = 12;
            // 
            // comboPassportEtc
            // 
            this.comboPassportEtc.FormattingEnabled = true;
            this.comboPassportEtc.Location = new System.Drawing.Point(128, 101);
            this.comboPassportEtc.Name = "comboPassportEtc";
            this.comboPassportEtc.Size = new System.Drawing.Size(192, 20);
            this.comboPassportEtc.TabIndex = 13;
            // 
            // comboLandingPort
            // 
            this.comboLandingPort.FormattingEnabled = true;
            this.comboLandingPort.Location = new System.Drawing.Point(440, 71);
            this.comboLandingPort.Name = "comboLandingPort";
            this.comboLandingPort.Size = new System.Drawing.Size(140, 20);
            this.comboLandingPort.TabIndex = 16;
            // 
            // txtLandingDate
            // 
            this.txtLandingDate.Location = new System.Drawing.Point(686, 69);
            this.txtLandingDate.Name = "txtLandingDate";
            this.txtLandingDate.Size = new System.Drawing.Size(140, 21);
            this.txtLandingDate.TabIndex = 19;
            // 
            // GTF_COS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtLandingDate);
            this.Controls.Add(this.comboLandingPort);
            this.Controls.Add(this.comboPassportEtc);
            this.Controls.Add(this.comboResidence);
            this.Controls.Add(this.txtExpireDate);
            this.Controls.Add(this.txtNationality);
            this.Controls.Add(this.txtBirthday);
            this.Controls.Add(this.txtPassportNo);
            this.Controls.Add(this.txtGender);
            this.Controls.Add(this.txtPassportNm);
            this.Controls.Add(this.lbLandingDate);
            this.Controls.Add(this.lbExpireDate);
            this.Controls.Add(this.lbNationality);
            this.Controls.Add(this.lbLandingPort);
            this.Controls.Add(this.lbGender);
            this.Controls.Add(this.lbPassportNo);
            this.Controls.Add(this.lbBirthday);
            this.Controls.Add(this.lbPassportEtc);
            this.Controls.Add(this.lbResidence);
            this.Controls.Add(this.lbPassportNm);
            this.Name = "GTF_COS";
            this.Size = new System.Drawing.Size(899, 154);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbPassportNm;
        private System.Windows.Forms.Label lbResidence;
        private System.Windows.Forms.Label lbPassportEtc;
        private System.Windows.Forms.Label lbBirthday;
        private System.Windows.Forms.Label lbPassportNo;
        private System.Windows.Forms.Label lbGender;
        private System.Windows.Forms.Label lbLandingPort;
        private System.Windows.Forms.Label lbNationality;
        private System.Windows.Forms.Label lbExpireDate;
        private System.Windows.Forms.Label lbLandingDate;
        private System.Windows.Forms.TextBox txtPassportNm;
        private System.Windows.Forms.TextBox txtGender;
        private System.Windows.Forms.TextBox txtPassportNo;
        private System.Windows.Forms.TextBox txtBirthday;
        private System.Windows.Forms.TextBox txtNationality;
        private System.Windows.Forms.TextBox txtExpireDate;
        private System.Windows.Forms.ComboBox comboResidence;
        private System.Windows.Forms.ComboBox comboPassportEtc;
        private System.Windows.Forms.ComboBox comboLandingPort;
        private System.Windows.Forms.TextBox txtLandingDate;
    }
}
