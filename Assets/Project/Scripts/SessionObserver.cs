namespace Project.Scripts
{
    public class SessionObserver
    {
        private int _level = 1;
        public int Level => _level;

        public void SetLevel(int level)
        {
            _level = level;
        }
    }
}