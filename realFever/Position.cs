using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace realFever
{
    class Positions : Dictionary<Position, int>
    {
        public static Positions getAll()
        {
            Positions p = new Positions();
            p.Add(Position.G, 2);
            p.Add(Position.D, 5);
            p.Add(Position.M, 5);
            p.Add(Position.A, 3);
            return p;
        }
     }

    enum Position{
        G,
        D,
        M,
        A
    }
}
