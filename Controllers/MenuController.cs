using CampusEats.Api.Dtos;
using CampusEats.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampusEats.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly IMenuService _svc;

    public MenuController(IMenuService svc)
    {
        _svc = svc;
    }

    // GET /api/menu
    // Public endpoint
    [HttpGet]
    public ActionResult<IEnumerable<MenuItemDto>> GetAll()
    {
        var items = _svc.GetAll();

        return Ok(items);
    }

    // GET /api/menu/{id}
    // Public endpoint
    [HttpGet("{id}")]
    public ActionResult<MenuItemDto> GetById(int id)
    {
        var item = _svc.GetById(id);

        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    // POST /api/menu
    // Admin only
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public ActionResult<MenuItemDto> Create(
        [FromBody] CreateMenuItemDto dto)
    {
        var created = _svc.Create(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created);
    }

    // PUT /api/menu/{id}
    // Admin only
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Update(
        int id,
        [FromBody] CreateMenuItemDto dto)
    {
        var updated = _svc.Update(id, dto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE /api/menu/{id}
    // Admin only
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        var deleted = _svc.Delete(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}