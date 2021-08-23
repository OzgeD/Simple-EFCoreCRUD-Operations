using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFCoreCRUD.Entities
{
    public class SubUrls
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int UrlId { get; set; }
        [ForeignKey ("UrlId")]
        public Urls Urls { get; set; }


    }
}
