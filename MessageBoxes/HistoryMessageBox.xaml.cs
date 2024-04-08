using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Effects;


namespace EnglishScience
{
    /// <summary>
    /// Логика взаимодействия для CustomMessageBox.xaml
    /// </summary>
    public partial class HistoryMessageBox : Window
    {
        private Window _window;
        public HistoryMessageBox(List<string> translateHistory, Window window)
        {
            InitializeComponent();
            _window = window;
            btnOk.Visibility = Visibility.Visible;

            if (translateHistory == null || translateHistory.Count == 0)
                translateHistoryListBox.ItemsSource = new List<string> { "History is empty." };
            else
                translateHistoryListBox.ItemsSource = translateHistory;

            ApplyBlurEffect(); 
            this.ShowDialog();

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
            ClearBlurEffect();
        }

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void ApplyBlurEffect()
        {
            BlurEffect objBlur = new BlurEffect();
            objBlur.Radius = 5;
            _window.Effect = objBlur;
        }

        private void ClearBlurEffect()
        {
            _window.Effect = null;
        }
    }


}
