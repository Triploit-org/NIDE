using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIDE.command
{
    class Exit : Command
    {
        public override int getArgCount()
        {
            return 1;
        }

        public override string getName()
        {
            return "exit";
        }

        public override int run(List<string> args)
        {
            Environment.Exit(0);
            return 0;
        }
    }
}
