using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using RPOLab.FireBaseDB;
using RPOLab.Models;
using RPOLab.Strings;


namespace RPOLab
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        FireBaseService _service = new FireBaseService();
        ListView _list;
        public MainPage()
        {
            InitializeComponent();

            _service = new FireBaseService();

            _list = new ListView()
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Never,
                SeparatorVisibility = SeparatorVisibility.None,
                HasUnevenRows = true,
                SelectionMode = ListViewSelectionMode.None,
                ItemTemplate = new DataTemplate(() =>
                {
                    var filmItem = new RPOLab.Views.FilmView();
                    return new ViewCell()
                    {
                        View = new Frame()
                        {
                            HasShadow = true,
                            Margin = new Thickness(2, 4, 2, 4),
                            Padding = 0,
                            Content = filmItem,
                            CornerRadius = 20
                        },       
                    };
                })
            };

            _list.ItemTapped += new EventHandler<ItemTappedEventArgs>(list_ItemTapped);
            filmContainer.Children.Add(_list);
            

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var filter = _service.GetFilters();
            
            List<Film> filmList;
            if (filter.Cleared)
            {
                filmList = _service.GetFilms();
                _list.ItemsSource = filmList;
                return;
            }

            filmList = _service.GetFilteredFilms(filter);
            _list.ItemsSource = filmList;

        }

        private void list_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var tappedItem = e.Item as Film;
        }

        private async void settingsButton_Clicked(object sender, EventArgs e)
        {
            
            var settingsState = _service.GetSettings();
            var settingsPage = new SettingsPage(settingsState);
            await Navigation.PushAsync(settingsPage);
        }

        private async void filterButton_Clicked(object sender, EventArgs e)
        {
            var filter = _service.GetFilters();
            await Navigation.PushAsync(new FilterPage(filter));
        }

        private async void addNewFilm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewFilmPage());
        }
    }
}
