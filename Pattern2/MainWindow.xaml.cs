using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pattern2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        CancellationTokenSource cts;
        Semaphore sem = new Semaphore(1, 1);

        private async void Btn_Avvia_Click(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
            //WorkerAsync wrk = new WorkerAsync(10, 1000, cts);
            
            IProgress<int> progress = new Progress<int>(UpdateUI);
            WorkerProgressAsync wrk = new WorkerProgressAsync(sem, 10, 1000, cts, progress);
            //WorkerProgress wrk = new WorkerProgress(10,1000,cts,progress);
            await wrk.Start();

            MessageBox.Show("Mi dimentico del Thread secondario e non attendo il thread secondario per visualizzare questo messaggio");
        }

        private void UpdateUI(int i)
        {
            Lbl_Ris.Content = i.ToString();
        }

        private void Btn_Ferma_Click(object sender, RoutedEventArgs e)
        {
            if (cts != null)
            {
                cts.Cancel();
            }
        }
    }
}
