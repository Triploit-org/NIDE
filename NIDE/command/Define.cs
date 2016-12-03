using NIDE.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NIDE.command
{
    class Define : Command
    {
        public override int getArgCount()
        {
            return 3;
        }

        public override string getName()
        {
            return "let";
        }

        public override int run(List<string> args)
        {
            string var = args[1];
            string val = args[2];

            if (!Variables.existsVariable(var))
            {
                Variable v = new Variable(var, val, true);
                Variables.addVariable(v);
                return -2;
            }
            else
            {
                if (args[1] == "n6p")
                {
                    Variables.Ny6Path = args[2];
                    return 0;
                }
                else if (args[1] == "cfgp")
                {
                    Variables.ConfPath = args[2];
                    return 0;
                }
                else if (args[1] == "show_all")
                {
                    string vars = "";
                    string var1;
                    string val1;

                    for (int i = 0; i < Variables.getVariables().Count; i++)
                    {
                        val1 = Variables.getVariables()[i].getValue();
                        var1 = Variables.getVariables()[i].getName();

                        vars = (vars + var + " = " + val);
                        vars += "\n";
                    }

                    MessageBox.Show(Variables.ConfPath + "\n" + Variables.Ny6Path + "\n" + vars);
                    return 0;
                }
                else
                {
                    Variables.setVariable(var, val);
                    return -3;
                }

            }

            return 0;
        }
    }
}
