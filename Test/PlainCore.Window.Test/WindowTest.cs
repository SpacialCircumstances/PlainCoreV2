using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace PlainCore.Window.Test
{
    public class WindowTest
    {
        [Fact]
        public void TestCreate()
        {
            //Default window settings
            var window1 = new Window();
            Assert.True(window1.IsOpen);
            window1.Title = "Test";
            window1.Close();
        }

        [Fact]
        public void TestLoop()
        {
            //Should not crash
            var window1 = new Window(640, 480, "Test", true);
            var sw = new Stopwatch();
            sw.Start();
            while (window1.IsOpen)
            {
                if(sw.ElapsedMilliseconds > 2000)
                {
                    window1.Close();
                    break;
                }
                window1.PollEvents();
                window1.Display();
            }
        }
    }
}
