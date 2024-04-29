namespace P238MovieApp.DTO_s.MovieDTO_s;

public class MovieUpdateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int GenreId { get; set; }
    public double Price { get; set; }
    public double CostPrice { get; set; }
    public bool IsDeleted { get; set; }
}
