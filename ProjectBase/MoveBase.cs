using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using static ODDating.Variables;
using ZPBase;
using LISA;
using ZPExtensionsMethods;

namespace ODDating.ProjectBase
{
    public abstract class MoveBase : Common
    {
        public MoveBase(string startPageUrl)
        {
            Move0_BrovseStartPage(startPageUrl);
        }
        public void Move0_BrovseStartPage(string startPageUrl)
        {
            instance.ClearCache();
            instance.ClearCookie();
            project.Profile.Load(Account);
            instance.AllTabs.TryTabsNavigate(0, "yandex.ru");// костыль яндекс
            instance.AllTabs.TryTabsNavigate(0, startPageUrl, "yandex.ru");
            MoveCheckExtensions.CheckChangePassword();
        }
    }
}
