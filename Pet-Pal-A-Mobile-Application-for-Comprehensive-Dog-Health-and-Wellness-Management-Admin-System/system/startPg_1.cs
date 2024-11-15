using system;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgressBarExample
{
    public partial class startPg : Form
    {
        public startPg()
        {
            InitializeComponent();
            progressBar.Maximum = 100; // Set the maximum value of the ProgressBar
        }

        private Task ProcessData(List<string> list, IProgress<ProgressReport> progress)
        {
            int totalProcess = list.Count;
            return Task.Run(() =>
            {
                for (int i = 0; i < totalProcess; i++)
                {
                    var progressReport = new ProgressReport { PercentComplete = ((double)i / totalProcess) * 100 };
                    progress.Report(progressReport);
                    System.Threading.Thread.Sleep(10);
                }
            });
        }

        private async void btnStart_Click_1(object sender, EventArgs e)
        {
            btnStart.Enabled = false; // Disable the start button onece click
            List<string> list = new List<string>();
            for (int i = 0; i < 500; i++)
                list.Add(i.ToString());

            var progress = new Progress<ProgressReport>(report =>
            {
                if (progressBar.InvokeRequired)
                {
                    progressBar.Invoke(new Action(() =>
                    {
                        progressBar.Value = (int)report.PercentComplete;
                    }));
                }
                else
                {
                    progressBar.Value = (int)report.PercentComplete;
                }
            });

            try
            {
                await ProcessData(list, progress);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                btnStart.Enabled = true; // Re-enable the start button
            }

            this.Hide();

            registration regisForm = new registration();
            regisForm.Show();
        }

        private void startPg_Load(object sender, EventArgs e)
        {

        }
    }

    public class ProgressReport
    {
        public double PercentComplete { get; set; }
    }
}
