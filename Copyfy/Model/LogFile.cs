using Copyfy.View.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copyfy.Model
{
    class LogFile
    {
        private string path { get; set; }

        public LogFile()
        {
            InitPath();
            Create();
        }

        private void Create()
        {
            try 
            {
                FileInfo fi = new FileInfo(path);
                if (!fi.Exists)
                {
                    using (FileStream FS = File.Create(path));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InitPath()
        {
            path = DateTime.Today.ToString("d").Contains("/") ? $"Logs\\{DateTime.Today.ToString("d").Replace('/','.')}.txt" : $"Logs\\{DateTime.Today.ToString("d")}.txt";// If dateTime contains "/" because of the different cultures, need to change it to ".", because it is the name of the log file.
        }

        public void Write(string data)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine("\n*****************************");
                    sw.WriteLine(DateTime.Now.ToString());
                    sw.WriteLine();
                    sw.Write(data);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
