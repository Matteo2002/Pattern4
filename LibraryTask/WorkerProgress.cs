using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryTask
{
    public class WorkerProgress
    {
        CancellationTokenSource _cts;

        //Interfaccia (in questo caso aggiorna l'avanzamento del conteggio, quindi ci dirà a che punto è arrivato il nostro Thread secondario
        IProgress<int> _progress;

        int _max;
        int _delay;

        //Costruttore
        public WorkerProgress(int max, int delay, CancellationTokenSource cts, IProgress<int> progress)
        {
            _max = max;
            _delay = delay;
            _cts = cts;
            _progress = progress;
        }

        public void Start()
        {
            Task.Factory.StartNew(DoWork);
        }

        private void DoWork()
        {
            for (int i = 0; i < _max; i++)
            {
                NotifyProgress(_progress, i);
                Thread.Sleep(_delay);

                if (_cts.IsCancellationRequested)
                {
                    break;
                }
            }
        }

        private void NotifyProgress(IProgress<int> progress, int i)
        {
            progress.Report(i);
        }
    }
}
