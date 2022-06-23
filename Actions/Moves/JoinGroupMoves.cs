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

namespace ODDating.Actions.Moves
{
    public class JoinGroupMoves : MoveBase
    {
        public JoinGroupMoves(string startPageUrl) : base(startPageUrl)
        {
        }
        public void Move1_GoToMyGroups(params string[] xpaths)
        {
            string groupsButton = MovesXpaths[""][0];
            string exeptionMessage = $"Кнопка группы Move1_groupsButtonXpath не найдена, требуется проверка вручную";
            HtmlElement element = IsElementDownload(groupsButton, exeptionMessage);
            element.ClickAndWait();
        }
    }
}
