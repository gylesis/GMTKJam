using UnityEngine;

namespace Project.Scripts
{
    [CreateAssetMenu(menuName = "Static Data", fileName = "StaticData", order = 0)]
    public class StaticData : ScriptableObject
    {
        [SerializeField] private LayerMask _layersForCellCheck;
       
        public LayerMask LayersForCellCheck => _layersForCellCheck;
     
    }
}