namespace GTFCommTestApp
{
    partial class GTFformTestApp
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.btPrint = new System.Windows.Forms.Button();
            this.btScanPassport = new System.Windows.Forms.Button();
            this.gtF_COS1 = new GTFCommActiveX.GTF_COS();
            this.btTestPrint = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btPrint
            // 
            this.btPrint.Location = new System.Drawing.Point(603, 142);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(113, 21);
            this.btPrint.TabIndex = 1;
            this.btPrint.Text = "Print";
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // btScanPassport
            // 
            this.btScanPassport.Location = new System.Drawing.Point(484, 142);
            this.btScanPassport.Name = "btScanPassport";
            this.btScanPassport.Size = new System.Drawing.Size(113, 21);
            this.btScanPassport.TabIndex = 2;
            this.btScanPassport.Text = "Scan";
            this.btScanPassport.UseVisualStyleBackColor = true;
            this.btScanPassport.Click += new System.EventHandler(this.btScanPassport_Click);
            // 
            // gtF_COS1
            // 
            this.gtF_COS1.AutoScroll = true;
            this.gtF_COS1.buyer_birth = "";
            this.gtF_COS1.buyer_name = "";
            this.gtF_COS1.buyer_no = "";
            this.gtF_COS1.entry_date = "";
            this.gtF_COS1.entry_port = "01";
            this.gtF_COS1.gender_code = "";
            this.gtF_COS1.Location = new System.Drawing.Point(12, 12);
            this.gtF_COS1.Name = "gtF_COS1";
            this.gtF_COS1.nationality_code = "";
            this.gtF_COS1.pass_expirydt = "";
            this.gtF_COS1.passport_serial_no = "";
            this.gtF_COS1.passport_type = "01";
            this.gtF_COS1.print_count = 1;
            this.gtF_COS1.residence_name = "00000001";
            this.gtF_COS1.Size = new System.Drawing.Size(842, 124);
            this.gtF_COS1.TabIndex = 3;
            this.gtF_COS1.time_out = 20;
            // 
            // btTestPrint
            // 
            this.btTestPrint.Location = new System.Drawing.Point(722, 142);
            this.btTestPrint.Name = "btTestPrint";
            this.btTestPrint.Size = new System.Drawing.Size(113, 21);
            this.btTestPrint.TabIndex = 5;
            this.btTestPrint.Text = "Test Print";
            this.btTestPrint.UseVisualStyleBackColor = true;
            this.btTestPrint.Click += new System.EventHandler(this.btTestPrint_Click);
            // 
            // GTFformTestApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 169);
            this.Controls.Add(this.btTestPrint);
            this.Controls.Add(this.gtF_COS1);
            this.Controls.Add(this.btScanPassport);
            this.Controls.Add(this.btPrint);
            this.Name = "GTFformTestApp";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.Button btScanPassport;
        private GTFCommActiveX.GTF_COS gtF_COS1;
        private System.Windows.Forms.Button btTestPrint;
    }
}

