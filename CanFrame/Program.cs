using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CanFrame
{
    class Program
    {
        static void Main(string[] args)
        {
            string fName = @"C:\output\6397\6397.dat";

            CanData data = new CanData(fName);
            string[] temp = data.readings;
            Console.ReadLine();
        }
    }

    public class CanData
    {
        public string[] readings;
        public int[] ids;
        public string FileName { get; set; }
        public CanData(string mFileName)
        {
            readings = File.ReadAllLines("readings.txt");
            this.FileName = mFileName;
            Compare_dataset.Compare_dataset dataset = verifyFile();

            string[] outstr = dataset.getResult();

            string[] writedata = new string[outstr.Length + 1];
            int ct = 0;
            string headder = "TimeStamp,";
            for (int i = 0; i < readings.Length; i++)
            {
                if (i == 13 || i == 14)
                {
                    
                }else
                {
                    headder = headder + readings[i] + ",";
                    ct++;
                }

                
            }

            writedata[0] = headder;

            for (int i = 0; i < outstr.Length; i++)
            {
                writedata[i + 1] = outstr[i].Replace('|',',');
            }

            if (File.Exists(@"C:\Users\mhakin\Desktop\output.csv"))
            {
                File.Delete(@"C:\Users\mhakin\Desktop\output.csv");
            }

            File.WriteAllLines(@"C:\Users\mhakin\Desktop\output.csv", writedata);
        }

        private Compare_dataset.Compare_dataset verifyFile()
        {
            int[] tempint = new int[2];
            tempint[0] = 11;
            tempint[1] = 82;

            if (File.Exists(this.FileName))
            {
                
                ids = new int[readings.Length];

                for (int i = 0; i < ids.Length; i++)
                {
                    
                    ids[i] = i;

                    
                }
                Console.Write('\n');
                Compare_dataset.Compare_dataset candata = new Compare_dataset.Compare_dataset(this.FileName, ids);

                

                
                return candata;
            }

            return null;
            
        }
    }
}
