using System;
using System.Collections;
using Core.Data;
using Core.Interfaces;
using Logic;
using UnityEngine;

namespace UI
{
    public class LevelPassedUI : MonoBehaviour
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;
        private ISceneLoader _sceneLoader;
        private IPlayerProgressSaver _playerProgressSaver;
        private LevelConfig _levelConfig;


        public void Initialize(ISceneLoader sceneLoader, IPlayerProgressSaver playerProgressSaver, LevelConfig levelConfig)
        {
            _sceneLoader = sceneLoader;
            _playerProgressSaver = playerProgressSaver;
            _levelConfig = levelConfig;
        }
        
        private void OnEnable()
        {
            StartCoroutine(WaitingForClick());
        }

        private IEnumerator WaitingForClick()
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            TurnOff();
            _loadingCurtain.Show();
            _sceneLoader.Load($"Level {_levelConfig.NumberOfLevel+1}");
        }

        public void TurnOn()
        {
            gameObject.SetActive(true);
            _playerProgressSaver.SaveData(new PlayerData(_levelConfig.NumberOfLevel+1));
        }
        
        public void TurnOff()
        {
            gameObject.SetActive(false);
        }
    }
}