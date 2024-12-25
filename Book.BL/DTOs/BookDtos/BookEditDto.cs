using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.BL.DTOs.BookDtos
{
    public record BookEditDto
    {
        public string? Title { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public int? AuthorId { get; set; }
    }
}
