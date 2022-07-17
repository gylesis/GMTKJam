using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts
{
    public class LevelDeathService : MonoBehaviour
    {
        [SerializeField] private TMP_Text _deathText;

        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _skipButton;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private LevelAdvancer _levelAdvancer;

        [Inject]
        private void Init(LevelAdvancer levelAdvancer)
        {
            _levelAdvancer = levelAdvancer;
            
            Hide();
        }
        
        public void OnDied()
        {
            Show();
            
            _restartButton.onClick.AddListener((OnRestartButtonClicked));
            _skipButton.onClick.AddListener((OnSkipButtonClicked));
        }

        private void OnSkipButtonClicked()
        {
            _levelAdvancer.SkipLevel();
            Hide();
        }

        private void OnRestartButtonClicked()
        {
            _levelAdvancer.ResetLevel();
            
            Hide();
            _deathText.enabled = false;
        }

        public void Show()
        {
            _deathText.text = "Lol, you died";
            _canvasGroup.DOFade(1, 1);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
        
        private void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
        
        
    }
}