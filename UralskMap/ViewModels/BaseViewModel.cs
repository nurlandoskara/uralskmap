
using Caliburn.Micro;

namespace UralskMap.ViewModels
{
    public class BaseViewModel : Screen
    {
        public bool CanExecuteCommand(object parameter)
        {
            return true;
        }
    }
}