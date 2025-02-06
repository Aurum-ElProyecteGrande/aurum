using Aurum.Models.LayoutDTOs;

namespace Aurum.Services.LayoutServices
{
    public interface ILayoutService
    {
        Task<int> CreateOrUpdateBasic(LayoutDto layout);
        Task<int> CreateOrUpdateScientic(LayoutDto layout);
        Task<int> CreateOrUpdateDetailed(LayoutDto layout);
        Task<AllLayoutsDto> GetAll(int userId);
    }
}
