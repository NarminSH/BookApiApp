using Book.BL.DTOs.AuthorDtos;
using Book.BL.DTOs.BookDtos;
using Book.Core.Entities;
using Book.Data.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.BL.Services.Abstractions
{
    public interface IAuthorService
    {
        Task<Author> CreateAsync(AuthorCreateDto entityDto);
    }
}
