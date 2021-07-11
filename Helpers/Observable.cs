using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace SpaceShooter.Helpers
{
    /// <summary>
    /// class that Observes tchanged
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class Observable : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        /// <returns></returns>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Sets the specified storage.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage">The storage.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        protected void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }
        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
