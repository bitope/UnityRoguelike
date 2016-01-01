using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityRoguelike;

namespace UnityRoguelikeConsole
{
    class Program
    {
        private static Stage stage;

        static void Main(string[] args)
        {
            Rect a = new Rect(0, 0, 3, 3);
            Console.WriteLine(a.Trace().Count());

            int dungeon = 0;
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                MakeDungeon(dungeon++);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("TheStage.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, stage);
            stream.Close();
        }

        static void MakeDungeon(int seed)
        {
            stage = new Stage(23, 23);

            RoomDecorator rd = new RoomDecorator(stage);
            rd.ReadAll("rooms.txt");
            Generator g = new Generator(seed);

            g.DecorateRoom += rd.DecorateRoom;

            g.numRoomTries = 500;
            g.generate(stage);


            DijkstraMap dm = new DijkstraMap(stage,0);
            //dm.Compute(1,1,true);
            dm.Compute(stage.GetAll(Tiles.Brazier),false);
            dm.Display();

            dm.CalculatePath(21, 21);
            var path = dm.GetReversePath().ToList();

            Console.WriteLine("Seed: "+seed);

            int p = 0;
            for (int y = stage.height-1; y >=0; y--)
            {
                for (int x = 0; x < stage.width; x++)
                {
                    if (path.Contains(new Vec(x, y)))
                    {
                        Console.Write(path.IndexOf(new Vec(x,y)).ToString("D2"));
                        continue;
                    }
                    //Console.Write(stage[x,y].ToString().Substring(0,1));
                    switch (stage[x, y])
                    {
                        case Tiles.Floor:
                            Console.Write(" .");
                            break;
                        case Tiles.Wall:
                            Console.Write(" #");
                            break;
                        case Tiles.OpenDoor_NS:
                        case Tiles.OpenDoor_EW:
                            Console.Write("  ");
                            break;
                        case Tiles.ClosedDoor_EW:
                            Console.Write("||");
                            break;
                        case Tiles.ClosedDoor_NS:
                            Console.Write("==");
                            break;
                        case Tiles.Brazier:
                            Console.Write(" b");
                            break;
                        case Tiles.Pillar:
                            Console.Write(" p");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                Console.Write('\n');
            }
        }

        static void Temp_generate_floors()
        {
            var template = "public static int[] {0} = new[] {{ {1} }};";

            var lines = File.ReadAllLines("floortiles-list-2.txt");
            var d = new Dictionary<string, int>();

            int linecounter = 0;
            string lastline = lines[0];
            var n = new List<string>();
            var output = new List<string>();

            foreach (var line in lines)
            {
                var s = line.Trim();

                if (s == lastline)
                {
                    n.Add(linecounter.ToString());
                }
                else
                {
                    output.Add(string.Format(template, "f_" + lastline, String.Join(",", n.ToArray())));
                    n = new List<string>();
                    lastline = s;
                    n.Add(linecounter.ToString());
                }

                linecounter++;
            }

            File.WriteAllLines("output.txt", output.ToArray());

        }

        static void Temp_generate_walls()
        {
            var template = "public static int[] {0} = new[] {{ {1} }};";

            var lines = File.ReadAllLines("walltiles-list.txt");
            var d = new Dictionary<string, int>();

            int linecounter = 0;
            string lastline = lines[0];
            var n = new List<string>();
            var output = new List<string>();

            foreach (var line in lines)
            {
                var s = line.Trim();

                if (s == lastline)
                {
                    n.Add(linecounter.ToString());
                }
                else
                {
                    output.Add(string.Format(template, "w_" + lastline, String.Join(",", n.ToArray())));
                    n = new List<string>();
                    lastline = s;
                    n.Add(linecounter.ToString());
                }

                linecounter++;
            }

            File.WriteAllLines("output_w.txt", output.ToArray());

        }

    }
}
