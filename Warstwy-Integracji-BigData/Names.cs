using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warstwy_Integracji_BigData
{
    [DelimitedRecord(",")]
    public class Names
    {
        public string? Name { get; set; }

        public int Quantity { get; set; }

        public string? Sex { get; set; }


    }
}
