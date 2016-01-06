using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dungeon;
using UnityEngine;
using UnityEngine.Assertions;

namespace UnityRoguelike
{
    public enum ItemType
    {
        Undefined = 0,
        Food,
        Potion,
        Scroll,
        Weapon,
        Armor,
        MagicDevice
    }

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

    public enum SkillType
    {
        None,
        MacesFlails,
        ShortBlades,
        LongBlades,
        Axes,
        Polearms,
        Staves,
        Throwing,
        Slings,
        Crossbows,
        Bows
    }

    public enum MissileType
    {
        None,
        Needle,
        Stone,
        Dart,
        Arrow,
        Bolt,
        LargeRock,
        SlingBullet,
        Javelin,
        ThrowingNet
    }

    public enum SizeType
    {
        Undefined,
        Tiny,
        Little,
        Small,
        Medium,
        Large,
        Big,
        Giant
    }

    public enum DamageType
    {
        Undefined,
        NonMelee,
        Slicing,
        Slashing,
        Crushing,
        Chopping,
        Piercing,
        Stabbing,
    }

    [Serializable]
    public class Item: IComparer<Item>, IComparable<Item>
    {
        [SerializeField]
        private string icon;

        public string Name;
        public ItemType ItemType;
        public EquipmentSlot Equipmentslot;

        public int Mass;

        public Item()
        {
            Name = "Not set.";
            Mass = 1;
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

        public int Compare(Item a, Item b)
        {
            if (a == null)
                return 1;
            if (b == null)
                return -1;

            if (a.ItemType!=b.ItemType)
                return a.ItemType.CompareTo(b.ItemType);

            if (a.Name!=b.Name)
                return System.String.Compare(a.Name, b.Name, System.StringComparison.Ordinal);

            return a.GetHashCode().CompareTo(b.GetHashCode());
        }

        public int CompareTo(Item other)
        {
            return Compare(this, other);
        }
    }

    [Serializable]
    public class WeaponItem : Item
    {
        public int MinDamage;
        public int MaxDamage;

        public int Hit;
        public int Speed;
        public int StrWeight;

        public SkillType Skill;
        //HandsReqdType hands;
        public SizeType FitSize;     // actual size is one size smaller
        public MissileType Ammo;         // MI_NONE for non-launchers
        public bool isThrowable;

        public DamageType damageType;

        public WeaponItem():base()
        {
            ItemType = ItemType.Weapon;
        }
    }
}
