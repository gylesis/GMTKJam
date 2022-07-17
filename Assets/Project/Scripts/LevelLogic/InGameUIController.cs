using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts
{
    public class InGameUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _resetCubicSlotsButton;
        
        private UICubicSlotContainer _uiCubicSlotContainer;
        private LevelAdvancer _levelAdvancer;

        [Inject]
        private void Init(UICubicSlotContainer uiCubicSlotContainer)
        {
            _uiCubicSlotContainer = uiCubicSlotContainer;
            
            _resetCubicSlotsButton.onClick.AddListener((OnResetUICubicClicked));
        }

        private void OnResetUICubicClicked()
        {
            _uiCubicSlotContainer.ClearUISlots();
        }

        public void SetText(string str)
        {
            _text.text = str;
        }

        public async void HideText()
        {
            _text.enabled = true;
            await _text.DOFade(0, 1).AsyncWaitForCompletion();
            _text.enabled = false;
        }

        public void ShowText()
        {
            _text.enabled = true;
            _text.DOFade(1, 1);
        }
    }
}