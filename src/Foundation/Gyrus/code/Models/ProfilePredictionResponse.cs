using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Foundation.Gyrus.Models
{
    public class ProfilePredictionResponse
    {
        public Results Results { get; set; }
    }

    public class Results
    {
        public Profileprediction ProfilePrediction { get; set; }
    }

    public class Profileprediction
    {
        public string type { get; set; }
        public Value value { get; set; }
    }

    public class Value
    {
        public string[] ColumnNames { get; set; }
        public string[] ColumnTypes { get; set; }
        public string[][] Values { get; set; }
    }

}
