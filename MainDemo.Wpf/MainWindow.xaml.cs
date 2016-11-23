using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
using System.Windows.Threading;
using System;
using MaterialDesignDemo;

namespace MaterialDesignColors.WpfExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Lazy<MainWindow> lazy = new Lazy<MainWindow>(() => new MainWindow());
        public static MainWindow mainWindow { get { return lazy.Value; } }
        private MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
        }
        private void InitializeTimer()
        {
            timer.Text = DateTime.Now.ToString("hh:mm:ss tt");

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuToggleButton.IsChecked = false;
         
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            timer.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private async void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {
            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = {Text = ((ButtonBase) sender).Content.ToString()}
            };

            await DialogHost.Show(sampleMessageDialog, "RootDialog");            
        }
        private LoginWindow login;

        public void SetLoginForm(LoginWindow login)
        {
            this.login = login;
        }
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                login.Hide();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Hide();
            LoginWindow.loginForm.Show();
        }
    } 
}
