using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
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
            string add_in = "";
            while(true)
            {
                ConsoleKey CharInput = Console.ReadKey().Key;
                if (CharInput == ConsoleKey.Enter)
                {
                    Console.SetCursorPosition(0, Console.CursorTop+1);
                    break;
                }
                else if (CharInput == ConsoleKey.Tab) //Show Optional Files
                {
                    Length[1] = add_in.Length;
                    add_in = GetFilesAndDirectories(directory);
                }
                else if (CharInput == ConsoleKey.Backspace)
                {
                    string new_str = "";
                    try
                    {
                        for (int i = 0; i < Sentence.Length - 1; i++)
                        {
                            new_str += Sentence[i];
                        }
                    }
                    catch
                    {

                    }
                    Length[0] = Sentence.Length;
                    Sentence = new_str;
                }
                else
                {
                    if(char.Parse(((char)CharInput).ToString().ToLower())>='a' && char.Parse(((char)CharInput).ToString().ToLower())<='z')
                         Sentence += ((char)CharInput).ToString().ToLower();
                    else
                    {
                        Sentence += (char)CharInput;
                    }
                }
                Console.SetCursorPosition(len, Console.CursorTop);
                ClearLine(len,len+Length[0]+Length[1]);
                Console.SetCursorPosition(len, Console.CursorTop);
                Console.Write(Sentence+add_in);
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
            for (int i = length; i < max_length; i++)
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
    }
}
