using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;

namespace CopyAs
{
  internal static class Program
  {
    private static void Main(string[] arguments)
    {
      // arguments[0] = username to get data from DB
      // arguments[1] = password  to get data from DB
      // arguments[2] = domain to get data from DB; local domain by default (pc's name)
      // arguments[3] = chemin source
      // arguments[4] = chemin destination
      // arguments[5] = fichiers sources à copier list<string>
      
      string[] userInfo = GetDataFromDb(); // read ini file to get credentials

      List<string> lcrFiles = arguments.ToList();
      if (CreateBatchFile("copyFiles.bat", lcrFiles, userInfo[3]))
      {
        StartProcess(userInfo, "copyFiles.bat", "");
      }
      
      Console.WriteLine("Press anykey to exit:");
      Console.ReadKey();
    }

    private static bool CreateBatchFile(string batchFileName, List<string> filesToBeCopied, string destinationPath )
    {
      bool result = false;
      try
      {
        using (StreamWriter sw = new StreamWriter(batchFileName, false))
        {
          foreach (string file in filesToBeCopied)
          {
            sw.WriteLine($"copy {file} {AddSlash(destinationPath)}{file}");
          }
        }

        result = true;
      }
      catch (Exception exception)
      {
        Console.WriteLine($"There was an error: {exception}");
        result = false;
      }

      return result;
    }

    private static string AddSlash(string filePath)
    {
      return filePath.EndsWith("\\") ? filePath : filePath + "\\";
    }

    private static string[] GetDataFromDb()
    {
      string[] result = new string[4];
      // get credentials from ini file
      // result[0] = User Name
      // result[1] = Password
      // result[2] = Domain
      // result[3] = Export Path
      // add code to get data from DB
      //for now to test we input manually
      result[0] = "username";
      result[1] = "my super password";
      result[2] = "domain_name";
      result[3] = @"\\servername\drive$\directory";
      return result;
    }

    private static void StartProcess(string[] userInfo, string processFileName, string processArgument)
    {
      Process proc = new Process();
      SecureString ssPwd = new SecureString();
      proc.StartInfo.UseShellExecute = false;
      proc.StartInfo.FileName = processFileName; //"filename";
      proc.StartInfo.Arguments = processArgument; // "args...";
      proc.StartInfo.Domain = userInfo[2]; // "domainname";
      proc.StartInfo.UserName = userInfo[0]; //"username";
      string password = userInfo[1]; // password
      foreach (char c in password)
      {
        ssPwd.AppendChar(c);
      }

      password = string.Empty;
      proc.StartInfo.Password = ssPwd;
      proc.Start();
    }
  }
}
