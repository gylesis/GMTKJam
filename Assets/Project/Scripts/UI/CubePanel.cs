using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Project.Scripts.Raycast.Selecting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CubePanel : MonoBehaviour
{
    [SerializeField] private Vector2 _leftOffset;
    [SerializeField] private float _slideTime = 0.2f;
    [SerializeField] private Button _button;

    private SelectionManager _selectionManager;
    private RectTransform _rectTransform;
    private Vector2 _initPosition;
    private bool _isHidden;

    [Inject]
    void Init(SelectionManager selectionManager)
    {
        _rectTransform = GetComponent<RectTransform>();
        _initPosition = _rectTransform.anchoredPosition;
        _selectionManager = selectionManager;
    }

    public void ToggleCubeMenu()
    {
        if (_isHidden)
        {
            Show();
        }
        else
        {
            Hide();
        }

        _isHidden = !_isHidden;
    }

    private async void Hide()
    {
        _button.interactable = false;
        var buttonWidth = _button.GetComponent<RectTransform>().rect.width;
        await _rectTransform.DOAnchorPos(new Vector2(-_rectTransform.rect.width + buttonWidth, _initPosition.y), _slideTime).AsyncWaitForCompletion();
        _button.interactable = true;

        _selectionManager.Active = false;
    }
    private async void Show()
    {
        _button.interactable = false;
        var buttonWidth = _button.GetComponent<RectTransform>().rect.width;
        await _rectTransform.DOAnchorPos(_initPosition, _slideTime).AsyncWaitForCompletion();
        _button.interactable = true;

        _selectionManager.Active = true;
    }
}
