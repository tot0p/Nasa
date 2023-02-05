using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TLEMAITRE1_nasa.models;

namespace TLEMAITRE1_nasa
{
    /// <summary>
    /// Logique d'interaction pour Meteor.xaml
    /// </summary>
    public partial class Meteor : Window
    {

        #region attribut

        private NearEarthObject _neo;


        private BitmapImage _icon;

        private BitmapImage _background;


        #endregion

        #region Set

        public void SetNeo(NearEarthObject neo) => _neo = neo;

        public void SetIcon(BitmapImage icon) => _icon = icon;

        public void SetBackground(BitmapImage background) => _background = background;

        public void SetBackgrount(string url) => _background = new BitmapImage(new Uri(url));


        #endregion

        #region Get

        public NearEarthObject GetNeo() => _neo;

        public BitmapImage GetIcon() => _icon;

        public BitmapImage GetBackground() => _background;


        #endregion

        // for get full aproach of meteor
        private async Task<NearEarthObject?> GetAllData(NearEarthObject neo)
        {
            var request = new utils.Request(neo.Links.Self);

            return await request.GetAsync<NearEarthObject>("");
        }

        public Meteor(NearEarthObject neo, BitmapImage icon, string background)
        {
            InitializeComponent();
            _neo = neo;
            _icon = icon;
            _background = new BitmapImage(new Uri(background));
            this.Icon = _icon;
            this.Background = new ImageBrush(_background);
            _background.Freeze();
            (this.Width, this.Height) = Constante.GetScaledDimensions(_background.Width, _background.Height, 1.5);
            this.Title = "Meteor - " + _neo.Name;
        }

        // load history of meteor aproach
        private async void Grid_Loaded_MET(object sender, RoutedEventArgs e)
        {

            var resp = await GetAllData(_neo);

            if (resp != null)
            {
                _neo = resp;

            }


            Grid mainGrid = (Grid)sender;

            Brush? brush = (Brush?)new BrushConverter().ConvertFrom("#B2525252");
            if (brush == null)
            {
                brush = Brushes.Black;
            }

            int i = 1;
            foreach (CloseApproachData approachData in _neo.CloseApproachData)
            {

                Label Date = new Label
                {
                    Content = approachData.CloseApproachDateFull,
                    FontSize = 20,
                    Foreground = Brushes.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(0, 0, 0, 0)
                };

                Grid.SetColumn(Date, 0);
                Grid.SetRow(Date, i);

                mainGrid.Children.Add(Date);

                Label Distance = new Label
                {
                    Content = approachData.MissDistance.Kilometers + " km",
                    FontSize = 20,
                    Foreground = Brushes.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(0, 0, 0, 0)
                };

                Grid.SetColumn(Distance, 1);
                Grid.SetRow(Distance, i);

                mainGrid.Children.Add(Distance);


                i++;
                mainGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(1,GridUnitType.Auto)
                });
            }
        }

        //aff info of meteor
        private void InfoMeteor(object sender, RoutedEventArgs e)
        {
            MeteorInfo meteorInfo = new MeteorInfo(_neo, _icon, _background);
            meteorInfo.Show();
        }


        // load first grid for name of meteor
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Grid grid = (Grid)sender;

            Label title = (Label)grid.FindName("titleLabel");

            Button info = (Button)grid.FindName("InfoButton");

            info.Visibility = Visibility.Visible;

            title.Content = _neo.Name;
        }

    }
}
