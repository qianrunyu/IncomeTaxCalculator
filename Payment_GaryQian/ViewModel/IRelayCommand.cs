using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Payment_GaryQian.ViewModel {
    public interface IRelayCommand : ICommand {
        void RaiseCanExecuteChanged();
        void OnExecuteChanged(object sender, EventArgs e);
    }
}
