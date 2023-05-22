using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphalizator.Models {
    public class Word {
        public string Value {
            get
            {
                return $"{_value} ({Analyz.RootWord} - {Analyz.Type})";
            }
            set { _value = value; }
        }
        private string _value;
        public AnalyzedWord Analyz { get; set; }
    }
}
