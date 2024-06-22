using System;
using Core;
using Core.Data;
using Core.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private LoadingCurtain _loadingCurtain;
        
        private ISceneLoader _sceneLoader;
        private IPlayerProgressSaver _playerProgressSaver;

        public void Initialize(ISceneLoader sceneLoader, IPlayerProgressSaver playerProgressSaver)
        {
            _sceneLoader = sceneLoader;
            _playerProgressSaver = playerProgressSaver;
        }

        private void Start()
        {
            _playButton.onClick.AddListener(OnPLayButtonClick);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(OnPLayButtonClick);
        }

        private void OnPLayButtonClick()
        {
            _sceneLoader.Load($"Level {_playerProgressSaver.LoadData().CurrentLevelNumber}");
            gameObject.SetActive(false);
            _loadingCurtain.Show();
        }
    }
}