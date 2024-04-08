using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace EnglishScience
{
    /// <summary>
    /// Логика взаимодействия для CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        private readonly Window _window;
        public CustomMessageBox(string Message, string Header, MessageType Type, MessageButtons Buttons, Window window)
        {
            InitializeComponent();
            _window = window;

            txtMessage.Text = Message;
            switch (Type)
            {
                case MessageType.Info:
                    txtTitle.Text = "Info." + " " + Header;
                    break;

                case MessageType.Confirmation:
                    txtTitle.Text = "Confirmation" + " " + Header;
                    break;

                case MessageType.Success:
                    {
                        txtTitle.Text = "Success" + " " + Header;
                    }
                    break;

                case MessageType.Warning:
                    txtTitle.Text = "Warning" + " " + Header;
                    break;

                case MessageType.Error:
                    {
                        txtTitle.Text = "Error!" + " " + Header;
                    }
                    break;

                case MessageType.Custom:
                    txtTitle.Text = Header;
                    break;
            }

            switch (Buttons)
            {
                case MessageButtons.OkCancel:
                    btnYes.Visibility = Visibility.Collapsed; btnNo.Visibility = Visibility.Collapsed;
                    break;

                case MessageButtons.YesNo:
                    btnOk.Visibility = Visibility.Collapsed; btnCancel.Visibility = Visibility.Collapsed;
                    break;

                case MessageButtons.Ok:
                    btnOk.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Collapsed;
                    btnYes.Visibility = Visibility.Collapsed; btnNo.Visibility = Visibility.Collapsed;
                    break;
            }
            ApplyBlurEffect();
            this.ShowDialog();

        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
            ClearBlurEffect();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
            ClearBlurEffect();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
            ClearBlurEffect();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
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

    public enum MessageType
    {
        Info,
        Confirmation,
        Success,
        Warning,
        Error,
        Custom
    }

    public enum MessageButtons
    {
        OkCancel,
        YesNo,
        Ok,
    }

}