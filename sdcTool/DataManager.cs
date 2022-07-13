using System.Collections.Generic;
using System.Configuration;
using FIK.DAL;
using sdcTool.Models;
using sdcTool.msgItems;

namespace sdcTool
{
    public class DataManager
    {
        private SQL sqlDal = (SQL)null;
        private string counterId = "";

        public DataManager()
        {
            this.sqlDal = new SQL(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);
            this.counterId = ConfigurationManager.AppSettings["CounterId"];
        }

        public List<SalesMSDSL> GetNewSales(ref string msg)
        {
            List<SalesMSDSL> salesMsdslList = new List<SalesMSDSL>();
            return this.sqlDal.Select<SalesMSDSL>("select BarCode,(sQty) sQty,RPU- ISNULL(DiscAmtPrd,0) RPU,VATPrcnt ,Product,Invoice,CInvoice,rQty\r\n                            from TemSale" + this.counterId + " t\r\n                            where t.SDC_Flag='N' ", ref msg);
        }

        public List<SalesMSDSL> GetNewSalesReturn(ref string msg)
        {
            List<SalesMSDSL> salesMsdslList = new List<SalesMSDSL>();
            return this.sqlDal.Select<SalesMSDSL>("select BarCode,(sQty) sQty,RPU- ISNULL(DiscAmtPrd,0) RPU,VATPrcnt ,Product,Invoice,cInvoice_SDC CInvoice,rQty\r\n                            from TemSaleVoid" + this.counterId + " t\r\n                            where t.SDC_Flag='N' ", ref msg);
        }

        public bool UpdateSalesRecord(
          List<invoiceRes.GoodsInfoItem> goodsInfo,
          string url,
          string invoice,
          string vat,
          ref string msg)
        {
            List<string> SQL = new List<string>();
            foreach (invoiceRes.GoodsInfoItem goodsInfoItem in goodsInfo)
            {
                string str = "update TemSale" + this.counterId + " set SDC_Flag='Y',SDCLineVAT=" + goodsInfoItem.taxAmt + ",SDCTotalVAT=" + vat + ",SDC_Invoice='" + invoice + "',SDC_URL='" + url + "'\r\n                            where SDC_Flag='N' and BarCode='" + goodsInfoItem.itemCode + "' ";
                SQL.Add(str);
            }
            return this.sqlDal.ExecuteQuery(SQL, ref msg);
        }

        public bool UpdateSalesRecordCreditNote(
          List<creditNoteRes.GoodsInfoItem> goodsInfo,
          string url,
          string invoice,
          string vat,
          ref string msg)
        {
            List<string> SQL = new List<string>();
            foreach (creditNoteRes.GoodsInfoItem goodsInfoItem in goodsInfo)
            {
                string str = "update TemSaleVoid" + this.counterId + " set SDC_Flag='Y',SDCLineVAT=" + goodsInfoItem.vatAmt + ",SDCTotalVAT=" + vat + ",SDC_Invoice='" + invoice + "',SDC_URL='" + url + "'\r\n                            where SDC_Flag='N' and BarCode='" + goodsInfoItem.product_code + "' ";
                SQL.Add(str);
            }
            return this.sqlDal.ExecuteQuery(SQL, ref msg);
        }

