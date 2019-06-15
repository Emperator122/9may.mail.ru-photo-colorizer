using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _9mayPhotoColorizer
{
    static class log
    {
        public static void Debug(string msg)
        {
            MessageBox.Show(msg);
        }
        public static void Error(string msg, Exception e)
        {
            MessageBox.Show(msg);
            MessageBox.Show(e.Message);
        }
    }
}
