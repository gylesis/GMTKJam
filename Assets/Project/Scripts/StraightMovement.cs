namespace Project.Scripts
{
    public class StraightMovement : IPlayerMovement
    {
        private PlayerFacade _playerFacade;
        private LevelInfoService _levelInfoService;

        public StraightMovement(PlayerFacade playerFacade, LevelInfoService levelInfoService)
        {
            _levelInfoService = levelInfoService;
            _playerFacade = playerFacade;
        }
        
        public void Move()
        {
           // _levelInfoService.GetNeighboursCells()
        }
    }
}