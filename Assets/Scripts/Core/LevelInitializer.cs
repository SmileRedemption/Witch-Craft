using System;
using Core.Data;
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
        [SerializeField] private LevelPassedUI _levelPassedUI;
        
        private void Awake()
        {
            _levelPassedUI.Initialize(new SceneLoader(this), new PlayerProgressSaver(), _levelConfig);
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