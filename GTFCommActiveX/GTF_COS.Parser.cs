using System;
using System.Collections.Generic;
using System.Text;

namespace GTFCommActiveX
{
    partial class GTF_COS
    {
        public void setClearParseMap()
        {
            MapDocid.Clear();
            MapRetailer.Clear();
            MapGoods.Clear();
            MapTourist.Clear();
            MapAdsInfo.Clear();
        }

        public bool ParseParam(string docid, string retailer, string tourist, string goods, string adsinfo)
        {
            try
            {
                if (!getParseDocid(docid))
                    return false;

                if (!getParseRetailer(retailer))
                    return false;
                
                if (!getParseTourist(tourist))
                    return false;

                if (!getParseGoods(goods))
                    return false;

                if (!getParseAdsInfo(adsinfo))
                    return false;
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool getParseDocid(string docid)
        {
            try
            {
                string[] arrDocid = docid.Split('|');

                MapDocid.Add(DocID.LangCode, arrDocid[0]);
                MapDocid.Add(DocID.PublichType, arrDocid[1]);
                MapDocid.Add(DocID.RePublish, arrDocid[2]);
                MapDocid.Add(DocID.SlipNo, arrDocid[3]);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool getParseRetailer(string retailer)
        {
            try
            {
                string[] arrRatiler = retailer.Split('|');
                MapRetailer.Add(Retailer.TaxOffice, arrRatiler[0]);
                MapRetailer.Add(Retailer.TaxPlace, arrRatiler[1]);
                MapRetailer.Add(Retailer.Seller, arrRatiler[2]);
                MapRetailer.Add(Retailer.SellerAddr, arrRatiler[3]);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool getParseTourist(string tourist)
        {
            try
            {
                string[] arrTourist = tourist.Split('|');
                MapTourist.Add(Tourist.PassportType, getPassPortEtc(arrTourist[0]));
                MapTourist.Add(Tourist.PassportNo, arrTourist[1]);
                MapTourist.Add(Tourist.Name, arrTourist[2].ToUpper());
                MapTourist.Add(Tourist.National, arrTourist[3]);
                MapTourist.Add(Tourist.Birth, arrTourist[4]);
                MapTourist.Add(Tourist.Residence, arrTourist[5]);
                MapTourist.Add(Tourist.LandingPlace, arrTourist[6]);
                MapTourist.Add(Tourist.LandingDate, arrTourist[7]);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool getParseGoods(string goods)
        {
            try
            {
                /*//////////////////////////////////////////////////////////////////////////
                 * Format
                 * [0] : Sale Date          (8)
                 * [1] : Item Type Seq      (3)
                 * 
                 * { [2] ~ [7] X [1] }
                 * [2] : Item Type Code     (2)
                 * [3] : Goods Seq         (3)  
                 * { [4]~[7] x [3] }
                 * [4] : Goods Name
                 * [5] : Goods Unit Price
                 * [6] : Goods Qty
                 * [7] : Goods Amount
                 * 
                 * [8] : Tax Amt
                 * [9] : Sale Amt
                 * [10] : Refund Amt
                 * 
                 * [11] : Item Type Code
                 * [12] : Goods Seq
                 * { [13]~[16] x [12] }
                 * [13] : Goods Name
                 * [14] : Goods Unit Price
                 * [15] : Goods Qty
                 * [16] : Goods Amount
                 * 
                 * [17] : Tax Amt
                 * [18] : Sale Amt
                 * [19] : Refund Amt
                 * 
                 * [20] : Total Tax Amt
                 * [21] : Total Refund Amt
                 * [22] : Total Fee Amt
                 * [23] : Total Net Amt
                 * 
                 * ex)
                 * 20150310
                 * 2
                 * 
                 * 01
                 * 2
                 * a01 | 10000 | 1 | 10000
                 * a02 | 10000 | 1 | 10000
                 * 1600 | 20000 | 1280
                 * 
                 * 02
                 * 2
                 * b01 | 10000 | 1 | 10000
                 * b02 | 10000 | 1 | 10000
                 * 1600 | 20000 | 1280
                 * 
                 * 40000 | 2560 | 640 | 1920                
                //////////////////////////////////////////////////////////////////////////*/

                string[] arrGoods = goods.Split('|');
                MapGoods.Add(Goods.SaleDate, arrGoods[0]);
                MapGoods.Add(Goods.ItemTypeSeq, arrGoods[1]);

                int nOffSet = 2;

                for (int i = 0; i < Convert.ToInt32(MapGoods[Goods.ItemTypeSeq]); i++)
                {
                    Dictionary<string, object> ItemMap = new Dictionary<string, object>();
                    ItemMap.Add(Goods.ItemTypeCode, arrGoods[nOffSet++]);
                    ItemMap.Add(Goods.GoodsSeq, arrGoods[nOffSet++]);

                    for(int j = 0; j < Convert.ToInt32(ItemMap[Goods.GoodsSeq]); j++)
                    {
                        Dictionary<string, object> GoodsMap = new Dictionary<string, object>();
                        GoodsMap.Add(Goods.Name, arrGoods[nOffSet++]);
                        GoodsMap.Add(Goods.UnitPrice, arrGoods[nOffSet++]);
                        GoodsMap.Add(Goods.Qty, arrGoods[nOffSet++]);
                        GoodsMap.Add(Goods.Amt, arrGoods[nOffSet++]);
                        string ItemMapKey = String.Format("GoodsMap_{0}", j);
                        ItemMap.Add(ItemMapKey, (object)GoodsMap);
                    }

                    ItemMap.Add(Goods.TaxAmt, arrGoods[nOffSet++]);
                    ItemMap.Add(Goods.SaleAmt, arrGoods[nOffSet++]);
                    ItemMap.Add(Goods.RefundAmt, arrGoods[nOffSet++]);

                    string MapGoodsKey = String.Format("ItemsMap_{0}", i);
                    MapGoods.Add(MapGoodsKey, (object)ItemMap);
                }
                MapGoods.Add(Goods.TotalSaleAmt, arrGoods[nOffSet++]);
                MapGoods.Add(Goods.TotalRefundAmt, arrGoods[nOffSet++]);
                MapGoods.Add(Goods.TotalFeeAmt, arrGoods[nOffSet++]);
                MapGoods.Add(Goods.TotalNetAmt, arrGoods[nOffSet++]);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool getParseAdsInfo(string adsinfo)
        {
            try
            {
                if(adsinfo != null && adsinfo.Length > 0)
                {
                    // Type | URL | TEXT | TARGET
                    string[] arrAdsinfo = adsinfo.Split('|');
                    MapAdsInfo.Add(AdsInfo.Type, arrAdsinfo[0]);
                    MapAdsInfo.Add(AdsInfo.Target, arrAdsinfo[1]);

                    if (MapAdsInfo[AdsInfo.Type].Equals("02"))
                    {
                        MapAdsInfo.Add(AdsInfo.URL, arrAdsinfo[2]);
                    }
                    else if (MapAdsInfo[AdsInfo.Type].Equals("03"))
                    {
                        for (int i = 0; i < arrAdsinfo.Length - 2; i++)
                        {
                            string strKey = string.Format("{0}_{1}", AdsInfo.TEXT, i);
                            MapAdsInfo.Add(strKey, arrAdsinfo[i+2]);
                        }
                    }                    
                }                
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
