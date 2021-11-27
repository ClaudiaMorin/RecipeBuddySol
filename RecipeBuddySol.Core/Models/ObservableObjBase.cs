using System.ComponentModel;
using System.Security;

namespace RecipeBuddy.Core.Models
{
    public class ObservableObjBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //    }

        //}

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
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
