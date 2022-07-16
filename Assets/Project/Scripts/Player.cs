using UnityEngine;

namespace Project.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerCubicSlot[] _slots;
        public PlayerCubicSlot[] Slots
        {
            get => _slots;
            set => _slots = value;
        }
    }
}