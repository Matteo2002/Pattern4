using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryTask
{
    public class WorkerAsync
    {
        CancellationTokenSource _cts;
        int _max;
        int _delay;

        //Costruttore
        public WorkerAsync(int max, int delay, CancellationTokenSource cts)
        {
            _max = max;
            _delay = delay;
            _cts = cts;
        }

        public async Task Start()
        {
            await Task.Factory.StartNew(DoWork);
        }
        //async Tast<T>

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
