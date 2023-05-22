using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphalizator.Models {
    public class AnalyzedWord {
        public string RootWord { get; set; }
        public string Type { get; set; }
        public List<Qushimcha> Qushimchas { get; set; }
    }
}
