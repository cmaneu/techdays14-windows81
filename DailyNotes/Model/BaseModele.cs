using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace DailyNotes.Model
{
    /// <summary>
    /// Cette classe permet de gérer la propagation de notifications  des propriétés.
    /// </summary>
    public class BaseModele : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null, bool force = false)
        {
            if (Equals(storage, value) && !force)
                return false;

            storage = value;   

            if (App.Instance.AppRootVisual == null || App.Instance.AppRootVisual.Dispatcher.HasThreadAccess)
            {
                if (propertyName != null)
                    RaisePropertyChanged(propertyName);
            }
            else
            {
                // We do not await because it's used in properties
                // This call will block caller, but it's not a problem.
                Task t = App.Instance.AppRootVisual.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    if (propertyName != null)
                        RaisePropertyChanged(propertyName);
                }).AsTask();
            }

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
    }
}