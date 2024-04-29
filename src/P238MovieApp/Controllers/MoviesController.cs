using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P238MovieApp.Data;
using P238MovieApp.DTO_s.MovieDTO_s;
using P238MovieApp.Entities;

namespace P238MovieApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly AppDbContext _appDbContext;
 
    public MoviesController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var movies = await _appDbContext.Movies.ToListAsync();
        List<MovieGetDto> movieDTOs = new List<MovieGetDto>();

        movieDTOs = movies.Select(m => new MovieGetDto()
        {
            Id = m.Id,
            Name = m.Name,
            Description = m.Description,
            GenreId = m.GenreId,
            Price = m.Price
        }).ToList();

        //foreach (var movie in movies)
        //{
        //    MovieGetDto dto = new MovieGetDto()
        //    {
        //        Id = movie.Id,
        //        Name = movie.Name,
        //        Description = movie.Description,
        //        GenreId = movie.GenreId,
        //        Price = movie.Price
        //    };
        //}

        return Ok(movieDTOs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var movie = await _appDbContext.Movies.FindAsync(id);
        if (movie == null) return NotFound();

        MovieGetDto movieGetDto = new MovieGetDto()
        {
            Id = movie.Id,
            Name = movie.Name,
            Description = movie.Description,
            GenreId = movie.GenreId,
            Price = movie.Price
        };

        return Ok(movieGetDto);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create(MovieCreateDto dto)
    {
        Movie movie = new Movie()
        {
            Name = dto.Name,
            Description = dto.Description,
            GenreId = dto.GenreId,
            Price = dto.Price,
            CostPrice = dto.CostPrice,
            IsDeleted = dto.IsDeleted,
            CreatedDate =DateTime.Now,
            UpdatedDate = DateTime.Now
        };

        await _appDbContext.Movies.AddAsync(movie);
       await _appDbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("[action]/{id}")]
    public async Task<IActionResult> Update(int id, MovieUpdateDto dto)
    {
        var movie = await _appDbContext.Movies.FindAsync(id);

        if (movie == null)
        {
            return NotFound(); 
        }

        movie.Name = dto.Name;
        movie.Description = dto.Description;
        movie.GenreId = dto.GenreId;
        movie.Price = dto.Price;
        movie.CostPrice = dto.CostPrice;
        movie.IsDeleted = dto.IsDeleted;
        movie.UpdatedDate = DateTime.Now;

        await _appDbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var movie = await _appDbContext.Movies.FindAsync(id);

        if (movie == null)
        {
            return NotFound(); 
        }

        movie.IsDeleted = true;
        movie.UpdatedDate = DateTime.Now;

        await _appDbContext.SaveChangesAsync();

        return Ok();
    }

}
