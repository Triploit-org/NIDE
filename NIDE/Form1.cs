using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using FastColoredTextBoxNS;
using System.Text.RegularExpressions;
using System.Threading;
using System.Diagnostics;

namespace NIDE
{

    public partial class Window : Form
    {
        public static List<string> errors = new List<string>();
        public static List<string> infos = new List<string>();

        public void print(string text)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(print), new object[] { text });
                return;
            }

            console.AppendText(text+"\n");
            console.ScrollToCaret();
            console.Update();

            this.Text = Variables.file + " - NIDE";
        }

        public void saveFile()
        {
            if (File.Exists(Variables.file))
            {
                File.WriteAllText(Variables.file, fctb.Text);
            }
            else
            {
                print("Error: Die Datei " + Variables.file + " kann zum Speichern nicht erstellt werden. Sie wird jetzt erstellt. ");
                File.Create(Variables.file).Close();
                print("Info: Veruschen sie es jetzt erneut.");
            }

            this.Text = Variables.file + " - NIDE";
        }

        public void closeFile()
        {
            this.Text = Variables.file + " - NIDE";
        }

        public void openFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "C:\\Users\\" + Environment.UserName + "\\Documents\\";
            ofd.Filter = "Ny++6 Variables.file (*.n6)|*.txt|All Variables.files (*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;


            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fctb.Text = File.ReadAllText(ofd.FileName);
                Variables.file = ofd.FileName;
            }

            this.Text = Variables.file + " - NIDE";
        }

        public void saveFileAs()
        {
            SaveFileDialog ofd = new SaveFileDialog();

            ofd.InitialDirectory = "C:\\Users\\" + Environment.UserName + "\\Documents\\";
            ofd.Filter = "Ny++6 Variables.file (*.n6)|*.txt|All Variables.files (*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (!File.Exists(ofd.FileName))
                    File.Create(ofd.FileName).Close();

                File.WriteAllText(ofd.FileName, fctb.Text);
                Variables.file = ofd.FileName;
            }
            else
            {
                if (!File.Exists(ofd.FileName))
                    File.Create(ofd.FileName);

                File.WriteAllText(ofd.FileName, fctb.Text);
                print("Error: Konnte die Datei nicht als das angegebene Objekt speichern!");
            }

            this.Text = Variables.file + " - NIDE";
        }

        public void PrintEvent()
        {
            while(true)
            {
                if (Variables.Write)
                {
                    print(Variables.Text);
                    Variables.Write = false;
                }
            }
        }


        public Window()
        {
            InitializeComponent();
            Variables.file = ("C:\\Users\\" + Environment.UserName + "\\Documents\\unnamed.txt");
            errors.Add("Error: Unable Error = 0!");


            Thread demoThread = new Thread(new ThreadStart(this.PrintEvent));
            demoThread.Start();

            this.Text = Variables.file + " - NIDE";
        }

        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = "C:\\Users\\" + Environment.UserName + "\\Documents\\";
            ofd.Filter = "Ny++6 Variables.file (*.n6)|*.txt|All Variables.files (*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Variables.file = ofd.FileName;
                fctb.Text = File.ReadAllText(ofd.FileName);
            }
            else
            {
                print("Error: Konnte die Datei nicht finden!");
            }
        }

        private void speichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void speichernAlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileAs();
        }

        private void fctb_KeyDown(object sender, KeyEventArgs e)
        {
            this.Text = Variables.file + " - NIDE";

            if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
            {
                saveFile();
            }
            else if (e.KeyCode == Keys.F5)
            {
                print("Info: Running...");

                cmds.Run r = new cmds.Run();
                r.run(new List<string>());

                return;
            }
            else if (e.KeyCode == Keys.F6)
            {
                command.LoadConf c = new command.LoadConf();
                c.run(new List<string>());
                print("Info: Config geladen!");
            }
        }

        private static System.Drawing.Color greenC = System.Drawing.ColorTranslator.FromHtml("#00e676");
        private static System.Drawing.Color redC = System.Drawing.ColorTranslator.FromHtml("#f44336");
        private static System.Drawing.Color lightorangeC = System.Drawing.ColorTranslator.FromHtml("#ffc107");
        private static System.Drawing.Color orangeC = System.Drawing.ColorTranslator.FromHtml("#ff9800");
        private static System.Drawing.Color blueC = System.Drawing.ColorTranslator.FromHtml("#2196f3");
        private static System.Drawing.Color purpleC = System.Drawing.ColorTranslator.FromHtml("#E578F0");
        private static System.Drawing.Color lightblueC = System.Drawing.ColorTranslator.FromHtml("#00bcd4");
        private static System.Drawing.Color lightgrayC = System.Drawing.ColorTranslator.FromHtml("#757575");

        private static SolidBrush green = new SolidBrush(greenC);
        private static SolidBrush red = new SolidBrush(redC);
        private static SolidBrush orange = new SolidBrush(orangeC);
        private static SolidBrush blue = new SolidBrush(blueC);
        private static SolidBrush purple = new SolidBrush(purpleC);
        private static SolidBrush lightblue = new SolidBrush(lightblueC);
        private static SolidBrush lightgray = new SolidBrush(lightgrayC);

        Style GreenStyle = new TextStyle(lightgray, null, FontStyle.Italic);
        Style BoldStyle = new TextStyle(purple, null, FontStyle.Regular);
        Style IncludeStyle = new TextStyle(purple, null, FontStyle.Regular);
        Style InSOutPutStyle = new TextStyle(Brushes.DeepSkyBlue, null, FontStyle.Regular);
        Style FunctionStyle = new TextStyle(blue, null, FontStyle.Bold);

        Style StringStyle = new TextStyle(green, null, FontStyle.Regular);
        Style NumberStyle = new TextStyle(orange, null, FontStyle.Regular);
        Style NormalStyle = new TextStyle(Brushes.White, null, FontStyle.Regular);

        private void fctb_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Text = Variables.file + " - NIDE";

            e.ChangedRange.ClearStyle(NormalStyle);
            e.ChangedRange.SetStyle(BoldStyle, @"moi ");
            e.ChangedRange.SetStyle(BoldStyle, @"mod ");
            e.ChangedRange.SetStyle(BoldStyle, @"mos ");
            e.ChangedRange.SetStyle(BoldStyle, @"mul ");
            e.ChangedRange.SetStyle(BoldStyle, @"div ");
            e.ChangedRange.SetStyle(BoldStyle, @"sub ");
            e.ChangedRange.SetStyle(BoldStyle, @"add ");
            e.ChangedRange.SetStyle(BoldStyle, @"poi ");
            e.ChangedRange.SetStyle(BoldStyle, @"defi ");
            e.ChangedRange.SetStyle(BoldStyle, @"defs ");
            e.ChangedRange.SetStyle(NumberStyle, @"@");

            e.ChangedRange.SetStyle(BoldStyle, @"eqi ");
            e.ChangedRange.SetStyle(BoldStyle, @"nqi ");
            e.ChangedRange.SetStyle(BoldStyle, @"lqi ");
            e.ChangedRange.SetStyle(BoldStyle, @"gqi ");
            e.ChangedRange.SetStyle(BoldStyle, @"eqs ");
            e.ChangedRange.SetStyle(BoldStyle, @"lqs ");
            e.ChangedRange.SetStyle(BoldStyle, @"nqs ");
            e.ChangedRange.SetStyle(BoldStyle, @"gqs ");

            e.ChangedRange.SetStyle(BoldStyle, @"end");
            e.ChangedRange.SetStyle(BoldStyle, @"return");
            e.ChangedRange.SetStyle(BoldStyle, @"cl ");

            e.ChangedRange.SetStyle(BoldStyle, @"prv ");
            e.ChangedRange.SetStyle(BoldStyle, @"say ");
            e.ChangedRange.SetStyle(BoldStyle, @"inp ");

            e.ChangedRange.SetStyle(FunctionStyle, @"{[a-zA-Z0-9]*}");
            e.ChangedRange.SetStyle(BoldStyle, @"endf");
            e.ChangedRange.SetStyle(NumberStyle, @"[*[0-9]*\b]");
            e.ChangedRange.SetStyle(StringStyle, "\"[a-zA-Z0-9!-?_ =)(/&%$§!]*\"");

            e.ChangedRange.SetStyle(BoldStyle, @"%indic");
            e.ChangedRange.SetStyle(BoldStyle, @"%inc");
            e.ChangedRange.SetStyle(BoldStyle, @"%def");
            e.ChangedRange.SetStyle(BoldStyle, @"%undef");
            fctb.Update();

        }
        

        private void Window_Load(object sender, EventArgs e)
        {
            errors.Add("Zu viele Argumente!");
            errors.Add("Befehl nicht gefunden!");
            errors.Add("Zun wenige Argumente!");
            errors.Add("Variable nicht gefunden!");
            errors.Add("Datei nicht gefunden!");
            errors.Add("Error in der Config-Datei!");
            errors.Add("Ny++6 Pfad nicht gesetzt!");
            //errors.Add("");
            //errors.Add("");
            //errors.Add("");
            //errors.Add("");
            //errors.Add("");

            infos.Add("ERR == 0!");
            infos.Add("UNABLE ERROR!");
            infos.Add("Neue Variable hinzugefügt!");
            infos.Add("Variable gesetzt!");
            // infos.Add("");
            // infos.Add("");
            // infos.Add("");
            // infos.Add("");
        }

        private void input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (input.Text == "help")
                {
                    input.Text = "";
                }
                else
                {
                    Parser p = new Parser(input.Text);
                    p.initCommands();

                    print("> " + input.Text);
                    input.Text = "";

                    int err = p.parseCommand(p.parseCode());

                    // 0 = all is fine
                    // 1 = to many args
                    // 2 = command not found
                    // 3 = to few args
                    // 4 = variable not found
                    // 5 = file not found
                    // 6 = config error
                    // 7 = ny++6 path not set

                    // -2 = variable created
                    // -3 = variable set
                    // -4 = 
                    // -5 = 

                    if (err != 0 && err > 0)
                    {
                        print("Error: " + errors.ElementAt<string>(err));
                    }
                    else if (err < 0)
                    {
                        print("Info: " + infos[Math.Abs(err)]);
                    }
                }
                
            }
        }

        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmds.Run r = new cmds.Run();

            r.run(new List<string>());
        }
    }
}
