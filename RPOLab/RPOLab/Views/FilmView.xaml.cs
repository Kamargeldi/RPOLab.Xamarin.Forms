﻿using RPOLab.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RPOLab.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilmView : ContentView
    {
        Label rate = new Label();
        public FilmView()
        {
            InitializeComponent();

        }
    }
}