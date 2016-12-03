using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIDE
{
    class Output
    {
        public static void write(string s)
        {
            Variables.Write = true;
            Variables.Text = s;
        }    

        public static void writeText(string s)
        {
            Parser p = new Parser(s);
            List<string> cs = p.parseCode();
            string tmp = "";

            for (int i = 0; i < cs.Count; i++)
            {
                string c = cs[i];

                if (c.StartsWith("%"))
                {
                    string v = Variables.getVariable(c.Replace("%", "")).getValue();
                    c = v;                   
                }

                tmp = tmp + c + " ";
            }

            Variables.Write = true;
            Variables.Text = tmp;
        }
    }
}
