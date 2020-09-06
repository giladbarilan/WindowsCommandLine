using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp28
{
    class Program
    {
        public static DirectoryInfo directory;
        public static Regex regax = new Regex(@"\w:");
        public static void Main(string[] args)
        {
            #region SetUpEnviroment
            Console.Clear();
            Console.WriteLine("Sample cmd Program");
            Console.WriteLine("Note: Not all cmd components included");
            Console.WriteLine("To Get All Components Please Write help");
            Console.Title = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            #endregion
            regax = new Regex(@"\w:"); //Search for driver change
            var regax_open_file = new Regex(@"\w+\.\w+"); //open file stream
            var save_drivers_path = new Dictionary<string, string>();
            var components = Compodents.components;
            var explaination_to_component = Compodents.explaination_to_component;
            directory = new DirectoryInfo("C:\\"); // Cover dir change;
            Dictionary<string,Stack<string>> multidriver_cd = new Dictionary<string,Stack<string>>();


            while(true)
            {
                Console.Write(directory.FullName+">"); // Get Directory Place
                string Component = @Console.ReadLine();
                if(Component == "")
                {
                    continue; //Print The path again
                }
                #region OpenFile
                var ismatch = regax_open_file.IsMatch(Component);
                if(ismatch)
                {
                    var match_open_file = regax_open_file.Match(Component);
                    if(Component.Trim() == match_open_file.ToString()) //want to open file
                    {
                        try
                        {
                            Process.Start(@"cmd.exe", @"/"+@directory.FullName[0]+" "+@directory.FullName+@"\"+@Component);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    continue;
                }
                #endregion
                #region Change Driver
                var match = regax.IsMatch(Component);
                var syntax_match = regax.Match(Component);
                if (match == true && syntax_match.ToString() == Component.Trim())
                {
                    string matching;
                    Component = Component.Trim();
                    try
                    {
                        var driver_match = regax.Match(directory.FullName); //Get Current Driver
                        matching = driver_match.ToString();
                        if (save_drivers_path.ContainsKey(driver_match.ToString().ToUpper()) == false)
                        {
                            save_drivers_path.Add(driver_match.ToString().ToUpper(), directory.FullName);
                        }
                        else
                        {
                            save_drivers_path[driver_match.ToString().ToUpper()] = directory.FullName; //Set in the dictonary the current path on it
                        }
                        try
                        {
                            var dir = new DirectoryInfo(@Component + "\\");
                            dir.GetFiles(); //raise an error if driver not found.
                        }
                        catch
                        {
                            Console.WriteLine("No Driver Found");
                            continue;
                        }
                        if(save_drivers_path.ContainsKey(Component.ToUpper()) == false)
                        {
                            save_drivers_path.Add(Component.ToUpper(), Component.ToUpper());
                        }
                        directory = new DirectoryInfo(@save_drivers_path[Component.ToUpper()] +"\\");
                        if(multidriver_cd.ContainsKey(matching) == false)
                        {
                            multidriver_cd.Add(matching, new Stack<string>());
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Access Denied" + " "+e.Message);
                        continue;
                    }                   
                    continue;
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
                #region TypeCompodent
                if (Component.ToLower().StartsWith("type "))
                {
                    string file_name = Component.Split(' ')[1];
                    DirectorysAndFiles.TypeCompodent(file_name, directory.FullName);
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
                    string[] component_spliter;               
                    component_spliter = Component.Split(">>");//seperated to ECHO, file>>file.txt
                    if(component_spliter.Length == 1)
                    {
                        component_spliter = Component.Split(' ');
                        for (int i = 0; i < component_spliter.Length; i++)
                        {
                            if(i!=0)
                            {
                                Console.Write(component_spliter[i]+" ");
                            }
                        }
                        Console.WriteLine();
                        continue;
                    }
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
                if (Component.ToLower() == "help")
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
                if(Component.TrimStart().ToLower().Split(' ')[0] == "cd" || Component.TrimStart().ToLower().Split(' ')[0] == "chdir")
                {
                    string DirectoryMoveTo = "";
                    try
                    {
                        DirectoryMoveTo = Component.TrimStart().Split(' ')[1];
                    }
                    catch
                    {
                        Console.WriteLine("Please Apply Name Of Directory");
                    }
                    try
                    {
                        if (Directory.Exists(directory.FullName + "\\" + DirectoryMoveTo))
                        {
                            if(multidriver_cd.ContainsKey(regax.Match(directory.FullName).ToString()) == false)
                            {
                                multidriver_cd.Add(regax.Match(directory.FullName).ToString(), new Stack<string>());
                            }
                            multidriver_cd[regax.Match(directory.FullName).ToString()].Push(directory.FullName);
                            directory = new DirectoryInfo(NoGetNeededFunctions.FixDirectorySizes(DirectoryMoveTo,directory));
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
                    if (multidriver_cd[regax.Match(directory.FullName).ToString()].Count() > 0)
                    {
                        try
                        {
                            directory = new DirectoryInfo(multidriver_cd[regax.Match(directory.FullName).ToString()].Pop());
                        }
                        catch
                        {

                        }
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
                        Directory.Delete(directory.FullName + "\\" + dir_to_remove,true);
                    }
                    else
                    {
                        Console.WriteLine("Directory Not Found");
                    }
                }
                #endregion
                #region Copy
                if(Component.Split(' ')[0].ToLower() == "copy")
                {
                    try
                    {
                        NoGetNeededFunctions.CopyFile(@directory.FullName + @"\" + @Component.Split(' ')[1], @Component.Split(' ')[2]);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    continue;
                }
                #endregion
                #region XCopy
                if (Component.Split(' ')[0].ToLower() == "xcopy")
                {
                    try
                    {
                        NoGetNeededFunctions.CopyFile(@directory.FullName + @"\" + @Component.Split(' ')[1], @Component.Split(' ')[2]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    try
                    {
                        File.Delete(@directory.FullName + @"\" + @Component.Split(' ')[1]);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    continue;
                }

                #endregion

            }

        }
    }
}
