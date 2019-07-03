using System.Collections.ObjectModel;
using System.Linq;
using UralskMap.Models;

namespace UralskMap.ViewModels
{
    public class PhotoViewModel : BaseViewModel
    {
        private ObservableCollection<ImageItem> _items;
        private ImageItem _selectedItem;
        private int _selectedIndex;

        public ObservableCollection<ImageItem> Items
        {
            get => _items;
            set
            {
                _items = value;
                NotifyOfPropertyChange(() => Items);
            }
        }

        public ImageItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                NotifyOfPropertyChange(() => SelectedItem);
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (value > Items.Count - 1) value = 0;
                _selectedIndex = value;
                NotifyOfPropertyChange(() => SelectedIndex);
            }
        }


        public PhotoViewModel(ObservableCollection<ImageItem> items)
        {
            Items = items;
            SelectedItem = Items.FirstOrDefault();
        }
    }

}