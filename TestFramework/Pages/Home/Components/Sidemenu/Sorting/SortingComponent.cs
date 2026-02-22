using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Drivers;
namespace TestFramework.Pages.Home
{
    public class SortingComponent(DriverManager driver) : BasePage(driver)
    {
        private SortingCriteriaComponent _criteria;
        private SortingOrderComponent _order;

        public SortingCriteriaComponent Criteria => _criteria ??= new SortingCriteriaComponent(DriverMgr);
        public SortingOrderComponent Order => _order ??= new SortingOrderComponent(DriverMgr);
    }
}
