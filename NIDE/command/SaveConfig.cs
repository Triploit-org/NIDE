using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIDE.command
{
    class SaveConfig : Command
    {
        public override int getArgCount()
        {
            return 1;
        }

        public override string getName()
        {
            return "savcfg";
        }

        public override int run(List<string> args)
        {
            if (File.Exists(Variables.ConfPath))
            {
                File.Delete(Variables.ConfPath);
            }

            File.WriteAllText(Variables.ConfPath, "n6 " + Variables.Ny6Path);

            return 0;
        }
    }
}
