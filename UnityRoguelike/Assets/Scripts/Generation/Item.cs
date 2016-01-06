using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dungeon;
using UnityEngine;
using UnityEngine.Assertions;

namespace UnityRoguelike
{
    public enum EquipmentSlot
    {
        None = 0,

        Head = 50,
        Neck,
        Chest,
        RightHand,
        LeftHand,
        Waist,
        Legs,
        Feet
    }

    [Serializable]
    public class Item
    {
        //[NonSerialized] public UnityEngine.Sprite ItemIcon;

        [SerializeField]
        private string icon;
        public EquipmentSlot Equipmentslot;

        public Item()
        {
        }

        public string Icon
        {
            get { return icon; }
            set
            {
                if (icon != value)
                {
                    icon = value;
                    Assert.IsNotNull(SpriteResourceManager.Get(icon));
                }
            }
        }

        public bool isCursed()
        {
            return false;
        }
    }
}
