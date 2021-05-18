using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPOLab.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RPOLab
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoPage : ContentPage
    {
        public VideoPage(Film film)
        {
            InitializeComponent();
            videPlayer.Source = film.VideoUrl;
        }

        
    }
}