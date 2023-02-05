using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLEMAITRE1_nasa
{
    public static class Constante
    {

        private static double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
        private static double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight - 50;

        
        // Get a good screen size for apod
        public static (double, double) GetScaledDimensions(double width, double height)
        {
            

            if (width > screenWidth || height > screenHeight)
            {
                double ratio = width / height;

                if (width > screenWidth)
                {
                    width = screenWidth;
                    height = width / ratio;
                }

                if (height > screenHeight)
                {
                    height = screenHeight;
                    width = height * ratio;
                }
            }

            return (width, height);
        }


        // Get a good screen size for apod with a div factor
        public static (double, double) GetScaledDimensions(double width, double height,double div)
        {


            if (width > screenWidth/div || height > screenHeight/div)
            {
                double ratio = width / height;

                if (width > screenWidth/div)
                {
                    width = screenWidth/div;
                    height = width / ratio;
                }

                if (height > screenHeight/div)
                {
                    height = screenHeight/div;
                    width = height * ratio;
                }
            }

            return (width, height);
        }
    }
}
