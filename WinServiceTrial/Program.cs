﻿using System;
using System.Timers;
using Topshelf;

namespace WinServiceTrial
{
    class Program
    {
        static void Main(string[] args)
        {

            var rc = HostFactory.Run(x =>                                   //1
            {
                x.Service<TownCrier>(s =>                                   //2
                {
                    s.ConstructUsing(name => new TownCrier());                //3
                    s.WhenStarted(tc => tc.Start());                         //4
                    s.WhenStopped(tc => tc.Stop());                          //5
                });
                x.RunAsLocalSystem();                                       //6

                x.SetDescription("Sample Topshelf Host");                   //7
                x.SetDisplayName("Stuff");                                  //8
                x.SetServiceName("AStuff");                                  //9
            });                                                             //10

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());  //11
            Environment.ExitCode = exitCode;
        }
    }


    public class TownCrier
    {
        readonly Timer _timer;
        public TownCrier()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => Console.WriteLine("It is {0} and all is well", DateTime.Now);
        }
        public void Start() { _timer.Start(); }
        public void Stop() { _timer.Stop(); }
    }
}
