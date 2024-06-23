using System;
using Audio;
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
        [SerializeField] private Button _levelChooserButton;
        [SerializeField] private LevelChooserUI _levelChooserUI;
        
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
            _levelChooserUI.Initialize(_sceneLoader, _loadingCurtain);
            _levelChooserButton.onClick.AddListener(OnLevelChooseClicked);
        }

        private void OnLevelChooseClicked()
        {
            _playButton.gameObject.SetActive(false);
            _levelChooserUI.Show();
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(OnPLayButtonClick);
            _levelChooserButton.onClick.RemoveListener(OnLevelChooseClicked);
        }

        private void OnPLayButtonClick()
        {
            _sceneLoader.Load($"Level {_playerProgressSaver.LoadData().CurrentLevelNumber}");
            gameObject.SetActive(false);
            _loadingCurtain.Show();
        }
    }
}