using BookNest.Models.Entities;
using BookNest.Services;
using BookNest.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    [ApiController]
    [Route("shelfbooks")]
    public class ShelfBookController
    {
        private readonly ShelfBookService _shelfBookService;
        public ShelfBookController(ShelfBookService shelfBookService)
        {
            _shelfBookService = shelfBookService;
        }

        [HttpPost]
        public async Task<IBaseResponse> AddToShelf(string isbn, int shelfId)
        {
            var shelfbook = await _shelfBookService.Add(isbn, shelfId);
            return BaseResponse<ShelfBook>.SuccessResponse(shelfbook);
        }
    }
}
