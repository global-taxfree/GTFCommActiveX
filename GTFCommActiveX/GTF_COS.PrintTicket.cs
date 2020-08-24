using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using PrintDocumentDLL;
using System.Drawing;
using System.Resources;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;
using System.Reflection;
using System.Globalization;
using System.Threading;

namespace GTFCommActiveX
{
    partial class GTF_COS
    {
        public static int SlipType01 = 1;   // 수출면세물품구입기록표
        public static int SlipType02 = 2;   // 면세물품구매자확약서
        public static int SlipType03 = 3;   // 구입자보관용

        Dictionary<string, string> MapDocid = new Dictionary<string, string>();
        Dictionary<string, string> MapRetailer = new Dictionary<string, string>();
        Dictionary<string, string> MapTourist = new Dictionary<string, string>();
        Dictionary<string, object> MapGoods = new Dictionary<string, object>();
        Dictionary<string, string> MapAdsInfo = new Dictionary<string, string>();
        
        StringFormat stringFormat = new StringFormat();
        Font printFont = new Font("Meiryo", 8);

        public int JPNPrintTicket(string docid, string retailer, string tourist, string goods, string adsinfo)
        {
            setClearParseMap();

            int nRet = 0;

            try
            {
                if (!ParseParam(docid, retailer, tourist, goods, adsinfo))
                {
                    nRet = -1;
                    return nRet;
                }

                // 수출면세물품구입기록표
                UserPrintDocument printDocObj = new UserPrintDocument();
                printDocObj.UserPrintPageEvent += new UserPrintDocument.UserPrintPageEventHandler(eventPrintDoc01);
                printDocObj.Print();
                
                
                if (Convert.ToInt32(MapDocid[DocID.PublichType]) == SlipType02)
                {
                    // 면세물품구매자확약서
                    printDocObj = new UserPrintDocument();
                    printDocObj.UserPrintPageEvent += new UserPrintDocument.UserPrintPageEventHandler(eventPrintDoc02);
                    printDocObj.Print();                    
                }
                
                // 구입자 보관용
                printDocObj = new UserPrintDocument();
                printDocObj.UserPrintPageEvent += new UserPrintDocument.UserPrintPageEventHandler(eventPrintDoc03);
                printDocObj.Print();
                
            }
            catch(Exception e)
            {
                MessageBox.Show(this, e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nRet = -1;
            }

            return nRet;
        }
        
        // 수출면세물품구입기록표
        public void eventPrintDoc01(object sender, PrintPageEventArgs e)
        {
            float yPos = 0;
            
            PrintHeader(e, ref yPos, SlipType01);

            PrintRetailer(e, ref yPos);

            PrintGoodsDetails(e, ref yPos, false);

            PrintTouristDetails(e, ref yPos);
            
            PrintFooter(e, ref yPos, SlipType01);

            e.HasMorePages = false;
        }

        // 면세물품구매자확약서
        public void eventPrintDoc02(object sender, PrintPageEventArgs e)
        {
            float yPos = 0;

            PrintHeader(e, ref yPos, SlipType02);

            PrintRetailer(e, ref yPos);

            PrintGoodsDetails(e, ref yPos, false);

            PrintTouristDetails(e, ref yPos);
           
            PrintFooter(e, ref yPos, SlipType02);

            e.HasMorePages = false;
        }

        // 구입자 보관용
        public void eventPrintDoc03(object sender, PrintPageEventArgs e)
        {
            float yPos = 0;

            PrintHeader(e, ref yPos, SlipType03);

            PrintRetailer(e, ref yPos);

            PrintTouristDetails(e, ref yPos);

            PrintGoodsDetails(e, ref yPos, true);

            PrintFooter(e, ref yPos, SlipType03);

            e.HasMorePages = false;
        }
                
        public void PrintHeader(PrintPageEventArgs e, ref float yPos, int type)
        {
            try
            {
                // 1. Jpn Logo
                PrintJPNLogo(e, ref yPos);

                // 2. Slip Title (Republish)
                printFont = new Font("Meiryo", 8);
                string strText = "";
                switch (type)
                {
                    case 1: // 수출면세물품구입기록표
                        strText = getSlipText(Properties.ResourceString.SlipType01);
                        break;
                    case 2: // 면세물품구매자확약서
                        strText = getSlipText(Properties.ResourceString.SlipType02);
                        break;
                    case 3: // 구입자보관용
                        strText = getSlipText(Properties.ResourceString.SlipType03);
                        break;
                }
                e.Graphics.DrawString(strText, printFont, Brushes.Black, 0, yPos, stringFormat);

                if (MapDocid[DocID.LangCode].Equals(LangCode.en_US))
                    yPos += 8 * 2 * 3;  // font emSize * 2 * linecount
                else
                    yPos += printFont.GetHeight(e.Graphics) + 10;

                if (Convert.ToInt32(MapDocid[DocID.RePublish]) > 0)
                    PrintRePublish(e, ref yPos);

                // 3. Corp Logo
                PrintCorpLogo(e, ref yPos);

                // 4. Barcode            
                PrintBarcode(e, ref yPos);

                // Add Margin
                yPos += 70;

                // 5. Slip No.
                printFont = new Font("Meiryo", 7);
                e.Graphics.DrawString(String.Format("{0} : {1}", getSlipText(Properties.ResourceString.SlipNo), MapDocid[DocID.SlipNo]), printFont, Brushes.Black, 0, yPos, stringFormat);
                yPos += printFont.GetHeight(e.Graphics);

                yPos += 10;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }            
        }

        public void PrintFooter(PrintPageEventArgs e, ref float yPos, int type)
        {
            try
            {
                int printLineHeight = 10;

                printFont = new Font("Meiryo", 6);
                switch (type)
                {
                    case 1: // 수출면세물품구입기록표
                        {
                            PrintLine(e, ref yPos);
                            yPos += printLineHeight;

                            printFont = new Font("Meiryo", 6);
                            
                            Rectangle rect = new Rectangle(0, (int)yPos, 284, 36);
                            e.Graphics.DrawString(Properties.Resources.StringSlipType01Desc01, printFont, Brushes.Black, rect);
                            e.Graphics.DrawRectangle(Pens.White, Rectangle.Round(rect));
                            yPos += printFont.GetHeight(e.Graphics) + 26;

                            rect = new Rectangle(0, (int)yPos, 284, 24);
                            e.Graphics.DrawString(Properties.Resources.StringSlipType01Desc02, printFont, Brushes.Black, rect);
                            e.Graphics.DrawRectangle(Pens.White, Rectangle.Round(rect));
                            yPos += printFont.GetHeight(e.Graphics) + 14;

                            rect = new Rectangle(0, (int)yPos, 284, 36);
                            e.Graphics.DrawString(Properties.Resources.StringSlipType01Desc03, printFont, Brushes.Black, rect);
                            e.Graphics.DrawRectangle(Pens.White, Rectangle.Round(rect));
                            yPos += printFont.GetHeight(e.Graphics) + 26;

                            rect = new Rectangle(0, (int)yPos, 284, 60);
                            e.Graphics.DrawString(Properties.Resources.StringSlipType01Desc04, printFont, Brushes.Black, rect);
                            e.Graphics.DrawRectangle(Pens.White, Rectangle.Round(rect));
                            yPos += printFont.GetHeight(e.Graphics) + 50;

                            yPos += printLineHeight;

                            // 국가별 출력 구분
                            rect = new Rectangle(0, (int)yPos, 284, getRectHeight(Properties.ResourceString.SlipType01Desc01));
                            e.Graphics.DrawString(getLangString(Properties.ResourceString.SlipType01Desc01), printFont, Brushes.Black, rect);
                            e.Graphics.DrawRectangle(Pens.White, Rectangle.Round(rect));
                            yPos += printFont.GetHeight(e.Graphics) + getRectHeight(Properties.ResourceString.SlipType01Desc01) - printLineHeight;

                            rect = new Rectangle(0, (int)yPos, 284, getRectHeight(Properties.ResourceString.SlipType01Desc02));
                            e.Graphics.DrawString(getLangString(Properties.ResourceString.SlipType01Desc02), printFont, Brushes.Black, rect);
                            e.Graphics.DrawRectangle(Pens.White, Rectangle.Round(rect));
                            yPos += printFont.GetHeight(e.Graphics) + getRectHeight(Properties.ResourceString.SlipType01Desc02) - printLineHeight;

                            rect = new Rectangle(0, (int)yPos, 284, getRectHeight(Properties.ResourceString.SlipType01Desc03));
                            e.Graphics.DrawString(getLangString(Properties.ResourceString.SlipType01Desc03), printFont, Brushes.Black, rect);
                            e.Graphics.DrawRectangle(Pens.White, Rectangle.Round(rect));
                            yPos += printFont.GetHeight(e.Graphics) + getRectHeight(Properties.ResourceString.SlipType01Desc03) - printLineHeight;

                            rect = new Rectangle(0, (int)yPos, 284, getRectHeight(Properties.ResourceString.SlipType01Desc04)+1);
                            e.Graphics.DrawString(getLangString(Properties.ResourceString.SlipType01Desc04), printFont, Brushes.Black, rect);
                            e.Graphics.DrawRectangle(Pens.White, Rectangle.Round(rect));
                            yPos += printFont.GetHeight(e.Graphics) + getRectHeight(Properties.ResourceString.SlipType01Desc04) - printLineHeight;

                            yPos += printLineHeight;
                        }
                        break;
                    case 2: // 면세물품구매자확약서
                        {
                            PrintLine(e, ref yPos);

                            printFont = new Font("Meiryo", 6);

                            Rectangle rect = new Rectangle(0, (int)yPos, 284, 24);
                            e.Graphics.DrawString(Properties.Resources.StringSlipType02Desc01, printFont, Brushes.Black, rect);
                            e.Graphics.DrawRectangle(Pens.White, Rectangle.Round(rect));
                            yPos += printFont.GetHeight(e.Graphics) + 14;

                            // 국가별 출력 구분
                            rect = new Rectangle(0, (int)yPos, 284, getRectHeight(Properties.ResourceString.SlipType02Desc01));
                            e.Graphics.DrawString(getLangString(Properties.ResourceString.SlipType02Desc01), printFont, Brushes.Black, rect);
                            e.Graphics.DrawRectangle(Pens.White, Rectangle.Round(rect));
                            yPos += printFont.GetHeight(e.Graphics) + getRectHeight(Properties.ResourceString.SlipType02Desc01) - printLineHeight;

                            yPos += printLineHeight * 2;

                            rect = new Rectangle(0, (int)yPos, 284, 24);
                            e.Graphics.DrawString(Properties.Resources.StringSlipType02Desc02, printFont, Brushes.Black, rect);
                            e.Graphics.DrawRectangle(Pens.White, Rectangle.Round(rect));
                            yPos += printFont.GetHeight(e.Graphics) + 14;

                            // 국가별 출력 구분
                            rect = new Rectangle(0, (int)yPos, 284, getRectHeight(Properties.ResourceString.SlipType02Desc02));
                            e.Graphics.DrawString(getLangString(Properties.ResourceString.SlipType02Desc02), printFont, Brushes.Black, rect);
                            e.Graphics.DrawRectangle(Pens.White, Rectangle.Round(rect));
                            yPos += printFont.GetHeight(e.Graphics) + getRectHeight(Properties.ResourceString.SlipType02Desc02) - printLineHeight;
                            
                            yPos += printLineHeight * 5;
                            
                            StringFormat strFormat = new StringFormat();
                            strFormat.Alignment = StringAlignment.Far;

                            e.Graphics.DrawString(getLangString(Properties.ResourceString.Signature), new Font("Consolas", 8), Brushes.Black, new Rectangle(0, (int)yPos, 284, 16), strFormat);                            
                            yPos += printFont.GetHeight(e.Graphics);
                        }
                        break;
                    case 3: // 구입자보관용
                        {
                            yPos += printLineHeight * 5;
                        }
                        break;
                }

                PrintAds(e, ref yPos);

                PrintEndMark(e, ref yPos);
            }            
            catch (System.Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public void PrintRePublish(PrintPageEventArgs e, ref float yPos)
        {
            try
            {
                stringFormat.LineAlignment = StringAlignment.Center;
                if (MapDocid[DocID.LangCode].Equals("EN")) yPos += 25;

                string strRePublish = String.Format("[{0}]", getSlipText(Properties.ResourceString.RePublish));
                e.Graphics.DrawString(strRePublish, printFont, Brushes.Black, 80, yPos, stringFormat);

                yPos += printFont.GetHeight(e.Graphics);
                stringFormat.LineAlignment = StringAlignment.Near;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public void PrintJPNLogo(PrintPageEventArgs e, ref float yPos)
        {
            try
            {
                Bitmap jpn_logo = (Bitmap)Properties.Resources.jpn_logo;
                e.Graphics.DrawImage((Image)jpn_logo, new Rectangle(0, (int)yPos, jpn_logo.Width, jpn_logo.Height));
                yPos = jpn_logo.Height;
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        public void PrintCorpLogo(PrintPageEventArgs e, ref float yPos)
        {
            try
            {
                ResourceManager rm = Properties.Resources.ResourceManager;

                Bitmap corp_logo = (Bitmap)rm.GetObject("gtf_logo");
                e.Graphics.DrawImage((Image)corp_logo, new Rectangle(0, (int)yPos, corp_logo.Width, corp_logo.Height));
                yPos += corp_logo.Height;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public void PrintBarcode(PrintPageEventArgs e, ref float yPos)
        {
            try
            {
                var writer = new BarcodeWriter
                {
                    Format = (BarcodeFormat)BarcodeFormat.CODE_128,
                    Options = EncodingOptions ?? new EncodingOptions
                    {
                        Height = 58,
                        Width = 250
                    },
                    Renderer = (IBarcodeRenderer<Bitmap>)Activator.CreateInstance(Renderer)
                };

                Bitmap qrbmp = writer.Write(MapDocid[DocID.SlipNo]);

                Image img = (Image)qrbmp;
                Point p = new Point(0, (int)Math.Round(yPos));
                e.Graphics.DrawImage(img, p);
                yPos += printFont.GetHeight(e.Graphics);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public void PrintRetailer(PrintPageEventArgs e, ref float yPos)
        {
            try
            {
                PrintLine(e, ref yPos);

                printFont = new Font("Meiryo", 7, FontStyle.Bold);
                e.Graphics.DrawString(Properties.Resources.StringRetailer, printFont, Brushes.Black, 0, yPos, stringFormat);
                yPos += printFont.GetHeight(e.Graphics);

                PrintLine(e, ref yPos);

                // 1. 소속 세무서
                printFont = new Font("Meiryo", 7);
                e.Graphics.DrawString(String.Format("{0} : {1}", getSlipText(Properties.ResourceString.TaxOffice), MapRetailer[Retailer.TaxOffice]), printFont, Brushes.Black, 0, yPos, stringFormat);
                yPos += printFont.GetHeight(e.Graphics);

                // 2. 납세지            
                e.Graphics.DrawString(String.Format("{0} : {1}", getSlipText(Properties.ResourceString.TaxPlace), MapRetailer[Retailer.TaxPlace]), printFont, Brushes.Black, 0, yPos, stringFormat);
                yPos += printFont.GetHeight(e.Graphics);

                // 3. 판매자성명 혹은 상호
                string strTemp = String.Format("{0} : {1}", getSlipText(Properties.ResourceString.Seller), MapRetailer[Retailer.Seller]);
                if (strTemp.Length > 28)
                {
                    e.Graphics.DrawString(strTemp.Substring(0, 28) + "\r\n" + strTemp.Substring(28, strTemp.Length - 28), printFont, Brushes.Black, 0, yPos, stringFormat);
                    yPos += printFont.GetHeight(e.Graphics) + 10;
                }
                else
                {
                    e.Graphics.DrawString(strTemp, printFont, Brushes.Black, 0, yPos, stringFormat);
                    yPos += printFont.GetHeight(e.Graphics);
                }

                yPos += 10;

                // 4. 판매장 주소
                e.Graphics.DrawString(getSlipText(Properties.ResourceString.SellerAddr), printFont, Brushes.Black, 0, yPos, stringFormat);
                yPos += printFont.GetHeight(e.Graphics);

                if (MapRetailer[Retailer.SellerAddr].Length > 30)
                {
                    e.Graphics.DrawString(MapRetailer[Retailer.SellerAddr].Substring(0, 30) + "\r\n" + MapRetailer[Retailer.SellerAddr].Substring(30, MapRetailer[Retailer.SellerAddr].Length - 30), printFont, Brushes.Black, 0, yPos, stringFormat);
                    yPos += printFont.GetHeight(e.Graphics) + 10;
                }
                else
                {
                    e.Graphics.DrawString(MapRetailer[Retailer.SellerAddr], printFont, Brushes.Black, 0, yPos, stringFormat);
                    yPos += printFont.GetHeight(e.Graphics) + 10;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }            
        }

        public void PrintTouristDetails(PrintPageEventArgs e, ref float yPos)
        {
            try
            {
                PrintLine(e, ref yPos);
                printFont = new Font("Meiryo", 7, FontStyle.Bold);
                e.Graphics.DrawString(Properties.Resources.StringTouristDetails, printFont, Brushes.Black, 0, yPos, stringFormat);
                yPos += printFont.GetHeight(e.Graphics);
                PrintLine(e, ref yPos);

                printFont = new Font("Meiryo", 7);

                // 1. 여권 종류
                e.Graphics.DrawString(String.Format("{0} : {1}", getSlipText(Properties.ResourceString.PassPortType), MapTourist[Tourist.PassportType]), printFont, Brushes.Black, 0, yPos, stringFormat);
                yPos += printFont.GetHeight(e.Graphics);

                // 2. 여권 번호
                e.Graphics.DrawString(String.Format("{0} : {1}", getSlipText(Properties.ResourceString.PassPortNo), MapTourist[Tourist.PassportNo]), printFont, Brushes.Black, 0, yPos, stringFormat);
                yPos += printFont.GetHeight(e.Graphics);

                // 3. 구매자 성명
                e.Graphics.DrawString(String.Format("{0} : {1}", getSlipText(Properties.ResourceString.BuyerName), MapTourist[Tourist.Name]), printFont, Brushes.Black, 0, yPos, stringFormat);
                yPos += printFont.GetHeight(e.Graphics);

                // 4. 국적
                e.Graphics.DrawString(String.Format("{0} : {1}", getSlipText(Properties.ResourceString.National), MapTourist[Tourist.National]), printFont, Brushes.Black, 0, yPos, stringFormat);
                yPos += printFont.GetHeight(e.Graphics);

                // 5. 생년월일
                e.Graphics.DrawString(String.Format("{0} : {1}", getSlipText(Properties.ResourceString.Birth), MapTourist[Tourist.Birth]), printFont, Brushes.Black, 0, yPos, stringFormat);
                yPos += printFont.GetHeight(e.Graphics);

                // 6. 체류자격
                e.Graphics.DrawString(String.Format("{0} : {1}", getSlipText(Properties.ResourceString.Residence), MapTourist[Tourist.Residence]), printFont, Brushes.Black, 0, yPos, stringFormat);
                yPos += printFont.GetHeight(e.Graphics);

                // 7. 상륙지
                e.Graphics.DrawString(String.Format("{0} : {1}", getSlipText(Properties.ResourceString.LandingPlace), MapTourist[Tourist.LandingPlace]), printFont, Brushes.Black, 0, yPos, stringFormat);
                yPos += printFont.GetHeight(e.Graphics);

                // 8. 상륙년월일
                e.Graphics.DrawString(String.Format("{0} : {1}", getSlipText(Properties.ResourceString.LandingDate), MapTourist[Tourist.LandingDate]), printFont, Brushes.Black, 0, yPos, stringFormat);
                yPos += printFont.GetHeight(e.Graphics);

                yPos += 10;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }            
        }

        public void PrintGoodsDetails(PrintPageEventArgs e, ref float yPos, bool bIsCustomerCopy)
        {
            try
            {
                PrintLine(e, ref yPos);
                printFont = new Font("Meiryo", 7, FontStyle.Bold);
                e.Graphics.DrawString(Properties.Resources.StringGoodsDetails, printFont, Brushes.Black, 0, yPos, stringFormat);
                yPos += printFont.GetHeight(e.Graphics);
                PrintLine(e, ref yPos);

                // 1. 구입년월일
                e.Graphics.DrawString(String.Format("{0} : {1}", getSlipText(Properties.ResourceString.BuyDate), MapGoods[Goods.SaleDate]), printFont, Brushes.Black, 0, yPos, stringFormat);
                yPos += printFont.GetHeight(e.Graphics);

                for (int i = 0; i < Convert.ToInt32(MapGoods[Goods.ItemTypeSeq]); i++)
                {
                    string ItemMapKey = String.Format("ItemsMap_{0}", i);
                    Dictionary<string, object> ItemMap = (Dictionary<string, object>)MapGoods[ItemMapKey];

                    printFont = new Font("Meiryo", 7, FontStyle.Bold);
                    if (ItemMap[Goods.ItemTypeCode].Equals("01"))       // 소모품
                        e.Graphics.DrawString(getSlipText(Properties.ResourceString.COMM), printFont, Brushes.Black, 0, yPos, stringFormat);
                    else if (ItemMap[Goods.ItemTypeCode].Equals("02"))    // 일반품목
                        e.Graphics.DrawString(getSlipText(Properties.ResourceString.EXCOMM), printFont, Brushes.Black, 0, yPos, stringFormat);
                    yPos += printFont.GetHeight(e.Graphics);

                    printFont = new Font("Meiryo", 7);
                    // Layout 형식으로 출력 변경
                    StringFormat strFormat = new StringFormat();
                    strFormat.Alignment = StringAlignment.Center;

                    // 총 Width : 284로 계산.
                    // 품명
                    e.Graphics.DrawString(getLangString(Properties.ResourceString.GoodsName), printFont, Brushes.Black, new Rectangle(0, (int)yPos, 100, 12), strFormat);
                    // 단가
                    e.Graphics.DrawString(getLangString(Properties.ResourceString.GoodsPrice), printFont, Brushes.Black, new Rectangle(101, (int)yPos, 54, 12), strFormat);
                    // 수량
                    e.Graphics.DrawString(getLangString(Properties.ResourceString.GoodsAmt), printFont, Brushes.Black, new Rectangle(155, (int)yPos, 54, 12), strFormat);
                    // 판매가격
                    e.Graphics.DrawString(getLangString(Properties.ResourceString.SaleAmt), printFont, Brushes.Black, new Rectangle(209, (int)yPos, 75, 12), strFormat);

                    yPos += printFont.GetHeight(e.Graphics);

                    for (int j = 0; j < Convert.ToInt32(ItemMap[Goods.GoodsSeq]); j++)
                    {
                        string GoodsMapKey = String.Format("GoodsMap_{0}", j);
                        Dictionary<string, object> GoodsMap = (Dictionary<string, object>)ItemMap[GoodsMapKey];

                        // 품명
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString((string)GoodsMap[Goods.Name], printFont, Brushes.Black, new Rectangle(0, (int)yPos, 100, 12), strFormat);
                        // 단가
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(String.Format("{0}", Convert.ToInt32(GoodsMap[Goods.UnitPrice]).ToString("#,##0")), printFont, Brushes.Black, new Rectangle(101, (int)yPos, 54, 12), strFormat);
                        // 수량
                        e.Graphics.DrawString(String.Format("{0}", Convert.ToInt32(GoodsMap[Goods.Qty]).ToString("#,##0")), printFont, Brushes.Black, new Rectangle(155, (int)yPos, 54, 12), strFormat);
                        // 판매가격
                        e.Graphics.DrawString(String.Format("{0}", Convert.ToInt32(GoodsMap[Goods.Amt]).ToString("#,##0")), printFont, Brushes.Black, new Rectangle(209, (int)yPos, 75, 12), strFormat);

                        yPos += printFont.GetHeight(e.Graphics);
                    }

                    yPos += 10;

                    strFormat.Alignment = StringAlignment.Far;
                    // 세금금액
                    e.Graphics.DrawString(getSlipText(Properties.ResourceString.TaxAmt), printFont, Brushes.Black, new Rectangle(0, (int)yPos, 210, 12), strFormat);
                    e.Graphics.DrawString(String.Format("{0,13}", Convert.ToInt32(ItemMap[Goods.TaxAmt]).ToString("#,##0")), printFont, Brushes.Black, new Rectangle(211, (int)yPos, 73, 12), strFormat);
                    yPos += printFont.GetHeight(e.Graphics);
                    // 합계금액
                    e.Graphics.DrawString(getSlipText(Properties.ResourceString.TotalAmt), printFont, Brushes.Black, new Rectangle(0, (int)yPos, 210, 12), strFormat);
                    e.Graphics.DrawString(String.Format("{0,13}", Convert.ToInt32(ItemMap[Goods.SaleAmt]).ToString("#,##0")), printFont, Brushes.Black, new Rectangle(211, (int)yPos, 73, 12), strFormat);
                    yPos += printFont.GetHeight(e.Graphics);
                    // 환급금액
                    e.Graphics.DrawString(getSlipText(Properties.ResourceString.RefundAmt), printFont, Brushes.Black, new Rectangle(0, (int)yPos, 210, 12), strFormat);
                    e.Graphics.DrawString(String.Format("{0,13}", Convert.ToInt32(ItemMap[Goods.RefundAmt]).ToString("#,##0")), printFont, Brushes.Black, new Rectangle(211, (int)yPos, 73, 12), strFormat);
                    yPos += printFont.GetHeight(e.Graphics);
                }

                if (bIsCustomerCopy)
                {
                    PrintLine(e, ref yPos);
                    yPos += 10;

                    StringFormat strFormat = new StringFormat();
                    strFormat.Alignment = StringAlignment.Far;

                    printFont = new Font("Meiryo", 7, FontStyle.Bold);

                    // 총 판매금액
                    e.Graphics.DrawString(getSlipText(Properties.ResourceString.TotalSaleAmt), printFont, Brushes.Black, new Rectangle(0, (int)yPos, 210, 12), strFormat);
                    e.Graphics.DrawString(String.Format("{0,13}", Convert.ToInt32(MapGoods[Goods.TotalSaleAmt]).ToString("#,##0")), printFont, Brushes.Black, new Rectangle(211, (int)yPos, 73, 12), strFormat);
                    yPos += printFont.GetHeight(e.Graphics);
                    // 총 환급금액
                    e.Graphics.DrawString(getSlipText(Properties.ResourceString.TotalRefundAmt), printFont, Brushes.Black, new Rectangle(0, (int)yPos, 210, 12), strFormat);
                    e.Graphics.DrawString(String.Format("{0,13}", Convert.ToInt32(MapGoods[Goods.TotalRefundAmt]).ToString("#,##0")), printFont, Brushes.Black, new Rectangle(211, (int)yPos, 73, 12), strFormat);
                    yPos += printFont.GetHeight(e.Graphics);
                    // 총 수수료
                    e.Graphics.DrawString(getSlipText(Properties.ResourceString.TotalFeeAmt), printFont, Brushes.Black, new Rectangle(0, (int)yPos, 210, 12), strFormat);
                    e.Graphics.DrawString(String.Format("{0,13}", Convert.ToInt32(MapGoods[Goods.TotalFeeAmt]).ToString("#,##0")), printFont, Brushes.Black, new Rectangle(211, (int)yPos, 73, 12), strFormat);
                    yPos += printFont.GetHeight(e.Graphics);
                    // 총 지불액
                    e.Graphics.DrawString(getSlipText(Properties.ResourceString.TotalNetAmt), printFont, Brushes.Black, new Rectangle(0, (int)yPos, 210, 12), strFormat);
                    e.Graphics.DrawString(String.Format("{0,13}", Convert.ToInt32(MapGoods[Goods.TotalNetAmt]).ToString("#,##0")), printFont, Brushes.Black, new Rectangle(211, (int)yPos, 73, 12), strFormat);
                    yPos += printFont.GetHeight(e.Graphics);

                    printFont = new Font("Meiryo", 7);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public void PrintAds(PrintPageEventArgs e, ref float yPos)
        {
            try
            {
                if (MapAdsInfo.Count == 0)
                    return;

                if (!MapAdsInfo[AdsInfo.Type].Equals("02") && !MapAdsInfo[AdsInfo.Type].Equals("03"))
                    return;

                PrintLine(e, ref yPos);

                if(MapAdsInfo[AdsInfo.Type].Equals("02"))       // Image
                {
                    Bitmap imgAds = getURLImage(MapAdsInfo[AdsInfo.URL]);
                    if (imgAds != null)
                    {
                        e.Graphics.DrawImage((Image)imgAds, new Rectangle(0, (int)yPos, imgAds.Width, imgAds.Height));
                        yPos += imgAds.Height;
                    }
                }
                if(MapAdsInfo[AdsInfo.Type].Equals("03"))       // Text
                {
                    for(int i = 0; i < MapAdsInfo.Count - 2; i++)
                    {
                        Font adsFont = new Font("Meiryo", 6);
                        string strKey = string.Format("{0}_{1}", AdsInfo.TEXT, i);
                        e.Graphics.DrawString(MapAdsInfo[strKey], adsFont, Brushes.Black, 0, yPos, stringFormat);
                        yPos += adsFont.GetHeight(e.Graphics);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public void PrintLine(PrintPageEventArgs e, ref float yPos)
        {
            try
            {
                printFont = new Font("Meiryo", 7);
                e.Graphics.DrawString(Properties.Resources.StringLine, printFont, Brushes.Black, 0, yPos, stringFormat);
                yPos += printFont.GetHeight(e.Graphics);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public void PrintEndMark(PrintPageEventArgs e, ref float yPos)
        {
            try
            {
                printFont = new Font("Meiryo", 4);
                e.Graphics.DrawString(Properties.Resources.StringEndMark, printFont, Brushes.Black, 0, yPos, stringFormat);
                yPos += printFont.GetHeight(e.Graphics);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
