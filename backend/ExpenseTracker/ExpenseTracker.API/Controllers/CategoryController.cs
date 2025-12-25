using ExpenseTracker.API.BLOs.IBlo;
using ExpenseTracker.API.Models.DTOs.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseTracker.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryBlo _categoryBlo;

        public CategoryController(ICategoryBlo categoryBlo)
        {
            _categoryBlo = categoryBlo;
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _categoryBlo.CreateAsync(dto, userId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            return Ok(await _categoryBlo.GetAllAsync(userId));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            return Ok(await _categoryBlo.GetByIdAsync(id, userId));
        }

        [HttpPut]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update(UpdateCategoryDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            return Ok(await _categoryBlo.UpdateAsync(dto, userId));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _categoryBlo.DeleteAsync(id, userId);
            return NoContent();
        }
    }
}
