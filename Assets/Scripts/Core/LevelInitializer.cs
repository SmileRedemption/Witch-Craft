using System;
using Core.Data;
using Core.Interfaces;
using Logic;
using UI;
using UnityEngine;

namespace Core
{
    public class LevelInitializer : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private GameField _gameField;
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private Score _score;
        [SerializeField] private LoadingCurtain _loadingCurtain;
        [SerializeField] private LevelPassedUI _levelPassedUI;
        [SerializeField] private BackMenuButton _backMenuButton;
        private ISceneLoader _sceneLoader;
        
        private void Awake()
        {
            _sceneLoader = new SceneLoader(this);
            _levelPassedUI.Initialize(_sceneLoader, new PlayerProgressSaver(), _levelConfig, _loadingCurtain);
            _backMenuButton.Initialize(_sceneLoader, _loadingCurtain);
            _gameField.Initialize();
            _score.Initialize(_levelConfig);
        }

        private void Start()
        {
            _score.LevelPassed += OnLevelPassed;
        }

        private void OnDestroy()
        {
            _score.LevelPassed -= OnLevelPassed;
        }

        private void OnLevelPassed()
        {
            _gameField.TurnOff();
            _levelPassedUI.TurnOn();
        }
    }
}