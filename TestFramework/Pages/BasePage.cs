using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Drivers;

namespace TestFramework.Pages
{
    public abstract class BasePage
    {
        protected readonly DriverManager DriverMgr;

        protected BasePage(DriverManager driverManager)
        {
            DriverMgr = driverManager;
        }

    }
}
