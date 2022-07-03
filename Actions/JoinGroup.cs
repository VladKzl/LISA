using ODDating.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZPExtensionsMethods;
using static ODDating.Variables;
using ODDating.Actions.Moves;
using ODDating.ProjectBase;

namespace ODDating.Actions
{
    public class JoinGroup : ActionBase<JoinGroupMoves>
    {
        public JoinGroup(string startPageUrl, List<string> xpaths )
        {
            Moves = new JoinGroupMoves(startPageUrl, xpaths);
            ON = groupsOn;
        }
        public override void RunAction()
        {
            if (ON)
            {
                Moves.Move0_GoToMyGroups();
                Moves.Move1_SearchGroup();
            }
        }
    }
}
