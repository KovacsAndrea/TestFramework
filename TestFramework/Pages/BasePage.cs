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
        // Protected înseamnă că doar "copiii" (clasele care moștenesc) pot vedea DriverMgr
        protected readonly DriverManager DriverMgr;

        // Constructorul care va fi forțat pe toate paginile
        protected BasePage(DriverManager driverManager)
        {
            DriverMgr = driverManager;
        }

    }
}
