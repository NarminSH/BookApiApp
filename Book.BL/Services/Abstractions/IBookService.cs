using Book.Data.Repositories.Abstractions;
using Entities= Book.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book.BL.DTOs.BookDtos;

namespace Book.BL.Services.Abstractions
{
    public interface IBookService 
    {
        Task<ICollection<Entities.Book>> GetAllAsync();
        Task<Entities.Book> CreateAsync(BookCreateDto entityDto);
        Task<Entities.Book> GetByIdAsync(int id);
        Task<bool> SoftDeleteAsync(int id);
        Task<bool> UpdateAsync(int id, BookCreateDto entityDto);
    }
}
