using System.Collections.Generic;
using SIMS.Data.Infrastructure;
using SIMS.Models;

namespace SIMS.Data.Repositories
{
    public interface IStyleSizeRepository : IRepository<StyleSize>
    {
        void RemoveAll();

        List<StyleSize> GetStyleSizeFromByBySearch(
            string searchtext,
            bool isFromStyleSize,
            string supId,
            bool isZeroStock = true);
    }
}
