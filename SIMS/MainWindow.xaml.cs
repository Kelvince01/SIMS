using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using CustomControls;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
using SIMS;
using SIMS.BLL;
using SIMS.Data.Infrastructure;
using SIMS.LICENSE;
using SIMS.Models;
using SIMS.Service;
using SIMS.UserControls;
using SIMS.UserControls.Accounts;
using SIMS.UserControls.Setups;
using SIMS.Windows;
using Color = System.Windows.Media.Color;
using Image = System.Windows.Controls.Image;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

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

        private ICampusService _serviceShop;

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
        private DispatcherTimer timer1;

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
            //LicnenceVerifier licnenceVerifier = new LicnenceVerifier();
            string msg = "";
            /*while (true)
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
            }*/
            if (this.demoThread == null)
                this.demoThread = new Thread(new ThreadStart(this.LoadData));
            if (!this.demoThread.IsAlive)
            {
                this.demoThread = new Thread(new ThreadStart(this.LoadData));
                this.demoThread.Start();
            }
            if (!(e is ThresholdReachedRoutedEventArgs))
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

        private void LoadThemeData(bool isThread = true)
        {
            ThemeSetting topSetup = this._serviceThemedata.GetTopSetup();
            if (topSetup == null)
                return;
            try
            {
                /*MetroColorStyle color = (MetroColorStyle)Enum.Parse(typeof(MetroColorStyle), topSetup.ButtonColor);
                if (isThread)
                {
                    this.SetStyle(color);
                    this.SetStyle2(Color.FromArgb(int.Parse(topSetup.BackgroundCOlor)));
                }
                else
                {
                    this.metroStyleManager.Style = color;
                    this.BackColor = Color.FromArgb(int.Parse(topSetup.BackgroundCOlor));
                    this.statusStrip1.BackColor = Color.FromName(color.ToString());
                }
                Application.DoEvents();*/
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("Theme Not set" + ex.Message);
            }
        }

        /*private void SetStyle(MetroColorStyle color)
        {
            if (this.btnAttendanceReport.InvokeRequired)
            {
                this.Invoke((Delegate)new MDIMainNew.SetStyleCallback(this.SetStyle), (object)color);
            }
            else
            {
                this.metroStyleManager.Style = color;
                this.statusStrip1.BackColor = Color.FromName(color.ToString());
            }
        }*/

        private void SetStyle2(Color color)
        {
            /*if (this.btnAttendanceReport.InvokeRequired)
            {
                this.Invoke((Delegate)new MDIMainNew.SetStyle2Callback(this.SetStyle2), (object)color);
            }
            else
            {
                this.BackColor = color;
                this.BackColor = color;
                this.Update();
                this.Refresh();
            }*/
        }

        private void ApplyMenuPermission()
        {
            List<DesktopMenu> list = this._serviceMenu.Gets().ToList<DesktopMenu>();
            foreach (DesktopMenu desktopMenu in list)
            {
                /*Control control = ((IEnumerable<Control>)this.Controls.Find(desktopMenu.UMenuName, true)).FirstOrDefault<Control>();
                if (control != null)
                {
                    control.MouseDown += new MouseEventHandler(this.myControl_MouseDown);
                    control.MouseMove += new MouseEventHandler(this.myControl_MouseMove);
                    control.MouseUp += new MouseEventHandler(this.myControl_MouseUp);
                }*/
            }
            ThemeSetting topSetup = this._serviceThemedata.GetTopSetup();
            if (topSetup != null && !string.IsNullOrEmpty(topSetup.ButtonsPossitionLeft))
            {
                foreach (ButtonModel buttonModel1 in JsonConvert.DeserializeObject<List<ButtonModel>>(topSetup.ButtonsPossitionLeft))
                {
                    /*Control control = ((IEnumerable<Control>)this.Controls.Find(buttonModel1.ButtonName, true)).FirstOrDefault<Control>();
                    if (control != null)
                    {
                        List<ButtonModel> orginalLocations = this.ButtonOrginalLocations;
                        ButtonModel buttonModel2 = new ButtonModel();
                        buttonModel2.ButtonName = control.Name;
                        ButtonModel buttonModel3 = buttonModel2;
                        /*Point location = control.Location;
                        int x = location.X;
                        buttonModel3.PosX = x;
                        ButtonModel buttonModel4 = buttonModel2;
                        location = control.Location;
                        int y = location.Y;
                        buttonModel4.PosY = y;
                        ButtonModel buttonModel5 = buttonModel2;
                        orginalLocations.Add(buttonModel5);
                        control.Location = new Point(buttonModel1.PosX, buttonModel1.PosY);#1#
                    }*/
                }
            }
            if (StaticData.UserId == StaticData.SystemUser)
            {
                foreach (DesktopMenu desktopMenu in list)
                {
                    /*Control control = ((IEnumerable<Control>)this.Controls.Find(desktopMenu.UMenuName, true)).FirstOrDefault<Control>();
                    if (control != null)
                        control.IsEnabled = true;*/
                }
                //this.btnSettings.Enabled = true;
            }
            else
            {
                foreach (DesktopMenu desktopMenu in list)
                {
                    /*Control control = ((IEnumerable<Control>)this.Controls.Find(desktopMenu.UMenuName, true)).FirstOrDefault<Control>();
                    if (control != null)
                        control.Enabled = false;*/
                }
                UsersDesktopMenu usersDesktopMenu = this._serviceUsersMenus.Gets(StaticData.UserId).FirstOrDefault<UsersDesktopMenu>();
                if (usersDesktopMenu == null)
                    return;
                string[] strArray = usersDesktopMenu.UMenuID.Split(',');
                foreach (DesktopMenu desktopMenu in list)
                {
                    /*Control control = ((IEnumerable<Control>)this.Controls.Find(desktopMenu.UMenuName, true)).FirstOrDefault<Control>();
                    if (control != null)
                    {
                        foreach (string str in strArray)
                        {
                            if (str == desktopMenu.UMenuID.ToString())
                                control.Enabled = true;
                        }
                    }*/
                }
                //this.btnSettings.Enabled = false;
            }
        }

        private void OpenPage(UserControl uc)
        {
            if (this.isMoveed)
                return;
            this.isDragging = false;
            if (this.Currentuc != null)
            {
                this.animator.swipe(false);
                this.Currentuc = (UserControl)null;
            }
            this.animator = new AnimationContrainterUserControl((Window)this);
            this.animator.Width = this.Width;
            this.animator.Height = this.Height - 40;
            /*this.animator.panelTop.Background = Color.FromName(((MetroColorStyle)Enum.Parse(typeof(MetroColorStyle), this._serviceThemedata.GetTopSetup().ButtonColor)).ToString());
            this.animator.Controls.Add((Control)uc);
            int x = (this.Width - uc.Width) / 2;
            int y = (this.animator.Height - uc.Height) / 2;
            uc.Location = new Point(x, y);
            Image pictureBox1 = new Image();
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = (Image)Resources.r_V2;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(0, this.animator.Height - 70);
            pictureBox1.Size = new Size(132, 68);
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            this.animator.Controls.Add((Control)pictureBox1);
            Image pictureBox2 = new Image();
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.BackgroundImage = (Image)Resources.LogoMsdsl;
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Cursor = Cursors.Hand;
            pictureBox2.Location = new Point(this.animator.Width - 165, this.animator.Height - 70);
            pictureBox2.Size = new Size(154, 66);
            pictureBox2.TabIndex = 12;
            pictureBox2.TabStop = false;
            this.animator.Controls.Add((Control)pictureBox2);*/
            this.Currentuc = uc;
            this.animator.swipe();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.IsEnabled = false;
            this.Init();
            IAttenantLogService attenantLogService = (IAttenantLogService)new AttenantLogService((IDbFactory)new DbFactory());
            if (attenantLogService.GetByShopAndDate(StaticData.ShopId, DateTime.Now) != null)
                return;
            attenantLogService.Create(new AttenantLog()
            {
                ShopID = StaticData.ShopId,
                InTime = new DateTime?(DateTime.Now)
            });
            attenantLogService.Save();
        }

        private void MainWindow_OnActivated(object sender, EventArgs e)
        {
            if (this.frmLastOpend == null)
                return;
            this.frmLastOpend.Activate();
        }

        private void MainWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.metroLogOffPanel1.SetToInitStage();
        }

        private void MainWindow_OnMouseMove(object sender, MouseEventArgs e)
        {
            this.PointToScreen(new Point(e.GetPosition(this).X, e.GetPosition(this).Y));
            if (e.GetPosition(this).X >= 65 && e.GetPosition(this).X <= 1217 && e.GetPosition(this).Y >= 427 && e.GetPosition(this).Y <= 619) ;
        }

        private void OpenPageNew(UserControl uc)
        {
            Tile metroTile1 = (Tile)null;
            /*foreach (object control in (ArrangedElementCollection)this.panelTask.Controls)
            {
                if (control is Tile metroTile2)
                {
                    if (metroTile2.Text == uc.Text)
                        metroTile1 = metroTile2;
                    else
                        (metroTile2.Tag as MetroWindow).WindowState = WindowState.Minimized;
                }
            }*/
            if (metroTile1 == null)
            {
                metroTile1 = new Tile();
                /*this.panelTask.Controls.Add((Control)metroTile1);
                metroTile1.ActiveControl = (Control)null;
                metroTile1.BackColor = Color.FromArgb(0, 121, 193);
                metroTile1.Cursor = Cursors.Hand;
                metroTile1.Dock = DockStyle.Left;
                metroTile1.Location = new Point(0, 0);
                metroTile1.Name = uc.Text;
                metroTile1.Size = new Size(97, 39);
                metroTile1.AutoSize = true;
                metroTile1.TabIndex = 15;
                metroTile1.Text = uc.Text;
                metroTile1.TextAlign = ContentAlignment.MiddleLeft;
                metroTile1.TileImage = (Image)Resources.appbar_starwars_sith;
                metroTile1.TileImageAlign = ContentAlignment.MiddleLeft;
                metroTile1.TileTextFontSize = TileTextSize.Small;
                metroTile1.UseCustomBackColor = true;
                metroTile1.UseSelectable = true;
                metroTile1.UseStyleColors = false;
                metroTile1.Click += new EventHandler(this.metroTileTaskButton_Click);*/
            }
            if (metroTile1.Tag != null)
                return;
            MetroWindow frm = new MetroWindow();
            /*frm.KeyPreview = true;
            frm.FormClosed += new FormClosedEventHandler(this.frm_FormClosedChile);
            this.AddKeyDownEvent(frm, uc);
            frm.WindowState = FormWindowState.Normal;
            frm.MaximizeBox = false;
            frm.Width = this.Width - 25;
            frm.Height = this.Height - 82;
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(1, 0);
            frm.Controls.Add((Control)uc);
            frm.Text = uc.Text;
            frm.LostFocus += new EventHandler(this.frm_LostFocus);
            int x = (this.Width - uc.Width) / 2;
            int y = (this.Height - uc.Height) / 2;
            uc.Location = new Point(x, y);
            Image pictureBox1 = new Image();
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = (Image)Resources.r_V2;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(0, frm.Height - 120);
            pictureBox1.Size = new Size(132, 68);
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            frm.Controls.Add((Control)pictureBox1);
            Image pictureBox2 = new Image();
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.BackgroundImage = (Image)Resources.LogoMsdsl;
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Cursor = Cursors.Hand;
            pictureBox2.Location = new Point(frm.Width - 165, frm.Height - 120);
            pictureBox2.Size = new Size(154, 66);
            pictureBox2.TabIndex = 12;
            pictureBox2.TabStop = false;
            frm.Controls.Add((Control)pictureBox2);*/
            metroTile1.Tag = (object)frm;
            frm.Show();
        }

        private void AddKeyDownEvent(MetroWindow frm, UserControl ucPass) => frm.KeyDown += (KeyEventHandler)((s, e) =>
        {
            /*if (ucPass.Text == StaticData.TabText.SupplierReturn)
            {
                if (!(ucPass is ucSupplierReturn ucSupplierReturn2))
                    return;
                ucSupplierReturn2.ucShopTransfer_KeyDown(s, e);
            }
            else if (ucPass.Text == StaticData.TabText.GroupSetup)
            {
                if (!(ucPass is ucGroupSetup ucGroupSetup2))
                    return;
                ucGroupSetup2.ucGroupSetup_KeyDown(s, e);
            }
            else
            {
                if (!(ucPass.Text == StaticData.TabText.CustomerReport) || !(ucPass is cuCustomerReport cuCustomerReport2))
                    return;
                cuCustomerReport2.cuCustomerReport_KeyDown(s, e);
            }*/
        });
    }
}
