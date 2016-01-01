using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityRoguelike
{
    public class DijkstraMap
    {
        private Stage map;

        private int diagonal_cost;
        private int width, height, nodes_max;
        public int[] distances; /* distances grid */
        public int[] nodes; /* the processed nodes */
        private List<Vec> path;

        public DijkstraMap(Stage map, float diagonalCost)
        {
            this.map = map;
            diagonal_cost = (int) ((diagonalCost*100.0f) + 0.1f);
            width = map.width;
            height = map.height;
            nodes_max = map.width*map.height;
            path = new List<Vec>();
        }

        public bool Bounds(int node)
        {
            int x = node%width;
            int y = node/width;

            return x >= 0 && x < width && y >= 0 && y < height;
        }

        public bool Bounds(int x, int y)
        {
            return x >= 0 && x < width && y >= 0 && y < height;
        }

        public int GetDistance(int x, int y)
        {
            if ((y*width + x) >= nodes_max)
                return Int32.MaxValue;

            var d = distances[y*width + x];
            return d;
        }

        public bool CalculatePath(int x, int y)
        {
            path.Clear();
            int[] step = new int[2];
            int px = x, py = y;
            int[] dx = {-1, 0, 1, 0, -1, 1, 1, -1, 0};
            int[] dy = {0, -1, 0, 1, -1, -1, 1, 1, 0};
            int[] xdistances = new int[8];

            int lowest_index;
            int imax = (diagonal_cost == 0 ? 4 : 8);

            if (!Bounds(x, y))
                return false;

//        if (getDistance(x, y)==Integer.MAX_VALUE)
//        	return false; // NOT REACHABLE

            do
            {
                int lowest;
                int i;
                var node = new Vec(px,py);
                path.Insert(0, node);
                for (i = 0; i < imax; i++)
                {
                    int cx = px + dx[i];
                    int cy = py + dy[i];
                    if (cx < width && cy < height && cx >= 0 && cy >= 0)
                        xdistances[i] = (int) GetDistance((int) cx, (int) cy);
                    else xdistances[i] = Int32.MaxValue;
                }
                lowest = (int) GetDistance(px, py);
                lowest_index = 8;
                for (i = 0; i < imax; i++)
                    if (xdistances[i] < lowest)
                    {
                        lowest = xdistances[i];
                        lowest_index = i;
                    }
                px += dx[lowest_index];
                py += dy[lowest_index];
            } while (lowest_index != 8);
            return true;
        }

        public bool CalculatePathInverted(int x, int y)
        {
            float factor = -1.5f;

            path.Clear();
            int[] step = new int[2];
            int px = x, py = y;
            int[] dx = {-1, 0, 1, 0, -1, 1, 1, -1, 0};
            int[] dy = {0, -1, 0, 1, -1, -1, 1, 1, 0};
            int[] xdistances = new int[8];

            int lowest_index;
            int imax = (diagonal_cost == 0 ? 4 : 8);

            if (!Bounds(x, y))
                return false;

//        if (getDistance(x, y)==Integer.MAX_VALUE)
//        	return false; // NOT REACHABLE

            do
            {
                int lowest;
                int i;
                var node = new Vec(px,py);
                path.Insert(0, node);
                for (i = 0; i < imax; i++)
                {
                    int cx = px + dx[i];
                    int cy = py + dy[i];
                    if (cx < width && cy < height && cx >= 0 && cy >= 0)
                        xdistances[i] = (int) (GetDistance((int) cx, (int) cy)*factor);
                    else xdistances[i] = Int32.MaxValue;
                }
                lowest = (int) (GetDistance(px, py)*factor);
                lowest_index = 8;
                for (i = 0; i < imax; i++)
                    if (xdistances[i] < lowest)
                    {
                        lowest = xdistances[i];
                        lowest_index = i;
                    }
                px += dx[lowest_index];
                py += dy[lowest_index];
            } while (lowest_index != 8);

            path.RemoveAt(path.Count-1);
            Debug.Log("---IF U SEE THIS! FIX! ALERT!");
            return true;
        }

        public void invertMap(float amount)
        {
            for (int d = 0; d < distances.Length; d++)
            {
                if (distances[d] == Int32.MaxValue)
                    continue; // DO NOT TOUCH WALLS
                distances[d] = (int) (distances[d]*-amount);
            }
        }

        public Vec GetStep()
        {
            if (path.Count < 1)
                return null;

            var p = path[path.Count - 1];
            path.RemoveAt(path.Count - 1);
            return p;
        }

        public IEnumerable<Vec> GetPath()
        {
            var p = path[path.Count - 1];
            path.RemoveAt(path.Count - 1);
            path.Reverse();
            return path;
        }

        public IEnumerable<Vec> GetReversePath()
        {
            if (path.Count > 1)
            {
                path.RemoveAt(0);
            }
            return path;
        }

        public void Compute(int rootX, int rootY, bool ignoreActors)
        {
            int mx = width;
            int my = height;
            int mmax = nodes_max;
            int root = (rootY*mx) + rootX;
            int index = 0;
            int lastIndex = 1;
            int[] dx = {-1, 0, 1, 0, -1, 1, 1, -1};
            int[] dy = {0, -1, 0, 1, -1, -1, 1, 1};
            int[] dd = {100, 100, 100, 100, diagonal_cost, diagonal_cost, diagonal_cost, diagonal_cost};
            int imax = diagonal_cost == 0 ? 4 : 8;
            distances = new int[mx*my];
            nodes = new int[mx*my];

            for (int i = 0; i < mmax; i++)
                distances[i] = Int32.MaxValue;

            distances[root] = 0;
            nodes[index] = root;

            do
            {
                int x = nodes[index]%mx;
                int y = nodes[index]/mx;
                int i;
                for (i = 0; i < imax; i++)
                {
                    int tx = x + dx[i];
                    int ty = y + dy[i];
                    if (tx < mx && ty < my && tx >= 0 && ty >= 0)
                    {
                        int dt = distances[nodes[index]];
                        int newNode;
                        float userDist = 0.0f;

                        if (dt == Int32.MaxValue) // Fix for MAXINTEGER +1 is large negative.
                            dt = 0;

                        dt += dd[i]; // ADD COST
//					if (ignoreActors && !map.canWalk(tx, ty))
//						dt+=500;	// EXPENSIVE SQUARE

                        newNode = (ty*mx) + tx;
                        if (distances[newNode] > dt)
                        {
                            int j;
                            if (!map.isOpenSpace(tx, ty))
                                continue;

                            distances[newNode] = dt;
                            j = lastIndex - 1;

                            while (j >= 0 && distances[nodes[j]] >= distances[newNode])
                            {
                                if (nodes[j] == newNode)
                                {
                                    int k = j;
                                    while (k <= lastIndex)
                                    {
                                        nodes[k] = nodes[k + 1]; //TODO: OUT OF BOUNDS HERE
                                        k++;
                                    }
                                    lastIndex--;
                                }
                                else
                                    nodes[j + 1] = nodes[j];
                                j--;
                            }
                            lastIndex++;
                            nodes[j + 1] = newNode;
                        }
                    }
                }
            } while (mmax > ++index);
        }

        public void Compute(IEnumerable<Vec> goals, bool ignoreActors)
        {
            int mx = width;
            int my = height;
            int mmax = nodes_max;
            int index = 0;
            int lastIndex = 1;
            int[] dx = {-1, 0, 1, 0, -1, 1, 1, -1};
            int[] dy = {0, -1, 0, 1, -1, -1, 1, 1};
            int[] dd = {100, 100, 100, 100, diagonal_cost, diagonal_cost, diagonal_cost, diagonal_cost};
            int imax = diagonal_cost == 0 ? 4 : 8;
            distances = new int[mx*my];
            nodes = new int[mx*my];
            for (int i = 0; i < mmax; i++)
                distances[i] = Int32.MaxValue;

            foreach (var goal in goals)
            {
                int slot = goal.x + goal.y*mx;
                distances[slot] = 0;
                nodes[index] = slot;
                index++;
                lastIndex++;
            }
            lastIndex = index;
            index = 0;

            do
            {
                int x = nodes[index]%mx;
                int y = nodes[index]/mx;
                int i;
                for (i = 0; i < imax; i++)
                {
                    int tx = x + dx[i];
                    int ty = y + dy[i];
                    if (tx < mx && ty < my && tx >= 0 && ty >= 0)
                    {
                        int dt = distances[nodes[index]];
                        int newNode;
                        float userDist = 0.0f;

                        if (dt == Int32.MaxValue)
                            dt = 0;

                        dt += dd[i];
//					if (ignoreActors && !map.canWalk(tx, ty))
//						dt+=500;	// EXPENSIVE SQUARE
                        newNode = (ty*mx) + tx;
                        if (distances[newNode] > dt)
                        {
                            int j;
                            if (!map.isOpenSpace(tx, ty)) // || (!ignoreActors && !map.canWalk(tx, ty)))
                                continue;

                            distances[newNode] = dt;
                            j = lastIndex - 1;

                            while (j >= 0 && distances[nodes[j]] >= distances[newNode])
                            {
                                if (nodes[j] == newNode)
                                {
                                    int k = j;
                                    while (k <= lastIndex)
                                    {
                                        nodes[k] = nodes[k + 1];
                                        k++;
                                    }
                                    lastIndex--;
                                }
                                else
                                    nodes[j + 1] = nodes[j];
                                j--;
                            }
                            lastIndex++;
                            nodes[j + 1] = newNode;
                        }
                    }
                }
            } while (mmax > ++index);
        }

        public void addxx(DijkstraMap map, float factor)
        {
            for (int i = 0; i < distances.Length; i++)
                if (distances[i] != Int32.MaxValue)
                    distances[i] += (int) (map.distances[i]*factor);
        }

        public void Display()
        {
            for (int y = map.height - 1; y >= 0; y--)
            {
                for (int x = 0; x < map.width; x++)
                {
                    if (distances[(y*map.width) + x] == Int32.MaxValue)
                        Console.Write("  ");
                    else
                    Console.Write((distances[(y*map.width) + x]/100).ToString("D2"));
                }
                Console.Write("\n");
            }
        }
    }
}