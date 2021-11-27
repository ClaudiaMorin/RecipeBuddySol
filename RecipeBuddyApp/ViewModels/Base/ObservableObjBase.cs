using System.ComponentModel;
using Microsoft.UI.Xaml;
using System.Runtime.CompilerServices;
using System.Security;

namespace RecipeBuddy
{
    public class ObservableObjBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string foo = "foo";

        //protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        //    //if (PropertyChanged != null)
        //    //{
        //    //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //    //}

        //}

        /// <summary>
        /// New INotifyPropertyChanged base for UWP
        /// </summary>
        /// <param name="name"></param>
        protected void RaisePropertyChanged([CallerMemberName] string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        //protected void OnPropertyChanged(SecureString name)
        //{
        //    PropertyChangedEventHandler handler = PropertyChanged;
        //    if (handler != null)
        //    {
        //        handler(this, new PropertyChangedEventArgs(name));
        //    }
        //}
    }
}
