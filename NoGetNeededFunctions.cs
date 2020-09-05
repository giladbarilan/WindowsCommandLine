using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp28
{
    public class NoGetNeededFunctions
    {
        private static int index = 0; //Place On Directories
        private static int[] Length =
        {
            0,0
        };
        #region Not_Currently_Used
        public static string Input(DirectoryInfo directory, int len)
        {
            string Sentence = "";
            string plugin = "";
            bool FirstBool = false;
            int save_cursor_left = 0;
            while (true)
            {
                ConsoleKey CharInput = Console.ReadKey().Key;
                if (CharInput == ConsoleKey.Enter)
                {
                    break;
                }
                else if (CharInput == ConsoleKey.Backspace || CharInput == ConsoleKey.Delete)
                {
                    try
                    {
                        if(Sentence.Length>0)
                            Sentence = Sentence.Substring(0, Sentence.Length - 1);
                        if (Sentence.Length > 0)
                        {
                            if (save_cursor_left==Console.CursorLeft)
                            {
                                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
                                Console.Write(" ");
                                FirstBool = true;
                            }
                            else
                            {
                                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                                Console.Write(" ");
                            }
                        }
                        else
                        {
                            Console.SetCursorPosition(len-1, Console.CursorTop);
                            Console.Write(" ");
                            Console.SetCursorPosition(len-1, Console.CursorTop);
                        }                       
                    }
                    catch
                    {

                    }
                }
                else if(CharInput == ConsoleKey.Tab)
                {
                    Console.SetCursorPosition(len+Sentence.Length, Console.CursorTop);
                    plugin = GetFilesAndDirectories(directory);
                    Console.Write(" " + plugin);
                }
                else
                {
                    if (Console.CapsLock)
                    {
                        Sentence += (char)CharInput;
                    }
                    else
                    {
                        Sentence += ((char)CharInput).ToString().ToLower();
                    }
                    save_cursor_left = Console.CursorLeft;
                }
            }
            return Sentence;
        }

        private static string GetFilesAndDirectories(DirectoryInfo info)
        {
            string str_to_ret = "";
            if (index >= (info.GetDirectories().Length-1 + info.GetFiles().Length-1)) //No more files/Dir to cover
            {
                index = 0;
                return info.GetDirectories()[index].Name;
            }
            else
            {
                if(index >= info.GetDirectories().Length) //finish with dirs
                {
                    str_to_ret = info.GetFiles()[index].Name;
                    index++;;
                }
                else //return dir
                {
                    str_to_ret = info.GetDirectories()[index].Name;
                    index++;
                }
            }
            return str_to_ret;
        }

        private static void ClearLine(int length, int max_length)
        {
            for (int i = length; i <= max_length; i++)
            {
                Console.Write(' ');
            }
        }
        #endregion

        public static string FixDirectorySizes<T>(string Moving_Direcotry, T directory)
        {
            string FullName = "";
            if(typeof(DirectoryInfo) == typeof(T))
            {
                //Setting Enviroment
                DirectoryInfo dir = null;
                try
                {
                    dir = directory as DirectoryInfo;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                FullName = dir.FullName + "\\";

                //Find the directory that fits to the directory to move to
                foreach(var folder in dir.GetDirectories())
                {
                    if(folder.Name.ToLower() == Moving_Direcotry.ToLower())
                    {
                        FullName += folder.Name;
                        break;
                    }
                }
            }
            return FullName;
        }

        /// <summary>
        /// Copy and paste files.
        /// </summary>
        /// <param name="copy"> The file to copy.</param>
        /// <param name="paste_path">The path to paste.</param>
        public static void CopyFile(string copy, string paste_path)
        {
            FileInfo file = null;
            if(Program.regax.IsMatch(copy) == false)
            {
                @copy = Program.directory + "\\" + copy;
            }
            if(Program.regax.IsMatch(paste_path) == false)
            {
                @paste_path = Program.directory + "\\" + paste_path;
            }
            try
            {
                file = new FileInfo(copy);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            try
            {
                file.CopyTo(paste_path);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
