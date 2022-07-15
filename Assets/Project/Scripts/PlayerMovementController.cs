namespace Project.Scripts
{
    public class PlayerMovementController 
    {
        private PlayerFacade _playerFacade;

        private IPlayerMovement _playerMovement;

        public PlayerMovementController(PlayerFacade playerFacade)
        {
            _playerFacade = playerFacade;
            
            playerFacade.SpawnPlayer();
        }

        public void SetPlayerMovement(IPlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
        }

        public void Move()
        {
            _playerMovement.Move();
        }
    }
}   