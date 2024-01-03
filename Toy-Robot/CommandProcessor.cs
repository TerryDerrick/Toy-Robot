using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Toy_Robot
{
    public class CommandProcessor
    {
        public ToyRobot toyRobot = null;
        private readonly CancellationTokenSource cancellationTokenSource;

        public enum Commands
        {
            HELP,
            EXIT,
            PLACE,
            MOVE,
            LEFT,
            RIGHT,
            REPORT,

        }

        public CommandProcessor(CancellationTokenSource cts)
        {
            cancellationTokenSource = cts;
        }

        public string ProcessCommand(string command)
        {
            if (command == null || string.IsNullOrWhiteSpace(command))
            {
                throw new ArgumentException("Invalid Command");
            }

            var commandArgs = command.Split(' ');
            var commandSubArgs = Array.Empty<string>();

            if (commandArgs.Length > 1)
            {
                commandSubArgs = commandArgs[1].Split(",");
            }

            if (!Enum.TryParse(commandArgs[0], out Commands enumCommand))
            {
                throw new Exception("Invalid Command");
            }

            switch (enumCommand)
            {
                case Commands.HELP:
                    //TODO add help information
                    break;

                case Commands.EXIT:
                    cancellationTokenSource.Cancel();
                    return null;

                case Commands.PLACE:
                    toyRobot = new ToyRobot(commandSubArgs[0], commandSubArgs[1], commandSubArgs[2]);
                    break;

                case Commands.MOVE:
                    if (toyRobot == null)
                        throw new Exception("Cannot issue MOVE command to a Toy Robot who has not been placed.");

                    toyRobot.MoveRobot();
                    break;

                case Commands.LEFT:
                    if (toyRobot == null)
                        throw new Exception("Cannot issue LEFT command to a Toy Robot who has not been placed.");

                    toyRobot.RotateRobot("LEFT");
                    break;

                case Commands.RIGHT:
                    if (toyRobot == null)
                        throw new Exception("Cannot issue RIGHT command to a Toy Robot who has not been placed.");

                    toyRobot.RotateRobot("RIGHT");
                    break;

                case Commands.REPORT:
                    if (toyRobot == null)
                        throw new Exception("Cannot issue REPORT command to a Toy Robot who has not been placed.");

                    return toyRobot.Report();
            }

            return null;
        }
    }
}
