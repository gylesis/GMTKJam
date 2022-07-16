using UnityEngine;

namespace Project.Scripts
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Cell[] _cells;

        public Cell[] Cells => _cells;

        private void Awake()
        {
            _cells = GetComponentsInChildren<Cell>();
        }
    }
}