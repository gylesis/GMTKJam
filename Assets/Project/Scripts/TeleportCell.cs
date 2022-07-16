using UnityEngine;

namespace Project.Scripts
{
    public class TeleportCell : Cell
    {
        [SerializeField] private Cell _targetCell;

        public Cell TargetCell => _targetCell;
        
        private void Reset()
        {
            var tryGetComponent = TryGetComponent<Cell>(out var cell);

            if (tryGetComponent)
            {
                _pivot = cell.Pivot;
                _cellView = cell.CellView;
            }
        }
    }
}