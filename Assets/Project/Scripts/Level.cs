using System;
using UnityEngine;

namespace Project.Scripts
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Cell[] _cells;
        [SerializeField] private int _requiredCellNumber;

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

        private void OnValidate()
        {
            
            _cells = GetComponentsInChildren<Cell>();

            foreach (Cell cell in _cells)
            {
                if (cell == _startCell)
                {
                    cell.CellView.StartMarker.SetActive(true);
                    cell.CellView.FinishMarker.SetActive(false);

                }
                else if (cell == _finishCell)
                {
                    cell.CellView.FinishMarker.SetActive(true);
                    cell.CellView.StartMarker.SetActive(false);
                }
                else
                {
                    cell.CellView.FinishMarker.SetActive(false);
                    cell.CellView.StartMarker.SetActive(false);
                    cell.CellView.JumpSpriteRenderer.SetActive(false);
                    cell.CellView.TeleportSpriteRenderer.SetActive(false);
                }

                if (cell is JumpingCell jumpingCell && jumpingCell.JumpCell != null)
                {

                    jumpingCell.JumpCell.CellView.JumpSpriteRenderer.SetActive(true);
                }

                if (cell is TeleportCell teleportCell && teleportCell.TargetCell != null)
                {
                    teleportCell.TargetCell.CellView.JumpSpriteRenderer.SetActive(true);
                }
            }
            
        }

        public void PlacePlayer(Transform player)
        {
            player.transform.position = _startCell.Pivot.position + (Vector3.up * (player.transform.localScale.x / 2));
        }
    }
}