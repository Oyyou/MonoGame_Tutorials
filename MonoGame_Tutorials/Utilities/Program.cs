using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
  class Program
  {
    static void Main(string[] args)
    {
      DeleteSillyFolders();
    }

    private static void DeleteSillyFolders()
    {
      var test = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;

      CheckAndDelete(test);
    }

    private static void CheckAndDelete(string path)
    {
      var foldersToDelete = new List<string>()
      {
        "Bin",
        "Obj",
      };

      var directories = Directory.GetDirectories(path);

      foreach (var directory in directories)
      {
        var directoryName = new DirectoryInfo(directory).Name;

        if (foldersToDelete.Any(c => string.Equals(c, directoryName, StringComparison.CurrentCultureIgnoreCase)))
        {
          try
          {
            Directory.Delete(directory, true);
          }
          catch (UnauthorizedAccessException)
          {
            Console.WriteLine("This is expected");
          }
          catch (Exception e)
          {
            throw new Exception(e.ToString());
          }

          continue;
        }

        CheckAndDelete(directory);
      }
    }
  }
}
