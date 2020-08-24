using System;
using System.Collections.Generic;
using System.Text;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;
using PassScanDLL;
using System.Resources;
using System.Globalization;
using System.Threading;

namespace GTFCommActiveX
{
    partial class GTF_COS
    {
        public void InitializeUI()
        {
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("ko");            

            InitComboResidence();
            InitComboLandingPort();
            InitComboPassportEtc();

            Renderer = typeof(BitmapRenderer);
            passobj.CallReceivedData += new PassScan.receivedEventHandler(event_CallReceivedData);
        }

        public void InitComboResidence()
        {
            this.comboResidence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboResidence.Items.Add(new Item("00000001", Properties.ResourceResidence.StringResidence01));            
            this.comboResidence.Items.Add(new Item("00000002", Properties.ResourceResidence.StringResidence02));
            this.comboResidence.Items.Add(new Item("00000003", Properties.ResourceResidence.StringResidence03));
            this.comboResidence.Items.Add(new Item("00000004", Properties.ResourceResidence.StringResidence04));
            this.comboResidence.Items.Add(new Item("00000005", Properties.ResourceResidence.StringResidence05));
            this.comboResidence.Items.Add(new Item("00000006", Properties.ResourceResidence.StringResidence06));
            this.comboResidence.Items.Add(new Item("00000007", Properties.ResourceResidence.StringResidence07));
            this.comboResidence.Items.Add(new Item("00000008", Properties.ResourceResidence.StringResidence08));
            this.comboResidence.Items.Add(new Item("00000009", Properties.ResourceResidence.StringResidence09));
            this.comboResidence.Items.Add(new Item("00000010", Properties.ResourceResidence.StringResidence10));
            this.comboResidence.Items.Add(new Item("00000011", Properties.ResourceResidence.StringResidence11));
            this.comboResidence.Items.Add(new Item("00000012", Properties.ResourceResidence.StringResidence12));
            this.comboResidence.Items.Add(new Item("00000013", Properties.ResourceResidence.StringResidence13));
            this.comboResidence.Items.Add(new Item("00000014", Properties.ResourceResidence.StringResidence14));
            this.comboResidence.Items.Add(new Item("00000015", Properties.ResourceResidence.StringResidence15));
            this.comboResidence.Items.Add(new Item("00000016", Properties.ResourceResidence.StringResidence16));
            this.comboResidence.Items.Add(new Item("00000017", Properties.ResourceResidence.StringResidence17));
            this.comboResidence.Items.Add(new Item("00000018", Properties.ResourceResidence.StringResidence18));
            this.comboResidence.Items.Add(new Item("00000019", Properties.ResourceResidence.StringResidence19));
            this.comboResidence.Items.Add(new Item("00000020", Properties.ResourceResidence.StringResidence20));
            this.comboResidence.Items.Add(new Item("00000021", Properties.ResourceResidence.StringResidence21));
            this.comboResidence.Items.Add(new Item("00000022", Properties.ResourceResidence.StringResidence22));
            this.comboResidence.Items.Add(new Item("00000023", Properties.ResourceResidence.StringResidence23));
            this.comboResidence.Items.Add(new Item("00000024", Properties.ResourceResidence.StringResidence24));
            this.comboResidence.Items.Add(new Item("00000025", Properties.ResourceResidence.StringResidence25));
            this.comboResidence.Items.Add(new Item("00000026", Properties.ResourceResidence.StringResidence26));
            this.comboResidence.Items.Add(new Item("00000027", Properties.ResourceResidence.StringResidence27));
            this.comboResidence.DisplayMember = "Text";
            this.comboResidence.ValueMember = "Value";
            this.comboResidence.SelectedIndex = 0;
        }

