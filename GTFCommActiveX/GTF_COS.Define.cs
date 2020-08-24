using System;
using System.Collections.Generic;
using System.Text;

namespace GTFCommActiveX
{
    partial class GTF_COS
    {
        public class DocID
        {
            public const string LangCode = "LangCode";
            public const string PublichType = "PublishType";
            public const string RePublish = "RePublish";
            public const string SlipNo = "SlipNo";
        }

        public class Retailer
        {
            public const string TaxOffice = "TaxOffice";
            public const string TaxPlace = "TaxPlace";
            public const string Seller = "Seller";
            public const string SellerAddr = "SellerAddr";
        }

        public class Tourist
        {
            public const string PassportType = "PassportType";
            public const string PassportNo = "PassportNo";
            public const string Name = "Name";
            public const string National = "National";
            public const string Birth = "Birth";
            public const string Residence = "Residence";
            public const string LandingPlace = "LandingPlace";
            public const string LandingDate = "LandingDate";
        }

        public class Goods
        {
            public const string SaleDate = "SaleDate";
            public const string ItemTypeCode = "ItemTypeCode";
            public const string ItemTypeSeq = "ItemTypeSeq";
            public const string GoodsSeq = "GoodsSeq";
            public const string Name = "Name";
            public const string UnitPrice = "UnitPrice";
            public const string Qty = "Qty";
            public const string Amt = "Amt";

            public const string TaxAmt = "TaxAmt";
            public const string SaleAmt = "SaleAmt";
            public const string RefundAmt = "RefundAmt";

            public const string TotalSaleAmt = "TotalSaleAmt";
            public const string TotalRefundAmt = "TotalRefundAmt";
            public const string TotalFeeAmt = "TotalFeeAmt";
            public const string TotalNetAmt = "TotalNetAmt";
        }

        public class AdsInfo
        {
            public const string Type = "Type";
            public const string Target = "Target";
            public const string URL = "URL";
            public const string TEXT = "TEXT";
        }

        public class LangCode
        {
            public const string ko_KR = "KR";
            public const string en_US = "EN";
            public const string zh_CN = "CN";
        }
    }
}
