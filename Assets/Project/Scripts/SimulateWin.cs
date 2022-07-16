
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class SimulateWin : MonoBehaviour
    {
        [Inject]
        private LevelAdvancer _levelAdvancer;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                _levelAdvancer.GoNextLevel();
            }
        }
    }
}