        public void InitComboLandingPort()
        {
            this.comboLandingPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLandingPort.Items.Add(new Item("01", Properties.ResourceLandingPort.StringLandingPort01));
            this.comboLandingPort.Items.Add(new Item("02", Properties.ResourceLandingPort.StringLandingPort02));
            this.comboLandingPort.Items.Add(new Item("03", Properties.ResourceLandingPort.StringLandingPort03));
            this.comboLandingPort.Items.Add(new Item("04", Properties.ResourceLandingPort.StringLandingPort04));
            this.comboLandingPort.Items.Add(new Item("05", Properties.ResourceLandingPort.StringLandingPort05));
            this.comboLandingPort.Items.Add(new Item("06", Properties.ResourceLandingPort.StringLandingPort06));
            this.comboLandingPort.Items.Add(new Item("07", Properties.ResourceLandingPort.StringLandingPort07));
            this.comboLandingPort.Items.Add(new Item("08", Properties.ResourceLandingPort.StringLandingPort08));
            this.comboLandingPort.Items.Add(new Item("09", Properties.ResourceLandingPort.StringLandingPort09));
            this.comboLandingPort.Items.Add(new Item("10", Properties.ResourceLandingPort.StringLandingPort10));
            this.comboLandingPort.Items.Add(new Item("11", Properties.ResourceLandingPort.StringLandingPort11));
            this.comboLandingPort.Items.Add(new Item("12", Properties.ResourceLandingPort.StringLandingPort12));
            this.comboLandingPort.Items.Add(new Item("13", Properties.ResourceLandingPort.StringLandingPort13));
            this.comboLandingPort.Items.Add(new Item("14", Properties.ResourceLandingPort.StringLandingPort14));
            this.comboLandingPort.Items.Add(new Item("15", Properties.ResourceLandingPort.StringLandingPort15));
            this.comboLandingPort.Items.Add(new Item("16", Properties.ResourceLandingPort.StringLandingPort16));
            this.comboLandingPort.Items.Add(new Item("17", Properties.ResourceLandingPort.StringLandingPort17));
            this.comboLandingPort.Items.Add(new Item("18", Properties.ResourceLandingPort.StringLandingPort18));
            this.comboLandingPort.Items.Add(new Item("19", Properties.ResourceLandingPort.StringLandingPort19));
            this.comboLandingPort.Items.Add(new Item("20", Properties.ResourceLandingPort.StringLandingPort20));
            this.comboLandingPort.Items.Add(new Item("21", Properties.ResourceLandingPort.StringLandingPort21));
            this.comboLandingPort.Items.Add(new Item("22", Properties.ResourceLandingPort.StringLandingPort22));
            this.comboLandingPort.Items.Add(new Item("23", Properties.ResourceLandingPort.StringLandingPort23));
            this.comboLandingPort.Items.Add(new Item("24", Properties.ResourceLandingPort.StringLandingPort24));
            this.comboLandingPort.Items.Add(new Item("25", Properties.ResourceLandingPort.StringLandingPort25));
            this.comboLandingPort.Items.Add(new Item("26", Properties.ResourceLandingPort.StringLandingPort26));
            this.comboLandingPort.Items.Add(new Item("27", Properties.ResourceLandingPort.StringLandingPort27));
            this.comboLandingPort.Items.Add(new Item("28", Properties.ResourceLandingPort.StringLandingPort28));
            this.comboLandingPort.Items.Add(new Item("29", Properties.ResourceLandingPort.StringLandingPort29));
            this.comboLandingPort.Items.Add(new Item("30", Properties.ResourceLandingPort.StringLandingPort30));
            this.comboLandingPort.Items.Add(new Item("31", Properties.ResourceLandingPort.StringLandingPort31));
            this.comboLandingPort.Items.Add(new Item("32", Properties.ResourceLandingPort.StringLandingPort32));
            this.comboLandingPort.Items.Add(new Item("33", Properties.ResourceLandingPort.StringLandingPort33));
            this.comboLandingPort.Items.Add(new Item("34", Properties.ResourceLandingPort.StringLandingPort34));
            this.comboLandingPort.Items.Add(new Item("35", Properties.ResourceLandingPort.StringLandingPort35));
            this.comboLandingPort.DisplayMember = "Text";
            this.comboLandingPort.ValueMember = "Value";
            this.comboLandingPort.SelectedIndex = 0;
        }

        public void InitComboPassportEtc()
        {
            this.comboPassportEtc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPassportEtc.Items.Add(new Item("01", Properties.ResourcePassportEtc.StringPassportEtc01));  // 여권
            this.comboPassportEtc.Items.Add(new Item("02", Properties.ResourcePassportEtc.StringPassportEtc02));   // 외교관
            this.comboPassportEtc.Items.Add(new Item("03", Properties.ResourcePassportEtc.StringPassportEtc03));  // 승무원상륙허가서
            this.comboPassportEtc.Items.Add(new Item("04", Properties.ResourcePassportEtc.StringPassportEtc04));   // 긴급상륙허가서
            this.comboPassportEtc.Items.Add(new Item("05", Properties.ResourcePassportEtc.StringPassportEtc05));   // 조난상륙허가서
            this.comboPassportEtc.DisplayMember = "Text";
            this.comboPassportEtc.ValueMember = "Value";
            this.comboPassportEtc.SelectedIndex = 0;
        }
    }
}
