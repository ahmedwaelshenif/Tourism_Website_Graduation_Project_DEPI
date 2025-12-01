using Tourism.IRepository;
using Tourism.Models;

namespace Tourism.Repository
{
    public class TourGuideRepository: ITourGuideRepository
    {
        private readonly TourismDbContext _context;

        public TourGuideRepository(TourismDbContext context)
        {
            _context = context;
        }

    }
}
