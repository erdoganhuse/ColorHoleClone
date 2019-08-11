using System.Collections.Generic;
using UnityEngine;

namespace Core.Data.Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level/LevelData")]
    public class LevelData : ScriptableObject
    {
        public int Id;
        public List<StageData> Stages;
    }
}