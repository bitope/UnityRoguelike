using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityRoguelike;

namespace Dungeon
{

    public static class ItemFactory
    {
        private static Dictionary<string, Item> items;

        private static MersenneTwister mt;

        public static void Initialize(int seed = 0)
        {
            mt=new MersenneTwister(seed);
            items = new Dictionary<string, Item>();

            items.Add("club", new WeaponItem()
            {
                Name = "Club",
                Icon = "club",
                Ammo = MissileType.None,
                Skill = SkillType.MacesFlails,
                damageType = DamageType.Crushing,
                Equipmentslot = EquipmentSlot.RightHand,
                FitSize = SizeType.Small,
                Hit = 3,
                MinDamage = 5,
                MaxDamage = 5,
                Speed = 13,
                Mass = 50,
                StrWeight = 7,
                isThrowable = false
            });
            items.Add("dagger", new WeaponItem()
            {
                Name = "Dagger",
                Icon = "dagger",
                Ammo = MissileType.None,
                Skill = SkillType.ShortBlades,
                damageType = DamageType.Stabbing,
                Equipmentslot = EquipmentSlot.RightHand,
                FitSize = SizeType.Little,
                Hit = 6,
                MinDamage = 4,
                MaxDamage = 4,
                Speed = 10,
                Mass = 20,
                StrWeight = 1,
                isThrowable = true
            });
            items.Add("hand_axe1", new WeaponItem()
            {
                Name = "Hand axe",
                Icon = "hand_axe1",
                Ammo = MissileType.None,
                Skill = SkillType.Axes,
                damageType = DamageType.Chopping,
                Equipmentslot = EquipmentSlot.RightHand,
                FitSize = SizeType.Little,
                Hit = 3,
                MinDamage = 7,
                MaxDamage = 7,
                Speed = 13,
                Mass = 80,
                StrWeight = 6,
                isThrowable = true
            });
            items.Add("long_sword1", new WeaponItem()
            {
                Name = "Long sword",
                Icon = "long_sword1",
                Ammo = MissileType.None,
                Skill = SkillType.LongBlades,
                damageType = DamageType.Slashing,
                Equipmentslot = EquipmentSlot.RightHand,
                FitSize = SizeType.Medium,
                Hit = 1,
                MinDamage = 10,
                MaxDamage = 10,
                Speed = 14,
                Mass = 160,
                StrWeight = 3,
                isThrowable = false
            });
            items.Add("executioner_axe1", new WeaponItem()
            {
                Name = "Executioner's axe",
                Icon = "executioner_axe1",
                Ammo = MissileType.None,
                Skill = SkillType.Axes,
                damageType = DamageType.Chopping,
                Equipmentslot = EquipmentSlot.RightHand,
                FitSize = SizeType.Large,
                Hit = -6,
                MinDamage = 20,
                MaxDamage = 20,
                Speed = 20,
                Mass = 280,
                StrWeight = 9,
                isThrowable = false
            });

        }

        public static Item GetRandom()
        {
            return mt.PickOne(items.Values);
        }
    }
}