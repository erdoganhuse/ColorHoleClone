using UnityEngine;

namespace Core.Data.Level
{
    [CreateAssetMenu(fileName = "StageData", menuName = "Game/Level/StageData")]
    public class StageData : ScriptableObject
    {
        public GameObject Prefab;
    }
}