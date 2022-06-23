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
        public JoinGroup(bool on, string startPageUrl, List<string> xpaths ) : base(on, startPageUrl, xpaths)
        {
            Moves = new JoinGroupMoves(StartPageUrl);
        }
        public override void RunAction()
        {
            Moves.Move1_GoToMyGroups(Xpaths[0]);
        }
    }
}
