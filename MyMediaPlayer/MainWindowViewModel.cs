using System;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using System.Windows.Input;

namespace MyMediaPlayer
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private MediaState mediaState = MediaState.Stop;
        private string filePath = string.Empty;
        private double volume =50;

        public ICommand PlayCommand { get; private set; }
        public ICommand PauseCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        public ICommand BrowseCommand { get; private set; }

        public MediaState MediaState
        {
            get => mediaState;
            set
            {
                if (mediaState != value)
                {
                    mediaState = value;
                    OnPropertyChanged(nameof(MediaState));
                }
            }
        }

        public string FilePath
        {
            get => filePath;
            set
            {
                if (filePath != value)
                {
                    filePath = value;
                    OnPropertyChanged(nameof(FilePath));
                    OnPropertyChanged(nameof(SourceUri));
                }
            }
        }

        public Uri SourceUri
        {
            get
            {
                if (string.IsNullOrEmpty(FilePath))
                {
                    return null;
                }
                else
                {
                    return new Uri(FilePath);
                }
            }
        }

        public double Volume
        {
            get => volume;
            set
            {
                if (volume != value)
                {
                    volume = Math.Round(value);
                    OnPropertyChanged(nameof(Volume));
                }
            }
        }

        public MainWindowViewModel()
        {
            PlayCommand = new RelayCommand(x => true, Play);
            PauseCommand = new RelayCommand(x => true, Pause);
            StopCommand = new RelayCommand(x => true, Stop);
            BrowseCommand = new RelayCommand(x => true, Browse);
        }

        // Play Button Code
        private void Play(object sender)
        {
            // start the video using LoadedBehiour
            MediaState = MediaState.Play;
        }

        // Pause Button Code
        private void Pause(object sender)
        {
            // pause the media element (me) using same LoadedBehiour property
            MediaState = MediaState.Pause;
        }

        // Stop Button Code
        private void Stop(object sender)
        {
            // stop the running media element using same LoadedBehiour property
            MediaState = MediaState.Stop;
        }

        // Broswe Button Code
        private void Browse(object sender)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                // set the filters
                fileDialog.Filter = "MP3 Files (*.mp3)|*.mp3|MP4 File (*.mp4)|*.mp4|3GP File (*.3gp)|*.3gp|Audio File (*.wma)|*.wma|MOV File (*.mov)|*.mov|AVI File (*.avi)|*.avi|Flash Video(*.flv)|*.flv|Video File (*.wmv)|*.wmv|MPEG-2 File (*.mpeg)|*.mpeg|WebM Video (*.webm)|*.webm|All files (*.*)|*.*";
                // set the initial directory optional
                fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                // display the dialog
                fileDialog.ShowDialog();
                // get the currently selected video / audio file path and store it in string variable
                string filename = fileDialog.FileName;
                if (filename != "")
                {
                    // display this path of selected video / audio path to the text box called "tb"
                    FilePath = filename;

                    MediaState = MediaState.Play;
                }
                else
                {
                    MessageBox.Show("No Selection", "Empty");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show("Error Text: " + e1.Message);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
