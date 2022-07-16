using Project.Scripts.Raycast;
using Project.Scripts.Raycast.Selecting;
using UnityEngine;

namespace Project.Scripts.Raycast
{
    public class InteractionResponce : ISelectionResponse
    {
        public void OnSelect(ICustomSelectable selection)
        {
            if (Input.GetMouseButtonDown(0))
            {
                selection.Interact();
            }
        }

        public void OnDeselect(ICustomSelectable selection)
        {
            //
        }
    }
}
