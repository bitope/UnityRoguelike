using System.Collections.Generic;
using UnityEngine;

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
}