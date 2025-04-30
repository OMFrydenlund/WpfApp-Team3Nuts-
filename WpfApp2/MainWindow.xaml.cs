using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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

        private bool isManualChange = false;

        public MainWindow()
        {
            InitializeComponent();
            debug();
    

            // Initialize NAudio
            deviceEnumerator = new MMDeviceEnumerator();  // Create a new instance to manage audio devices
            defaultDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            // Get the default audio output device (like speakers or headphones)


        }

        

        public void debug()
        {
            if (VolumeTextBlock.IsLoaded)
            {
                VolumeTextBlock.Text = "It's loaded!";
            }
            else
            {
                VolumeTextBlock.Loaded += (s, e) => { VolumeTextBlock.Text = "Now it's loaded!"; };
            }
        }

        

        public void MouseOverMove(object sender, MouseEventArgs e)
        {
            
                
                float randomVolume = random.Next(0, 101);
                VolumeSlider.Value = randomVolume; // Set to random value
                
            
        }


        public void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            if (VolumeTextBlock.IsLoaded)
            {
                SetSystemVolume((float)e.NewValue / 100);
                VolumeTextBlock.Text = GetRandomSpan((float)e.NewValue);
            }
        }

        public string GetRandomSpan(float randomVolume)
        {
            float minValue = randomVolume - random.Next(0, 20);
            float maxValue = randomVolume + random.Next(0, 20);

            return $"Your volume is between {minValue} and {maxValue}";
        }

        private void SetSystemVolume(float volume)
        {
            if (defaultDevice == null)
            {
                return;
            }
            // Sets the volume to the system's master volume level
            defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = volume;
        }
    }
}