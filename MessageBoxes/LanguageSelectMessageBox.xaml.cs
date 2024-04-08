using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace EnglishScience
{
    /// <summary>
    /// Логика взаимодействия для CustomMessageBox.xaml
    /// </summary>
    public partial class LanguageSelectMessageBox : Window
    {
        private readonly Window _window;
        public string SelectedLanguage { get; private set; }

        public LanguageSelectMessageBox(string Message, string Header, Window window)
        {
            InitializeComponent();
            _window = window;
            txtMessage.Text = Message;
            txtTitle.Text = Header;

            ApplyBlurEffect();
            this.ShowDialog();

        }

        private void btnEng_Click(object sender, RoutedEventArgs e)
        {
            SelectedLanguage = "english";
            this.DialogResult = true;
            this.Close();
            ClearBlurEffect();
        }

        private void btnPl_Click(object sender, RoutedEventArgs e)
        {
            SelectedLanguage = "polish";
            this.DialogResult = false;
            this.Close();
            ClearBlurEffect();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
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