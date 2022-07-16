using System;
using UnityEngine;

namespace Project.Scripts
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Cell[] _cells;

        [SerializeField] private Cell _startCell;
        [SerializeField] private Cell _finishCell;

        public Cell StartCell => _startCell;
        public Cell FinishCell => _finishCell;

        public event Action<Level> FinishCellMoved; 
        
        public Cell[] Cells => _cells;

        private void Awake()
        {
            _cells = GetComponentsInChildren<Cell>();
        }

        public void PlacePlayer(Transform player)
        {
            player.transform.position = _startCell.Pivot.position + (Vector3.up * (player.transform.localScale.x / 2));
        }
    }
    
}