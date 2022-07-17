using UnityEngine;

namespace Project.Scripts
{
    public class PlayerCubicSlotData
    {
        public IPlayerMovement Movement;
        public MoveSide MoveSide;
        public Vector3 Forward { get; set; }
    }
}