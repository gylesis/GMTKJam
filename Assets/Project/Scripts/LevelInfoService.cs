using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts
{
    public class LevelInfoService
    {
        private Level _currentLevel;

        private readonly Dictionary<int, List<int>> _neighbours = new Dictionary<int, List<int>>();
        private readonly Dictionary<int, Cell> _cells = new Dictionary<int, Cell>();

        public void OnLevelSpawned(Level level)
        {
            foreach (Cell currentCell in level.Cells)
            {
                var overlapSphere = Physics.OverlapSphere(currentCell.transform.position, currentCell.transform.localScale.x);
                int cellHashCode = currentCell.GetHashCode();

                List<int> neighbours = new List<int>();

                foreach (Collider collider in overlapSphere)
                {
                    if (collider.TryGetComponent<Cell>(out var neighbour))
                    {
                        var neighbourHashCode = neighbour.GetHashCode();

                        if(cellHashCode == neighbourHashCode) continue;
                        
                        neighbours.Add(neighbourHashCode);
                        
                        if (_cells.ContainsKey(neighbourHashCode) == false)
                            _cells.Add(neighbourHashCode, neighbour);
                    }
                }

                _neighbours.Add(cellHashCode, neighbours);
            }
        }

        public List<Cell> GetNeighboursCells(int currentCellId)
        {
            var neighbours = _neighbours[currentCellId];

            List<Cell> cells = new List<Cell>();

            foreach (var neighbourId in neighbours)
            {
                Cell cell = _cells[neighbourId];

                cells.Add(cell);
            }

            return cells;
        }
    }
}