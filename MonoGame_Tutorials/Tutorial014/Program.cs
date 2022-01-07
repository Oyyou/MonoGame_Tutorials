using System;

namespace Tutorial014
{
  public static class Program
  {
    [STAThread]
    static void Main()
    {
      using (var game = new Game1())
        game.Run();
    }
  }
}

