using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Program Files (x86)\Cobian Backup 11\DB\MainList.lst";
            // Hashtable logdata = new Hashtable();
            List<string> taskname = new List<string>();
            List<string> weektaskname = new List<string>();
            List<string> monthtaskname = new List<string>();
            List<string> yeartaskname = new List<string>();
            //string filename2 = "log_copy";
            //DateTime dt = new DateTime(1990, 1, 1);
            //string filename = "";
            //new DirectoryInfo(@"C:\1") - путь к папке с архивами





            //File.Copy(path + filename, path + filename2, true);

            string[] readText = File.ReadAllLines(path);

            //File.Delete(path + filename2);
            
            foreach (string confstring in readText.Reverse())
            {
                if (confstring.Contains("Name="))
                {
                    if (confstring.Contains("Name=Week"))
                        weektaskname.Add(confstring.Remove(0, 5));
                    if (confstring.Contains("Name=Month"))
                        monthtaskname.Add(confstring.Remove(0, 5));
                    if (confstring.Contains("Name=Year"))
                        yeartaskname.Add(confstring.Remove(0, 5));
                    else
                        taskname.Add(confstring.Remove(0,5));
                    
                }
                
            }

            string message = "{\"data\":[";
            foreach (string s in taskname)
            {
                message += ("{\"{#TASKNAME}\":\"" + s + "\"},");
            }

            foreach (string s in weektaskname)
            {
                message += ("{\"{#WEEKTASKNAME}\":\"" + s + "\"},");
            }

            foreach (string s in monthtaskname)
            {
                message += ("{\"{#MONTHTASKNAME}\":\"" + s + "\"},");
            }

            foreach (string s in yeartaskname)
            {
                message += ("{\"{#YEARTASKNAME}\":\"" + s + "\"},");
            }

            message = message.Trim(',');
            message += "]}";

                Console.WriteLine(message);

            


           





            Console.ReadLine();
        } //main
    }
}
