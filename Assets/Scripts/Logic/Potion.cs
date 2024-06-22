using UnityEngine;

namespace Logic
{
    [CreateAssetMenu(menuName = "Item/Potion")]
    public class Potion : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public PotionType PotionType { get; private set; }
    }
}