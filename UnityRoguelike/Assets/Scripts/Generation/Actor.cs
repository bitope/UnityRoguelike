using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public event PropertyChangedEventHandler PropertyChanged;

        public Actor()
        {
            inventory = new Item[58];
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
            inventory[id] = value;
            //OnPropertyChanged("Inventory");
        }

        public Item GetInventory(int id)
        {
            return inventory[id];
        }

        public Item GetInventory(EquipmentSlot id)
        {
            return inventory[(int)id];
        }

        public void SignalInventory()
        {
            OnPropertyChanged("Inventory");
        }

    }
}