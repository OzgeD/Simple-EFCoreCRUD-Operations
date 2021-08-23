using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreCRUD.Entities
{
    public class Urls
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string SourceCode { get; set; }
        public bool IsCheck { get; set; }

        public List<SubUrls> SubUrlses { get; set; }
    }
}
