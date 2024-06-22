using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Logic
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        private LevelConfig _levelConfig;

        private IDictionary<PotionType, int> _scoreForTypePotion = new Dictionary<PotionType, int>()
        {
            {PotionType.Blue, 50},
            {PotionType.Green, 75},
            {PotionType.Yellow, 100},
            {PotionType.Red, 150},
            {PotionType.Violet, 200}
        };

        private int _countInRow = 1; 
        
        public int CurrentScore { get; private set; }

        public event Action LevelPassed;

        public void Initialize(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
        }

        public void UpdateScore(PotionType potionType, int matchLenght, bool isInRow)
        {
            int score; 
            if (isInRow)
            {
                _countInRow += 1;
                if (_countInRow is >= 2 and <= 3)
                {
                    score = _scoreForTypePotion[potionType] * matchLenght * 2;
                }
                else
                {
                    score = _scoreForTypePotion[potionType] * matchLenght * 3;
                }
            }
            else
            {
                score = _scoreForTypePotion[potionType] * matchLenght;
                _countInRow = 1;
            }
            
            if (CurrentScore + score >= _levelConfig.ScoreToChange)
            {
                _scoreText.text = $"Score: {_levelConfig.ScoreToChange}";
                LevelPassed?.Invoke();
                return;
            }

            CurrentScore += score;
            _scoreText.text = $"Score: {CurrentScore}";
        }
    }
}