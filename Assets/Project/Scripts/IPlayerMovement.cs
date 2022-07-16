using System;

namespace Project.Scripts
{
    public interface IPlayerMovement
    {
        event Action<Cell> Moved;
        void Move(Cell moveCell);
    }   
}