using System;
using System.Collections.Generic;
using System.Text;
using PassScanDLL;

namespace GTFCommActiveX
{
    partial class GTF_COS
    {
        public int PassScan()
        {
            if (!passobj._serialPort.IsOpen)
            {
                if (passobj.Open())
                {
                    //lbMessage.Text = passobj.portno + " Open 성공!";
                }
                else
                {
                    //lbMessage.Text = passobj.portno + " Open 실패!";
                    OnGetPassportInfo("", -1);
                }
            }

            if (passobj._serialPort.IsOpen)
            {
                passobj.timeout = _timeout;
                passobj.Scan();
                //lbMessage.Text = "스캔시작...";
            }
            return 1;
        }

        public int PassSetInfo()
        {
            txtPassportNm.Text = _buyer_name.ToUpper();
            txtNationality.Text = _nationality_code;
            txtGender.Text = _gender_code;
            txtPassportNo.Text = _passport_serial_no;
            txtBirthday.Text = _buyer_birth;
            txtExpireDate.Text = _pass_expirydt;
            txtLandingDate.Text = _entry_date;

            if (!_entry_port.Equals(""))
            {
                comboLandingPort.SelectedIndex = Convert.ToInt32(_entry_port) - 1;
            }

            if (!_residence_name.Equals(""))
            {
                comboResidence.SelectedIndex = Convert.ToInt32(_residence_name) - 1;
            }

            if (!_passport_type.Equals(""))
            {
                comboPassportEtc.SelectedIndex = Convert.ToInt32(_passport_type) - 1;
            }

            return 1;
        }

        void event_CallReceivedData(object sender, receiveEventArgs e)
        {
            writeMessage(e.MRZ1, e.MRZ2);
        }

        public void writeMessage(string mrz1, string mrz2)
        {
            try
            {
                if (this.InvokeRequired == false)
                {
                    if (passobj.errcode == 1)
                    {
                        parsePassportInfo(mrz1, mrz2);
                        //lbMessage.Text = "스캔완료";
                    }
                    else
                    {
                        //lbMessage.Text = "스캔 재시도!";
                        OnGetPassportInfo("", -2);
                    }
                }
                else
                {
                    writeMessageDelegate dd = new writeMessageDelegate(writeMessage);

                    object[] t = new object[] { mrz1, mrz2 };
                    //this.BeginInvoke(dd, t);
                    //동기방식  
                    this.Invoke(dd, t);
                }
            }
            catch (Exception er)
            {
                Console.WriteLine(er.ToString());
            }
        }

        /// <summary>
        /// Raise Event
        /// </summary>
        private void OnGetPassportInfo(string param, int ret)
        {
            // 인자값을 넘긴다.
            if (getPassportInfo != null)
            {
                Invoke(getPassportInfo, param, ret);
                Console.WriteLine(param);
            }
        }
    }
}
