using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P238ApiIntro.Entities;

namespace P238ApiIntro.Controllers;

[Route("api/[controller]")] // localhost:7060/api/students
[ApiController]
public class StudentsController : ControllerBase
{
    private List<Student> _students = new List<Student>()
    {
        new Student() { Id = 1, Name = "Elxan"},
        new Student() { Id = 1, Name = "Onur"},
        new Student() { Id = 1, Name = "Aydan"},
        new Student() { Id = 1, Name = "Lale"},
    };
    [HttpGet("")] // localhost:7060/api/students
    //[Route("")]
    public IActionResult GetAll()
    {
        return Ok(_students);
    }
    [HttpGet("[action]/{id}")] // localhost:7060/api/students/GetById/id
    //[Route("{id}")]
    public IActionResult GetById(int id)
    {
        var std = _students.Find(x => x.Id == id);
        if(std is null) { return NotFound(); }
        return Ok(std);
    }
    [HttpPost("")] // localhost:7060/api/students (post)
    public IActionResult Create(Student student)
    {
        _students.Add(student);
        return NoContent();
    }
    [HttpPut("[action]/{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] Student student)
    {
        var std = _students.Find(x=>x.Id == id);
         std.Name = student.Name;
        return Ok(std);
    }
    [HttpDelete("[action]/{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var std = _students.Find(x=>x.Id==id);
        _students.Remove(std);
        return NoContent();
    }
}
