using System;
using FluentBehaviourTree;
using System.Threading;

namespace Tes_Behaviour_Tree_Fluent
{
    class Program
    {
        static void Main(string[] args)
        {
            IBehaviourTreeNode B;
            BehaviourTreeBuilder BT = new BehaviourTreeBuilder();

            int robot1 = 0,
                robot2 = 1,
                robot3 = 1;

            string commandStop = "s",
                commandStart = " ";

            B = BT
                .Sequence("Root")
                    #region Cek Koneksi Robot
                    .Selector("Check Robot Striker Connection")
                        .Do("Robot 1 & Robot 2", t =>
                         {
                             if (robot1 == 1 && robot2 == 1)
                             {
                                 Console.WriteLine("Robot 1 & 2 Menyala");
                                 return BehaviourTreeStatus.Success;
                             }
                             else
                             {
                                 Console.WriteLine("");
                                 return BehaviourTreeStatus.Failure;
                             }
                         })
                        .Do("Robot 1 Only", t =>
                        {
                            if (robot1 == 1 && robot2 == 0)
                            {
                                Console.WriteLine("Robot 1 Menyala");
                                return BehaviourTreeStatus.Success;
                            }
                            else
                            {
                                Console.WriteLine("");
                                return BehaviourTreeStatus.Failure;
                            }
                        })
                        .Do("Robot 2 Only", t =>
                        {
                            if (robot1 == 0 && robot2 == 1)
                            {
                                Console.WriteLine("Robot 2 Menyala");
                                return BehaviourTreeStatus.Success;
                            }
                            else
                            {
                                Console.WriteLine("");
                                return BehaviourTreeStatus.Failure;
                            }
                        })
                    .End()
                    .Selector("Check Robot Keeper Connection")
                        .Do("Robot On", t =>
                        {
                            if (robot3 == 1)
                            {
                                Console.WriteLine("Robot Kiper Menyala");
                                return BehaviourTreeStatus.Success;
                            }
                            else
                            {
                                Console.WriteLine("");
                                return BehaviourTreeStatus.Failure;
                            }
                        })
                    .End()
            #endregion
                    #region Strategi
                    .Selector("Strategi")
                        #region Perintah Referee
                        .Sequence("Command Stop")
                            .Do("Check Command", t =>
                            {
                                if (commandStop == "s")
                                {
                                    Console.WriteLine("Masuk Perintah Stop");
                                    return BehaviourTreeStatus.Success;
                                }
                                else return BehaviourTreeStatus.Failure;
                            })
                            .Do("Execute Command",t=>
                            {
                                return BehaviourTreeStatus.Success;
                            })
                        .End()
                        .Sequence("Command Start")
                            .Do("Check Command", t =>
                            {
                                if (commandStart == "S")
                                {
                                    Console.WriteLine("Masuk Perintah Start");
                                    return BehaviourTreeStatus.Success;
                                }
                                else return BehaviourTreeStatus.Failure;
                            })
                            .Do("Execute Command", t =>
                            {
                                return BehaviourTreeStatus.Success;
                            })
                        .End()
                        .Sequence("Command ")
                        #endregion
                    .End()
                    #endregion
                .End()
                .Build();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Robot 1 = " + robot1);
                Console.WriteLine("Robot 2 = " + robot2);
                Console.WriteLine("Robot 3 = " + robot3);

                Console.WriteLine("Stop = " + commandStop);
                Console.WriteLine("Start = " + commandStart );

                Console.WriteLine(Environment.NewLine + DateTime.Now);
                B.Tick(new TimeData());
                Thread.Sleep(20);
            }
        }
    }
}
