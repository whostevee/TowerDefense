namespace _Project.Scripts.Back
{
    public class Timer
    {
        private float _accumulator;
        public float Delay { get; set; }

        public Timer(float delay)
        {
            Delay = delay;
        }

        public bool Tick(float deltaTime)
        {
            _accumulator += deltaTime;
            if (_accumulator < Delay)
            {
                return false;
            }

            _accumulator = 0f;
            return true;
        }

        public void SetReady()
        {
            _accumulator = Delay;
        }
    }
}