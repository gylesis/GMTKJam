﻿using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts
{
    public class TeleportMovement : IPlayerMovement
    {
        public event Action<Cell> Moved;
    
        private readonly PlayerFacade _playerFacade;
        private readonly LevelInfoService _levelInfoService;

        public TeleportMovement(PlayerFacade playerFacade, LevelInfoService levelInfoService)
        {
            _levelInfoService = levelInfoService;
            _playerFacade = playerFacade;
        }

        public async void Move(Cell cellToMove, Vector2 direction)
        {
            Debug.Log("Tp");
            var movePos = cellToMove.Pivot.position + (Vector3.up * (_playerFacade.Transform.localScale.x / 2));

            _playerFacade.Transform.position = movePos;

           // await Task.Delay(100);
            
            //await _playerFacade.Transform.DOMove(movePos, 1).SetEase(Ease.InQuad).AsyncWaitForCompletion();

            Cell cell = _levelInfoService.GetPlayerBottomCell();

            Moved?.Invoke(cell);
        }
    }
}