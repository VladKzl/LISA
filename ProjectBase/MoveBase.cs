using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using static ODDating.Variables;
using  static ZPBase.Base;
using LISA;
using ZPExtensionsMethods;

namespace ODDating.ProjectBase
{
    public abstract class MoveBase : Common
    {
        public List<string> xPaths { get; set; }
        public MoveBase(string startPageUrl, List<string> xPaths)
        {
            this.xPaths = xPaths;
            BrowseStartPage(startPageUrl);
        }
        public void BrowseStartPage(string startPageUrl)
        {
            instance.ClearCache();
            instance.ClearCookie();
            project.Profile.Load(AccountPath);
            /*instance.AllTabs.TryTabsNavigate(0, "yandex.ru");// костыль яндекс*/
            instance.AllTabs.TryTabsNavigate(0, startPageUrl, "yandex.ru");
            MoveCheckExtensions.CheckChangePassword();
        }
    }
}
