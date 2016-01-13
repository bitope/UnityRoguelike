using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dungeon
{
    public class State
    {
        public string From { get; private set; }
        public string To { get; private set; }
        public Func<bool> Condition { get; private set; }

        public State(string from, string to, Func<bool> condition)
        {
            From = from;
            To = to;
            Condition = condition;
        }
    }

    public class StateMachine
    {
        private List<State> states;
        public string Current { get; private set; }
        public Action<string> StateChanged;

        public StateMachine()
        {
            states = new List<State>();
            Current = "Start";
        }

        public void Add(State state)
        {
            states.Add(state);
        }

        public bool Transision()
        {
            foreach (var state in states.Where(i => i.From == Current | (i.From == "Any" && i.To != Current)))
            {
                if (state.Condition())
                {
                    Current = state.To;
                    OnStateChanged();
                    return true;
                }
            }

            return false;
        }

        protected virtual void OnStateChanged()
        {
            if (StateChanged != null)
            {
                StateChanged(Current);
            }
        }

    }
}

