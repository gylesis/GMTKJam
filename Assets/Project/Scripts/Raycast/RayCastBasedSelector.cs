using UnityEngine;

namespace Assets.Scripts._3D.Selecting
{
    public class RayCastBasedSelector : MonoBehaviour, ISelector
    {
        [SerializeField] private float length;
        private ICustomSelectable selection;
        public void Check(Ray ray)
        {
            selection = null;
            if (!Physics.Raycast(ray, out var hit, length)) return;

            if (hit.transform.TryGetComponent(out ICustomSelectable potentialSelection))
            {
                selection = potentialSelection;
            }

        }

        public ICustomSelectable GetSelection()
        {
            return selection;
        }
    }
}