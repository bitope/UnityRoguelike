using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dungeon
{
	public class State
	{
		//private string from;
		public string From { get; private set; }

		//private string to;
		public string To { get; private set; }

		private Func<bool> condition;
		public Func<bool> Condition { get { return condition; } }

		public State (string from, string to, Func<bool> condition)
		{
			From = from;
			To = to;
			this.condition = condition;	
		}


	}

	public class StateMachine
	{
		private List<State> states;
		private string currentState;
		public string Current { get { return currentState; } }

		public Action<string> StateChanged;

		public StateMachine ()
		{
			states = new List<State> ();
			currentState = "Start";	
		}

		public void Add(State state)
		{
			states.Add (state);	
		}

		public bool Transision()
		{
			foreach (var state in states.Where(i=>i.From == currentState | i.From == "Any")) { //
				if (state.Condition()) {
					currentState = state.To;
					OnStateChanged ();
					return true;
				}
			}

			return false;
		}

		protected virtual void OnStateChanged()
		{
			if (StateChanged != null) {
				StateChanged (currentState);
			}
		}

	}
}

