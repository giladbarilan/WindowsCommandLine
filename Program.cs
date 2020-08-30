using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ConsoleApp28
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Sample cmd Program");
            Console.WriteLine("Note: Not all cmd components included");
            Console.WriteLine("To Get All Components Please Write \\help");
            var components = Compodents.components;
            var explaination_to_component = Compodents.explaination_to_component;
            DirectoryInfo directory = new DirectoryInfo("C:\\"); // Cover dir change;
            var cd_changes = new Stack<object>();
            while(true)
            {
                Console.Write(directory.FullName+">"); // Get Directory Place
                string Component = Console.ReadLine(); // Get Component
                #region TypeCompodent
                if(Component.ToLower().StartsWith("type "))
                {
                    string file_name = Component.Split(' ')[1];
                    DirectorysAndFiles.TypeCompodent(file_name, directory.FullName);
                }
                #endregion
                #region Check Weather Component Exsist
                if (components.Any(x=>x.Split(' ')[0] == Component.ToLower().Split(' ')[0]) == false) // Managing if the component exsist
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(new SyntaxErrorException().Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                #endregion
                #region Dir Component
                if (Component.ToLower() == "dir")
                {
                    Compodents.DirPrinterFunction(directory.GetFiles());
                    Compodents.DirPrinterFunction(directory.GetDirectories());
                }
                #endregion
                #region CleanScreen - cls
                if (Component.ToLower() == "cls")
                {
                    Console.Clear();
                    continue;
                }
                #endregion
                #region CreateFile-ECHO
                if (Component.ToLower().StartsWith("echo")) //ex: ECHO file>>file.txt
                {
                    string[] component_spliter = Component.Split(">>");//seperated to ECHO, file>>file.txt
                    string[] seperate_echo_before_two_arrows = component_spliter[0].Split(' ');
                    string InputText = ""; //text
                    foreach(var nonspace in seperate_echo_before_two_arrows)
                    {
                        if(nonspace.ToLower()!="echo")
                        {
                            InputText += nonspace+' ';
                        }
                    }
                    string file_name = component_spliter[1]; //file name

                    if(File.Exists(file_name) == false)
                    {
                        try
                        {
                            File.WriteAllText(directory.FullName + "\\"+ file_name, InputText);
                        }
                        catch
                        {
                            Console.WriteLine("Access Denied");
                        }
                    }
                }
                #endregion
                #region Help
                if (Component.ToLower() == "\\help")
                {
                    Compodents.Help();
                }
                #endregion
                #region ManageColors color
                if(Component.ToLower() == "color \\help")
                {
                    for(int i=0;i<15;i++)
                    {
                        Console.WriteLine(i+" "+(ConsoleColor)i);
                    }
                    continue;
                }
                if (Component.TrimStart().ToLower().Split(' ')[0] == "color")
                {
                    string Color_to_change_to = "";
                    try
                    {
                        Color_to_change_to = Component.Split(' ')[1];
                    }
                    catch
                    {
                        continue;
                    }
                    int GetColorFromCmd = 0;
                    try
                    {
                        GetColorFromCmd = int.Parse(Color_to_change_to);
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please Input Numbers to get a color change");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }
                    try
                    {
                        Console.ForegroundColor = (ConsoleColor)GetColorFromCmd;
                    }
                    catch
                    {
                        Console.WriteLine("Color Not Exsist");
                        continue;
                    }
                }
                #endregion
                #region MoveDirectory
                if(Component.TrimStart().ToLower().Split(' ')[0] == "cd")
                {
                    string DirectoryMoveTo = "";
                    try
                    {
                        DirectoryMoveTo = Component.TrimStart().ToLower().Split(' ')[1];
                    }
                    catch
                    {
                        Console.WriteLine("Please Apply Name Of Directory");
                    }
                    try
                    {
                        if (Directory.Exists(directory.FullName + "\\" + DirectoryMoveTo))
                        {
                            cd_changes.Push(directory.FullName);
                            directory = new DirectoryInfo(directory.FullName + "\\" + DirectoryMoveTo);
                        }
                        else
                        {
                            Console.WriteLine("Directory Not Exists");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Access Denied");
                    }
                }
                #endregion
                #region MoveDirectoryBack
                if (Component.Trim().ToLower() == "cd..")
                {
                    if (cd_changes.Count() > 0)
                    {
                        directory = new DirectoryInfo(cd_changes.Pop().ToString());
                    }
                }
                #endregion
                #region Exit
                if(Component.ToLower() == "exit")
                {
                    Environment.Exit(1);
                }
                #endregion
                #region MakeDirectory
                if(Component.StartsWith("mkdir "))
                {
                    string dir_name_to_create = Component.Split(' ')[1];
                    if(Directory.Exists(directory.FullName+"\\"+dir_name_to_create) == false)
                    {
                        Directory.CreateDirectory(directory.FullName + "\\" + dir_name_to_create);
                    }
                    else
                    {
                        Console.WriteLine("Directory Already Exists");
                    }
                }
                #endregion
                #region RemoveFile
                if(Component.Split(' ')[0].ToLower() == "del")
                {
                    string file_to_delete = Component.ToLower().Split(' ')[1];
                    if(File.Exists(directory.FullName+"\\"+file_to_delete))
                    {
                        File.Delete(directory.FullName + "\\" + file_to_delete);
                    }
                    else
                    {
                        Console.WriteLine("File Not Found");
                    }
                }
                #endregion
                #region RemoveDirectory
                if(Component.ToLower().Split(' ')[0] == "rmdir")
                {
                    string dir_to_remove = Component.ToLower().Split(' ')[1];
                    if(Directory.Exists(directory.FullName +"\\"+dir_to_remove))
                    {
                        Directory.Delete(directory.FullName + "\\" + dir_to_remove);
                    }
                    else
                    {
                        Console.WriteLine("Directory Not Found");
                    }
                }
                #endregion

            }

        }
    }
}
