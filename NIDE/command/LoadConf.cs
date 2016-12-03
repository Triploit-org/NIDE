using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NIDE.command
{
    class LoadConf : Command
    {
        public override int getArgCount()
        {
            return 1;
        }

        public override string getName()
        {
            return "lcfg";
        }

        public override int run(List<string> args)
        {
            Output.writeText("Info: Lade Config...");

            if (!File.Exists(Variables.ConfPath))
                File.Create(Variables.ConfPath).Close();

            string[] fcontent = File.ReadAllLines(Variables.ConfPath);

            for (int i = 0; i < fcontent.Length; i++)
            {
                Parser p = new Parser(fcontent[i]);
                if (p.parseCode()[0] == "n6")
                {
                    Variables.Ny6Path = p.parseCode()[1];
                }
                else
                {
                    return 6;
                }
            }

            Output.writeText("Info: Ohne Fehler beendet!");
            return 0;
        }
    }
}
