using System;
using System.Collections.Generic;
using SIMS.Models;

namespace SIMS.Service
{
    public interface IDashboardService
    {
        Decimal GetCurrentStock();

        Decimal SalesGrowth();

        Decimal TodasySales();

        Decimal TodaysReceive();

        //List<Sale> GetTopSalesItem(string fromDate, string ToDate, string shopid);

        List<DashBoardYearModel> SalesYearVsPrvYear(string shopId);

        List<StyleSize> GetReorderItems();

        //List<Sale> GetGroupSales(string fromDate, string ToDate, string shopid);

        Decimal MonthlySales();

        Decimal MonthlyReceive();
    }
}
