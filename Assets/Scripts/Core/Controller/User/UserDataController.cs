using CodeStage.AntiCheat.ObscuredTypes;
using Core.Data.User;
using UnityEngine;
using Utilities.Enums;
using Zenject;

namespace Core.Controller.User
{
    public class UserDataController
    {
        private const string UserDataKey = "UserDataKey";

        private readonly UserData _userData;
        private readonly UserData _defaultUserData;

        public UserCurrency Currency => _userData.UserCurrency;
        public UserProgress Progress => _userData.UserProgress;

        public UserDataController([Inject(Id = BindingIds.DefaultUserData)] UserData defaultUserData)
        {
            _defaultUserData = defaultUserData;
            
            if (!ObscuredPrefs.HasKey(UserDataKey)) Reset();
            
            _userData = Object.Instantiate(_defaultUserData);
            JsonUtility.FromJsonOverwrite(ObscuredPrefs.GetString(UserDataKey), _userData);
        }

        ~UserDataController() { }
        
        public void Save()
        {
            ObscuredPrefs.SetString(UserDataKey, JsonUtility.ToJson(_userData,true));
        }

        public void Reset()
        {
            ObscuredPrefs.SetString(UserDataKey, JsonUtility.ToJson(_defaultUserData,true));
        }

        private static string GetUserName()
        {
            return $"User-{SystemInfo.deviceUniqueIdentifier}";
        }
        
        #region User Currency Methods
                
        #endregion
        
        #region User Progress Methods

        #endregion
    }
}