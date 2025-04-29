using System.Data;
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
using NAudio.CoreAudioApi;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {       
        private Random random = new Random();
        //NAudio
        private MMDeviceEnumerator deviceEnumerator; // This will enumerate audio devices
        private MMDevice defaultDevice; // This represents the default audio output device

        public MainWindow()
        {
            InitializeComponent();

            // Initialize NAudio
            deviceEnumerator = new MMDeviceEnumerator();  // Create a new instance to manage audio devices
            defaultDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            // Get the default audio output device (like speakers or headphones)

            // Check if defaultDevice is null
            if (defaultDevice == null)
            {
                VolumeTextBlock.Text = "No default audio device found. Please check your audio settings.";
                return; // Exit if no audio device is available
            }
            else
            {
                // If a device is found, display information about it
                VolumeTextBlock.Text = $"Default Audio Device: {defaultDevice.DeviceFriendlyName}";
                VolumeTextBlock.Text += $"State: {defaultDevice.State}";
                VolumeTextBlock.Text += $"Volume Level: {defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar * 100}%";
            }

            VolumeTextBlock.Text = $"Volume: {VolumeSlider.Value}"; // This can proceed if defaultDevice is valid

        }

        public void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.NewValue != e.OldValue)
            {
                float randomVolume = random.Next(0, 101);
                VolumeSlider.Value = randomVolume;
                SetSystemVolume(randomVolume / 100);
                VolumeTextBlock.Text = $"Volume: {randomVolume.ToString()}";
            }
        }

        private void SetSystemVolume(float volume)
        {
            // Sets the volume to the system's master volume level
            defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = volume;
        }
    }
}