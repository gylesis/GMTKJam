using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class LevelButtonWithLabel : LevelButton
    {
        private const string GameplaySceneName = "Main";
        
        private Button _button;             
        private TMP_Text _label;
        private SessionObserver _sessionObserver;
        private ZenjectSceneLoader _sceneLoader;

        [Inject]
        private void Init(ZenjectSceneLoader sceneLoader, SessionObserver sessionObserver)
        {
            _sessionObserver = sessionObserver;
            _sceneLoader = sceneLoader;
        }
        
        public override void Init(int id)
        {
            _button = GetComponent<Button>();
            _label = GetComponentInChildren<TMP_Text>();
            _label.text = id.ToString();

            _button.onClick.AddListener(() =>
            {
                _sceneLoader.LoadSceneAsync(GameplaySceneName, LoadSceneMode.Single);
                _sessionObserver.SetLevel(id);
            });
        }
    }
}