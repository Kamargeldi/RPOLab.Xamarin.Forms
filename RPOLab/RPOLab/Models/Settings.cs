using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RPOLab.Models
{
    public class Settings
    {
        public int FontSize { get; set; }
        public string  FontName { get; set; }
        public bool DarkMode { get; set; }
        public string Language { get; set; }

    }
}
