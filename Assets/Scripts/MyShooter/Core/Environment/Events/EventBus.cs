using System;
using System.Collections.Generic;

namespace MyShooter.Core.Environment.Events
{
	public class EventBus
	{
		private Dictionary<Type, IGameEvent> _typesToEventsMap = new Dictionary<Type, IGameEvent>();

		public TEventType GetEvent<TEventType>()
			where TEventType : IGameEvent, new()
		{
			var type = typeof(TEventType);

			if(!_typesToEventsMap.ContainsKey(type))
			{
				var newEvent = new TEventType();
				_typesToEventsMap.Add(type, newEvent);
			}

			return (TEventType)_typesToEventsMap[type];
		}
	}
}
