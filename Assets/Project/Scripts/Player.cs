using UnityEngine;

namespace Project.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerCubicSlot[] _slots;
        public PlayerCubicSlot[] Slots => _slots;
    }
}