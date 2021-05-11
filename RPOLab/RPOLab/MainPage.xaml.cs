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
                    
                    Image image = new Image()
                    {
                        BackgroundColor = Color.White,
                        WidthRequest = 100,
                        HeightRequest = 100,
                        Aspect = Aspect.Fill
                    };

                    image.SetBinding(Image.SourceProperty, "ImageUrl");

                    Frame frame = new Frame()
                    {
                        HasShadow = true,
                        Padding = 0,
                        CornerRadius = 20,
                        Content = image
                    };

                    StackLayout layout = new StackLayout();

                    var nameLabel = new Label()
                    {
                        FontAttributes = FontAttributes.Bold,
                        FontFamily = "Arial",
                        FontSize = 8,
                        TextColor = Color.DarkGreen,
                        Margin = new Thickness(2, 2, 2, 0),
                        Text = Strings.Strings.NewPage_Name,
                    };
                    layout.Children.Add(nameLabel);


                    var name = new Label()
                    {
                        FontAttributes = FontAttributes.Bold,
                        FontFamily = "Arial",
                        FontSize = 14,
                        TextColor = Color.DarkGreen,
                        Margin = new Thickness(2, 2, 2, 0),
                        BackgroundColor = Color.White,
                    };
                    name.SetBinding(Label.TextProperty, "Name");
                    layout.Children.Add(name);


                    var yearLabel = new Label()
                    {
                        FontAttributes = FontAttributes.Bold,
                        FontFamily = "Arial",
                        FontSize = 8,
                        TextColor = Color.DarkGreen,
                        Margin = new Thickness(2, 2, 2, 0),
                        Text = Strings.Strings.NewPage_Year,
                        TabIndex = 2
                    };
                    layout.Children.Add(yearLabel);


                    var year = new Label()
                    {
                        FontAttributes = FontAttributes.None,
                        FontFamily = "Arial",
                        FontSize = 10,
                        Margin = new Thickness(2, 0, 2, 2),
                        TextColor = Color.DarkGreen,
                        TabIndex = 3
                    };
                    year.SetBinding(Label.TextProperty, "Year");
                    layout.Children.Add(year);


                    var producerLabel = new Label()
                    {
                        FontAttributes = FontAttributes.Bold,
                        FontFamily = "Arial",
                        FontSize = 8,
                        TextColor = Color.DarkGreen,
                        Margin = new Thickness(2, 2, 2, 0),
                        Text = Strings.Strings.NewPage_Producer,
                        TabIndex = 4
                    };
                    layout.Children.Add(producerLabel);


                    var producer = new Label()
                    {
                        FontAttributes = FontAttributes.None,
                        FontFamily = "Arial",
                        FontSize = 10,
                        Margin = new Thickness(2, 0, 2, 2),
                        TextColor = Color.DarkGreen,
                        TabIndex = 5
                    };
                    year.SetBinding(Label.TextProperty, "Producer");
                    layout.Children.Add(producer);


                    layout.Margin = new Thickness(10, 0, 0, 0);
                    layout.Padding = new Thickness(0, 0, 0, 0);
                    layout.Spacing = 0;

                    var result = new FlexLayout();
                    result.BackgroundColor = Color.Turquoise;
                    result.Margin = 0;
                    result.Padding = 4;
                    result.Children.Add(frame);
                    result.Children.Add(layout);

                    
                    return new ViewCell()
                    {
                        View = new Frame()
                        {
                            HasShadow = true,
                            Margin = new Thickness(2, 4, 2, 4),
                            Padding = 0,
                            Content = result,
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
