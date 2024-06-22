using UnityEngine;

namespace Logic
{
    [CreateAssetMenu(menuName = "Item/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public int NumberOfLevel;
        public int ScoreToChange;
    }
}