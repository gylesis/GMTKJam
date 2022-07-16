using UnityEngine;

namespace Project.Scripts
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] protected Transform _pivot;
        [SerializeField] protected CellView _cellView;

        public CellView CellView => _cellView;
        public Transform Pivot => _pivot;

        public CellData Data;

        private void Awake()
        {
            Data = new CellData();
            
            Data.ID = GetHashCode();
        }
    }


    public class CellData
    {
        public int ID { get; set; } = -1;
        public IPlayerMovement PlayerMovement { get; set; }
    }
}