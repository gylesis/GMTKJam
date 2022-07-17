using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts
{
    public class LevelFinishService : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private TMP_Text _congratulationSign;

        [SerializeField] private CanvasGroup _canvasGroup;  
        
        private LevelAdvancer _levelAdvancer;

        [Inject]
        private void Init(LevelAdvancer levelAdvancer)
        {
            _levelAdvancer = levelAdvancer;
            
            _restartButton.onClick.AddListener((OnRestartButtonClicked));
            _continueButton.onClick.AddListener((OnContinueButtonClicked));
        }

        public void Finish()
        {
           
            Show();
        }

        public async void Show()
        {
            _congratulationSign.text = "Level cleared";

            await _canvasGroup.DOFade(1, 1).AsyncWaitForCompletion();
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
        
        private void OnContinueButtonClicked()
        {
            Debug.Log("continue button");
            _levelAdvancer.GoNextLevel();
            
            Hide();
        }

        private void OnRestartButtonClicked()
        {
            _levelAdvancer.ResetLevel();
            Hide();
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveAllListeners();
            _continueButton.onClick.RemoveAllListeners();
        }
    }
}