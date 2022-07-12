using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SIMS.Data.Infrastructure;
using SIMS.Data.Repositories;
using SIMS.Models;

namespace SIMS.Service
{
    public class StyleSizeService : IStyleSizeService
    {
        public readonly IStyleSizeRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        /*public readonly ISupplierRepository _repositorySup;
        public readonly IPGroupRepostiry _repositoryGroup;
        public readonly IProductRepository _repositoryProduct;
        public readonly IBrandTypeRepository _repositoryBrand;
        public readonly IBuyRepository _repositoryBuy;*/

        public StyleSizeService(IDbFactory idbFactory)
        {
            this._repository = (IStyleSizeRepository)new StyleSizeRepository(idbFactory);
            /*this._repositorySup = (ISupplierRepository)new SupplierRepository(idbFactory);
            this._repositoryGroup = (IPGroupRepostiry)new PGroupRepostiry(idbFactory);
            this._repositoryProduct = (IProductRepository)new ProductRepository(idbFactory);
            this._repositoryBrand = (IBrandTypeRepository)new BrandTypeRepository(idbFactory);
            this._repositoryBuy = (IBuyRepository)new BuyRepository(idbFactory);*/
            this._unitOfWork = (IUnitOfWork)new UnitOfWork(idbFactory);
        }

        public IEnumerable<StyleSize> Gets(string name = null) => string.IsNullOrEmpty(name) ? this._repository.GetAll() : this._repository.GetAll().Where<StyleSize>((Func<StyleSize, bool>)(c => c.BTName == name));

        public StyleSize Get(Decimal id) => this._repository.GetById(id);

        public StyleSize Get(string name) => this._repository.GetAll().Where<StyleSize>((Func<StyleSize, bool>)(c => c.sBarcode == name)).FirstOrDefault<StyleSize>();

        public StyleSize GetByBarcode(string barcode) => this._repository.GetAll().Where<StyleSize>((Func<StyleSize, bool>)(c => c.Barcode == barcode)).FirstOrDefault<StyleSize>();

        public void Create(StyleSize model)
        {
            if (this._repository.GetMany((Expression<Func<StyleSize, bool>>)(m => m.Barcode == model.Barcode)).FirstOrDefault<StyleSize>() != null)
                throw new Exception("A product already enter with this barcode ");
            this._repository.Add(model);
        }

        public void Update(StyleSize model) => this._repository.Update(model);

        public void Remove(StyleSize model)
        {
            throw new NotImplementedException();
        }

        /*public void Remove(StyleSize model)
        {
            if (this._repositoryBuy.GetMany((Expression<Func<Buy, bool>>)(m => m.sBarCode == model.sBarcode)).FirstOrDefault<Buy>() != null)
                throw new Exception("This reocrd allready in used ,delete not possible");
            this._repository.Delete(model);
        }*/

        public void RemoveAll() => this._repository.RemoveAll();

        public void Save() => this._unitOfWork.Commit();

        public bool IsDuplicate(StyleSize model)
        {
            string id = model.BTID;
            return this._repository.GetMany((Expression<Func<StyleSize, bool>>)(m => m.BTName == model.BTName && id != "" && m.BTID != id)).FirstOrDefault<StyleSize>() != null;
        }

