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
            //string filename2 = "log_copy";
            //DateTime dt = new DateTime(1990, 1, 1);
            //string filename = "";
            //new DirectoryInfo(@"C:\1") - путь к папке с архивами
            
            
            
            

            //File.Copy(path + filename, path + filename2, true);

            string[] readText = File.ReadAllLines(path);

            //File.Delete(path + filename2);
            int n = 0;
            foreach (string confstring in readText.Reverse())
            {
                if (confstring.Contains("Name="))
                {
                    taskname.Add(confstring.Remove(0,5));
                    n++;
                }
                
            }

            string message = "{\"data\":[";
            foreach (string s in taskname)
            {
                message += ("{\"{#TASKNAME}\":\"" + s + "\"},");
            }

            message = message.Trim(',');
            message += "]}";

                Console.WriteLine(message);

            


            //string message ="The task " + logdata["name"] + " done with " + logdata["errors"] + " errors.";





           // Console.ReadLine();
        } //main
    }
}
