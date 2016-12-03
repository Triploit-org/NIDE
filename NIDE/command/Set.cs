using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NIDE.command
{
    class Set : Command
    {
        public override int getArgCount()
        {
            return 3;
        }

        public override string getName()
        {
            return "set";
        }

        public override int run(List<string> args)
        {
            return 0;
        }
    }
}