        public List<StyleSize> GetServerStyleSize()
        {
            Result result1 = new SQLDAL("Server").Select("SELECT CMPIDX ,sBarcode ,Barcode ,CSSID ,SSID ,SSName ,PrdID ,PrdName ,CBTID ,\r\n                                                               BTID ,BTName ,GroupID ,GroupName ,FloorID ,DiscPrcnt ,VATPrcnt ,\r\n                                                               PrdComm ,CPU ,RPU ,RPP ,WSP ,WSQ ,DisContinued ,SupID ,\r\n                                                               SupName ,UserID ,ENTRYDT ,ZoneID ,Point ,Reorder ,\r\n                                                               MaxOrder \r\n                                                        FROM StyleSize");
            if (!result1.ExecutionState)
                return (List<StyleSize>)null;
            List<StyleSize> serverStyleSize = new List<StyleSize>();
            for (int index = 0; index < result1.Data.Rows.Count; ++index)
            {
                StyleSize styleSize = new StyleSize();
                styleSize.CMPIDX = result1.Data.Rows[index]["CMPIDX"].ToString();
                styleSize.sBarcode = result1.Data.Rows[index]["sBarcode"].ToString();
                styleSize.Barcode = result1.Data.Rows[index]["Barcode"].ToString();
                styleSize.CSSID = result1.Data.Rows[index]["SSID"].ToString();
                styleSize.SSName = result1.Data.Rows[index]["SSName"].ToString();
                styleSize.PrdID = result1.Data.Rows[index]["PrdID"].ToString();
                styleSize.PrdName = result1.Data.Rows[index]["PrdName"].ToString();
                styleSize.CBTID = result1.Data.Rows[index]["CBTID"].ToString();
                styleSize.BTID = result1.Data.Rows[index]["BTID"].ToString();
                styleSize.BTName = result1.Data.Rows[index]["BTName"].ToString();
                styleSize.GroupID = result1.Data.Rows[index]["GroupID"].ToString();
                styleSize.GroupName = result1.Data.Rows[index]["GroupName"].ToString();
                styleSize.FloorID = result1.Data.Rows[index]["FloorID"].ToString();
                Decimal result2;
                Decimal.TryParse(result1.Data.Rows[index]["DiscPrcnt"].ToString(), out result2);
                styleSize.DiscPrcnt = new Decimal?(result2);
                Decimal.TryParse(result1.Data.Rows[index]["VATPrcnt"].ToString(), out result2);
                styleSize.VATPrcnt = new Decimal?(result2);
                Decimal.TryParse(result1.Data.Rows[index]["PrdComm"].ToString(), out result2);
                styleSize.PrdComm = new Decimal?(result2);
                Decimal.TryParse(result1.Data.Rows[index]["CPU"].ToString(), out result2);
                styleSize.CPU = new Decimal?(result2);
                Decimal.TryParse(result1.Data.Rows[index]["RPU"].ToString(), out result2);
                styleSize.RPU = new Decimal?(result2);
                Decimal.TryParse(result1.Data.Rows[index]["RPP"].ToString(), out result2);
                styleSize.RPP = new Decimal?(result2);
                Decimal.TryParse(result1.Data.Rows[index]["WSP"].ToString(), out result2);
                styleSize.WSP = new Decimal?(result2);
                Decimal.TryParse(result1.Data.Rows[index]["WSQ"].ToString(), out result2);
                styleSize.WSQ = new Decimal?(result2);
                styleSize.DisContinued = result1.Data.Rows[index]["DisContinued"].ToString();
                styleSize.SupID = result1.Data.Rows[index]["SupID"].ToString();
                styleSize.SupName = result1.Data.Rows[index]["SupName"].ToString();
                styleSize.UserID = result1.Data.Rows[index]["UserID"].ToString();
                styleSize.ENTRYDT = new DateTime?(DateTime.Parse(result1.Data.Rows[index]["ENTRYDT"].ToString()));
                styleSize.ZoneID = result1.Data.Rows[index]["ZoneID"].ToString();
                Decimal.TryParse(result1.Data.Rows[index]["Point"].ToString(), out result2);
                styleSize.Point = new Decimal?(result2);
                Decimal.TryParse(result1.Data.Rows[index]["Reorder"].ToString(), out result2);
                styleSize.Reorder = new Decimal?(result2);
                Decimal.TryParse(result1.Data.Rows[index]["MaxOrder"].ToString(), out result2);
                styleSize.MaxOrder = new Decimal?(result2);
                serverStyleSize.Add(styleSize);
            }
            return serverStyleSize;
        }

        public List<StyleSize> GetStyleSizeBySearch(
          string searchText,
          bool isFromStyleSize,
          string supId,
          bool isZeroStock = true)
        {
            return this._repository.GetStyleSizeFromByBySearch(searchText, isFromStyleSize, supId, isZeroStock);
        }

