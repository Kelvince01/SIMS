using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CustomControls;
using Newtonsoft.Json;
using SIMS;
using SIMS.BLL;
using SIMS.Data.Infrastructure;
using SIMS.LICENSE;
using SIMS.Models;
using SIMS.Service;
using SIMS.UserControls;
using SIMS.UserControls.Accounts;
using SIMS.Windows;

namespace SIMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IThemeSettingService _serviceThemedata;
        private IDashboardService _serviceDashboard;
        private IAttenantLogService _serviceAttendLog;
        private IUsersDesktopMenusService _serviceUsersMenus;
        private IGlobalSetupService _serviceGlobal;
        private IUsersDesktopService _serviceUser;

        //private IShopListService _serviceShop;
        //private ICounterService _serviceCounter;
        //private ISaleService _serviceSale;
        //private ICircularPriceChangedService _servicePriceChanged;
        private IDesktopMenuService _serviceMenu;

        private AnimationContrainterUserControl animator;
        private UserControl Currentuc;
        private Window frmLastOpend;
        private Thread demoThread = (Thread)null;
        private SplashScreenUI frm;
        private bool isConnectionOk = false;
        private List<ButtonModel> ButtonOrginalLocations = new List<ButtonModel>();
        private Control mycontrol;
        private int x;
        private int y;
        private bool isDragging;
        private bool isMoveed = false;
        private int clickOffsetX;
        private int clickOffsetY;

        public MainWindow()
        {
            InitializeComponent();
            StaticData staticData = new StaticData();
            this.Init();
            this.Opacity = 0.0;

            /*this.panelLeft.DragOver += new DragEventHandler(this.panel_DragOver);
            this.panelLeft.DragDrop += new DragEventHandler(this.panel_DragDrop);
            this.panelTopMiddle.DragOver += new DragEventHandler(this.panel_DragOver);
            this.panelTopMiddle.DragDrop += new DragEventHandler(this.panel_DragDrop);
            this.panelBottomMiddle.DragOver += new DragEventHandler(this.panel_DragOver);
            this.panelBottomMiddle.DragDrop += new DragEventHandler(this.panel_DragDrop);
            this.panelAccounts.DragOver += new DragEventHandler(this.panel_DragOver);
            this.panelAccounts.DragDrop += new DragEventHandler(this.panel_DragDrop);
            this.panelRight.DragOver += new DragEventHandler(this.panel_DragOver);
            this.panelRight.DragDrop += new DragEventHandler(this.panel_DragDrop);*/
        }

        private void Init()
        {
            IDbFactory idbFactory = (IDbFactory)new DbFactory();
            this._serviceGlobal = (IGlobalSetupService)new GlobalSetupService(idbFactory);
            this._serviceUser = (IUsersDesktopService)new UsersDesktopService(idbFactory);
            //this._serviceShop = (IShopListService)new ShopListService(idbFactory);
            //this._serviceCounter = (ICounterService)new CounterService(idbFactory);
            this._serviceUsersMenus = (IUsersDesktopMenusService)new UsersDesktopMenusService(idbFactory);
            this._serviceMenu = (IDesktopMenuService)new DesktopMenuService(idbFactory);
            //this._serviceSale = (ISaleService)new SaleService(idbFactory);
            //this._servicePriceChanged = (ICircularPriceChangedService)new CircularPriceChangedService(idbFactory, StaticData.DownloadOperationNames.CircularPriceChanged);
            this._serviceAttendLog = (IAttenantLogService)new AttenantLogService(idbFactory);
            this._serviceDashboard = (IDashboardService)new DashboardService(idbFactory);
            this._serviceThemedata = (IThemeSettingService)new ThemeSettingService(idbFactory);
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.lblVersion.Content = "Version : SIMS - " + new winAboutBox().AssemblyVersion;
            LicnenceVerifier licnenceVerifier = new LicnenceVerifier();
            string msg = "";
            while (true)
            {
                if (licnenceVerifier.checkNwrightDate(ref msg))
                {
                    if (!licnenceVerifier.checkNwrightLicence(ref msg))
                    {
                        new winLicenseExpire().ShowDialog();
                    }
                    else
                        break;
                }
                else
                {
                    new winLicenseExpire().ShowDialog();
                }
            }
            if (this.demoThread == null)
                this.demoThread = new Thread(new ThreadStart(this.LoadData));
            if (!this.demoThread.IsAlive)
            {
                this.demoThread = new Thread(new ThreadStart(this.LoadData));
                this.demoThread.Start();
            }
            if (!(e is ThresholdReachedEventArgs))
            {
                SplashScreenUI splashScreenUi = new SplashScreenUI();

                splashScreenUi.Closed += new EventHandler(this.MainWindow_OnClosed);

                splashScreenUi.Show();
            }
            else
                this.MainWindow_OnClosed(sender, (EventArgs)null);
            this.Focus();
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            if (!this.isConnectionOk)
            {
                Application.Current.Shutdown();
            }
            else
            {
                /*try
                {
                    Application.DoEvents();
                    LoginWindows loginForms = new LoginWindows();
                    loginForms.ShowDialog();
                    if (!loginForms.isLogIn)
                        Application.Current.Shutdown();
                    this.metroLogOffPanel1.SetUser(StaticData.UserId);
                    this.ApplyCircularPriceChnage();
                    this.ApplyMenuPermission();
                    bool? nullable = StaticData.globalSetup.IsAccountsOn;
                    int num2;
                    if (nullable.HasValue)
                    {
                        nullable = StaticData.globalSetup.IsAccountsOn;
                        num2 = !nullable.Value ? 1 : 0;
                    }
                    else
                        num2 = 1;
                    if (num2 == 0)
                        this.panelAccounts.Visible = true;
                    else
                        this.panelAccounts.Visible = false;
                    nullable = StaticData.globalSetup.AllowCreditSales;
                    int num3;
                    if (nullable.HasValue)
                    {
                        nullable = StaticData.globalSetup.AllowCreditSales;
                        num3 = !nullable.Value ? 1 : 0;
                    }
                    else
                        num3 = 1;
                    if (num3 == 0)
                        this.btnCreditCollection.Visible = true;
                    else
                        this.btnCreditCollection.Visible = false;
                    this.cbArrangement.Visible = false;
                    this.lblArrangement.Visible = false;
                    this.btnReset.Visible = false;
                    if (StaticData.UserId == StaticData.SystemUser)
                    {
                        this.cbArrangement.Visible = true;
                        this.btnReset.Visible = true;
                        this.lblArrangement.Visible = true;
                    }
                    else
                    {
                        UsersDesktop usersDesktop = new UsersDesktopService((IDbFactory)new DbFactory()).Get(StaticData.UserId);
                        if (usersDesktop != null && usersDesktop.HasHomeButtonEditPermi == "Y")
                        {
                            this.cbArrangement.Visible = true;
                            this.lblArrangement.Visible = true;
                            this.btnReset.Visible = true;
                        }
                    }
                    this.lblCounter.Text = " Counter : " + StaticData.CounterId;
                    this.lblShop.Text = " Shop : " + StaticData.ShopId + " / " + StaticData.ShopName;
                    this.lblUser.Text = " Login By : " + StaticData.UserId;
                    this.Opacity = 1.0;
                    this.timer1.Enabled = true;
                    this.Show();
                }
                catch (Exception ex)
                {
                    int num = (int)MessageBox.Show(ex.Message);
                    Application.Current.Shutdown();
                }*/
            }
        }

        private void LoadData()
        {
            this.Init();
            try
            {
                StaticData staticData = new StaticData();
                StaticData.isAllDataLoaded = false;
                //this.LoadThemeData();
                DBChanger dbChanger = new DBChanger();
                dbChanger.InitializeMainLogTable();
                dbChanger.ExecuteChanges();
                GlobalSetup topSetup = this._serviceGlobal.GetTopSetup();
                if (topSetup == null)
                {
                    int num1 = (int)MessageBox.Show("Global Setup not configured");
                }
                else
                {
                    StaticData.ShopId = topSetup.StoreId;
                    StaticData.globalSetup = topSetup;
                    /*ShopList byId = this._serviceShop.GetById(topSetup.StoreId);
                    StaticData.ServerDate = new ServerService().GetServerDate();
                    if (byId != null)
                    {
                        StaticData.ShopName = byId.ShopName;
                        StaticData.ShopAddr = string.Format("{0},{1},{2}", (object)byId.VillAreaRoad, (object)byId.Post, (object)byId.District);
                    }
                    else
                    {
                        int num2 = (int)MessageBox.Show("Shop Name and adress cast is fail");
                    }
                    StaticData.MacAddress = GlobalClass.GetMacAddressAll();
                    Counter counerByMacAll = this._serviceCounter.GetCounerByMacAll(GlobalClass.GetMacAddressAll());
                    if (counerByMacAll != null)
                    {
                        StaticData.CounterId = counerByMacAll.CounterID;
                        StaticData.CounterName = counerByMacAll.CounterName;
                    }*/
                    if (StaticData.ServerDate.Date != DateTime.Now.Date)
                    {
                        int num3 = (int)MessageBox.Show("Computer date validation fail, please correct the date before using this software");
                        Application.Current.Shutdown();
                    }
                    StaticData.isAllDataLoaded = true;
                    this.isConnectionOk = true;
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
                Application.Current.Shutdown();
            }
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            List<ButtonModel> buttonModelList = new List<ButtonModel>();
            /*
            foreach (DesktopMenu desktopMenu in this._serviceMenu.Gets().ToList<DesktopMenu>())
            {
                Control control = ((IEnumerable<Control>)this.Controls.Find(desktopMenu.UMenuName, true)).FirstOrDefault<Control>();
                if (control != null)
                {
                    ButtonModel buttonModel1 = new ButtonModel();
                    buttonModel1.ButtonName = control.Name;
                    ButtonModel buttonModel2 = buttonModel1;
                    Point location = control.PointToScreen(new Point());
                    int x = (int)location.X;
                    buttonModel2.PosX = x;
                    ButtonModel buttonModel3 = buttonModel1;
                    location = control.PointFromScreen(new Point());
                    int y = (int)location.Y;
                    buttonModel3.PosY = y;
                    buttonModelList.Add(buttonModel1);
                }
            }
            */
            try
            {
                string str = JsonConvert.SerializeObject((object)buttonModelList);
                ThemeSetting topSetup = this._serviceThemedata.GetTopSetup();
                if (topSetup != null)
                {
                    topSetup.ButtonsPossitionLeft = str;
                    this._serviceThemedata.Update(topSetup);
                    this._serviceThemedata.Save();
                }
            }
            catch (Exception ex)
            {
            }
            if (this.Currentuc == null || !(this.Currentuc.Content == StaticData.TabText.ChartOfAccounts))
                return;
            new ucChartOfAccounts();
        }
    }
}
