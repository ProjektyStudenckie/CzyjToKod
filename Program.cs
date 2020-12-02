﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InzynieriaOprogramowania
{
    class Program
    {
        static void Main(string[] args)
        {
            // files to be re-intrerpreted
            string path1 = @"txt1.txt";
            string path2 = @"txt2.txt";

            ReadFiles(path1, path2);
        }

        static private void ReadFiles(string path1, string path2)
        {
            string[] file1 = File.ReadLines(path1).ToArray();
            string[] file2 = File.ReadLines(path2).ToArray();

            SetupFile(file1, file2);
        }

        static private void SetupFile(string[] file1, string[] file2)
        {
            // Trim all lines from whites spaces
            for (int i = 0; i < file1.Length; i++)
                file1[i] = file1[i].Trim();

            for (int i = 0; i < file2.Length; i++)
                file2[i] = file2[i].Trim();

            WriteFiles(ArrangeFile(file1.ToList()).ToArray(), ArrangeFile(file2.ToList()).ToArray());
        }

        static private List<string> ArrangeFile(List<string> file)
        {
            string[] variables = { "string", "int", "float", "char", "bool" };
            int varCounter = 0;

            // delete comments
            for (int i = 0; i < file.Count; i++)
            {
                if (file[i].Contains("//"))
                    file.RemoveAt(i);
            }

            // todo funckje 
            // todo iteracje zmiennych

            // find and replace variables' names
            for (int i = 0; i < file.Count; i++)
            {
                for (int j = 0; j < variables.Length; j++)
                {
                    if (file[i].Contains(variables[j]))
                    {
                        string replacableString = "";
                        file[i] = file[i].Replace(variables[j], "");

                        foreach (char c in file[i])
                        {
                            if (c.Equals('='))
                                break;
                            else
                                replacableString += c;
                        }

                        replacableString = replacableString.TrimStart();
                        file[i] = file[i].Replace(replacableString, $"{variables[j]} zmienna{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(variables[j].ToLower())}{varCounter} ").Trim();

                        varCounter += 1;
                        break;
                    }
                }
            }


            for (int i = 0; i < file.Count; i++)
            {
                // delete empty lines
                if (file[i].Equals("") || file[i].Equals(null))
                    file.RemoveAt(i);
            }

            return file;
        }

        static private void WriteFiles(string[] file1, string[] file2)
        {

            // Create new directory and files
            string dir = Path.Combine(Directory.GetCurrentDirectory(), "ReInterpretedFiles");
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            string pathNewString1 = Path.Combine(dir, "reinterpreted_file_1.txt");
            string pathNewString2 = Path.Combine(dir, "reinterpreted_file_2.txt");

            // Write strings to files
            File.WriteAllLines(pathNewString1, file1);
            File.WriteAllLines(pathNewString2, file2);
        }
    }
}
