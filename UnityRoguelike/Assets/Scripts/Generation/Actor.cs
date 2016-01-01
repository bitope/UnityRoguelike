using System;
using System.ComponentModel;

namespace UnityRoguelike
{
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



        public Vec NextPosition { get; set; }
    }
}