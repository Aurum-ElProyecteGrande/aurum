using Aurum.Data.Context;
using Aurum.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aurum.Repositories.LayoutRepository
{
    public class LayoutRepo : ILayoutRepo
    {
        private AurumContext _dbContext;

        public LayoutRepo(AurumContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CreateBasic(BasicLayout layout)
        {
            await _dbContext.BasicLayouts.AddAsync(layout);
            await _dbContext.SaveChangesAsync();
            return layout.BasicLayoutId;
        }

        public async Task<int> UpdateBasic(BasicLayout layout)
        {
            _dbContext.BasicLayouts.Update(layout);
            await _dbContext.SaveChangesAsync();
            return layout.BasicLayoutId;
        }

        public async Task<int> CreateScientic(ScienticLayout layout)
        {
            await _dbContext.ScienticLayouts.AddAsync(layout);
            await _dbContext.SaveChangesAsync();
            return layout.ScienticLayoutId;
        }
        public async Task<int> UpdateScientic(ScienticLayout layout)
        {
            _dbContext.ScienticLayouts.Update(layout);
            await _dbContext.SaveChangesAsync();
            return layout.ScienticLayoutId;
        }
        public async Task<int> CreateDetailed(DetailedLayout layout)
        {
            await _dbContext.DetailedLayouts.AddAsync(layout);
            await _dbContext.SaveChangesAsync();
            return layout.DetailedLayoutId;
        }
        public async Task<int> UpdateDetailed(DetailedLayout layout)
        {
            _dbContext.DetailedLayouts.Update(layout);
            _dbContext.DetailedLayouts.Update(layout);
            await _dbContext.SaveChangesAsync();
            return layout.DetailedLayoutId;
        }
        public async Task<BasicLayout>? GetBasic(int userId) => _dbContext.BasicLayouts
            .FirstOrDefault(l => l.UserId == userId);
        public async Task<ScienticLayout>? GetScientic(int userId) => _dbContext.ScienticLayouts
            .FirstOrDefault(l => l.UserId == userId);

        public async Task<DetailedLayout>? GetDetailed(int userId) => _dbContext.DetailedLayouts
            .FirstOrDefault(l => l.UserId == userId);

    }
}
