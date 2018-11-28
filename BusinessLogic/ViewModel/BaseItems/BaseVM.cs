using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using BusinessLogic.Annotations;

namespace BusinessLogic.ViewModel
{
    [DataContract]
    public class BaseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}