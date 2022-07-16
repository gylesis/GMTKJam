using Project.Scripts.Raycast;
using Project.Scripts.Raycast.Selecting;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Raycast
{
    public class RayCastBasedSelector : MonoBehaviour, ISelector
    {
        [Inject]
        private StaticData _staticData;

        private ICustomSelectable selection;
        public void Check(Ray ray)
        {
            selection = null;
            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, _staticData.LayersForCubeSidesCheck)) return;
           
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