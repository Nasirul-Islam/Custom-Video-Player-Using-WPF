using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VideoPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int flag = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        // Load a video file when the window loads (for testing)
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mediaElement.Source = new Uri("C:/path_to_your_video.mp4");
        }

        // Open the file dialog to select a video file
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".mp4"; // Default file extension
            dlg.Filter = "Media Files|*.mp4;*.wmv;*.avi"; // Filter for video formats

            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                // Set the selected file to the media element's source
                mediaElement.Source = new Uri(dlg.FileName);
                mediaElement.Play();
            }
        }

        // Volume Slider
        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.Volume = volumeSlider.Value;
            // Update the volume percentage display
            int volumePercentage = (int)(volumeSlider.Value * 100);
            volumePercentageText.Text = $"Volume: {volumePercentage}%";
        }

        // Play Button
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
        }

        // Pause Button
        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
        }

        // Stop Button
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
        }

        // Skip Forward 10 seconds
        private void SkipForwardButton_Click(object sender, RoutedEventArgs e)
        {
            SkipForward();
        }

        // Skip Backward 10 seconds
        private void SkipBackwardButton_Click(object sender, RoutedEventArgs e)
        {
            SkipBackward();
        }

        // Method to handle key down events (keyboard input)
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Check for the Right arrow key press
            if (e.Key == Key.Right)
            {
                SkipForward();
            }
            // Check for the Left arrow key press
            else if (e.Key == Key.Left)
            {
                SkipBackward();
            }
            // Check if the C key is pressed
            if (e.Key == Key.C && flag == 0)
            {
                mediaElement.Pause();
                flag = 1;
            }
            else if (e.Key == Key.C && flag == 1)
            {
                mediaElement.Play();
                flag = 0;
            }
            // Check if the up arrow key is pressed
            if (e.Key == Key.Up)
            {
                if (volumeSlider.Value < 1) // Ensure volume doesn't exceed 100%
                {
                    volumeSlider.Value += 0.05; // Increase volume by 10%
                }
            }
            // Check if the down arrow key is pressed
            else if (e.Key == Key.Down)
            {
                if (volumeSlider.Value > 0) // Ensure volume doesn't go below 0%
                {
                    volumeSlider.Value -= 0.05; // Decrease volume by 10%
                }
            }
        }

        // Helper method to skip forward 10 seconds
        private void SkipForward()
        {
            if (mediaElement.Source != null && mediaElement.NaturalDuration.HasTimeSpan)
            {
                TimeSpan newPosition = mediaElement.Position + TimeSpan.FromSeconds(10);
                if (newPosition < mediaElement.NaturalDuration.TimeSpan)
                {
                    mediaElement.Position = newPosition;
                }
                else
                {
                    mediaElement.Position = mediaElement.NaturalDuration.TimeSpan;
                }
            }
        }

        // Helper method to skip backward 10 seconds
        private void SkipBackward()
        {
            if (mediaElement.Source != null)
            {
                TimeSpan newPosition = mediaElement.Position - TimeSpan.FromSeconds(10);
                if (newPosition > TimeSpan.Zero)
                {
                    mediaElement.Position = newPosition;
                }
                else
                {
                    mediaElement.Position = TimeSpan.Zero;
                }
            }
        }
    }
}