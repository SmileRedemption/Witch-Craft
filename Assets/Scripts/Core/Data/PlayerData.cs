using System;

namespace Core.Data
{
    [Serializable]
    public class PlayerData
    {
        public int CurrentLevelNumber;

        public PlayerData(int currentLevelNumber)
        {
            CurrentLevelNumber = currentLevelNumber;
        }
    }
}