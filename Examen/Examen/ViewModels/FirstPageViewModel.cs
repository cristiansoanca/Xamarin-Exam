using Examen.Utils;
using Examen.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Examen.ViewModels
{
	public class FirstPageViewModel : ViewModelBase
	{
        private string _favoriteColor;
        public string FavortieColor
        {
            get { return _favoriteColor; }
            set { SetProperty(ref _favoriteColor, value); }
        }
        private string _favoriteBand;
        public string FavoriteBand
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

        public ICommand ContinueClicked { get; set; }

        public FirstPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IEventAggregator eventAggregator)
            : base(navigationService, dialogService, eventAggregator)
        {
            ContinueClicked = new Command(Button_Clicked);
        }

        private Preferences _pref = new Preferences();
        private bool _colorIsSet, _bandIsSet;

        private async void Button_Clicked()
        {

            if (!string.IsNullOrEmpty(FavortieColor.ToString())) {
                _pref.FavoriteColor = FavortieColor;
                _colorIsSet = true;
            } else
            {
                await DialogService.DisplayAlertAsync("", "Please enter input to color entry", "OK");
            }

            if (!string.IsNullOrEmpty(FavoriteBand))
            {
                _pref.FavoriteBand = FavoriteBand;
                _bandIsSet = true;
            }
            else
            {
                await DialogService.DisplayAlertAsync("", "Please enter input to band entry", "OK");
            }

            if (_colorIsSet && _bandIsSet)
            {
                await NavigationService.NavigateAsync(nameof(MainPage));

                EventAggregator.GetEvent<MessageSentEvent>().Publish(_pref);
            }

        }
    }
}
