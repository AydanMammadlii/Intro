using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P238MovieApp.Data;
using P238MovieApp.DTO_s.GenreDTO_s;
using P238MovieApp.DTO_s.MovieDTO_s;
using P238MovieApp.Entities;

namespace P238MovieApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public GenresController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var genres = await _appDbContext.Genres.ToListAsync();
        List<GenreGetDto> genreDTOs = new List<GenreGetDto>();

        genreDTOs = genres.Select(m => new GenreGetDto()
        {
            Id = m.Id,
            Name = m.Name
        }).ToList();

        return Ok(genreDTOs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var genre = await _appDbContext.Genres.FindAsync(id);
        if (genre == null) return NotFound();

        GenreGetDto genreGetDto = new GenreGetDto()
        {
            Id = genre.Id,
            Name = genre.Name,
        };

        return Ok(genreGetDto);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create(GenreCreateDto dto)
    {
        Genre genre = new Genre()
        {
            Name = dto.Name,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now
        };

        await _appDbContext.Genres.AddAsync(genre);
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("[action]/{id}")]
    public async Task<IActionResult> Update(int id, GenreUpdateDto dto)
    {
        var genre = await _appDbContext.Genres.FindAsync(id);

        if (genre == null)
        {
            return NotFound();
        }

        genre.Name = dto.Name;
        genre.IsDeleted = dto.IsDeleted;
        genre.UpdatedDate = DateTime.Now;

        await _appDbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var genre = await _appDbContext.Genres.FindAsync(id);

        if (genre == null)
        {
            return NotFound();
        }

        genre.IsDeleted = true;
        genre.UpdatedDate = DateTime.Now;

        await _appDbContext.SaveChangesAsync();

        return Ok();
    }
}