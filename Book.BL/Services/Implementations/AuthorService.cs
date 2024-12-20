using AutoMapper;
using Book.BL.DTOs.AuthorDtos;
using Book.BL.Services.Abstractions;
using Book.Core.Entities;
using Book.Data.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.BL.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepo;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepo, IMapper mapper)
        {
            _authorRepo = authorRepo;
            _mapper = mapper;
        }

        public async Task<Author> CreateAsync(AuthorCreateDto entityDto)
        {
            Author createdAuthor = _mapper.Map<Author>(entityDto);
            createdAuthor.CreatedAt = DateTime.UtcNow.AddHours(4);
            Author authorEntity = await _authorRepo.CreateAsync(createdAuthor);
            await _authorRepo.SaveChangesAsync();
            return authorEntity;
        }

        
    }
}
