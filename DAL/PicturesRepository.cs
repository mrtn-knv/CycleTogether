using DAL.Contracts;
using DAL.Models;


namespace DAL
{
    public class PicturesRepository : Repository<PictureEntry>, IImageRepository
    {
    }
}
