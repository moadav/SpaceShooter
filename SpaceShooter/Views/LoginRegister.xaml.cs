using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SpaceShooter.ViewModels;
using SpaceShooter.DataCRUD;
using Windows.UI.Xaml.Navigation;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpaceShooter
{
    /// <summary>
    /// A page that handles login functionality
    /// </summary>
    public sealed partial class LoginRegister : Page
    {
        /// <summary>
        /// Gets the login view model.
        /// </summary>
        /// <returns>
        /// The login view model.
        /// </returns>
        public LoginViewModel LoginViewModel { get; } = new LoginViewModel();

        public LoginRegister()
        {
            this.InitializeComponent();
            Loaded += Page_LoadedAsync;
            GetElementsFromScreen();
            DataContext = LoginViewModel;
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }
        /// <summary>
        /// Gets the elements from LoginRegister xaml
        /// </summary>
        private void GetElementsFromScreen()
        {
            LoginViewModel.getObjectsFromScreen(StartScreen, LoginScreen, ReturnToLogin, RegisterScreen);
            LoginViewModel.RegisterScreen(RegisterUsername, RegisterPassword);
            LoginViewModel.GetNotification(ErrorMessage, LoginErrorMessages);
            LoginViewModel.LoginScreen(LoginUsername, LoginPassword);
        }
        /// <summary>
        /// Handles the LoadedAsync event of the Page control and gets all users <see cref="ApiData.GetAllUsersAsync()"/>
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void Page_LoadedAsync(object sender, RoutedEventArgs e) =>
            await ApiData.GetAllUsersAsync();

        /// <summary>
        /// Returns the user back to mainpPage Page
        /// </summary>
        public void LoginToMain()
        {
            if (LoginViewModel.LoginUser())
                Frame.Navigate(typeof(MainPage), null);
            else
                LoginViewModel.UserDoesntExistMessage();
        }
    }
}
