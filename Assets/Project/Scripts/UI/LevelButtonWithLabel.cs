
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts.UI
{

    [RequireComponent(typeof(Button))]
    public class LevelButtonWithLabel : LevelButton
    {
        [Inject]
        private LevelSceneLoader _levelSceneLoader;

        private Button _button;
        private TMP_Text _label;

        public override void Init(int id)
        {
            _button = GetComponent<Button>();
            _label = GetComponentInChildren<TMP_Text>();
            _button.onClick.AddListener(() => _levelSceneLoader.LoadLevelById(id));
            _label.text = id.ToString();
        }

    }
}