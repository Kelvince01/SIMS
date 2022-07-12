using SIMS.Models;
using System;
using System.Collections.Generic;

namespace SIMS
{
    public class StaticData
    {
        public static string ShopId;
        public static string ShopName;
        public static string ShopAddr;
        public static string CounterId;
        public static string CounterName;
        public static string UserId;
        public static string UserName;
        public static GlobalSetup globalSetup;
        public static DateTime ServerDate;
        public static string SystemUser = "admin";
        public static string SystemUserPass = "masteropen";
        public static string DefaultSupplierId = "0000";

        public static string MacAddress { get; set; }

        public static bool isAllDataLoaded { get; set; }

        public class TabText
        {
            public static string DownloadProduct = "Download Product";
            public static string ShopReturn = "Shop Return";
            public static string ShopReceive = "Shop Receive";
            public static string CounterSetup = "Counter Setup";
            public static string ReportInvoiceReprint = "Invoice Reprint";
            public static string SupplierReturn = "Supplier Return";
            public static string DamageLost = "Damage Lost";
            public static string ShopLoginOut = "Shop Log in Out";
            public static string DownloadPromotion = "Download Promotion";
            public static string PackageManagement = "Package Management";
            public static string DownloadPriceChange = "Price Change";
            public static string DownloadCircularDiscount = "Discount Circular";
            public static string ExpenseEntry = nameof(ExpenseEntry);
            public static string UserManagement = "User Management";
            public static string MenuDistribution = "Menu Distribution";
            public static string SalesReport = "Sales Report";
            public static string EmployeeAttendance = "Employee Attendance";
            public static string AttendanceReports = "Attendance Reports";
            public static string ShopReceiveReports = "Shop Receive Reports";
            public static string DamageLostReports = "Damage Lost Reports";
            public static string ShopTransferReports = "Shop Transfer Reports";
            public static string StockReports = "Stock Reports";
            public static string Settings = nameof(Settings);
            public static string CircularDiscountReport = "Circular Discount Reports";
            public static string GroupSetup = "Group Setup";
            public static string ProductSetup = "Product Setup";
            public static string BrandTypeSetup = "Brand Type Setup";
            public static string SupplierSetup = "Supplier Setup";
            public static string StyleSize = "Style Size";
            public static string CustomerSetup = "Customer Setup";
            public static string CustomerCateogrySetup = "Customer Category Setup";
            public static string EmployeeSetup = "Employee Setup";
            public static string SalesManSetup = "Sales Man Setup";
            public static string PurchaseOrder = "Purchase Order";
            public static string PackageReport = "Package Report";
            public static string PromotionReport = "Promotion Report";
            public static string BarcodePrint = "Barcode Print";
            public static string Inventory = nameof(Inventory);
            public static string ExportData = "Export Data";
            public static string PurchaseOrderReport = "Purchase Order Report";
            public static string VatReport = "Vat Report";
            public static string ChartOfAccounts = "Chart Of Accounts";
            public static string VoucherEntry = "Voucher Entry";
            public static string AccountsReport = "Accounts Report";
            public static string CreditCardList = "Credit Card";
            public static string CreditCollection = "Credit Collection";
            public static string CustomerReport = "Customer Report";
        }

        public class DownloadOperationNames
        {
            public static string PromotionDownload = nameof(PromotionDownload);
            public static string PackageIssueDownload = nameof(PackageIssueDownload);
            public static string PackageIssueDetailsDownload = nameof(PackageIssueDetailsDownload);
            public static string PackageDownload = nameof(PackageDownload);
            public static string CircularPriceChanged = nameof(CircularPriceChanged);
            public static string CircularDiscountDownload = nameof(CircularDiscountDownload);
        }

        public class CustomerCategory
        {
            public static string CatPlatinum = "PLATINUM";
            public static string CatSilver = "SILVER";
            public static string CatGold = "GOLD";

            public string CatName { get; set; }

            public List<StaticData.CustomerCategory> SelectAll() => new List<StaticData.CustomerCategory>()
      {
        new StaticData.CustomerCategory()
        {
          CatName = StaticData.CustomerCategory.CatPlatinum
        },
        new StaticData.CustomerCategory()
        {
          CatName = StaticData.CustomerCategory.CatSilver
        },
        new StaticData.CustomerCategory()
        {
          CatName = StaticData.CustomerCategory.CatGold
        }
      };
        }
    }
}
