using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConsoleApp28
{
    public class DirectorysAndFiles
    {
        public static void TypeCompodent(string file_name,string full_path)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(file_name + "-");
            Console.ForegroundColor = ConsoleColor.White;
            try
            {
                Console.WriteLine(File.ReadAllText(full_path + '\\' + file_name));
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Access Denied " + e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }




    }
}
