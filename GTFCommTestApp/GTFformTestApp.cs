using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GTFCommTestApp
{    
    public partial class GTFformTestApp : Form
    {
        public GTFformTestApp()
        {
            InitializeComponent();


        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            
        }

        private void btScanPassport_Click(object sender, EventArgs e)
        {
            gtF_COS1.PassScan();
        }

        private void btTest_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(gtF_COS1.residence_name);
            //gtF_COS1.entry_port = "17";
            gtF_COS1.PassSetInfo();
        }

        private void btTestPrint_Click(object sender, EventArgs e)
        {
            string docid = "KR|01|0|20600019150313102211";
            string retailer = "宮古島税務署(沖縄県)|宮古島市 宮古郡|hakata_pshop|서울특별시 중구 퇴계로 131 (충무로2가 64-5) 신일빌딩 9층";
            string tourist = "04|M19845876|doohyun jung|韓国|19840321|外交|大阪国際フェリーターミナル|20150302|20190518";
            string goods = "20150313|2";            
            goods += "|01|2";
            goods += "|MildSeven 1mg|1000|10|10000";
            goods += "|otoko bread|1600|5|8000";
            goods += "|1440|18000|1152";
            goods += "|02|2";
            goods += "|Sony PlayStation4|20000|1|20000";
            goods += "|etc|3333|3|10000";
            goods += "|2400|30000|1920";
            goods += "|48000|3072|768|2304";
            //string adsinfo = "02|01|http://localhost:7004/upload/000001/20150313/gtf이벤트.jpg";
            //string adsinfo = "03|01|AD Test 01|AD Test 02 : ABCD|AD Test 03 : EFGHIJK|AD Test 04 : TEST Line 4|AD Test 05 : OTL...";
            string adsinfo = "";

            gtF_COS1.JPNPrintTicket(docid, retailer, tourist, goods, adsinfo);
        }
    }
}
