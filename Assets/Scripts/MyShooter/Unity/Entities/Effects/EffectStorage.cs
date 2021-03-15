using MyShooter.Core.Entities.GeneralEnums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyShooter.Unity.Entities.Effects
{
	public class EffectStorage
	{
		private const int InfiniteStacks = 0;

		private readonly Dictionary<StackabilityType, Func<Effect, Effect, bool>> _effectComparers
			= new Dictionary<StackabilityType, Func<Effect, Effect, bool>>()
			{
				{ StackabilityType.KeepLatestAdded, (x, y) => x.EffectId < y.EffectId },
				{ StackabilityType.KeepSmallestValues, (x, y) => x.Compare(y) },
				{ StackabilityType.KeepBiggestValues, (x, y) => y.Compare(x) },
			};

		private BattleEntity _holder;

		private Dictionary<string, Dictionary<int, Effect>> _effects = new Dictionary<string, Dictionary<int, Effect>>();

		public void Initialize(BattleEntity entity)
		{
			_holder = entity;
			FindEffects(entity);
		}

		private void FindEffects(BattleEntity entity)
		{
			var effects = entity.Structure.Effects.GetComponentsInChildren<Effect>();
			var effectsDict = effects
				.Where(effect => effect.Type == EffectType.Fundamental) // not sure; mb there's an effect granting a new effect to bullets or smth?..
				.ToDictionary(effect => effect.Name);
			foreach(var effectName in effectsDict.Keys)
			{
				// we don't use AddEffect here because in this moment of time BattleEntity can't have any effects violating stacking rules on Effects Transform?
				// Be careful with this, tho
				if (!_effects.ContainsKey(effectName))
					_effects.Add(effectName, new Dictionary<int, Effect>());
				var effect = effectsDict[effectName];
				_effects[effectName].Add(effect.EffectId, effect);
			}
		}

		public void AddEffect(Effect effect)
		{
			var name = effect.Name;
			var effectId = effect.EffectId;
			if (!_effects.ContainsKey(name)) // if this is a first effect with this name, add dict for this name
				_effects.Add(name, new Dictionary<int, Effect>());
			else // in other case, we need to check effect stackability
			{
				var currentDict = _effects[name];
				var maxStacks = effect.State.MaxStackAmount;
				var stacks = currentDict.Count;
				if (maxStacks != InfiniteStacks && stacks < maxStacks) return; // if effect has infinite stacks or max stacks not reached
				else
				{
					var stackabilityType = effect.State.StackabilityType;
					if (stackabilityType == StackabilityType.KeepEarliestAdded) return;
					Effect removeCandidate = GetRemoveEffectCandidate(currentDict, stackabilityType);
					if (_effectComparers[stackabilityType](effect, removeCandidate)) return; // if a newcomer effect can't override candidate, just return
					currentDict.Remove(removeCandidate.EffectId); // in other case, remove candidate
				}
			}

			_effects[name].Add(effectId, effect);
		}

		public void RemoveEffect(Effect effect)
		{
			var name = effect.Name;
			var effectId = effect.EffectId;
			if (!_effects.ContainsKey(name)) return; // if we don't have a dict of this effect - probably will never happen?..

			var currentDict = _effects[name];
			if (!currentDict.ContainsKey(effectId)) return; // if dict of an effect name does not contain this exact effect.

			currentDict.Remove(effectId);
			if (currentDict.Count == 0) // it this was a last effect with this name, remove whole dict of name
				_effects.Remove(name);
		}

		private Effect GetRemoveEffectCandidate(Dictionary<int, Effect> dictionaryToSearch, StackabilityType stackabilityType)
		{
			Effect stackCandidate = null;
			foreach (var id in dictionaryToSearch.Keys)
			{
				var curStack = dictionaryToSearch[id];
				if (stackCandidate == null || _effectComparers[stackabilityType](stackCandidate, curStack))
					stackCandidate = curStack;
			}

			return stackCandidate;
		}
	}
}
