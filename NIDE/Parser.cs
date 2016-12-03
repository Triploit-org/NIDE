using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIDE
{
    class Parser
    {
        private string code;
        private List<Command> cmds = new List<Command>();

        public Parser(string code)
        {
            this.code = code;
        }

        public List<Command> getCommands()
        {
            return this.cmds;
        }

        public void initCommands()
        {
            this.cmds.Add(new NIDE.cmds.Run());
            this.cmds.Add(new NIDE.command.LoadConf());
            this.cmds.Add(new NIDE.command.Set());
            this.cmds.Add(new NIDE.command.SaveConfig());
            this.cmds.Add(new NIDE.command.Define());
            this.cmds.Add(new NIDE.command.Echo());
            this.cmds.Add(new NIDE.command.Exit());
        }

        public List<string> parseCode()
        {
            List<string> cmds = new List<string>();
            string tmp = "";

            for (int i = 0; i < code.Length; i++)
            {
                if (code[i] == '\"')
                {
                    for (int j = i; code[j] != '\"' && j < code.Length; j++)
                    {
                        tmp += code[j];
                        i = j + 1;
                    }
                }
                else if (code[i] == ' ' || code[i] == ';' || code[i] == ',')
                {
                //    for (int j = i; (code[j] == ' ' || code[j] == ';' || code[j] == ',') && j < code.Length; j++)
                //    {
                //        i = j;
                //    }

                    cmds.Add(tmp);
                    tmp = "";
                }
                else
                {
                    tmp += code[i];
                }
            }

            cmds.Add(tmp);
            return cmds;
        }

        public int parseCommand(List<string> args)
        {
            for (int i = 0; i < cmds.Count; i++)
            {
                if (args[0] == cmds[i].getName())
                {
                    if (cmds[i].getArgCount() == -1)
                    {
                        cmds[i].run(args);
                        return 0;
                    }

                    if (args.Count > cmds[i].getArgCount())
                    {
                        return 1;
                    }
                    else if (args.Count < cmds[i].getArgCount())
                    {
                        return 3;
                    }
                    
                    return cmds[i].run(args);
                }
            }

            return 2;
        }
    }
}
