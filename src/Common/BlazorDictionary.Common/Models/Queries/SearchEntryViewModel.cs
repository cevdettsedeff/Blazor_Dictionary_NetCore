using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Common.Models.Queries
{
    public class SearchEntryViewModel
    {
        public Guid Id { get; set; } // tıklandığında gidebilmek için id'de lazım.
        public string Subject { get; set; }
    }
}
