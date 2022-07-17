using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Project.Scripts
{
    public class LevelNameText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
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