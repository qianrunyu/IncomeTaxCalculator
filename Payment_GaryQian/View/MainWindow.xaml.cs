using System.Windows;
using System.Windows.Input;
using Payment_GaryQian.ViewModel;

namespace Payment_GaryQian
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainEntry : Window
    {
        private MainWindowViewModel vm;
        public MainEntry(MainWindowViewModel vm)
        {
            this.vm = vm;
            InitializeComponent();
            this.DataContext = this.vm;
        }
        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
