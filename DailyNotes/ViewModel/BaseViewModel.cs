using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DailyNotes.ViewModel
{
    /// <summary>
    /// Cette classe permet de gérer la propagation de notifications  des propriétés.
    /// </summary>
    internal class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null, bool force = false)
        {
            if (Equals(storage, value) && !force)
                return false;

            storage = value;

            if (propertyName != null)
                RaisePropertyChanged(propertyName);
      
            return true;
        }

        protected virtual void RaisePropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler eventHandler = PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
