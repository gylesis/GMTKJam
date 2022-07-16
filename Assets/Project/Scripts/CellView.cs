using UnityEngine;

namespace Project.Scripts
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;

        [SerializeField] private GameObject _startMarker;
        [SerializeField] private GameObject _finishMarker;
        [SerializeField] private GameObject _jumpSpriteRenderer;
        [SerializeField] private GameObject _teleportSpriteRenderer;

        public GameObject StartMarker => _startMarker;
        public GameObject FinishMarker => _finishMarker;
        public GameObject TeleportSpriteRenderer => _teleportSpriteRenderer;
        public GameObject JumpSpriteRenderer => _jumpSpriteRenderer;

        public void Highlight(Color color)
        {
            _renderer.material.color = color;
        }
        
    }
}