using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyShooter.Unity.LevelLayout
{ 
	[CreateAssetMenu(menuName = "My Assets/Level Generator")]
	public class LayoutGenerator : ScriptableObject
	{
		[SerializeField] private string _startingRoomSceneName;
		[SerializeField] private string[] _roomSceneNames;
		[SerializeField] private string[] _treasureRoomSceneNames;
		[SerializeField] private string[] _bossRoomSceneNames;
		[SerializeField] private int _roomAmountBeforeBoss = 5;

		[System.NonSerialized] private List<string> _roomsList = new List<string>();
		[System.NonSerialized] private List<string> _treasureRoomsList = new List<string>();
		[System.NonSerialized] private List<string> _bossRoomList = new List<string>();

		[System.NonSerialized] private bool _isInitialized = false;
		[System.NonSerialized] private RoomType _nextRoomType = RoomType.StartingRoom;
		[System.NonSerialized] private int _currentRoomIndex;

		private List<string> CurrentGenerationList
		{
			get
			{
				if (_nextRoomType == RoomType.RegularRoom) return _roomsList;
				if (_nextRoomType == RoomType.TreasureRoom) return _treasureRoomsList;
				if (_nextRoomType == RoomType.BossRoom) return _bossRoomList;
				return null;
			}
		}
		private bool TimeForBossRoom => _currentRoomIndex > 0 && _currentRoomIndex % _roomAmountBeforeBoss == 0;

		public string GenerateNewRoomName()
		{
			InitializeIfNeeded();

			if (TimeForBossRoom)
			{
				if (_nextRoomType == RoomType.RegularRoom || _nextRoomType == RoomType.StartingRoom)
					_nextRoomType = RoomType.TreasureRoom;
				else if (_nextRoomType == RoomType.TreasureRoom)
					_nextRoomType = RoomType.BossRoom;
				else if (_nextRoomType == RoomType.BossRoom)
				{
					_nextRoomType = RoomType.RegularRoom;
					_currentRoomIndex++;
				}
			}
			else
			{
				if (_nextRoomType == RoomType.StartingRoom)
					_nextRoomType = RoomType.RegularRoom;
				_currentRoomIndex++;
			}

			return GetNewRoom();
		}

		private void InitializeIfNeeded()
		{
			if (_isInitialized) return;

			_roomsList = _roomSceneNames.ToList();
			_treasureRoomsList = _treasureRoomSceneNames.ToList();
			_bossRoomList = _bossRoomSceneNames.ToList();

			_isInitialized = true;
		}

		private string GetNewRoom()
		{
			if (_nextRoomType == RoomType.StartingRoom) return _startingRoomSceneName;

			var curGenList = CurrentGenerationList;
			var index = GenerateRandomIndex(curGenList.Count - 1);
			string generatedRoomName = curGenList[index];
			curGenList.RemoveAt(index);
			return generatedRoomName;
		}

		private int GenerateRandomIndex(int maxIndex) => Random.Range(0, maxIndex);
	}
}
