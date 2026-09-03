using System;

namespace opengl
{
    public static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (var game = new Project.Game1(args))
                game.Run();
        }
    }
}
