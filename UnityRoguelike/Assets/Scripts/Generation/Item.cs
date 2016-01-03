﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dungeon;
using UnityEngine;

namespace UnityRoguelike
{
    public enum EquipmentSlot
    {
        None,

        Head,
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
        [NonSerialized] public UnityEngine.Sprite ItemIcon;

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
                    SetItemIcon();
                }
            }
        }

        public void SetItemIcon()
        {
            ItemIcon = SpriteResourceManager.Get(icon);
        }
    }
}
