using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIDE.objects
{
    class Variable
    {
        private string name = "";
        private string value = "";
        private bool write = true;

        public Variable(string name, string value, bool write)
        {
            this.name = name;
            this.value = value;
            this.write = write;
        }

        public string getName()
        {
            return name;
        }

        public string getValue()
        {
            return value;
        }

        public void setName(string n)
        {
            this.name = n;
        }

        public void setValue(string v)
        {
            if (this.write)
                this.value = v;
        }

        public bool isWriteAble()
        {
            return write;
        }
    }
}
