using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RPOLab.Models
{
    public class Film
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public int Rating { get; set; }

        public string Language { get; set; }

        public string Genre { get; set; }

        public string Producer { get; set; }

        public string ImageUrl { get; set; }

        public string VideoUrl { get; set; }

        public bool HasVideo { get; set; }

        public bool HasImage { get; set; }
        

        [JsonIgnore]
        public string Key { get; set; }
    }
}
