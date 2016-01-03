using System.Collections.Generic;
using UnityEngine;
using UnityRoguelike;

namespace Dungeon
{
    public static class SpriteResourceManager
    {
        public static Dictionary<string, Sprite> _sprites;

        public static void Initialize()
        {
            _sprites = new Dictionary<string, Sprite>();
            
            //BUG: Unity randomly refuses to find all sprites if not force-loaded.
            Resources.LoadAll("", typeof(Sprite));

            var allSprites = Resources.FindObjectsOfTypeAll<Sprite>();
            foreach (var sprite in allSprites)
            {
                Debug.Log(sprite.name);
                _sprites.Add(sprite.name, sprite);
            }
        }

        public static Sprite Get(string sprite)
        {
            if (_sprites.ContainsKey(sprite))
                return _sprites[sprite];

            return _sprites["unknown"];
        }
    }

    public static class Util
    {
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

            return UnityEngine.Random.Range(0, y) < x;
        }

        public static Vec GetVecPosition(Vector3 v)
        {
            return new Vec((int)(v.x + 0.5f), (int)(v.z + 0.5f));
        }
    }
}