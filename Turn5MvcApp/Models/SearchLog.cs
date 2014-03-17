using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Turn5MvcApp.Models
{
    [Table("SearchLog")]
    public class SearchLog
    {
        [Key]
        public Guid SessionId { get; set; }

        public string KeyWords { get; set; }
        public string ResultNames { get; set; }
    }
}