using System.Collections.Generic;

namespace SIMS.BLL
{
    public class ChangeLogs
    {
        public string Date { get; set; }

        public string VersionNo { get; set; }

        public string ChangeLog { get; set; }

        public List<ChangeLogs> GetAll() => new List<ChangeLogs>()
    {
      new ChangeLogs()
      {
        Date = "02/22/2017",
        VersionNo = "1.8.113.36",
        ChangeLog = "* Fix Void ,and Promotion Problem"
      },
      new ChangeLogs()
      {
        Date = "",
        VersionNo = "",
        ChangeLog = "* Fix Employee Attendence problem"
      },
      new ChangeLogs()
      {
        Date = "",
        VersionNo = "",
        ChangeLog = "* Item Edit Flexiblity"
      },
      new ChangeLogs()
      {
        Date = "01/29/2017",
        VersionNo = "1.8.112.35",
        ChangeLog = "* Stock report add cost value"
      },
      new ChangeLogs()
      {
        Date = "",
        VersionNo = "",
        ChangeLog = "* Dashboard timer load "
      },
      new ChangeLogs()
      {
        Date = "11/13/2016",
        VersionNo = "1.7.111.34",
        ChangeLog = "* Receive multi product selction problem"
      },
      new ChangeLogs()
      {
        Date = "",
        VersionNo = "",
        ChangeLog = "* Sales screen color fix"
      },
      new ChangeLogs()
      {
        Date = "",
        VersionNo = "",
        ChangeLog = "* Sales Item Serial number added"
      },
      new ChangeLogs()
      {
        Date = "",
        VersionNo = "",
        ChangeLog = "* Style size barocde check"
      },
      new ChangeLogs()
      {
        Date = "11/06/2016",
        VersionNo = "1.7.110.33",
        ChangeLog = "* Add Barcode Print Support"
      },
      new ChangeLogs()
      {
        Date = "",
        VersionNo = "",
        ChangeLog = "* UI for Daily shop sales report selection"
      },
      new ChangeLogs()
      {
        Date = "11/06/2016",
        VersionNo = "1.6.109.33",
        ChangeLog = "* Add Decimal place suport for sale"
      },
      new ChangeLogs()
      {
        Date = "",
        VersionNo = "",
        ChangeLog = "* Add without attendance sales support "
      },
      new ChangeLogs()
      {
        Date = "11/03/2016",
        VersionNo = "1.5.108.33",
        ChangeLog = "* Shop Receive ,Item Delete Bug Fix "
      },
      new ChangeLogs()
      {
        Date = "10/26/2016",
        VersionNo = "1.5.107.32",
        ChangeLog = "* Group,Brand,Product Data sorting. "
      },
      new ChangeLogs()
      {
        Date = "",
        VersionNo = "1.5.107.32",
        ChangeLog = "* CPU, RPU Decimal Place add "
      },
      new ChangeLogs()
      {
        Date = "10/24/2016",
        VersionNo = "1.4.106.32",
        ChangeLog = "* Sales bug fix"
      },
      new ChangeLogs()
      {
        Date = "",
        VersionNo = "1.4.106.32",
        ChangeLog = "* receive report fix"
      },
      new ChangeLogs()
      {
        Date = "10/23/2016",
        VersionNo = "1.4.105.31",
        ChangeLog = "* Auto db changes deploye to client"
      },
      new ChangeLogs()
      {
        Date = "",
        VersionNo = "1.4.105.31",
        ChangeLog = "* Difference type invoice added"
      },
      new ChangeLogs()
      {
        Date = "10/20/2016",
        VersionNo = "1.3.104.31",
        ChangeLog = "* Menu distribution bug fix "
      },
      new ChangeLogs()
      {
        Date = "10/20/2016",
        VersionNo = "1.3.103.30",
        ChangeLog = "* UI Button Changed "
      },
      new ChangeLogs()
      {
        Date = "10/19/2016",
        VersionNo = "1.2.103.30",
        ChangeLog = "* Supplier received changed"
      },
      new ChangeLogs()
      {
        Date = "",
        VersionNo = "",
        ChangeLog = "* Sales Search option changed "
      },
      new ChangeLogs()
      {
        Date = "10/05/2016",
        VersionNo = "1.1.103.30",
        ChangeLog = "* Dash board released"
      },
      new ChangeLogs()
      {
        Date = "10/05/2016",
        VersionNo = "1.0.103.30",
        ChangeLog = "* Minor bug fixed"
      },
      new ChangeLogs()
      {
        Date = "10/01/2016",
        VersionNo = "1.0.99.29",
        ChangeLog = "* Inital realease of Retail master desktop"
      }
    };
    }
}
