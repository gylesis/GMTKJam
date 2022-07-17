using UnityEngine;

namespace Project.Scripts
{
    public class DeathFinishHandler
    {
        private readonly LevelFinishService _levelFinishService;
        private readonly LevelDeathService _levelDeathService;
        private SoundPlayer _soundPlayer;

        public DeathFinishHandler(LevelFinishService levelFinishService, LevelDeathService levelDeathService, SoundPlayer soundPlayer)
        {
            _soundPlayer = soundPlayer;
            _levelDeathService = levelDeathService;
            _levelFinishService = levelFinishService;
        }
        
        public void OnFinishCell()
        {
            _soundPlayer.PlayWinSound();
            _levelFinishService.Finish();
            Debug.Log("WIN!");
        }

        public void OnDeath()
        {
            _soundPlayer.PlayLoseSound();
            _levelDeathService.OnDied();
            Debug.Log("dead");
        }
        
    }
}