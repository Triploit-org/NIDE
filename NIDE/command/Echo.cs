using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIDE.command
{
    class Echo : Command
    {
        public override int getArgCount()
        {
            return -1;
        }

        public override string getName()
        {
            return "echo";
        }

        public override int run(List<string> args)
        {
            string tmp = "";
            for (int i = 1; i < args.Count; i++)
            {
                tmp = tmp + args[i] + " ";
            }
            // Output.write("Info: "+tmp);        
            Output.writeText(tmp);

            return 0;
        }
    }
}
