using MyShooter.Unity.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyShooter.Core.Entities.States.Modified
{
	[Serializable]
	public class ModifiableValue
	{
		[SerializeField] private float _innerValue;

		private SortedDictionary<int, Dictionary<string, ValueModifier>> _additiveModifiersByPriority = new SortedDictionary<int, Dictionary<string, ValueModifier>>();
		private SortedDictionary<int, Dictionary<string, ValueModifier>> _percentModifiersByPriority = new SortedDictionary<int, Dictionary<string, ValueModifier>>();
		private Dictionary<ValueModifierType, SortedDictionary<int, Dictionary<string, ValueModifier>>> _dictionaryByType;
		private SortedSet<int> _allPrioritiesSorted = new SortedSet<int>();

		[SerializeField] protected float _lastCalculatedValue;
#if UNITY_EDITOR
		[SerializeField] protected bool _wasRecalculatedOnce;
#endif
		protected bool _isDirty = true;

		public float FinalValue
		{
			get
			{
				if (_isDirty) Recalculate();
				return _lastCalculatedValue;
			}
		}

		public ModifiableValue()
		{
			_lastCalculatedValue = _innerValue;
			_dictionaryByType = new Dictionary<ValueModifierType, SortedDictionary<int, Dictionary<string, ValueModifier>>>()
			{
				{ ValueModifierType.Additive, _additiveModifiersByPriority },
				{ ValueModifierType.Percent, _percentModifiersByPriority },
			};
		}

		public void AddModifier(string name, float innerValue, int stackId)
		{
			var newModifier = new ValueModifier(name, innerValue, stackId);
			var matchingDictionary = _dictionaryByType[newModifier.Type];

			if (!matchingDictionary.ContainsKey(newModifier.Priority))
			{
				// if we don't event have such a priority, adding it to the all priorities list
				if (!_allPrioritiesSorted.Contains(newModifier.Priority))
					_allPrioritiesSorted.Add(newModifier.Priority);

				// adding new dictionary for priority
				matchingDictionary.Add(newModifier.Priority, new Dictionary<string, ValueModifier>());

				// adding our modifier
				matchingDictionary[newModifier.Priority].Add(name, newModifier);
				_isDirty = true;
			}
			else
				_isDirty = matchingDictionary[newModifier.Priority][name].AddStack(innerValue, stackId);

			if (_isDirty) Recalculate();
		}

		public void RemoveModifier(string name, int stackId)
		{
			var modifierData = ValueModifiersDataObject.Instance.ValueModifiers[name];
			var priority = modifierData.Priority;
			var type = modifierData.Type;
			var matchingDict = _dictionaryByType[type];

			// if modifier was already removed, doing nothing
			if (!matchingDict.ContainsKey(priority) || !matchingDict[priority].ContainsKey(name)) return;

			var modifier = matchingDict[priority][name];
			modifier.RemoveStack(stackId); // removing stack from modifier
			if (modifier.StackAmount == 0)
			{
				matchingDict[priority].Remove(name); // if last stack removed, removing the whole modifier
				if (matchingDict[priority].Count == 0) // if we removed last modifier of a certain priority, removing that priority from dictionary (TODO: Do we need do this?)
					matchingDict.Remove(priority);
			}

			// if no other modifier with certain priority exists, removing priority from priority list
			if (!_dictionaryByType[ValueModifierType.Additive].ContainsKey(priority) ||
				_dictionaryByType[ValueModifierType.Percent].ContainsKey(priority))
				_allPrioritiesSorted.Remove(priority);

			Recalculate();
		}

		private void Recalculate()
		{
			var newValue = _innerValue;
			foreach(var priority in _allPrioritiesSorted)
				newValue = RecalculateForPriority(priority);

			_lastCalculatedValue = newValue;
#if UNITY_EDITOR
			_wasRecalculatedOnce = true;
#endif
			_isDirty = false;
		}

		private float RecalculateForPriority(int priority)
		{
			var fullAddition = 0f;
			if (_additiveModifiersByPriority.ContainsKey(priority))
			{
				var additionsDict = _additiveModifiersByPriority[priority];
				foreach (var modifierName in additionsDict.Keys)
					fullAddition += additionsDict[modifierName].StackedInnerValue;
			}

			var totalPercentChange = 0f;
			if (_percentModifiersByPriority.ContainsKey(priority))
			{
				var percentsDict = _percentModifiersByPriority[priority];
				foreach (var modifierName in percentsDict.Keys)
					totalPercentChange += percentsDict[modifierName].StackedInnerValue;
			}

			return (_innerValue + fullAddition) * (1 + totalPercentChange);
		}
	}
}
