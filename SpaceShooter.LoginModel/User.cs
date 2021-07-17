using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SpaceShooter.LoginModel
{
    /// <summary>
    /// This class handles how a user is build in the database
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class User : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        /// <returns></returns>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// The username
        /// </summary>
        private string username;
        /// <summary>
        /// The password
        /// </summary>
        private string password;
        /// <summary>
        /// The totalscore
        /// </summary>
        private int totalscore;
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <returns>
        /// The user identifier.
        /// </returns>
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <returns>
        /// The username.
        /// </returns>
        [StringLength(50)]
        [Required]
        public string Username
        {
            get => username;


            set
            {
                if (Equals(username, value)) return;
                username = value;

                OnPropertyChanged(nameof(Username));
                OnPropertyChanged(nameof(ScoreInfo));
            }

        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <returns>
        /// The password.
        /// </returns>
        [StringLength(50)]
        [Required]
        public string Password
        {
            get => password;
            set
            {
                if (Equals(password, value)) return;
                password = value;

                OnPropertyChanged(nameof(Password));
            }
        }


        /// <summary>
        /// Gets or sets the totalscore.
        /// </summary>
        /// <returns>
        /// The totalscore.
        /// </returns>
        public int Totalscore
        {
            get => totalscore;
            set
            {
                if (Equals(totalscore, value)) return;
                totalscore = value;

                OnPropertyChanged(nameof(Totalscore));
                OnPropertyChanged(nameof(ScoreInfo));
            }
        }

        /// <summary>
        /// Gets the score information.
        /// </summary>
        /// <returns>
        /// The score information.
        /// </returns>
        public string ScoreInfo => $"Username: {Username} and TotalPoints : {Totalscore}";


        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"Username: {Username} and TotalPoints : {Totalscore}";
        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));



    }
}
