using System;
using CommandLine;
using CommandLine.Text;
using Snake.Source.Control;
using Snake.Source.Option;

namespace Snake
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        public class Options
        {
            [Option('c', "control", Required = true, HelpText = "Define which AI to use")]
            public ControllerType AIName { get; set; }
        }

        static void RunOptions(Options opts)
        {
            GameOptions.controller = opts.AIName;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Parser parser = new Parser(with =>
            {
                with.CaseInsensitiveEnumValues = true;
            });

            parser.ParseArguments<Options>(args)
                .WithParsed<Options>(options => RunOptions(options));

            var game = new MainGame();
            game.Run();
        }
    }
#endif
}
