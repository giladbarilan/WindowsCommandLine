﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp28
{
    public static class Compodents //Covers All Basic Compodents those that not connected directly to something
    {
        public static List<string> explaination_to_component = new List<string>()
        {
                "See All files& directories on the current working directory",
                "Change Color -- color (number value)",
                "Clean Screen",
                "Get All Components",
                "Create File -- echo \"text input\" >> \"filename.txt\"",
                "Change Directory -- cd \"directory name\"",
                "Return To Last Directory",
                "close the cmd",
                "make dir -- mkdir \"directory name\"",
                "remove file -- del \"file name\"",
                "remove directory -- rmdir \"directory name\""
        };
        public static List<string> components = new List<string>()
            {
                "dir",//See All files& directories on the current working directory
                "color",//Change Color
                "cls",//Clean Screen
                "\\help", //Get All Components
                "echo", //Create File
                "cd",//Change Directory
                "cd..",//Return To Last Directory
                "exit",//close the cmd
                "mkdir",//make dir
                "del",//remove file
                "rmdir",//remove directory
                "type",//Return All Bytes
            };
        public static void DirPrinterFunction<T>(T[] array)
        {
            if (typeof(T) == typeof(FileInfo))
            {
                FileInfo[] file_array = array as FileInfo[];
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Files = ");
                Console.ForegroundColor = ConsoleColor.White;
                foreach (var file in file_array)
                {
                    Console.WriteLine(file.Name);
                }
            }
            else if (typeof(T) == typeof(DirectoryInfo))
            {
                DirectoryInfo[] dir_array = array as DirectoryInfo[];
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Directory = ");
                Console.ForegroundColor = ConsoleColor.White;
                foreach (var dir in dir_array)
                {
                    Console.WriteLine(dir.Name + "\\");
                }
            }
            else
            {
                foreach(var v in array)
                {
                    Console.WriteLine(v);
                }
            }
        }

        public static void Help()
        {
            int counter = 0;
            foreach (var q in components)
            {
                Console.WriteLine(q + " -- " + explaination_to_component[counter]);
                counter++;
            }
        }




    }
}