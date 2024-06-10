using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MOVEf_WPF
{
    public partial class Options : Page
    {
        //private MainWindow parentWin;
        //public MainWindow ParentWin { get => parentWin; set => parentWin = value; }
        public Options()
        {
            InitializeComponent();


        }


        private void back_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.Content = mainWindow.pnlMainGrid;
            //MainWindow home = new MainWindow();
            //home.Show();
            //ParentWin.Close();
        }
    }
}
