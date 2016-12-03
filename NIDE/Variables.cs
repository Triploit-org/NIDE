using NIDE.objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NIDE
{
    class Variables
    {
        public static string InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return textBox.Text;
        }


        public static string Ny6Path = "[NOTSET]";
        public static string ConfPath = "C:\\Users\\" + Environment.UserName + "\\Documents\\nideconf.cfg";
        public static string file = "";

        public static bool Write = false;
        public static string Text = "";

        private static List<Variable> vars = new List<Variable>();

        public static Variable getVariable(string name)
        {
            name = name.Replace("%", "");
            for (int i = 0; i < vars.Count; i++)
            {
                if (vars[i].getName() == name)
                {
                    return vars[i];
                }
            }

            Variable v = new Variable("[<NOTFOUND:N>]", "[<NOTFOUND:V>]", false);
            return v;
        }

        public static List<Variable> getVariables()
        {
            return vars;
        }

        public static void addVariable(Variable v)
        {
            v.setName(v.getName().Replace("%", ""));
            vars.Add(v);
        }

        public static void setVariable(string name, string value)
        {
            name = name.Replace("%", "");
            for (int i = 0; i < vars.Count; i++)
            {
                if (vars[i].getName() == name)
                {
                    vars[i].setValue(value);
                }
            }
        }

        public static bool existsVariable(string name)
        {
            name = name.Replace("%", "");
            for (int i = 0; i < vars.Count; i++)
            {
                if (vars[i].getName() == name)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
