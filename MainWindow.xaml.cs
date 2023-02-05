using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TLEMAITRE1_nasa.models;

namespace TLEMAITRE1_nasa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region attribut

        private utils.Request _request;

        private Apod _apod;

        private Feed _feed;

        private BitmapImage _icon;

        private Grid _mainGrid;

        #endregion

        #region Set

        public void SetRequest(utils.Request request) => _request = request;

        public void SetApod(Apod apod) => _apod = apod;

        public void SetFeed(Feed feed) => _feed = feed;

        public void SetIcon(BitmapImage icon) => _icon = icon;

        public void SetMainGrid(Grid grid) => _mainGrid = grid;

        #endregion

        #region Get

        public Grid GetMainGrid() => _mainGrid;

        public utils.Request GetRequest() => _request;

        public Apod GetApod() => _apod;

        public Feed GetFeed() => _feed;

        public BitmapImage GetIcon() => _icon;

        #endregion


        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            _icon = new BitmapImage(new Uri("https://www.nasa.gov/sites/default/files/thumbnails/image/nasa-logo-web-rgb.png"));

            //set window icon
            this.Icon = _icon;

            _mainGrid = new Grid();


            utils.Env.Load();

            _request = new utils.Request("https://api.nasa.gov");

            _apod = new Apod();

            _feed = new Feed();

        }

        #endregion

        private async Task<Apod?> InitApod()
        {

            //get the response from the request
            var response = await _request.GetAsync<Apod>("/planetary/apod?thumbs=true&api_key=" + Environment.GetEnvironmentVariable("NASA_API_KEY"));

            return response;

        }

        // load the apod in background
        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var rep = await InitApod();
            if (rep != null)
            {
                _apod = rep;

                BitmapImage img = new BitmapImage(new Uri(_apod.Url));

                img.DownloadCompleted += (s, e) =>
                {

                    (this.Width, this.Height)
                     = Constante.GetScaledDimensions(img.Width, img.Height);

                };

                img.DownloadFailed += (s, e) =>
                {
                    this.Height = 500;
                    this.Width = 500;
                };



                //set background image
                this.Background = new ImageBrush(img);

                // create varible grid for get the label title
                //object Title =   this.FindName("titleLabel");
                Grid grid = (Grid)sender;
                Label label = (Label)grid.FindName("titleLabel");
                label.Content = _apod.Title;

                Button button = (Button)grid.FindName("InfoButton");
                button.Visibility = Visibility.Visible;

                Button button2 = (Button)grid.FindName("PhotoButton");
                button2.Visibility = Visibility.Visible;

                this.Title += " - " + _apod.Title;
            }


        }


        //load the feed NASA
        private async void Grid_Loaded_MET(object sender, RoutedEventArgs e)

        {
            var response = await _request.GetAsync<Feed>("/neo/rest/v1/feed?api_key=" + Environment.GetEnvironmentVariable("NASA_API_KEY"));

            _mainGrid = (Grid)sender;

            ((Button)this.FindName("BNext")).Visibility = Visibility.Visible;
            ((Button)this.FindName("BPrev")).Visibility = Visibility.Visible;

            Load_Meteor(response);



            
        }


        // load list of meteor
        private void Load_Meteor(Feed? response)
        {
            if (response != null)
            {
                _feed = response;

                Label range = (Label)this.FindName("Day");
                range.Content = _feed.GetRange();


                Brush? brush = (Brush?)new BrushConverter().ConvertFrom("#B2525252");
                if (brush == null)
                {
                    brush = Brushes.Black;
                }

                //for all meteorithe of the day

                int i = 1;
                foreach (string date in _feed.NearEarthObjects.Keys)
                {

                    foreach (var item in _feed.NearEarthObjects[date])
                    {

                        //add name

                        Label Name = new Label
                        {
                            Content = item.Name,
                            FontSize = 20,
                            Foreground = Brushes.White,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Margin = new Thickness(0, 0, 0, 0)
                        };

                        Grid.SetColumn(Name, 0);
                        Grid.SetRow(Name, i);

                        _mainGrid.Children.Add(Name);

                        // add Distance

                        Label Distance = new Label
                        {
                            Content = item.CloseApproachData[0].MissDistance.Kilometers + " km",
                            FontSize = 20,
                            Foreground = Brushes.White,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Margin = new Thickness(0, 0, 0, 0)
                        };

                        Grid.SetColumn(Distance, 1);
                        Grid.SetRow(Distance, i);

                        _mainGrid.Children.Add(Distance);


                        //Add Button Show All Detail

                        Button button = new Button
                        {
                            Content = "Show detail",
                            FontSize = 20,
                            Foreground = Brushes.White,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Margin = new Thickness(15, 0, 0, 0),
                            Background = brush,
                            Tag = item
                        };

                        button.Click += (s, e) =>
                        {
                            Window win = new Meteor(item, _icon, _apod.Url);
                            win.Show();
                        };
                        Grid.SetColumn(button, 2);
                        Grid.SetRow(button, i);

                        _mainGrid.Children.Add(button);




                        i++;
                        _mainGrid.RowDefinitions.Add(new RowDefinition
                        {
                            Height = new GridLength()
                        });


                    }

                }

            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        //for the button next to next range of day meteor
        private async void Next(object sender, RoutedEventArgs e)
        {
            _mainGrid.Children.Clear();
            var rep = await utils.Request.GetAsyncFullPath<Feed>(_feed.Links.Next);

            Load_Meteor(rep);

        }

        // for the button prev to prev range of day meteor
        private async void Previus(object sender, RoutedEventArgs e)
        {
            _mainGrid.Children.Clear();
            var rep = await utils.Request.GetAsyncFullPath<Feed>(_feed.Links.Prev);

            Load_Meteor(rep);
        }

        // show apod description
        private void ApodInfo(object sender, RoutedEventArgs e)
        {
            ShowCustomMessageBox(_apod.Title, _apod.Explanation);
        }


        //make a custom message box with background apod
        private void ShowCustomMessageBox(string title, string text)
        {
            BitmapImage img = new BitmapImage(new Uri(_apod.Url));

            Brush? brush = (Brush?)new BrushConverter().ConvertFrom("#B2525252");
            if (brush == null)
            {
                brush = Brushes.Black;
            }

            Window messageBox = new Window
            {
                Icon = _icon,
                Title = title,
                Height = 400,
                Width = 600,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize,
                Content = new Border
                {
                    Background = new ImageBrush
                    {
                        ImageSource = img,
                        Stretch = Stretch.UniformToFill
                    },
                    Child = new Border
                    {
                        Margin = new Thickness(50, 50, 50, 50),
                        BorderBrush = brush,
                        Background = brush,
                        BorderThickness = new Thickness(2),
                        CornerRadius = new CornerRadius(30),
                        Child = new TextBlock
                        {
                            Text = text,
                            Foreground = Brushes.White,
                            VerticalAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            TextWrapping = TextWrapping.Wrap,
                            Margin = new Thickness(10, 0, 10, 0)
                        }
                    }
                },
            };
            messageBox.ShowDialog();
        }

        // show apod photo
        private void ApodFull(object sender, RoutedEventArgs e)
        {
            BitmapImage img = new BitmapImage(new Uri(_apod.Url));

            img.Freeze();

            (double width, double height) = Constante.GetScaledDimensions(img.Width, img.Height, 1.5);
            Window window = new Window
            {
                Icon = _icon,
                Title = _apod.Title + " - full image",
                Height = height,
                Width = width,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize,
                Background = new ImageBrush(img)
            };
            window.ShowDialog();
        }

    }
}
