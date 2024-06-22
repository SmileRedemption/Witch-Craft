using System;
using Core.Data.Extensions;
using UnityEngine;

namespace Core.Data
{
    public class PlayerProgressSaver : IPlayerProgressSaver
    {
        private const string Key = "ProgressKey";
        private const string DefaultValue = "";

        public void SaveData(PlayerData playerData)
        {
            PlayerPrefs.SetString(Key, playerData.ToJson());
        }

        public PlayerData LoadData()
        {
            return PlayerPrefs.HasKey(Key) == false ? new PlayerData(1) 
                : PlayerPrefs.GetString(Key).ToDeserialized<PlayerData>();
        }
    }
}