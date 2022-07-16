using System;
using UnityEngine;

namespace Project.Scripts
{
    public interface IPlayerMovement
    {
        event Action<Cell> Moved;
        void Move(Cell moveCell, Vector2 direction);
    }   
}