        public List<StyleSize> GetSupplier(string DefaultText)
        {
            throw new NotImplementedException();
        }

        public List<StyleSize> GetGroup(string DefaultText)
        {
            throw new NotImplementedException();
        }

        public List<StyleSize> GetProduct(string groupId, string DefaultText)
        {
            throw new NotImplementedException();
        }

        public List<StyleSize> GetBrand(string DefaultText)
        {
            throw new NotImplementedException();
        }

        /*public List<StyleSize> GetSupplier(string DefaultText)
        {
            IOrderedEnumerable<Supplier> orderedEnumerable = this._repositorySup.GetAll().OrderBy<Supplier, string>((Func<Supplier, string>)(m => m.Supname));
            List<StyleSize> supplier1 = new List<StyleSize>();
            supplier1.Add(new StyleSize()
            {
                SupID = DefaultText,
                SupName = DefaultText
            });
            foreach (Supplier supplier2 in (IEnumerable<Supplier>)orderedEnumerable)
                supplier1.Add(new StyleSize()
                {
                    SupID = supplier2.SupID,
                    SupName = supplier2.Supname
                });
            return supplier1;
        }

        public List<StyleSize> GetGroup(string DefaultText)
        {
            IOrderedEnumerable<PGroup> orderedEnumerable = this._repositoryGroup.GetAll().OrderBy<PGroup, string>((Func<PGroup, string>)(m => m.GroupName));
            List<StyleSize> group = new List<StyleSize>();
            group.Add(new StyleSize()
            {
                GroupID = DefaultText,
                GroupName = DefaultText
            });
            foreach (PGroup pgroup in (IEnumerable<PGroup>)orderedEnumerable)
                group.Add(new StyleSize()
                {
                    GroupID = pgroup.GroupID,
                    GroupName = pgroup.GroupName
                });
            return group;
        }

        public List<StyleSize> GetProduct(string groupId, string DefaultText)
        {
            IOrderedEnumerable<Product> orderedEnumerable = this._repositoryProduct.GetAll().Where<Product>((Func<Product, bool>)(m => m.GroupID == groupId && groupId != "ALL" || groupId == "ALL")).OrderBy<Product, string>((Func<Product, string>)(m => m.PrdName));
            List<StyleSize> product1 = new List<StyleSize>();
            product1.Add(new StyleSize()
            {
                PrdID = DefaultText,
                PrdName = DefaultText
            });
            foreach (Product product2 in (IEnumerable<Product>)orderedEnumerable)
                product1.Add(new StyleSize()
                {
                    PrdID = product2.PrdID,
                    PrdName = product2.PrdName
                });
            return product1;
        }

        public List<StyleSize> GetBrand(string DefaultText)
        {
            IOrderedEnumerable<BrandType> orderedEnumerable = this._repositoryBrand.GetAll().OrderBy<BrandType, string>((Func<BrandType, string>)(m => m.BTName)).OrderBy<BrandType, string>((Func<BrandType, string>)(m => m.BTName));
            List<StyleSize> brand = new List<StyleSize>();
            brand.Add(new StyleSize()
            {
                BTID = DefaultText,
                BTName = DefaultText
            });
            foreach (BrandType brandType in (IEnumerable<BrandType>)orderedEnumerable)
                brand.Add(new StyleSize()
                {
                    BTID = brandType.BTID,
                    BTName = brandType.BTName
                });
            return brand;
        }*/

        public List<StyleSize> GetBarcodesByGroupProductBrand(
          string groupId,
          string productId,
          string brandId)
        {
            return this._repository.GetAll().Where<StyleSize>((Func<StyleSize, bool>)(m => (m.GroupID == groupId && groupId != "ALL" || groupId == "ALL") && (m.BTID == brandId && brandId != "ALL" || brandId == "ALL") && (m.PrdID == productId && productId != "ALL" || productId == "ALL"))).ToList<StyleSize>();
        }
    }
}
