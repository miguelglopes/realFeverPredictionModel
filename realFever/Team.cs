using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace realFever
{

    class Teams : Dictionary<Team, int>
    {
        public static Teams getAll()
        {
            Teams t = new Teams();
            t.Add(Team.PRT, 64);
            t.Add(Team.SPO, 59);
            t.Add(Team.BEN, 59);
            t.Add(Team.BRG, 52);
            t.Add(Team.MAR, 33);
            t.Add(Team.RAV, 37);
            t.Add(Team.BEL, 27);
            t.Add(Team.VGM, 29);
            t.Add(Team.BOA, 33);
            t.Add(Team.TON, 25);
            t.Add(Team.PTM, 27);
            t.Add(Team.PFE, 21);
            t.Add(Team.FEI, 20);
            t.Add(Team.CHA, 36);
            t.Add(Team.VST, 21);
            t.Add(Team.AVE, 22);
            t.Add(Team.MOR, 19);
            t.Add(Team.EST, 21);


            return t;
        }
    }

    enum Team
    {
        SPO,
        BEN,
        PRT,
        BRG,
        VGM,
        EST,
        CHA,
        PFE,
        BEL,
        BOA,
        VST,
        AVE,
        MAR,
        RAV,
        FEI,
        TON,
        MOR,
        PTM
    }
}
