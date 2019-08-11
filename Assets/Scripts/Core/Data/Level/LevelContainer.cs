using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Data.Level
{
    [CreateAssetMenu(fileName = "LevelContainer", menuName = "Game/Level/LevelContainer")]
    public class LevelContainer : ScriptableObject
    {
        public List<LevelData> Levels;        
        
        public int GetNextLevelId(int levelIndex)
        {
            if (levelIndex + 1 >= Levels.Count)
                return Levels.Last().Id;

            return Levels[levelIndex + 1].Id;
        }

        public int GetLevelIndex(int levelId)
        {
            if (Levels.All(item => item.Id != levelId)) return -1;

            return Levels.FindLastIndex( item => item.Id == levelId);
        }
    }
}