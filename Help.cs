using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdpixie
{
    internal static class Help
    {
        public static string Description
        {
            get
            {
                if (File.Exists("help.txt"))
                {
                    return File.ReadAllText("help.txt");
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
