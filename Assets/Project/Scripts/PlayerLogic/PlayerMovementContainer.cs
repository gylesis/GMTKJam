using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Scripts
{
    public class PlayerMovementContainer
    {
        private Dictionary<Type, IPlayerMovement> _playerMovements;

        public PlayerMovementContainer(IPlayerMovement[] playerMovements)
        {
            _playerMovements = playerMovements.ToDictionary(x => x.GetType());
        }

        public IPlayerMovement GetMovement<TMovement>() where  TMovement : IPlayerMovement
        {
            return _playerMovements[typeof(TMovement)];
        }
        
    }
}