﻿using ODDating.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZPExtensionsMethods;
using static ODDating.Variables;
using ODDating.Actions.Moves;

namespace ODDating.Actions
{
    public class JoinGroup : ActionBase
    {
        public override string StartPageUrl { get; set; }
        JoinGroupMoves Moves { get; set; } = new JoinGroupMoves();
        public JoinGroup(string StartPageUrl)
        {
            this.StartPageUrl = StartPageUrl;
        }
        public override void RunAction()
        {
            Move0_BrowseStartPage();

        }
        public override void Move0_BrowseStartPage()
        {
            Moves.Move0(StartPageUrl);
        }
    }
}
