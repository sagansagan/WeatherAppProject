namespace WeatherAppProject
{
    public class Counter
    {
        private int _callcount = 0;
        private readonly object _lock = new object();

        public int Callcount 
        { 
            get { return _callcount; } 
            set { _callcount = value; }
        }

        public void Increment()
        {
            lock (_lock)
            {
                _callcount++;
            }
        }
    }
}
