using System;
using System.Diagnostics;
using System.Security;

namespace CopyAs
{
  internal static class Program
  {
    private static void Main(string[] arguments)
    {
      string[] userInfo = GetDataFromDb();
      StartProcess(userInfo);
      Console.WriteLine("Press anykey to exit:");
      Console.ReadKey();
    }

    private static string[] GetDataFromDb()
    {
      string[] result = new string[4];
      // add code to get data from DB
      return result;
    }

    private static void StartProcess(string[] userInfo)
    {
      Process proc = new Process();
      SecureString ssPwd = new SecureString();
      proc.StartInfo.UseShellExecute = false;
      proc.StartInfo.FileName = userInfo[0]; //"filename";
      proc.StartInfo.Arguments = userInfo[1]; // "args...";
      proc.StartInfo.Domain = userInfo[2]; // "domainname";
      proc.StartInfo.UserName = userInfo[3]; //"username";
      string password = "user entered password";
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