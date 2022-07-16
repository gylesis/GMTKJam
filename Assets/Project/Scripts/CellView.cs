using UnityEngine;

namespace Project.Scripts
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;

        public void Highlight(Color color)
        {
            _renderer.material.color = color;
        }
        
    }
}