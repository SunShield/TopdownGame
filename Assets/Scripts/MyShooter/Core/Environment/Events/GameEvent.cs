using System;
using System.Collections.Generic;

namespace MyShooter.Core.Environment.Events
{
	public class GameEvent<TArgs> : IGameEvent
		where TArgs : GameEventArgs
	{
		private HashSet<Action<TArgs>> _globalSubscribedActions = new HashSet<Action<TArgs>>();
		private Dictionary<int, HashSet<Action<TArgs>>> _idSubscriptions = new Dictionary<int, HashSet<Action<TArgs>>>();

		public void SubscribeForGlobal(Action<TArgs> action)
		{
			_globalSubscribedActions.Add(action);
		}

		public void UnsubscribeFromGlobal(Action<TArgs> action)
		{
			_globalSubscribedActions.Remove(action);
		}

		public void SubscribeForId(int id, Action<TArgs> action)
		{
			if (!_idSubscriptions.ContainsKey(id))
				_idSubscriptions.Add(id, new HashSet<Action<TArgs>>());

			_idSubscriptions[id].Add(action);
		}

		public void UnsubscribeFromId(int id, Action<TArgs> action)
		{
			if (!_idSubscriptions.ContainsKey(id)) return;

			_idSubscriptions[id].Remove(action);
		}

		public void InvokeForGlobal(TArgs args)
		{
			// TODO: Find more solid approach able to properly handle situations where subscribers are being destroyed during iteration through collection.
			foreach(var subscribedAction in _globalSubscribedActions)
			{
				subscribedAction.Invoke(args);
			}
		}

		public void InvokeForId(int id, TArgs args)
		{
			if (!_idSubscriptions.ContainsKey(id))
			{
				return;
			}

			var actions = _idSubscriptions[id];
			foreach (var subscribedAction in actions)
			{
				subscribedAction.Invoke(args);
			}
		}
	}
}
