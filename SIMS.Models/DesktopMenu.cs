using System;

namespace SIMS.Models
{
    public class DesktopMenu
    {
        public Decimal UMenuID { get; set; }

        public string UMenuName { get; set; }

        public string MenuTitle { get; set; }

        public int? OrderLevel { get; set; }

        public bool? enable { get; set; }

        public bool Checked { get; set; }
    }
}
