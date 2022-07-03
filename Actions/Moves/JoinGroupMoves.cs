using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZennoLab.CommandCenter;
using static ZPExtensionsMethods.HtmlEllementExtentions;
using static ZPExtensionsMethods.TabExtentions;
using static ZPExtensionsMethods.MoveCheckExtensions;
using ODDating.ProjectBase;
using static ODDating.Variables;
using ZPExtensionsMethods;
using static ZPBase.Base;
using static ODDating.DBHelpers.HelperGroups;

namespace ODDating.Actions.Moves
{
    public class JoinGroupMoves : MoveBase
    {
        public JoinGroupMoves(string startPageUrl, List<string> xpaths) : base(startPageUrl, xpaths)
        {
        }
        public void Move0_GoToMyGroups()
        {
            string exeptionMessage = "Кнопка группы Move0_GoToMyGroups не найдена, требуется проверка вручную";
            HtmlElement element = IsElementDownload(xPaths[0], exeptionMessage);
            element.ClickAndWait();
        }
        public void Move1_SearchGroup()
        {
            string exeptionMessage = "Поле поиска Move1_SearchGroup не найдено, требуется проверка вручную";
            HtmlElement element = IsElementDownload(xPaths[1], exeptionMessage);
            element.Focus();
            instance.SendText(GroupShortName, 100);
            instance.SendText("{ENTER}", 15);
            /*instance.ActiveTab.KeyEvent("enter", "press", "");*/
            instance.ActiveTab.WaitDownloading();
        }
        public void Move3_ClickOnGroup(string nameGroup)
        {
            string exeptionMessage = "Имя группы Move3_clickOnGroupXpath не найдено, требуется проверка вручную";
            string clickOnGroup = xPaths[1].Replace("replaceNameGroup", nameGroup);
/*            HtmlElement element = IsElementDownload(clickOnGroupReplace, exeptionMessage);
            element.ClickAndWait();*/
        }
    }
}
