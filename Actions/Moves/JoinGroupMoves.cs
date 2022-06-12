using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ZPExtensionsMethods.HtmlEllementExtentions;
using static ZPExtensionsMethods.TabExtentions;
using static ZPExtensionsMethods.MoveCheckExtensions;
using static ZPBase.ProgramBase;

namespace ODDating.Actions.Moves
{
    class JoinGroupMoves : MoveBase
    {
        public void Move0(string StartPageUrl)
        {
            instance.ClearCache();
            instance.ClearCookie();
            project.Profile.Load(Account);
            instance.AllTabs.TryTabsNavigate(0, "yandex.ru");// костыль яндекс
            instance.AllTabs.TryTabsNavigate(0, StartPageUrl, "yandex.ru");
            CheckChangePassword();
        }
    }
}
