using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RPOLab.Models
{
    public class Filter
    {
        public string Name { get; set; }
        public string Producer { get; set; }
        public int Year { get; set; }
        public int Rating { get; set; }
        public bool HasImage { get; set; }
        public bool HasVideo { get; set; }
        
        public bool Cleared { get; set; }
    }
}
