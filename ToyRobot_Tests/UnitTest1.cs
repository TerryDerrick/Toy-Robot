using System.ComponentModel.DataAnnotations;
using Toy_Robot;

namespace ToyRobot_Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        // Unit tests based on examples in README.MD
        [Test]
        public void Test1()
        {
            var expectedOutput = "Output: 0,1,NORTH";
            var msg = string.Empty;

            CancellationTokenSource cts = new();
            CommandProcessor commandProcessor = new(cts);
          
            commandProcessor.ProcessCommand("PLACE 0,0,NORTH");
            commandProcessor.ProcessCommand("MOVE");
            msg = commandProcessor.ProcessCommand("REPORT");
            Assert.That(msg, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void Test2()
        {
            var expectedOutput = "Output: 0,0,WEST";
            var msg = string.Empty;

            CancellationTokenSource cts = new();
            CommandProcessor commandProcessor = new(cts);

            commandProcessor.ProcessCommand("PLACE 0,0,NORTH");
            commandProcessor.ProcessCommand("LEFT");
            msg = commandProcessor.ProcessCommand("REPORT");

            Assert.That(msg, Is.EqualTo(expectedOutput)); ;
        }

        [Test]
        public void Test3()
        {
            var expectedOutput = "Output: 3,3,NORTH";
            var msg = string.Empty;

            CancellationTokenSource cts = new();
            CommandProcessor commandProcessor = new(cts);

            commandProcessor.ProcessCommand("PLACE 1,2,EAST");
            commandProcessor.ProcessCommand("MOVE");
            commandProcessor.ProcessCommand("MOVE");
            commandProcessor.ProcessCommand("LEFT");
            commandProcessor.ProcessCommand("MOVE");
            msg = commandProcessor.ProcessCommand("REPORT");

            Assert.That(msg, Is.EqualTo(expectedOutput));
        }

        // Some Negative Units tests to look for obvious failures.

        [Test]
        public void Test4()
        {
            var expectedOutput = "Initial Placement Position is not on the table";

            CancellationTokenSource cts = new();
            CommandProcessor commandProcessor = new(cts);

            bool exceptionThrown = false;
            try
            {
                commandProcessor.ProcessCommand("PLACE 6,2,EAST");
            }
            catch (Exception ex)
            {
                exceptionThrown = true;
                Assert.That(ex.Message, Is.EqualTo(expectedOutput));
            }

            if (!exceptionThrown)
                Assert.Fail();
            
        }

        [Test]
        public void Test5()
        {
            var expectedOutput = "Invalid MOVE command, Toy Robot would fall of Table and be destroyed";
            var msg = string.Empty;

            CancellationTokenSource cts = new();
            CommandProcessor commandProcessor = new(cts);

            bool exceptionThrown = false;
            try
            {
                commandProcessor.ProcessCommand("PLACE 0,5,NORTH");
                commandProcessor.ProcessCommand("MOVE");
                commandProcessor.ProcessCommand("MOVE");
                msg = commandProcessor.ProcessCommand("REPORT");
            }
            catch (Exception ex)
            {
                exceptionThrown = true;
                Assert.That(ex.Message, Is.EqualTo(expectedOutput));
            }

            if (!exceptionThrown)
                Assert.Fail();
        }
    }
}