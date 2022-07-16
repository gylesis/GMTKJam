using System.Collections;
using System.Collections.Generic;
using Project.Scripts.Raycast;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Raycast.Selecting
{
    [RequireComponent(typeof(IRayProvider))]
    [RequireComponent(typeof(ISelector))]
    [RequireComponent(typeof(ISelectionResponse))]

    public class SelectionManager : MonoBehaviour
    {
        private IRayProvider _rayProvider;
        private ISelector _selector;
        private ISelectionResponse _selectionResponse;

        private ICustomSelectable _selection;

        [Inject]
        private void Init(IRayProvider rayProvider, ISelector selector, ISelectionResponse selectionResponse)
        {
            _rayProvider = rayProvider;
            _selector = selector;
            _selectionResponse = selectionResponse;
        }

        private void Update()
        {
           HandleSelection();
        }

        private void HandleSelection()
        {
            if (_selection != null)
            {
                _selectionResponse.OnDeselect(_selection);
            }

            _selector.Check(_rayProvider.CreateRay());
            _selection = _selector.GetSelection();

            
            if (_selection != null)
            {
                _selectionResponse.OnSelect(_selection);
            }

        }
    }
}