        public List<SaleClient> GetNewSale(string counterId, ref string message) => this.sqlDal.Select<SaleClient>("\r\n                            BEGIN\r\n\r\n\r\n                            DECLARE @TableTemp TABLE (\r\n\t                            SaleDt DATE,\r\n\t                            PrdName NVARCHAR(50),\r\n\t                            BTName NVARCHAR(50),\r\n\t                            SSName NVARCHAR(50),\r\n\t                            CLName NVARCHAR(50),\r\n\t                            SL_ST_Name NVARCHAR(50),\r\n\t                            Sqty MONEY,\r\n\t                            Rqty MONEY,\r\n\t                            RPU MONEY,\r\n\t                            DiscAmtPrd MONEY,\r\n\t                            VatPrcnt MONEY,\r\n\t                            InvoiceNo NVARCHAR(50),\r\n\t                            PayType NVARCHAR(50),\r\n\t                            CardName NVARCHAR(50),\r\n\t                            Vat MONEY,\r\n\t                            ReturnedAmt MONEY,\r\n\t                            CshAmt MONEY,\r\n\t                            CrdAmt MONEY,\r\n\t                            SalesMan NVARCHAR(50),\r\n\t                            MRName NVARCHAR(50),\r\n\t                            rVatAmt MONEY,\r\n\t                            DiscAmt MONEY,\r\n\t                            NetAmt MONEY,\r\n\t                            ShopId NVARCHAR(50),\r\n\t                            ShopName NVARCHAR(500),\r\n\t                            CounterId NVARCHAR(50),\r\n\t                            CounterName NVARCHAR(500),\r\n\t                            PaidAmt MONEY,\r\n\t                            ChageAmt MONEY,\r\n\t                            TSEC NVARCHAR(50),\r\n\t                            VAT_REG_NO NVARCHAR(50),\r\n\t                            Barcode NVARCHAR(50),\r\n\r\n\t                            SupID NVARCHAR(50),\r\n\t                            SupName NVARCHAR(500),\r\n\t                            PrvCusId NVARCHAR(500),\r\n\t                            PrvCusName NVARCHAR(500),\r\n\t                            Point MONEY,\r\n\t                            TotalEarn MONEY,\r\n\t                            RedeemPoint MONEY,\r\n\t                            CardNo NVARCHAR(500),\r\n\t                            ShopAddress NVARCHAR(150),\r\n                                TotalAmt money,\r\n                                Discount MONEY,\r\n                                SDC_URL  NVARCHAR(MAX),\r\n                                SDC_Invoice NVARCHAR(100),\r\n                                DiscPrcnt money\r\n                            )\r\n\r\n                            DECLARE @InvoiceNo NVARCHAR(50)\r\n                            DECLARE @TableName NVARCHAR(50)\r\n                            DECLARE @SQL NVARCHAR(max)\r\n\r\n                            SET @InvoiceNo = (SELECT TOP 1 Invoice FROM dbo.SSummary WHERE (IsPrinted IS NULL OR ISNULL(IsPrinted,'N')='N') and CounterID='" + counterId + "' AND SDC_Invoice IS NOT NULL)\r\n\r\n                             if(left(@InvoiceNo,2)='CR' ) \r\n                                begin \r\n                                    set @TableName=  RIGHT(left(@InvoiceNo,8),6)\r\n                                end\r\n                            else begin\r\n                                  set @TableName=  LEFT(@InvoiceNo,6)\r\n                            end\r\n\r\n                              SET @SQL = 'SELECT a.saledt,a.prdname,a.btname,a.ssname,'''' clname,'''' slname,a.sqty,a.rqty,a.rpu,a.discamtprd,a.vatprcnt,b.invoice,b.paytype,b.cardname,b.vat,b.returnedamt,b.cshamt,b.crdamt,b.salesman,b.mrname,b.rVAT, \r\n\t                                b.discamt,b.netamt,b.shopid,(SELECT TOP 1 company FROM dbo.cProfile),b.counterid,'''',b.paidamt,b.changeamt,b.tsec,(SELECT TOP 1 VatReg FROM dbo.cProfile) VatReg\r\n\t                                ,a.BarCode\r\n\t                                ,a.SupID,a.SupName,a.PrvCusID,a.CusName,\r\n\t                                b.Point, 0 totalEarn,0 PointRedeem,b.Cardno\r\n\t                                ,(SELECT TOP 1 address FROM dbo.cProfile), b.TotalAmt,a.Discount,(select SDC_URL from SSummary where Invoice='''+@InvoiceNo+''') SDC_URL,\r\n                                        (select SDC_Invoice from SSummary where Invoice='''+@InvoiceNo+''') SDC_Invoice,a.DiscPrcnt\r\n\t                                  FROM [Sale'+ @TableName +'] a inner join [dbo].[ssummary] b on a.invoice=b.invoice \r\n\t                            WHERE b.Invoice='''+@InvoiceNo+''' and b.counterid=''" + counterId + "'' '\r\n\r\n\t\t                            INSERT INTO @TableTemp\r\n\t\t\t                            EXEC sp_executesql  @SQL\r\n\r\n\r\n                            SELECT * FROM @TableTemp\r\n\r\n\r\n                            END\t\r\n\r\n                            ", ref message);

        public bool UpdateSaleSendAcknoledgement(string invoiceNo, out string msg)
        {
            msg = "";
            return this.sqlDal.ExecuteQuery(new List<string>()
      {
        "update SSummary set IsPrinted='Y' where Invoice='" + invoiceNo + "' "
      }, ref msg);
        }
    }
}
