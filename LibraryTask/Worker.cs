using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryTask
{
    public class Worker
    {
        CancellationTokenSource _cts;
        int _max;
        int _delay;

        //Costruttore
        public Worker(int max, int delay, CancellationTokenSource cts)
        {
            _max = max;
            _delay = delay;
            _cts = cts;
        }

        public void Start()
        {
            Task.Factory.StartNew(DoWork);
        }

        private void DoWork()
        {
            for (int i = 0; i < _max; i++)
            {
                Thread.Sleep(_delay);

                if (_cts.IsCancellationRequested)
                {
                    break;
                }
            }
        }
    }
}
