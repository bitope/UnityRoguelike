using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace UnityRoguelike
{
    [Serializable]
    public class Actor : INotifyPropertyChanged
    {
        private int _currentHp;
        private int _maxHp;
        private Vec _position;

        private Item[] inventory;
        private Item[] equippedItems;

        public event PropertyChangedEventHandler PropertyChanged;
        public string Name;

        public Actor()
        {
            inventory = new Item[50];
            equippedItems = new Item[8];
            // 0-49 är inventory.
            // 50-57 är equipped.
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public int CurrentHp
        {
            get { return _currentHp; }
            set
            {
                if (value != _currentHp)
                {
                    _currentHp = value;
                    OnPropertyChanged("CurrentHp");                    
                }
            }
        }

        public int MaxHp
        {
            get { return _maxHp; }
            set
            {
                if (value != _maxHp)
                {
                    _maxHp = value;
                    OnPropertyChanged("MaxHp");                    
                }
            }
        }

        public Vec Position
        {
            get { return _position; }
            set
            {
                if (value != _position)
                {
                    _position = value;
                    OnPropertyChanged("Position");
                }
            }
        }

        public Vec NextPosition { get; set; }

        public int FindEmptyInventorySlot()
        {
            for (int i = 0; i < 50; i++)
            {
                if (inventory[i]!=null)
                    continue;
                return i;
            }

            return -1;
        }

        public bool PickupItem(UnityEngine.GameObject o)
        {
            var ic = o.GetComponent<ItemController>();
            if (ic == null)
                return false;

            var slot = FindEmptyInventorySlot();
            if (slot==-1)
                return false;

            inventory[slot] = ic.Item;
            OnPropertyChanged("Inventory");
            return true;
        }

        public void SetInventory(int id, Item value)
        {
            if (id<50)
                inventory[id] = value;
            else
            {
                equippedItems[id - 50] = value;
            }
            
            //OnPropertyChanged("Inventory");
        }

        public Item GetInventory(int id)
        {
            if (id < 50)
                return inventory[id];
            else
            {
                return equippedItems[id - 50];
            }
        }

        public Item GetInventory(EquipmentSlot id)
        {
            return GetInventory((int)id);
        }

        public void SortInventory()
        {
            Array.Sort(inventory, delegate(Item a, Item b)
            {
                if (a != null)
                    return a.CompareTo(b);
                return 1;
            });

            SignalInventory();
        }

        public void SignalInventory()
        {
            OnPropertyChanged("Inventory");
        }

    }
}