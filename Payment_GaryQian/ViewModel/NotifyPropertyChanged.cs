using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_GaryQian.ViewModel
{
    public class NotifyPropertyChange : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
