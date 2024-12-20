using BookNest.Dtos.Shelf;
using BookNest.Models.Entities;
using BookNest.Services;
using BookNest.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShelfController
    {
        private readonly ShelfService shelfService;

        public ShelfController(ShelfService shelfServicee)
        {
            shelfService = shelfServicee;
        }

        [HttpPost]
        public async Task<IBaseResponse> CreateShelf(CreateShelfDto shelfDto)
        {
            var shelf = await shelfService.CreateShelf(shelfDto);
            return BaseResponse<Shelf>.SuccessResponse(shelf);
        }

        [HttpGet]
        public async Task<IBaseResponse> GetShelves(CancellationToken cancellation)
        {
            var shelves = await shelfService.GetShelves(cancellation);
            return BaseResponse<List<Shelf>>.SuccessResponse(shelves);
        }

        [HttpGet("{id}")]
        public async Task<IBaseResponse> GetById(int id)
        {
            var shelf = await shelfService.GetById(id);
            return BaseResponse<Shelf>.SuccessResponse(shelf);
        }

    }
}
