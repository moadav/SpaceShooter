using Microsoft.EntityFrameworkCore;
using SpaceShooter.LoginModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceShooter.AdataAccess
{
    /// <summary>
    /// the UserLoginContext
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class UserLoginContext : DbContext
    {


        public UserLoginContext()
        {

        }
        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserLoginContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public UserLoginContext(DbContextOptions<UserLoginContext> options) : base(options) { }



    }
}
