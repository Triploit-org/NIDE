using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace NIDE.cmds
{
    class Run : Command
    {
      
        public override int run(List<string> args)
        {
            if (Variables.Ny6Path == "[NOTSET]")
            {
                string d = "Pfad";
                d = Variables.InputBox("Ny++6 Pfad", "Der Ny++6 Pfad wurde noch nicht gesetzt, er muss zum Auführen gesetzt werden. Bitte geben sie den Pfad zu der Datei ny6.exe an oder laden sie ihre Config-Datei(lcfg/F6)!", ref d);

                if (File.Exists(d))
                {
                    Process p1 = new Process();
                    ProcessStartInfo inf1 = new ProcessStartInfo();

                    inf1.FileName = "cmd.exe";
                    inf1.Arguments = "/k " + Variables.Ny6Path + " " + Variables.file;

                    p1.StartInfo = inf1;
                    p1.Start();

                    return 0;
                }
                else
                {
                    return 5;
                }

                return 7;
            }

            if (!File.Exists(Variables.Ny6Path))
            {
                return 5;
            }

            Process p = new Process();
            ProcessStartInfo inf = new ProcessStartInfo();

            inf.FileName = "cmd.exe";
            inf.Arguments = "/k " + Variables.Ny6Path + " " + Variables.file;

            p.StartInfo = inf;
            p.Start();

            return 0;
        }

        public override int getArgCount()
        {
            return 1;
        }

        public override string getName()
        {
            return "run";
        }
    }
}
