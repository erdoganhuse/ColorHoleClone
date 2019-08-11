using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Data.User
{
    [CreateAssetMenu(fileName = "UserData", menuName = "Game/User/UserData")]
    public class UserData : SerializedScriptableObject
    {
        public UserProgress UserProgress;
        public UserCurrency UserCurrency;
    }
}
