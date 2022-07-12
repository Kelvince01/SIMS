using System;
using System.Collections.Generic;
using SIMS.Models;

namespace SIMS.Service
{
    public interface IStyleSizeService
    {
        IEnumerable<StyleSize> Gets(string name = null);

        StyleSize Get(Decimal id);

        StyleSize Get(string name);

        void Create(StyleSize model);

        void Update(StyleSize model);

        void Remove(StyleSize model);

        void Save();

        bool IsDuplicate(StyleSize model);

        void RemoveAll();

        StyleSize GetByBarcode(string barcode);

        List<StyleSize> GetServerStyleSize();

        List<StyleSize> GetStyleSizeBySearch(
            string searchText,
            bool isFromStyleSize,
            string supId,
            bool isZeroStock = true);

        List<StyleSize> GetSupplier(string DefaultText);

        List<StyleSize> GetGroup(string DefaultText);

        List<StyleSize> GetProduct(string groupId, string DefaultText);

        List<StyleSize> GetBrand(string DefaultText);

        List<StyleSize> GetBarcodesByGroupProductBrand(
            string groupId,
            string productId,
            string brandId);
    }
}
