using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ZPExtensionsMethods.HtmlEllementExtentions;
using static ZPExtensionsMethods.TabExtentions;
using static ZPExtensionsMethods.MoveCheckExtensions;
using static ZPBase.Base;
using ODDating.ProjectBase;

namespace ODDating.Actions.Moves
{
    public class JoinGroupMoves : MoveBase
    {
        public JoinGroupMoves(string startPageUrl) : base(startPageUrl)
        {
        }
        public static void Move1_GoToMyGroups()
        {
            string groupsButton = proj.GlobalVariables["GroupJoiner", "Move1_groupsButtonXpath"].Value;
            string exeptionMessage = $"Кнопка группы Move1_groupsButtonXpath не найдена, требуется проверка вручную";
            HtmlElement element = MovesCheck.IsElementDownload(groupsButton, exeptionMessage);
            element.ClickAndWait();
        }
    }
}
