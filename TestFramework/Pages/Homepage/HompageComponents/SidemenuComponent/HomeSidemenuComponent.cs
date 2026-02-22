using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Drivers;

namespace TestFramework.Pages.Homepage
{
    public class HomeSidemenuComponent(DriverManager driver) : BasePage(driver)
    {
        private FilterComponent _filter = null!;
        private SortingComponent _sorting = null!;

        public FilterComponent Filter => _filter ??= new FilterComponent(DriverMgr);
        public SortingComponent Sorting => _sorting ??= new SortingComponent(DriverMgr);

    }
}
