using System;
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

        /*private void OnValidate()
        {
            Vector3 transformLocalPosition = transform.localPosition;

            transformLocalPosition.x = Mathf.Round(transformLocalPosition.x);
            transformLocalPosition.y = Mathf.Round(transformLocalPosition.y);
            transformLocalPosition.z = Mathf.Round(transformLocalPosition.z);

            transform.localPosition = transformLocalPosition;
        }*/
    }
}