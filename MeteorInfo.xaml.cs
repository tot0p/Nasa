using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TLEMAITRE1_nasa.models;

namespace TLEMAITRE1_nasa
{
    /// <summary>
    /// Logique d'interaction pour MeteorInfo.xaml
    /// </summary>
    public partial class MeteorInfo : Window
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

        public MeteorInfo(NearEarthObject neo, BitmapImage icon, BitmapImage background)
        {
            InitializeComponent();
            _neo = neo;
            _icon = icon;
            _background = background;
            this.Icon = _icon;
            this.Background = new ImageBrush(_background);
            (this.Width, this.Height) = Constante.GetScaledDimensions(_background.Width, _background.Height, 1.5);
            this.Title = "Meteor Info - " + _neo.Name;
        }


        private void Grid_Loaded (object sender, RoutedEventArgs e)
        {
            Grid grid = (Grid)sender;

            Label title = (Label)grid.FindName("titleLabel");

            title.Content = _neo.Name;

            
        }

        private void Def_Loaded(object sender, RoutedEventArgs e)
        {
            Grid grid = (Grid)sender;


            Label amh = (Label)grid.FindName("amh");
            amh.Content += _neo.AbsoluteMagnitudeH.ToString();

            Label dmin = (Label)grid.FindName("dmin");
            dmin.Content += _neo.EstimatedDiameter["kilometers"].EstimatedDiameterMin.ToString() + " km";

            Label dmax = (Label)grid.FindName("dmax");
            dmax.Content += _neo.EstimatedDiameter["kilometers"].EstimatedDiameterMax.ToString() + " km";

            Label ipha = (Label)grid.FindName("ipha");
            ipha.Content += _neo.IsPotentiallyHazardousAsteroid.ToString();

            Label iso = (Label)grid.FindName("iso");
            iso.Content += _neo.IsSentryObject.ToString();
        }
    }
}
