using UnityEngine;

namespace Project.Scripts
{
    public class JumpingCell : Cell
    {
        [SerializeField] private Cell _jumpCell;
        
        public Cell JumpCell => _jumpCell;

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