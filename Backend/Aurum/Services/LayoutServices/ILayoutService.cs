using Aurum.Data.Entities;
using Aurum.Models.LayoutDTOs;

namespace Aurum.Services.LayoutServices
{
    public interface ILayoutService
    {
        Task<int> CreateOrUpdateBasic(LayoutDto layout, string userId);
        Task<int> CreateOrUpdateScientic(LayoutDto layout, string userId);
        Task<int> CreateOrUpdateDetailed(LayoutDto layout, string userId);
        Task<AllLayoutsDto> GetAll(string userId);
    }
}
