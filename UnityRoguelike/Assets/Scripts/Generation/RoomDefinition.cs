using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UnityRoguelike
{
    public class RoomDefinition
    {
        public Rect size;
        public int[,] tiles;
        public int chance;

        public RoomDefinition(int width, int height)
        {
            chance = 100;
            size = new Rect(0,0,width,height);
            tiles = new int[width,height];
        }

    }

    public class RoomDecorator
    {
        public List<RoomDefinition> Definitions;
        private Stage stage;
        private MersenneTwister mt;

        public RoomDecorator(Stage stage)
        {
            mt = new MersenneTwister(0);
            this.stage = stage;
            Definitions = new List<RoomDefinition>();
        }

        public void ReadAll(string file)
        {
            var lines = File.ReadAllLines(file);
            ReadAll(lines);
        }

        public void DecorateRoom(object sender, DecorateRoomEventArgs e)
        {
            var r = Definitions.Where(i => i.size.width == e.Room.width && i.size.height == e.Room.height).ToList();

            if (!r.Any())
                return;

            var pick = mt.PickOne(r);
            while (mt.Next(100) > pick.chance)
            {
                pick = mt.PickOne(r);
            }
            
            for (int y = e.Room.top; y < e.Room.bottom; y++)
            {
                for (int x = e.Room.left; x < e.Room.right; x++)
                {
                    char c = (char)pick.tiles[x - e.Room.left, y - e.Room.top];
                    if (c=='.') continue;

                    if (c == 'b') { stage[x, y] = Tiles.Brazier; }
                    if (c == 'p') { stage[x, y] = Tiles.Pillar; }
                    if (c == 'a') { stage[x, y] = Tiles.Altar; }
                    if (c == '{') { stage[x, y] = Tiles.Fountain; }

                }
            }

        }

        public void ReadAll(string[] lines)
        {
            RoomDefinition def = null;
            bool roomActive = false;
            int lx = 0, ly = 0, w = 0, h = 0;
            foreach (var line in lines)
            {
                if (line.StartsWith("BEGIN"))
                {
                    var props = line.Split(' ');
                    w = Int32.Parse(props[1]);
                    h = Int32.Parse(props[2]);
                    int chance = Int32.Parse(props[3]);

                    def = new RoomDefinition(w, h);
                    def.chance = chance;

                    roomActive = true;
                    continue;
                }

                if (line.StartsWith("END"))
                {
                    lx = 0;
                    ly = 0;
                    roomActive = false;
                    Definitions.Add(def);
                    def = null;
                    continue;
                }

                if (roomActive)
                {
                    lx = 0;
                    foreach (var c in line.ToCharArray())
                    {
                        def.tiles[lx, ly] = (int)c;
                        lx++;
                        if (lx == w)
                            break;
                    }
                    ly++;
                    continue;
                }
            }
        }
    }
}