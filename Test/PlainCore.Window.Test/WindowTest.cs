using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PlainCore.Window.Test
{
    public class WindowTest
    {
        [Fact]
        void TestCreate()
        {
            //Default window settings
            var window1 = new Window();
            Assert.True(window1.IsOpen);
            window1.Title = "Test";
            window1.Close();
        }

        [Fact]
        void TestLoop()
        {
            //Should not crash
            var window1 = new Window(640, 480, "Test", true);
            var counter = 0;
            while (window1.IsOpen)
            {
                counter++;
                if (counter > 50) window1.Close();
                window1.PollEvents();
                window1.Display();
            }
        }
    }
}
