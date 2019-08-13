using DAL.Contracts;
using DAL.Data;
using DAL.Models;

namespace DAL
{
    public class PicturesRepository : DbRepository<PictureEntry>, IImageRepository
    {
        public PicturesRepository(CycleTogetherDbContext context) : base(context)
        {
        }
    }
}
