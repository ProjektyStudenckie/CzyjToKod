using System;

namespace CzyjToKod.Model
{
    static class PlagiarismCheck
    {
        public static int TestForPlagiarism(string file1Path, string file2Path)
        {
            Console.WriteLine($"Check Code - {file1Path} {file2Path}");

            Reinterpreter.StartReinterpreting(file1Path, file2Path);

            return new Random().Next(100);
        }
    }
}
