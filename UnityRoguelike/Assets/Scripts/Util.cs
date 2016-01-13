using System.Linq;
using UnityEngine;
using UnityRoguelike;

namespace Dungeon
{
    public static class Util
    {
        public static Random rng = new Random();

        public static Vector2 GetTileUvOffset(int tileId)
        {
            int x = tileId%32;
            int y = tileId/32;

            float _x = x/32.0f;
            // subtract height of one tile, to pull the offset to the correct 
            // position. (otherwise you get the top-left corner instead of the bottom-left.)
            float _y = 1.0f - ((y + 1)/32.0f); // -(1/32.0f); 

            return new Vector2(_x, _y);
        }

        public static void FixCubeUv(GameObject cube)
        {
            var mf = cube.GetComponent<MeshFilter>();
            Mesh mesh = null;
            if (mf != null)
                mesh = mf.mesh;

            if (mesh == null || mesh.uv.Length != 24)
            {
                Debug.LogError("This is prob. not a primitive.cube - Aborting.");
                return;
            }

            var uvs = mesh.uv;

            //// Front
            //uvs[0] = new Vector2(0, 0);
            //uvs[1] = new Vector2(1, 0);
            //uvs[2] = new Vector2(0, 1);
            //uvs[3] = new Vector2(1, 1);

            //// Top
            //uvs[8] = new Vector2(0, 0);
            //uvs[9] = new Vector2(1, 0);
            //uvs[4] = new Vector2(0, 1);
            //uvs[5] = new Vector2(1, 1);

            // Back  - Needs to be flipped top 2 bottom. 
            uvs[10] = new Vector2(0, 1);
            uvs[11] = new Vector2(1, 1);
            uvs[6] = new Vector2(0, 0);
            uvs[7] = new Vector2(1, 0);

            //// Bottom
            //uvs[12] = new Vector2(0, 0);
            //uvs[14] = new Vector2(1, 0);
            //uvs[15] = new Vector2(0, 1);
            //uvs[13] = new Vector2(1, 1);

            //// Left
            //uvs[16] = new Vector2(0, 0);
            //uvs[18] = new Vector2(1, 0);
            //uvs[19] = new Vector2(0, 1);
            //uvs[17] = new Vector2(1, 1);

            //// Right        
            //uvs[20] = new Vector2(0, 0);
            //uvs[22] = new Vector2(1, 0);
            //uvs[23] = new Vector2(0, 1);
            //uvs[21] = new Vector2(1, 1);

            mesh.uv = uvs;
        }


        public static bool XChanceInY(int x, int y)
        {
            if (x <= 0)
                return false;
            if (x >= y)
                return true;

            return Random.Range(0, y) < x;
        }

        public static Vec GetVecPosition(Vector3 v)
        {
            return new Vec((int)(v.x + 0.5f), (int)(v.z + 0.5f));
        }

        public static Vector3 Nudge(this Vector3 v, float maxOffset)
        {
            return v+Random.insideUnitSphere*maxOffset;
        }

        public static Vector3 Convert(this Vec v, float height)
        {
            return new Vector3(v.x, height -0.5f , v.y);
        }

        public static bool IsAdjacentTo(this Vec v, Vec other)
        {
            return Direction.cardinal.Select(i => i + v).Any(i => i == other);
        }
    }
}