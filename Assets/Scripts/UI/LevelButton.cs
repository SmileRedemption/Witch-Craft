using System;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private int _level;
        public Button Button => _button;
        public int Level => _level;

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}