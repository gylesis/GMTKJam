using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Scripts
{
    public class LevelInfoService
    {
        private Level _currentLevel;

        private readonly Dictionary<int, List<int>> _neighbours = new Dictionary<int, List<int>>();
        private readonly Dictionary<int, Cell> _cells = new Dictionary<int, Cell>();
        private Dictionary<Vector2, Cell> _positions = new Dictionary<Vector2, Cell>();

        private readonly PlayerFacade _playerFacade;
        private readonly StaticData _staticData;


        public LevelInfoService(PlayerFacade playerFacade, StaticData staticData)
        {
            _staticData = staticData;
            _playerFacade = playerFacade;
        }

        public void OnLevelSpawned(Level level)
        {
            foreach (Cell currentCell in level.Cells)
            {
                var overlapSphere =
                    Physics.OverlapSphere(currentCell.transform.position, currentCell.transform.localScale.x);
                int cellHashCode = currentCell.GetHashCode();

                List<int> neighbours = new List<int>();

                foreach (Collider collider in overlapSphere)
                {
                    if (collider.TryGetComponent<Cell>(out var neighbour))
                    {
                        var neighbourHashCode = neighbour.GetHashCode();

                        if (cellHashCode == neighbourHashCode) continue;

                        neighbours.Add(neighbourHashCode);

                        var pos = new Vector2(neighbour.transform.position.x, neighbour.transform.position.z);

                        if (_positions.ContainsKey(pos) == false)
                            _positions.Add(pos, neighbour);

                        if (_cells.ContainsKey(neighbourHashCode) == false)
                            _cells.Add(neighbourHashCode, neighbour);
                    }
                }

                _neighbours.Add(cellHashCode, neighbours);
            }
        }

        public Cell GetForwardCell()
        {
            var vector2 = new Vector2(_playerFacade.Transform.localPosition.x, _playerFacade.Transform.localPosition.z);

            vector2.y += 1;
            
            return _positions[vector2];
        }

        public Cell GetLeftCell()
        {
            var vector2 = new Vector2(_playerFacade.Transform.localPosition.x, _playerFacade.Transform.localPosition.z);

            vector2.x -= 1;
            
            return _positions[vector2];
        }
        
        public Cell GetPlayerBottomCell()
        {
            var sphereCast = Physics.SphereCast(_playerFacade.Transform.position,
                _playerFacade.Transform.localScale.x / 2 - 0.1f, Vector3.down,
                out var hit, _staticData.LayersForCellCheck);

            if (sphereCast)
            {
                var tryGetComponent = hit.collider.TryGetComponent<Cell>(out var cell);

                if (tryGetComponent)
                {
                    return cell;
                }
            }

            return null;
        }

        private List<Cell> GetNeighboursCells(int currentCellId)
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

        public List<Cell> GetNeighboursCells(Vector3 pos)
        {
            var cells = _cells.Values.OrderBy(x => (x.transform.position - pos).sqrMagnitude).ToList();

            var neighboursCells = GetNeighboursCells(cells.First().Data.ID);

            return neighboursCells;
        }
    }
}