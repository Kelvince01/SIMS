using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SIMS.Data.Infrastructure;
using SIMS.Models;

namespace SIMS.Data.Repositories
{
    public class StyleSizeRepository : RepositoryBase<StyleSize>, IStyleSizeRepository, IRepository<StyleSize>
    {
        public StyleSizeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public void RemoveAll()
        {
            DbSet<StyleSize> data = base.DbContext.StyleSizes;
            base.DbContext.StyleSizes.RemoveRange(data);
        }

        public List<StyleSize> GetStyleSizeFromByBySearch(string searchtext, bool isFromStyleSize, string supId, bool isZeroStock = true)
        {
            throw new NotImplementedException();
        }

        /*
        public List<StyleSize> GetStyleSizeFromByBySearch(string searchtext, bool isFromStyleSize, string supId, bool isZeroStock = true)
        {
            List<StyleSize> result;
            if (!isFromStyleSize)
            {
                int stock = 0;
                if (!isZeroStock)
                {
                    stock = -999999;
                }
                var data = (from m in base.DbContext.StyleSizes
                            join b in base.DbContext.Buys on m.Barcode equals b.BarCode
                            join prd in base.DbContext.Products on m.PrdID equals prd.PrdID
                            join bt in base.DbContext.BrandTypes on m.BTID equals bt.BTID
                            join gr in base.DbContext.PGroups on m.GroupID equals gr.GroupID
                            join sup in base.DbContext.Suppliers on m.SupID equals sup.SupID
                            where (m.Barcode.Contains(searchtext) || bt.BTName.Contains(searchtext) || gr.GroupName.Contains(searchtext) || prd.PrdName.Contains(searchtext) || m.SSName.Contains(searchtext)) && b.balQty > (decimal?)((decimal)stock) && ((m.SupID == supId && supId != "All") || supId == "All")
                            select new
                            {
                                Barcode = m.Barcode,
                                BTID = m.BTID,
                                BTName = bt.BTName,
                                CBTID = m.CBTID,
                                CMPIDX = m.CMPIDX,
                                CPU = m.CPU,
                                CSSID = m.CSSID,
                                DisContinued = m.DisContinued,
                                DiscPrcnt = m.DiscPrcnt,
                                ENTRYDT = m.ENTRYDT,
                                FloorID = m.FloorID,
                                GroupID = m.GroupID,
                                GroupName = gr.GroupName,
                                MaxOrder = m.MaxOrder,
                                Point = m.Point,
                                PrdComm = m.PrdComm,
                                PrdID = m.PrdID,
                                PrdName = prd.PrdName,
                                Reorder = m.Reorder,
                                RPP = m.RPP,
                                RPU = b.RPU,
                                sBarcode = m.sBarcode,
                                SSID = m.SSID,
                                SSName = m.SSName,
                                SupID = m.SupID,
                                SupName = sup.Supname,
                                UserID = m.UserID,
                                VATPrcnt = m.VATPrcnt,
                                WSP = m.WSP,
                                WSQ = m.WSQ,
                                ZoneID = m.ZoneID,
                                BalQty = b.balQty,
                                IsWeighing = m.IsWeighing,
                                IsPcs = m.IsPcs,
                                AutoIdForBarcode = m.AutoIdForBarcode
                            } into m
                            orderby m.AutoIdForBarcode descending
                            select m).Take(30);
                List<StyleSize> datas = new List<StyleSize>();
                foreach (var i in data)
                {
                    StyleSize s = new StyleSize
                    {
                        Barcode = i.Barcode,
                        BTID = i.BTID,
                        BTName = i.BTName,
                        CBTID = i.CBTID,
                        CMPIDX = i.CMPIDX,
                        CPU = i.CPU,
                        CSSID = i.CSSID,
                        DisContinued = i.DisContinued,
                        DiscPrcnt = i.DiscPrcnt,
                        ENTRYDT = i.ENTRYDT,
                        FloorID = i.FloorID,
                        GroupID = i.GroupID,
                        GroupName = i.GroupName,
                        MaxOrder = i.MaxOrder,
                        Point = i.Point,
                        PrdComm = i.PrdComm,
                        PrdID = i.PrdID,
                        PrdName = i.PrdName,
                        Reorder = i.Reorder,
                        RPP = i.RPP,
                        RPU = i.RPU,
                        sBarcode = i.sBarcode,
                        SSID = i.SSID,
                        SSName = i.SSName,
                        SupID = i.SupID,
                        SupName = i.SupName,
                        UserID = i.UserID,
                        VATPrcnt = i.VATPrcnt,
                        WSP = i.WSP,
                        WSQ = i.WSQ,
                        ZoneID = i.ZoneID,
                        BalQty = i.BalQty.Value,
                        IsWeighing = i.IsWeighing,
                        IsPcs = i.IsPcs
                    };
                    datas.Add(s);
                }
                result = datas;
            }
            else
            {
                var data = (from m in base.DbContext.StyleSizes
                            join prd in base.DbContext.Products on m.PrdID equals prd.PrdID
                            join bt in base.DbContext.BrandTypes on m.BTID equals bt.BTID
                            join gr in base.DbContext.PGroups on m.GroupID equals gr.GroupID
                            join sup in base.DbContext.Suppliers on m.SupID equals sup.SupID
                            join b in base.DbContext.Buys on m.Barcode equals b.BarCode into tBuy
                            from b in tBuy.DefaultIfEmpty<Buy>()
                            where (m.Barcode.Contains(searchtext) || bt.BTName.Contains(searchtext) || gr.GroupName.Contains(searchtext) || prd.PrdName.Contains(searchtext) || m.SSName.Contains(searchtext)) && ((m.SupID == supId && supId != "All") || supId == "All")
                            select new
                            {
                                Barcode = m.Barcode,
                                BTID = m.BTID,
                                BTName = bt.BTName,
                                CBTID = m.CBTID,
                                CMPIDX = m.CMPIDX,
                                CPU = m.CPU,
                                CSSID = m.CSSID,
                                DisContinued = m.DisContinued,
                                DiscPrcnt = m.DiscPrcnt,
                                ENTRYDT = m.ENTRYDT,
                                FloorID = m.FloorID,
                                GroupID = m.GroupID,
                                GroupName = gr.GroupName,
                                MaxOrder = m.MaxOrder,
                                Point = m.Point,
                                PrdComm = m.PrdComm,
                                PrdID = m.PrdID,
                                PrdName = prd.PrdName,
                                Reorder = m.Reorder,
                                RPP = m.RPP,
                                RPU = m.RPU,
                                sBarcode = m.sBarcode,
                                SSID = m.SSID,
                                SSName = m.SSName,
                                SupID = m.SupID,
                                SupName = sup.Supname,
                                UserID = m.UserID,
                                VATPrcnt = m.VATPrcnt,
                                WSP = m.WSP,
                                WSQ = m.WSQ,
                                ZoneID = m.ZoneID,
                                BalQty = b.balQty,
                                IsWeighing = m.IsWeighing,
                                IsPcs = m.IsPcs,
                                AutoIdForBarcode = m.AutoIdForBarcode
                            } into m
                            orderby m.AutoIdForBarcode descending
                            select m).Take(30);
                List<StyleSize> datas = new List<StyleSize>();
                foreach (var i in data)
                {
                    StyleSize s = new StyleSize
                    {
                        Barcode = i.Barcode,
                        BTID = i.BTID,
                        BTName = i.BTName,
                        CBTID = i.CBTID,
                        CMPIDX = i.CMPIDX,
                        CPU = i.CPU,
                        CSSID = i.CSSID,
                        DisContinued = i.DisContinued,
                        DiscPrcnt = i.DiscPrcnt,
                        ENTRYDT = i.ENTRYDT,
                        FloorID = i.FloorID,
                        GroupID = i.GroupID,
                        GroupName = i.GroupName,
                        MaxOrder = i.MaxOrder,
                        Point = i.Point,
                        PrdComm = i.PrdComm,
                        PrdID = i.PrdID,
                        PrdName = i.PrdName,
                        Reorder = i.Reorder,
                        RPP = i.RPP,
                        RPU = i.RPU,
                        sBarcode = i.sBarcode,
                        SSID = i.SSID,
                        SSName = i.SSName,
                        SupID = i.SupID,
                        SupName = i.SupName,
                        UserID = i.UserID,
                        VATPrcnt = i.VATPrcnt,
                        WSP = i.WSP,
                        WSQ = i.WSQ,
                        ZoneID = i.ZoneID,
                        BalQty = ((i.BalQty != null) ? i.BalQty.Value : 0m),
                        IsWeighing = i.IsWeighing,
                        IsPcs = i.IsPcs
                    };
                    datas.Add(s);
                }
                result = datas;
            }
            return result;
        }
        */
    }
}
