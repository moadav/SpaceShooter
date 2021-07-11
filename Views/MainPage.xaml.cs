using SpaceShooter.DataCRUD;
using SpaceShooter.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SpaceShooter
{
    /// <summary>
    /// This page handles the user functionality when the user logs on
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Gets the main page view model.
        /// </summary>
        /// <returns>
        /// The main page view model.
        /// </returns>
        public MainPageViewModel MainPageViewModel { get; } = new MainPageViewModel();
        /// <summary>
        /// Gets the spill application view model.
        /// </summary>
        /// <returns>
        /// The spill application view model.
        /// </returns>
        public SpillAppViewModel SpillAppViewModel { get; } = new SpillAppViewModel();
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = MainPageViewModel;
            MainPageViewModel.GetScreenElements(HighScoreScreen, EditPassword, EditUsername, EditScreen, WelcomeScreen, Return);
            MainPageViewModel.GetTextBox(newUsername, newPassword);
            MainPageViewModel.GetErrorMessage(UsernameErrorMessage, PasswordErrorMessage);
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        /// <summary>
        /// Initiates the game.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void InitiateGame(object sender, RoutedEventArgs e)
        {
            SpillAppViewModel.Restart_Game();
            this.Frame.Navigate(typeof(SpillApp), null);

        }
        /// <summary>
        /// Logs the user off.
        /// </summary>
        public void LogOff()
        {
            this.Frame.Navigate(typeof(LoginRegister), null);
        }
        /// <summary>
        /// Deletes the user.
        /// </summary>
        public async void DeleteUser()
        {
            await ApiData.DeleteUserAsync(LoginViewModel.Loggedin);
            LogOff();
            MainPageViewModel.EmptyAllMessages();
            MainPageViewModel.ReturnButton();
          
        }
    }
}
