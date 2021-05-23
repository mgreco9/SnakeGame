using System;
using CommandLine;

namespace Snake
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        private static string _controllerOpt;

        public class Options
        {
            [Option('c', "control", Required = false, HelpText = "Define which AI to use")]
            public string AIName { get; set; }
        }

        static void RunOptions(Options opts)
        {
            _controllerOpt = opts.AIName;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptions);

            var game = new MainGame()
            {
                controllerOpt = _controllerOpt
            };
            game.Run();
        }
    }
#endif
}
