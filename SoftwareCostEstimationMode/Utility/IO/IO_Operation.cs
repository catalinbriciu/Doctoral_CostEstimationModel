using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SoftwareCostEstimationMode.Utility.IO
{
    public class IO_Operation 
    {
        string LineRead;
        StreamReader stream;
        StreamWriter stream_out;
        public void SetFileToBeRead(string file_name)
        {
            this.stream = new StreamReader(file_name);
        }
        public void SetFileToBeWrite(string file_name)
        {
            this.stream_out = new StreamWriter(file_name);
        }
        public bool IsInputFileEnd()
        {
            if(this.stream == null)
                return true;
            else
            {
                if (this.LineRead == null)
                    return true;
                else
                    return false;
            }
        }
        public string ReadLine()
        {
            if (this.stream != null)
            {
                this.LineRead = this.stream.ReadLine();
            }
            else
            {
                this.LineRead = null; 
            }
           return this.LineRead;
        }
        public void WriteLine(string Line)
        {
            if (this.stream_out != null)
            {
                stream_out.WriteLine(Line);
            }
            else
            {
                /*do nothing*/
            }
        }
        public void CloseInputFile(string file_name)
        {
              if(this.stream != null)
              {
                  this.stream.Close();
              }
        }
        public void CloseOutputFile(string file_name)
        {
            if (this.stream_out != null)
            {
                this.stream_out.Close();
            }
        }
    }
}
