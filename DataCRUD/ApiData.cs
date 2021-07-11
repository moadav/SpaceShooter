using SpaceShooter.LoginModel;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;

namespace SpaceShooter.DataCRUD
{
    /// <summary>
    /// class that handles all api communication
    /// </summary>
    public static class ApiData
    {
        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <returns>
        /// A list containing all users created from the database through api <see cref="GetAllUsersAsync()"/>
        /// </returns>
        public static ObservableCollection<User> TheUsers { get; set; }

        static readonly Uri BaseUri = new Uri("http://localhost:64521/api/Users");
        static readonly HttpClient httpClient = new HttpClient();
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <returns>
        /// A HttpContent object <see cref="HttpContent"/>
        /// </returns>
        static HttpContent Content { get; set; }

        /// <summary>
        /// Gets all users asynchronous.
        /// </summary>
        /// <exception cref="HttpRequestException"> Thrown when the HttpRequest fails</exception>
        internal static async Task GetAllUsersAsync()
        {
            try
            {
                TheUsers = new ObservableCollection<User>();
                var result = await httpClient.GetAsync(BaseUri);
                string json = await result.Content.ReadAsStringAsync();
                var user = DeserializeObject<User[]>(json);

                foreach (var User in user)
                    TheUsers.Add(User);
            }
            catch (HttpRequestException exception)
            {
                Console.WriteLine("Error with getting data from API ");
                Console.WriteLine(exception.Message);
            }
        }



        /// <summary>
        /// Updates the users asynchronous.
        /// </summary>
        /// <param name="aUser">a user object</param>
        /// <exception cref="HttpRequestException"> Thrown when the HttpRequest fails</exception>
        internal static async Task UpdateUsersAsync(User aUser)
        {
            try
            {
                var user = SerializeObject(aUser);
                Content = new StringContent(user, Encoding.UTF8, "application/json");
                await httpClient.PutAsync(BaseUri + "/" + aUser.UserId, Content);
                await GetAllUsersAsync();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Error Did not find user");
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Creates the user asynchronous and updates the list <see cref="TheUsers"/>
        /// </summary>
        /// <param name="aUser">a user object</param>
        /// <exception cref="HttpRequestException"> Thrown when the HttpRequest fails</exception>
        internal static async void CreateUserAsync(User aUser)
        {
            try
            {
                TheUsers.Add(aUser);
                var user = SerializeObject(aUser);
                Content = new StringContent(user, Encoding.UTF8, "application/json");
                await httpClient.PostAsync(BaseUri, Content);
                await GetAllUsersAsync();


            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Could not Register User, unexpected values");
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Deletes the user asynchronous.
        /// </summary>
        /// <param name="aUser">a user object</param>
        /// <exception cref="HttpRequestException"> Thrown when the HttpRequest fails</exception>
        internal static async Task DeleteUserAsync(User aUser)
        {
            try
            {
                await httpClient.DeleteAsync(BaseUri + "/" + aUser.UserId);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Could not Delete User, Did not find User");
                Console.WriteLine(e.Message);
            }
        }
    }
}
