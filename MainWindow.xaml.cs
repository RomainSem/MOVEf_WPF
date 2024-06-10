using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;

namespace MOVEf_WPF
{
    public partial class MainWindow : Window
    {
        List<string> vidExtensions { get; } = new() { ".mp4", ".m4v", ".3gp", ".m4p", ".mpg", ".mpeg", ".mp2", ".mpe", ".mpv", ".avi", ".mkv", ".mov", ".qt", ".wmv", ".flv", ".swf", ".webm", ".ogg", ".avchd" };
        List<string> imgExtensions { get; } = new() { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".psd", ".ai", ".svg", ".ico", ".webp" };
        List<string> audioExtensions { get; } = new() { ".mp3", ".wav", ".wma", ".flac", ".aac", ".alac", ".aiff", ".dsd", ".pcm", ".m4a", ".m4b", ".m4r", ".mka", ".amr", ".ape", ".dts", ".m2ts", ".m2v", ".m3u", ".m3u8", ".mka", ".mpg", ".mts", ".ts", ".vob", };
        List<List<string>> allCategories { get; } = new();

        List<string> filesNotSupported { get; } = new();

        Color color = (Color)ColorConverter.ConvertFromString("#FF313338");


        public MainWindow()
        {
            InitializeComponent();
            App.ParentWindowRef = this;
            allCategories.Add(vidExtensions);
            allCategories.Add(imgExtensions);
            allCategories.Add(audioExtensions);

            txt.Text = "Drag and drop files here";
            //myCommandLineArgs = Environment.GetCommandLineArgs();

        }

        private void pnlMainGrid_DragEnter(object sender, DragEventArgs e)
        {
            pnlMainGrid.Background = Brushes.DarkGray;
            txt.Background = Brushes.DarkGray;
            lvEntries.Background = Brushes.DarkGray;
        }

        private void pnlMainGrid_DragLeave(object sender, DragEventArgs e)
        {
            SolidColorBrush solidColorBrush = new SolidColorBrush(color);
            pnlMainGrid.Background = solidColorBrush;
            txt.Background = solidColorBrush;
            lvEntries.Background = solidColorBrush;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Options options = new Options();
            this.Content = options;
            options.ParentWin = this;
        }

        private void pnlMainGrid_Drop(object sender, DragEventArgs e)
        {
            SolidColorBrush solidColorBrush = new SolidColorBrush(color);
            pnlMainGrid.Background = solidColorBrush;
            txt.Background = solidColorBrush;
            lvEntries.Background = solidColorBrush;

            string location;
            //char separator = Path.DirectorySeparatorChar;

            if (e.Data.GetData(DataFormats.FileDrop, true) is string[] droppedFilesPaths)
            {
                foreach (var file in droppedFilesPaths)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    location = fileInfo.FullName /*fileInfo.DirectoryName + separator + fileInfo.Name*/;
                    Item item = new Item()
                    {
                        Name = fileInfo.Name,
                        Path = location,
                        Extension = fileInfo.Extension
                    };
                    using (var fs = fileInfo.OpenRead())
                    {
                        try
                        {
                            lvEntries.Items.Add(location);
                            MoveFilesToNewDir(item);
                            //txt.Visibility = Visibility.Hidden;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void MoveFilesToNewDir(Item item)
        {
            string newFilePath = @"C:\Users\romai\Documents\"; // CHANGE WITH USER'S DESIRED PATH
            bool flag = false;

            foreach (var list in allCategories)
            {
                if (list != null)
                {
                    foreach (var extension in list)
                    {
                        if (extension.Contains(item.Extension))
                        {
                            //MessageBox.Show(allCategories.IndexOf(list).ToString());
                            switch (allCategories.IndexOf(list))
                            {
                                case 0: // Videos
                                    item.Category = "Videos";
                                    item.NewPath = newFilePath + "Vidéos\\" + item.Name;
                                    flag = true;
                                    break;

                                case 1: // Images
                                    item.Category = "Images";
                                    item.NewPath = newFilePath + /*"Images\\" +*/ item.Name;
                                    flag = true;
                                    break;

                                case 2: // Audio
                                    item.Category = "Music";
                                    item.NewPath = newFilePath + "Musique\\" + item.Name;
                                    flag = true;
                                    break;

                                default:
                                    filesNotSupported.Add(item.Path);
                                    MessageBox.Show(item.Extension + " File extension not supported.");
                                    flag = true;
                                    break;
                            }
                            if (flag)
                            {
                                break;
                            }
                        }
                    }
                }
                if (flag)
                {
                    break;
                }
            }

            char[] driveChar = item.Path.ToCharArray();
            char[] newPathDriveChar = item.NewPath.ToCharArray();

            if (driveChar[0] == newPathDriveChar[0])
            {
                File.Move(item.Path, item.NewPath);
                MessageBox.Show("ALLO");
            }
            else
            {
                File.Copy(item.Path, item.NewPath);
                MessageBox.Show("ALLO222222");
            }


            //File.Delete(actualFilePath);
            txt.Visibility = Visibility.Visible;
            txt.Text = "Files moved successfully to " + newFilePath;
        }
    }


}
