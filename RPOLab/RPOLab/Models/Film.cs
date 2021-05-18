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

        private int _rating;
        public int Rating
        {
            get => _rating;
            set
            {
                this._rating = value;
                R1 = _rating > 0;
                R2 = _rating > 1;
                R3 = _rating > 2;
                R4 = _rating > 3;
                R5 = _rating > 4;
            }
        }

        public string Language { get; set; }

        public string Genre { get; set; }

        public string Producer { get; set; }

        public string ImageUrl { get; set; }

        public string VideoUrl { get; set; }

        public bool HasVideo { get; set; }

        public bool HasImage { get; set; }
        

        [JsonIgnore]
        public string Key { get; set; }
        
        [JsonIgnore]
        public bool R1 { get; set; }
        [JsonIgnore]
        public bool R2 { get; set; }
        [JsonIgnore]
        public bool R3 { get; set; }
        [JsonIgnore]
        public bool R4 { get; set; }
        [JsonIgnore]
        public bool R5 { get; set; }
    }
}
