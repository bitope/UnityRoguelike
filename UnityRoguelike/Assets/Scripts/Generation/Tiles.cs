using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

//[NonSerialized]

namespace UnityRoguelike
{
    public enum Tiles
    {
        Floor,
        Wall,
        OpenDoor_NS,
        OpenDoor_EW,
        ClosedDoor_NS,
        ClosedDoor_EW,
        Brazier,
        Pillar
    }

    [Serializable]
    public class Actor : INotifyPropertyChanged
    {
        private int _currentHp;
        private int _maxHp;
        private Vec _position;

        public event PropertyChangedEventHandler PropertyChanged;

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


    }
}
