using Examen.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Examen.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _favoriteColor;
        public string FavoriteColor
        {
            get { return _favoriteColor; }
            set { SetProperty(ref _favoriteColor, value); }
        }
        private string _favoriteBand;
        public string FavortieBand
        {
            get { return _favoriteBand; }
            set { SetProperty(ref _favoriteBand, value); }
        }
        private bool _xamarinLike;
        public bool XamarinLike
        {
            get { return _xamarinLike; }
            set { SetProperty(ref _xamarinLike, value); }
        }

        public string Content { get; set; }
        public string[] Children;

        private ObservableCollection<PostItem> _items = new ObservableCollection<PostItem>();
        public ObservableCollection<PostItem> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }
        public readonly INavigationService NavigationMethod;

        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IEventAggregator eventAggregator)
            : base(navigationService, dialogService, eventAggregator)
        {
            NavigationMethod = navigationService;
            eventAggregator.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);

            Items = new ObservableCollection<PostItem>(PostDatabase.Instance.GetItemsAsync().Result);

        }

        private void MessageReceived(Preferences pref)
        {
            FavortieBand = pref.FavoriteBand;
            FavoriteColor = pref.FavoriteColor;
            XamarinLike = pref.XamarinLike;
        }
    }
}
