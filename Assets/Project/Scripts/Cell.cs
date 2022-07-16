using UnityEngine;

namespace Project.Scripts
{
    public class Cell : MonoBehaviour
    {
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
    }
}