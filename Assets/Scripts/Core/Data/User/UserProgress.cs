using System;

namespace Core.Data.User
{
    [Serializable]
    public class UserProgress
    {
        public int LastPlayedLevelId;
        public int MaxAchievedLevelId;
    }
}