using System;
using System.Diagnostics;
using System.Windows;

namespace WPF_WSL2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            string command = CommandInput.Text;

            if (string.IsNullOrEmpty(command))
            {
                MessageBox.Show("Please enter a command.");
                return;
            }

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "wsl",
                    Arguments = $"-d Ubuntu {command}", // Execute the command from the TextBox
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();

            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();
                OutputBox.Items.Add(line);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            OutputBox.Items.Clear(); // Clears the output ListBox
        }

    }
}
