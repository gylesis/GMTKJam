
using Project.Scripts.Raycast.Selecting;
using UnityEngine;

namespace Project.Scripts.Raycast
{
    public class MousePositionRayProvider : MonoBehaviour, IRayProvider
    {
        [SerializeField] private Camera camera;
        public Ray CreateRay()
        {
            Vector3 screenPosition = Input.mousePosition;
            screenPosition.z = camera.nearClipPlane;
            return camera.ScreenPointToRay(screenPosition);
        }
    }
}
