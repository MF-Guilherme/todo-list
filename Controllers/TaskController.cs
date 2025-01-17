using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Data;
using Task = TodoList.Models.Task;
namespace TodoList.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly AppDbContext _context;

    public TaskController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult GetAll() => Ok(_context.Tasks.ToList());
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
     var task = _context.Tasks.Find(id);
     return task is null ? NotFound() : Ok(task);
    }
    
    [HttpPost]
    public IActionResult Create(Task task)
    {
        _context.Tasks.Add(task);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Task updatedTask)
    {
        var task = _context.Tasks.Find(id);
        if (task is null) return NotFound();
        
        task.Title = updatedTask.Title;
        task.Description = updatedTask.Description;
        task.IsCompleted = updatedTask.IsCompleted;
        _context.SaveChanges();
        
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("finalized/{id}")]
    public IActionResult FinalizedTask(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task is null) return NotFound();
        
        task.IsCompleted = true;
        _context.SaveChanges();
        
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }
    

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task is null) return NotFound();

        _context.Tasks.Remove(task);
        _context.SaveChanges();
        
        return NoContent();
    }
}