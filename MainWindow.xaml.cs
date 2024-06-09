using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace MOVEf_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> vidExtensions { get; } = new() { ".mp4", ".m4v", ".3gp", ".m4p", ".mpg", ".mpeg", ".mp2", ".mpe", ".mpv", ".avi", ".mkv", ".mov", ".qt", ".wmv", ".flv", ".swf", ".webm", ".ogg", ".avchd" };
        List<string> imgExtensions { get; } = new() { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".psd", ".ai", ".svg", ".ico", ".webp" };
        List<string> audioExtensions { get; } = new() { ".mp3", ".wav", ".wma", ".flac", ".aac", ".alac", ".aiff", ".dsd", ".pcm", ".m4a", ".m4b", ".m4r", ".mka", ".amr", ".ape", ".dts", ".m2ts", ".m2v", ".m3u", ".m3u8", ".mka", ".mpg", ".mts", ".ts", ".vob", };
        List<List<string>> allCategories { get; } = new();

        List<string> filesNotSupported { get; } = new();

        ObservableCollection<string> droppedFilesPaths { get; set; }
        //List<string> droppedFilesPaths { get; } = new();
        Color color = (Color)ColorConverter.ConvertFromString("#FF313338");


        public MainWindow(/*string[] myCommandLineArgs*/)
        {
            InitializeComponent();
            droppedFilesPaths = new ObservableCollection<string>();
            txt.Text = "Drag and drop files here";

            allCategories.Add(vidExtensions);
            allCategories.Add(imgExtensions);
            allCategories.Add(audioExtensions);

            //myCommandLineArgs = Environment.GetCommandLineArgs();



            //string actualFilePath = myCommandLineArgs[1];
            //string fileExtension = Path.GetExtension(actualFilePath);
            string newFilePath = @"C:\Users\romai\Documents\";

            //Console.WriteLine(actualFilePath);
            //Console.WriteLine("File extension: " + fileExtension);

            //foreach (var list in allCategories)
            //{
            //    if (list != null)
            //    {
            //        foreach (var extension in list)
            //        {
            //            if (extension.Contains(fileExtension))
            //            {
            //                switch (list.ToString())
            //                {
            //                    case "vidExtensions":
            //                        newFilePath += "Videos\\";
            //                        break;

            //                    case "imgExtensions":
            //                        newFilePath += "Images\\";
            //                        break;

            //                    case "audioExtensions":
            //                        newFilePath += "Music\\";
            //                        break;

            //                    default:
            //                        filesNotSupported.Add(actualFilePath);
            //                        Console.WriteLine(actualFilePath + "File extension not supported.");
            //                        break;
            //                }
            //            }
            //        }
            //    }
            //}

            //File.Copy(actualFilePath, newFilePath + Path.GetFileName(actualFilePath));
            //File.Delete(actualFilePath);

            //Console.WriteLine("File moved successfully to " + newFilePath);
            //Console.ReadLine();


        }

        private void pnlMainGrid_DragEnter(object sender, DragEventArgs e)
        {
            pnlMainGrid.Background = Brushes.DarkGray;
            txt.Background = Brushes.DarkGray;

            if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
            }
            else
            {
            }
        }

        private void pnlMainGrid_DragLeave(object sender, DragEventArgs e)
        {
            SolidColorBrush solidColorBrush = new SolidColorBrush(color);
            pnlMainGrid.Background = solidColorBrush;
            txt.Background = solidColorBrush;
        }

        private void pnlMainGrid_Drop(object sender, DragEventArgs e)
        {
            SolidColorBrush solidColorBrush = new SolidColorBrush(color);
            pnlMainGrid.Background = solidColorBrush;
            txt.Background = solidColorBrush;

            string location;
            char separator = Path.DirectorySeparatorChar;

            if (e.Data.GetData(DataFormats.FileDrop, true) is string[] droppedFilesPaths)
            {
                foreach (var file in droppedFilesPaths)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    using (var fs = fileInfo.OpenRead())
                    {
                        try
                        {
                            location = fileInfo.DirectoryName + separator + fileInfo.Name;
                            txt.Visibility = Visibility.Hidden;
                            this.droppedFilesPaths.Add(location);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }

        
    }
}