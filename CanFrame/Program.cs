using System;
using System.Collections.Generic;
using System.IO;

namespace CanFrame
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "h")
            {
                Console.WriteLine("type test to run test file or enter the full path of the .dat file you wish to read in.");
                Console.WriteLine(@"Example:> CanFrame.exe c:\output\6082\6082.dat");
                return;
            }
            
            if (args.Length > 0)
            {
                string fName;
                string testfName = "6082.dat";
                if (args[0] == "test")
                {
                    Console.WriteLine("Running Test File 6082.dat");
                    fName = testfName;
                }else
                {
                    Console.WriteLine(args[0]);
                    fName = args[0];
                }
                
                

                CanData data = new CanData(fName);
                string[] temp = data.readings;
            }else
            {
                Console.WriteLine("Please enter a valid filename or type h for help");
            }
            
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

            if (File.Exists(mFileName + ".csv"))
            {
                File.Delete(mFileName + ".csv");
            }

            File.WriteAllLines(mFileName + ".csv", writedata);

            Console.WriteLine("Wrote " + mFileName +  ".csv");
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

    public class LogActivity
    {
        List<string> logdata = new List<string>();
        bool bufferEmpty = true;
        public void writeLog(string logMessage)
        {
            logdata.Add(logMessage);
            bufferEmpty = false;
        }


        public bool writeLogData(string sFileName)
        {

            if (!bufferEmpty)
            {
                if (File.Exists(sFileName))
                {
                    File.AppendAllLines(sFileName, logdata);
                }
                else
                {
                    File.WriteAllLines(sFileName, logdata);
                }
                logdata.Clear();
                bufferEmpty = true;
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
