using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SpaceShooter.Helpers;
using SpaceShooter.LoginModel;
using SpaceShooter.DataCRUD;

namespace SpaceShooter.ViewModels
{
    /// <summary>
    /// class that helps the LoginViewModel page to abstract some methods
    /// </summary>
    /// <seealso cref="SpaceShooter.Helpers.Observable" />
    public class LoginViewModel : Observable
    {

        /// <summary>
        /// Gets or sets the start screen.
        /// </summary>
        /// <returns>
        /// a canvas element containing the Startscreen
        /// </returns>
        private Canvas StartScreen { get; set; }
        /// <summary>
        /// Gets or sets the login screen.
        /// </summary>
        /// <returns>
        /// a canvas element containing the login screen
        /// </returns>
        private Canvas ALoginScreen { get; set; }
        /// <summary>
        /// Gets or sets the register screen
        /// </summary>
        /// <returns>
        /// a canvas element containing the register screen
        /// </returns>
        private Canvas ARegisterScreen { get; set; }
        /// <summary>
        /// Gets or sets the return button.
        /// </summary>
        /// <returns>
        /// a button which the user can interact with
        /// </returns>
        private Button AReturnButton { get; set; }
        /// <summary>
        /// Gets or sets the register username.
        /// </summary>
        /// <returns>
        /// a textbox element which registers the username
        /// </returns>
        private TextBox RegisterUsername { get; set; }
        /// <summary>
        /// Gets or sets the register password.
        /// </summary>
        /// <returns>
        /// a textbox element which registers the password
        /// </returns>
        private TextBox RegisterPassword { get; set; }
        /// <summary>
        /// Gets or sets the login username.
        /// </summary>
        /// <returns>
        /// a textbox element that checks if login username information is correct
        /// </returns>
        private TextBox LoginUsername { get; set; }
        /// <summary>
        /// Gets or sets the login password.
        /// </summary>
        /// <returns>
        /// a textbox element that checks if login password information is correct
        /// </returns>
        private TextBox LoginPassword { get; set; }
        /// <summary>
        /// Gets or sets the register notification message.
        /// </summary>
        /// <returns>
        /// a TextBlock element which notifies the user of registering issues
        /// </returns>
        private TextBlock RegisterNotificationMessage { get; set; }
        /// <summary>
        /// Gets or sets the login notification message.
        /// </summary>
        /// <returns>
        /// a TextBlock element which notifies the user of login issues
        /// </returns>
        private TextBlock LoginNotificationMessage { get; set; }
        /// <summary>
        /// Gets or sets the loggedin.
        /// </summary>
        /// <returns>
        /// gets the user that has logged on as a User object.
        /// </returns>
        public static User Loggedin { get; set; }
        /// <summary>
        /// Gets the elements that is used on the LoginRegister Page
        /// </summary>
        /// <param name="startScreen">The startscreen Canvas element <see cref="Canvas"/></param>
        /// <param name="login">The login Canvas element. <see cref="Canvas"/></param>
        /// <param name="returnB"> the return button element from the page <see cref="Button"/></param>
        /// <param name="registerScreen">The RegisterScreen Canvas element <see cref="Canvas"/></param>
        public void getObjectsFromScreen(Canvas startScreen, Canvas login, Button returnB, Canvas registerScreen)
        {
            this.StartScreen = startScreen;
            ALoginScreen = login;
            AReturnButton = returnB;
            ARegisterScreen = registerScreen;
        }
        /// <summary>
        /// Gets the notification Text blocks
        /// </summary>
        /// <param name="registerErrorMessage">the register message Textblock <see cref="TextBlock"/>.</param>
        /// <param name="loginErrorMessage"> the login message textblock <see cref="TextBlock"/> .</param>
        public void GetNotification(TextBlock registerErrorMessage, TextBlock loginErrorMessage)
        {
            RegisterNotificationMessage = registerErrorMessage;
            LoginNotificationMessage = loginErrorMessage;
        }
        /// <summary>
        /// Gets the Text box elements for registerscreen
        /// </summary>
        /// <param name="registerErrorMessage">the register username Textbox <see cref="TextBox"/>.</param>
        /// <param name="loginErrorMessage"> the register password textbox <see cref="TextBox"/> .</param>
        public void RegisterScreen(TextBox registerUsername, TextBox registerPassword)
        {
            RegisterUsername = registerUsername;
            RegisterPassword = registerPassword;
        }
        /// <summary>
        /// Gets the Text box elements for loginscreen
        /// </summary>
        /// <param name="registerErrorMessage">the login username Textbox <see cref="TextBox"/>.</param>
        /// <param name="loginErrorMessage"> the login password textbox <see cref="TextBox"/> .</param>
        public void LoginScreen(TextBox loginUsername, TextBox loginPassword)
        {
            LoginUsername = loginUsername;
            LoginPassword = loginPassword;

        }
        /// <summary>
        /// takes the user to the login screen
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void Login(object sender, RoutedEventArgs e)
        {
            StartScreen.Visibility = Visibility.Collapsed;
            ALoginScreen.Visibility = Visibility.Visible;
            AReturnButton.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// takes the user to the register screen
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void Register(object sender, RoutedEventArgs e)
        {
            StartScreen.Visibility = Visibility.Collapsed;
            ARegisterScreen.Visibility = Visibility.Visible;
            AReturnButton.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// returns the user back to startscreen
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void Return(object sender, RoutedEventArgs e)
        {
            ALoginScreen.Visibility = Visibility.Collapsed;
            ARegisterScreen.Visibility = Visibility.Collapsed;
            StartScreen.Visibility = Visibility.Visible;
            EmptyAllTexts();
        }
        /// <summary>
        /// Empties the register text boxes.
        /// </summary>
        public void EmptyRegisterTexts()
        {
            RegisterUsername.Text = string.Empty;
            RegisterPassword.Text = string.Empty;
        }
        /// <summary>
        /// Empties the login text boxes.
        /// </summary>
        private void EmptyLoginTexts()
        {
            LoginUsername.Text = string.Empty;
            LoginPassword.Text = string.Empty;
        }
        /// <summary>
        /// Empties all text boxes.
        /// </summary>
        private void EmptyAllTexts()
        {
            EmptyRegisterTexts();
            EmptyLoginTexts();
            EmptyRegisterErrorMessage();
            EmptyLoginErrorMessage();
        }
        /// <summary>
        /// Registers the user to the database through api
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void ConfirmAccount(object sender, RoutedEventArgs e)
        {
            if (!IfUserIsTaken())
            {
                ApiData.CreateUserAsync(CreateUser());
                UserIsRegisteredMessage();
                EmptyRegisterTexts();
            }
            else
                UserIsTakenMessage();
        }
        /// <summary>
        /// User is taken message.
        /// </summary>
        private void UserIsTakenMessage() =>
          RegisterNotificationMessage.Text = "Username is taken!";
        /// <summary>
        /// User is registered message.
        /// </summary>
        private void UserIsRegisteredMessage() =>
             RegisterNotificationMessage.Text = "User Registered";
        /// <summary>
        /// notifies that the user does not exist
        /// </summary>
        public void UserDoesntExistMessage()
        {
            LoginNotificationMessage.Text = "User Does not Exist";
        }
        /// <summary>
        /// Logins the user.
        /// </summary>
        /// <param name="user">The user.</param>
        private void LoginTheUser(User user)
        {
            Loggedin = user;
        }
        /// <summary>
        /// Checks whether the user exists, if it exists login the user if not send a error message to the user
        /// </summary>
        /// <returns>
        /// true whether the user exists and false if not
        /// </returns>
        public bool LoginUser()
        {
            if (!LoginUsername.Text.Equals(string.Empty) && !LoginPassword.Text.Equals(string.Empty))
            {
                foreach (User user in ApiData.TheUsers)
                {
                    if (user.Username.Equals(LoginUsername.Text) && user.Password.Equals(LoginPassword.Text))
                    {
                        LoginTheUser(user);
                        EmptyAllTexts();
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the username is taken
        /// </summary>
        /// <returns>
        /// true if the username is taken and false if not
        /// </returns>
        private bool IfUserIsTaken()
        {
            foreach (User user in ApiData.TheUsers)
            {
                if (user.Username.Equals(RegisterUsername.Text))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <returns>
        /// a User object <see cref="User"/>
        /// </returns>
        private User CreateUser()
        {
            User newUser = new User
            {
                Username = RegisterUsername.Text,
                Password = RegisterPassword.Text
            };
            return newUser;
        }
        /// <summary>
        /// Empties the login error message.
        /// </summary>
        private void EmptyLoginErrorMessage()
        => LoginNotificationMessage.Text = string.Empty;

        /// <summary>
        /// Empties the register error message.
        /// </summary>
        private void EmptyRegisterErrorMessage() =>
            RegisterNotificationMessage.Text = string.Empty;
    }
}












