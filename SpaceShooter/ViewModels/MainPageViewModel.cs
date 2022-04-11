using SpaceShooter.DataCRUD;
using SpaceShooter.LoginModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SpaceShooter.ViewModels
{
    /// <summary>
    /// class that helps the MainPage Page abstract its methods to
    /// </summary>
    public class MainPageViewModel
    {
        /// <summary>
        /// Gets or sets the edit password section.
        /// </summary>
        /// <returns>
        /// a Canvas element containing the edit password section
        /// </returns>
        private Canvas EditPassword { get; set; }
        /// <summary>
        /// Gets or sets the edit username section.
        /// </summary>
        /// <returns>
        /// a Canvas element containing the edit username section
        /// </returns>
        private Canvas EditUsername { get; set; }
        /// <summary>
        /// Gets or sets the edit screen section.
        /// </summary>
        /// <returns>
        /// a Canvas element containing the edit screen section
        /// </returns>
        private Canvas EditScreen { get; set; }
        /// <summary>
        /// Gets or sets the welcome screen section.
        /// </summary>
        /// <returns>
        /// a Canvas element containing the welcome screen section
        /// </returns>
        private Canvas WelcomeScreen { get; set; }
        /// <summary>
        /// Gets or sets the return button.
        /// </summary>
        /// <returns>
        /// a button which the user can press
        /// </returns>
        private Button AReturnButton { get; set; }
        /// <summary>
        /// Gets or sets the new username section.
        /// </summary>
        /// <returns>
        /// a textbox element that changes the username of the user
        /// </returns>
        private TextBox NewUsername { get; set; }
        /// <summary>
        /// Gets or sets the new username section.
        /// </summary>
        /// <returns>
        /// a textbox element that changes the password of the user
        /// </returns>
        private TextBox NewPassword { get; set; }
        /// <summary>
        /// Gets or sets the username notification.
        /// </summary>
        /// <returns>
        /// a textblock element that gives the user messages about username
        /// </returns>
        private TextBlock UsernameNotification { get; set; }
        /// <summary>
        /// Gets or sets the username notification.
        /// </summary>
        /// <returns>
        /// a textblock element that gives the user messages about password
        /// </returns>
        private TextBlock PasswordNotification { get; set; }
        /// <summary>
        /// Gets or sets the stack panel.
        /// </summary>
        /// <returns>
        /// A stackpanel that handles the displaying of top 5 users
        /// </returns>
        private StackPanel StackPanel { get; set; }

        public SpillAppViewModel SpillAppViewModel { get; } = new SpillAppViewModel();
        public LoginViewModel LoginViewModel { get; } = new LoginViewModel();
        /// <summary>
        /// Gets or sets the getusers5 list
        /// </summary>
        /// <returns>
        /// returns a list of 5 User objects, which has the highest TotalScore <see cref="User.Totalscore"/>
        /// </returns>
        public ObservableCollection<User> GetUsers5 { get; set; } = new ObservableCollection<User>();
        /// <summary>
        /// Gets the screen elements for MainPage Page
        /// </summary>
        /// <param name="stackPanel">The stack panel element <see cref="Windows.UI.Xaml.Controls.StackPanel"/></param>
        /// <param name="editPassword">The editpassword element <see cref="Canvas"/></param>
        /// <param name="editUsername">The editusername element <see cref="Canvas"/></param>
        /// <param name="editScreen">The editscreen element.<see cref="Canvas"/></param>
        /// <param name="welcomeScreen">The welcomescreen element<see cref="Canvas"/></param>
        /// <param name="returnB">The Return Button element.<see cref="Button"/></param>
        public void GetScreenElements(StackPanel stackPanel, Canvas editPassword, Canvas editUsername, Canvas editScreen, Canvas welcomeScreen, Button returnB)
        {
            this.EditPassword = editPassword;
            this.EditUsername = editUsername;
            this.EditScreen = editScreen;
            this.WelcomeScreen = welcomeScreen;
            AReturnButton = returnB;
            this.StackPanel = stackPanel;
        }
        /// <summary>
        /// Gets the error message text blocks
        /// </summary>
        /// <param name="message1">The message textblock. <see cref="TextBlock"/></param>
        /// <param name="message2">The message2 textblock. <see cref="TextBlock"/></param>
        public void GetErrorMessage(TextBlock message1, TextBlock message2)
        {
            PasswordNotification = message2;
            UsernameNotification = message1;
        }
        /// <summary>
        /// Gets the text boxes
        /// </summary>
        /// <param name="newUsername">The new username textbox <see cref="TextBox"/></param>
        /// <param name="newPassword">The new password textbox <see cref="TextBox"/></param>
        public void GetTextBox(TextBox newUsername, TextBox newPassword)
        {
            NewUsername = newUsername;
            this.NewPassword = newPassword;
        }

        /// <summary>
        ///Returns the user back to welcomescreen <see cref="WelcomeScreen"/>
        /// </summary>
        public void ReturnButton()
        {
            WelcomeScreen.Visibility = Visibility.Visible;
            EditPassword.Visibility = EditUsername.Visibility = EditScreen.Visibility = AReturnButton.Visibility = StackPanel.Visibility = Visibility.Collapsed;
            EmptyAllMessages();
        }
        /// <summary>
        /// Empties all messages/>
        /// </summary>
        public void EmptyAllMessages()
        {
            EmptyTexts();
            EmptyMessages();
        }
        /// <summary>
        /// guides the user to the EditScreen element <see cref="EditScreen"/>
        /// </summary>
        public void UpdateTheUser()
        {

            EditScreen.Visibility = AReturnButton.Visibility = Visibility.Visible;
            WelcomeScreen.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// guides the user to the editusername element <see cref="EditUsername"/>
        /// </summary>
        public void EditTheUsername()
        {
            EditScreen.Visibility = Visibility.Collapsed;
            EditUsername.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// guides the user to the editpassword element <see cref="EditPassword"/>
        /// </summary>

        public void EditThePassword()
        {
            EditScreen.Visibility = Visibility.Collapsed;
            EditPassword.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Confirms the new password and updates the user database asynchronous
        /// </summary>
        public async void ConfirmnewPasswordEditAsync()
        {
            LoginViewModel.Loggedin.Password = NewPassword.Text;
            await ApiData.UpdateUsersAsync(LoginViewModel.Loggedin);
            PasswordNotification.Text = "Password changed!";
        }
        /// <summary>
        /// Empties the texts.
        /// </summary>
        private void EmptyTexts()
        {
            NewPassword.Text = string.Empty;
            NewUsername.Text = string.Empty;
        }
        /// <summary>
        /// Empties the messages.
        /// </summary>
        private void EmptyMessages()
        {
            UsernameNotification.Text = string.Empty;
            PasswordNotification.Text = string.Empty;
        }
        /// <summary>
        /// Confirms the new Username for the user asynchronous.
        /// </summary>
        public async void ConfirmNewUsernameEditAsync()
        {
            if (!CheckIfUsernameIsTaken())
            {
                LoginViewModel.Loggedin.Username = NewUsername.Text;
                await ApiData.UpdateUsersAsync(LoginViewModel.Loggedin);
                UsernameNotification.Text = "Username changed!";
            }
            else
                UsernameNotification.Text = "Username is taken!";
        }

        /// <summary>
        /// Checks if username is taken.
        /// </summary>
        /// <returns>
        /// true if it is taken and false if it is not
        /// </returns>
        private bool CheckIfUsernameIsTaken()
        {
            foreach (User user in ApiData.TheUsers)
                if (user.Username.Equals(NewUsername.Text))
                    return true;
            return false;
        }
        /// <summary>
        /// refreshes the lists and displays the top 5 User objects with highest totalscore <see cref="User.Totalscore"/>
        /// </summary>
        public void DisplayTop5Players()
        {
            LoadTop5UsersAsync();
            WelcomeScreen.Visibility = Visibility.Collapsed;
            AReturnButton.Visibility = StackPanel.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Loads the top5 users asynchronous, while using linq query
        /// </summary>
        internal async void LoadTop5UsersAsync()
        {
            GetUsers5.Clear();
            await ApiData.GetAllUsersAsync();

            foreach (User user in ReturnTop5HighestScore().ToList())
                GetUsers5.Add(user);
        }
        /// <summary>
        /// Returns the top5 highest score.
        /// </summary>
        /// <returns>
        /// uses linq to return a list with the 5 highest users
        /// </returns>
        private List<User> ReturnTop5HighestScore()
        {

            return (from highestScore in ApiData.TheUsers
                    where highestScore.Totalscore > 0
                    orderby highestScore.Totalscore descending
                    select highestScore).Take(5).ToList();
        }
    }
}
