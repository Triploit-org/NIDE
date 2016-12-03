using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIDE
{
    abstract class Command
    {
        public abstract int getArgCount();
        public abstract int run(List<string> args);
        public abstract string getName();
    }
}
