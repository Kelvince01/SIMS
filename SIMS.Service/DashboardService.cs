using System.Collections.Generic;
using SIMS.Data.Infrastructure;
using SIMS.Models;

namespace SIMS.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly IStyleSizeService _repositoryStyleSize;
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IDbFactory idbFactory)
        {
            this._repositoryStyleSize = (IStyleSizeService)new StyleSizeService(idbFactory);
            this._unitOfWork = (IUnitOfWork)new UnitOfWork(idbFactory);
        }

        public decimal GetCurrentStock()
        {
            throw new System.NotImplementedException();
        }

        public decimal SalesGrowth()
        {
            throw new System.NotImplementedException();
        }

        public decimal TodasySales()
        {
            throw new System.NotImplementedException();
        }

        public decimal TodaysReceive()
        {
            throw new System.NotImplementedException();
        }

        public List<DashBoardYearModel> SalesYearVsPrvYear(string shopId)
        {
            throw new System.NotImplementedException();
        }

        public List<StyleSize> GetReorderItems()
        {
            throw new System.NotImplementedException();
        }

        public decimal MonthlySales()
        {
            throw new System.NotImplementedException();
        }

        public decimal MonthlyReceive()
        {
            throw new System.NotImplementedException();
        }
    }
}
