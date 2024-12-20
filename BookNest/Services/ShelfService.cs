using BookNest.DataAccess;
using BookNest.Dtos.Shelf;
using BookNest.Models.Entities;
using BookNest.Utils;

namespace BookNest.Services
{
    public class ShelfService
    {
        private readonly ShelfDao shelfDao;
        public ShelfService(ShelfDao shelfDao) 
        { 
            this.shelfDao = shelfDao;
        }

        public async Task<Shelf> CreateShelf(CreateShelfDto shelfDto)
        {
            var shelf = new Shelf(shelfDto);
            var dbShelf = await shelfDao.AddAsync(shelf);
            if (dbShelf == null)
                throw new CustomException(System.Net.HttpStatusCode.BadRequest, "Shelf couldnt be created!");
            return dbShelf;
        } 

        public async Task<List<Shelf>> GetShelves(CancellationToken cancellationToken)
        {
            return await shelfDao.GetAll(cancellationToken);
        }

        public async Task<Shelf> GetById(int id)
        {
            var shelf = await shelfDao.GetById(id);
            if (shelf == null) throw new NotFoundException($"Shelf with id: {id} doesnt exist");
            return shelf;
        }

        //public async Task<Shelf> ChangeName(int id, string name)
        //{
        //    var shelf = await get
        //}
    }
}
