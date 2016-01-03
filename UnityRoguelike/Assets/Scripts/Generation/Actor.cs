﻿using System;
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
            inventory = new Item[50];
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

        public Item[] Inventory
        {
            get { return inventory; }
        }

        public int FindEmptyInventorySlot()
        {
            for (int i = 0; i < inventory.Length; i++)
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

            Inventory[slot] = ic.Item;
            OnPropertyChanged("Inventory");
            return true;
        }
    }
}