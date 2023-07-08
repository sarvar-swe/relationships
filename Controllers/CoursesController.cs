using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("/api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly AppDbContext dbContext;

    public CoursesController(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpPost]
    public async  Task<IActionResult> CreateCourse([FromBody] CreateCourseDto dto)
    {
        var savedEntity = dbContext.Courses.Add(new Course
        {
            Name = dto.Name,
            StartDate = dto.StartDate,
            Price = dto.Price,
            DurationInMonths = dto.DurationInMonths
        });

        await dbContext.SaveChangesAsync();

        return CreatedAtAction(
            actionName: nameof(GetCourse),
            routeValues: new { Id = savedEntity.Entity.Id}, 
            value: new GetCourseDto(savedEntity.Entity));
    }

    [HttpGet("{id}")]
    public async  Task<IActionResult> GetCourse([FromRoute] int id)
    {
        var course = await dbContext.Courses.FirstOrDefaultAsync(e => e.Id == id);
        if(course == null)
            return NotFound();
        
        return Ok(new GetCourseDto(course));
    }

    [HttpGet]
    public async  Task<IActionResult> GetCourses([FromQuery] string search)
    {
        var coursesQuery = dbContext.Courses.AsQueryable();

        if(false == string.IsNullOrWhiteSpace(search))
            coursesQuery = coursesQuery.Where(u => 
                u.Name.ToLower().Contains(search.ToLower()));

        var courses = await coursesQuery
            .Select(u => new GetCourseDto(u))
            .ToListAsync();

        return Ok(courses);
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> UpdateCourse([FromRoute] int Id, UpdateCourseDto updateCourse)
    {
        var course = await dbContext.Courses
            .FirstOrDefaultAsync(u => u.Id == Id);
        
        if(course is null)
            return NotFound();
        
        course.Name = updateCourse.Name;
        course.StartDate = updateCourse.StartDate;
        course.DurationInMonths = updateCourse.DurationInMonths;
        course.Price = updateCourse.Price;
       
        await dbContext.SaveChangesAsync();
        return Ok(course.Id);
    }


    [HttpPost("{id}/students")]
    public async  Task<IActionResult> AddCourseStudents([FromRoute] int id, [FromBody] AddCourseStudentsDto dto)
    {
        var course = await dbContext.Courses
            .Where(e => e.Id == id)
            .Include(e => e.Students)
            .FirstOrDefaultAsync(e => e.Id == id);
        if(course is null)
            return NotFound();

        if(dto.StudentsIds is null)
            return BadRequest("Send at least one student ID.");

        foreach(var studentId in dto.StudentsIds)
        {
            if(false == await dbContext.Users.AnyAsync(c => c.Id == studentId))
                return BadRequest("Student with ID {studentId} does not exist");
        }
        
        var students = await dbContext.Users
            .Where(u => dto.StudentsIds.Contains(u.Id))
            .ToListAsync();

        course.Students.AddRange(students);
        await dbContext.SaveChangesAsync();

        return Ok(new GetCourseDto(course));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse([FromRoute] int id)
    {
        var course = await dbContext.Courses.FirstOrDefaultAsync(u => u.Id == id);
        if(course is null)
            return NotFound();

        dbContext.Courses.Remove(course);
        await dbContext.SaveChangesAsync();

        return Ok();
    }

}