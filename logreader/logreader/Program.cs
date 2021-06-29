using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(5000);
            string path = @"C:\Program Files (x86)\Cobian Backup 11\Logs\";
            Hashtable logdata = new Hashtable();
            string filename2 = "log_copy";
            DateTime dt = new DateTime(1990, 1, 1);
            string filename = "";
            //new DirectoryInfo(@"C:\1") - путь к папке с архивами
            FileSystemInfo[] fileSystemInfo = new DirectoryInfo(path).GetFileSystemInfos();
            foreach (FileSystemInfo fileSI in fileSystemInfo)
            {

                if (dt < Convert.ToDateTime(fileSI.CreationTime))
                {
                    dt = Convert.ToDateTime(fileSI.CreationTime);
                    filename = fileSI.Name;
                }

            }

            //Console.WriteLine("last =>> " + filename + ", created " + dt);
            // Console.ReadLine();

            File.Copy(path + filename, path + filename2, true);

            string[] readText = File.ReadAllLines(path + filename2);

            File.Delete(path + filename2);

            foreach (string stringlog in readText.Reverse())
            {
                if (stringlog.Contains("Backup done for"))
                {
                    String[] words = stringlog.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    logdata.Add("date", words[0]);
                    logdata.Add("time", words[1]);
                    logdata.Add("name", words[8].TrimEnd('.'));
                    logdata.Add("errors", words[10].TrimEnd('.'));
                    logdata.Add("size", words[20]);
                    logdata.Add("size_units", words[21]);

                    break;
                }

            }

            //foreach (string s in logdata.Keys)
            // Console.WriteLine(s + ": " + logdata[s]);

            string message = "The task " + logdata["name"] + " done with " + logdata["errors"] + " errors." + " " + logdata["size"] + " " + logdata["size_units"];
         
            try
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Application";
                    eventLog.WriteEntry(message, EventLogEntryType.Warning, 6789, 1);
                }
            }
            catch
            {
                Console.WriteLine("Возникло исключение!");
            }
            finally
            {
                Console.WriteLine("Блок finally");
                Console.WriteLine(message);
            }

//Console.WriteLine(message);
            //Thread.Sleep(3000);
            //Console.ReadLine();
        } //main
    }
}
