using MyShooter.Core.Entities.GeneralEnums;
using MyShooter.Unity.Data;
using System;
using System.Collections.Generic;

namespace MyShooter.Core.Entities.States.Modified
{
	// TODO: Replace with inheritance to reduce class complicance?
	// but how will we add derived classes to the dictionary in scriptableobject w/o spending a time for implementing a custom add system (probably reflection-based?)
	public class ValueModifier
	{
		private static int _timeStamp;
		private readonly Dictionary<StackabilityType, Func<ValueModifierStack, ValueModifierStack, bool>> _stackComparers
			= new Dictionary<StackabilityType, Func<ValueModifierStack, ValueModifierStack, bool>>()
			{
						{ StackabilityType.KeepLatestAdded, (x, y) => x.TimeStamp < y.TimeStamp },
						{ StackabilityType.KeepSmallestValues, (x, y) => x.InnerValue > y.InnerValue },
						{ StackabilityType.KeepBiggestValues, (x, y) => x.InnerValue < y.InnerValue },
			};

		private ValueModifierData _data;
		private Dictionary<int, ValueModifierStack> _stacks = new Dictionary<int, ValueModifierStack>();

		private Func<ValueModifierStack, ValueModifierStack, bool> CurrentStackComparer => _stackComparers[StackabilityType];

		public string Name { get; private set; }
		public float StackedInnerValue { get; private set; }

		public int Priority => _data.Priority;
		public ValueModifierType Type => _data.Type;
		public StackabilityType StackabilityType => _data.StackabilityType;
		public int MaxStackAmount => _data.MaxStacksAmount;
		public bool HasMinValue => _data.HasMinValue;
		public float MinStackedInnerValue => _data.MinStackedInnerValue;
		public bool HasMaxValue => _data.HasMaxValue;
		public float MaxStackedInnerValue => _data.MaxStackedInnerValue;
		public bool HasStackCap => MaxStackAmount > 0;
		public int StackAmount => _stacks.Count;

		public ValueModifier(string name, float innerValue, int stackId)
		{
			Name = name;

			_data = ValueModifiersDataObject.Instance.ValueModifiers[name]; // burn in hell if you didn't add it to global objects properties dict!
			AddStack(innerValue, stackId);
		}

		public bool AddStack(float innerValue, int stackId)
		{
			ValueModifierStack newStack = CreateStack(innerValue, stackId);

			if (HasStackCap && StackAmount == MaxStackAmount)
			{
				if (StackabilityType == StackabilityType.KeepEarliestAdded) return false;
				else
				{
					var stackCandidate = GetRemoveCandidateStackId();
					if (CurrentStackComparer(newStack, stackCandidate)) return false;
					RemoveStackInternal(stackCandidate.Id);
				}
			}

			AddStackInternal(newStack);
			return true;
		}

		private ValueModifierStack CreateStack(float innerValue, int stackId)
		{
			var newStack = new ValueModifierStack(innerValue, stackId);
			SetStackTimestamp(newStack);
			return newStack;
		}

		private void SetStackTimestamp(ValueModifierStack stack)
		{
			stack.TimeStamp = _timeStamp;
			_timeStamp++;
		}

		private ValueModifierStack GetRemoveCandidateStackId()
		{
			ValueModifierStack stackCandidate = null;
			foreach(var id in _stacks.Keys)
			{
				var curStack = _stacks[id];
				if (stackCandidate == null || _stackComparers[StackabilityType](stackCandidate, curStack))
					stackCandidate = curStack;
			}

			return stackCandidate;
		}

		private void AddStackInternal(ValueModifierStack stack)
		{
			var id = stack.Id;
			_stacks.Add(id, stack);
			TrySetInnerValue(stack.InnerValue);
		}

		public void RemoveStack(int stackId)
		{
			if (!_stacks.ContainsKey(stackId)) return;

			RemoveStackInternal(stackId);
		}

		private void RemoveStackInternal(int stackId)
		{
			var removingStackValue = _stacks[stackId].InnerValue;
			TrySetInnerValue(-removingStackValue);
			_stacks.Remove(stackId);
		}

		private void TrySetInnerValue(float newValue)
		{
			StackedInnerValue = StackedInnerValue + newValue;
			if (HasMinValue)
				StackedInnerValue = UnityEngine.Mathf.Clamp(StackedInnerValue, MinStackedInnerValue, StackedInnerValue);
			if (HasMaxValue)
				StackedInnerValue = UnityEngine.Mathf.Clamp(StackedInnerValue, StackedInnerValue, MaxStackedInnerValue);
		}
	}
}
