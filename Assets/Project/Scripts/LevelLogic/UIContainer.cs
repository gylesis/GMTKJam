using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class UIContainer : MonoBehaviour
    {
        [SerializeField] private Button _restartLevelButton;
        
        public Button RestartLevelButton => _restartLevelButton;
    }
}