using Aurum.Data.Entities;
using Aurum.Models.LayoutDTOs;
using Aurum.Repositories.LayoutRepository;
using Microsoft.EntityFrameworkCore;
using System;

namespace Aurum.Services.LayoutServices
{
    public class LayoutService : ILayoutService
    {
        private ILayoutRepo _layoutRepo;

        public LayoutService(ILayoutRepo layoutRepo)
        {
            _layoutRepo = layoutRepo;
        }
        public async Task<int> CreateOrUpdateBasic(LayoutDto layout)
        {
            var currentLayout = await _layoutRepo.GetBasic(layout.UserId);

            if (currentLayout is not null)
            {
                currentLayout.Chart1 = layout.Charts[0];
                currentLayout.Chart2 = layout.Charts[1];
                currentLayout.Chart3 = layout.Charts[2];
                currentLayout.Chart4 = layout.Charts[3];
                currentLayout.Chart5 = layout.Charts[4];
                currentLayout.Chart6 = layout.Charts[5];
                currentLayout.Chart7 = layout.Charts[6];

                var updatedId = await _layoutRepo.UpdateBasic(currentLayout);
                return updatedId;
            }

            BasicLayout basicLayout = new()
            {
                UserId = layout.UserId,
                Chart1 = layout.Charts[0],
                Chart2 = layout.Charts[1],
                Chart3 = layout.Charts[2],
                Chart4 = layout.Charts[3],
                Chart5 = layout.Charts[4],
                Chart6 = layout.Charts[5],
                Chart7 = layout.Charts[6],
            };
            var createdId = await _layoutRepo.CreateBasic(basicLayout);
            return createdId;

        }
        public async Task<int> CreateOrUpdateScientic(LayoutDto layout)
        {
            var currentLayout = await _layoutRepo.GetScientic(layout.UserId);

            if (currentLayout is not null)
            {
                currentLayout.Chart1 = layout.Charts[0];
                currentLayout.Chart2 = layout.Charts[1];
                currentLayout.Chart3 = layout.Charts[2];
                currentLayout.Chart4 = layout.Charts[3];
                currentLayout.Chart5 = layout.Charts[4];
                currentLayout.Chart6 = layout.Charts[5];
                currentLayout.Chart7 = layout.Charts[6];
                currentLayout.Chart8 = layout.Charts[7];

                var updatedId = await _layoutRepo.UpdateScientic(currentLayout);
                return updatedId;
            }

            ScienticLayout scienticLayout = new()
            {
                UserId = layout.UserId,
                Chart1 = layout.Charts[0],
                Chart2 = layout.Charts[1],
                Chart3 = layout.Charts[2],
                Chart4 = layout.Charts[3],
                Chart5 = layout.Charts[4],
                Chart6 = layout.Charts[5],
                Chart7 = layout.Charts[6],
                Chart8 = layout.Charts[7]
            };

            var createdId = await _layoutRepo.UpdateScientic(scienticLayout);
            return createdId;
        }
        public async Task<int> CreateOrUpdateDetailed(LayoutDto layout)
        {

            var currentLayout = await _layoutRepo.GetDetailed(layout.UserId);

            if (currentLayout is not null)
            {
                currentLayout.Chart1 = layout.Charts[0];
                currentLayout.Chart2 = layout.Charts[1];
                currentLayout.Chart3 = layout.Charts[2];
                currentLayout.Chart4 = layout.Charts[3];
                currentLayout.Chart5 = layout.Charts[4];
                currentLayout.Chart6 = layout.Charts[5];
                currentLayout.Chart7 = layout.Charts[6];
                currentLayout.Chart8 = layout.Charts[7];
                currentLayout.Chart9 = layout.Charts[8];

                var updatedId = await _layoutRepo.UpdateDetailed(currentLayout);
                return updatedId;
            }

            DetailedLayout detailedLayout = new()
            {
                UserId = layout.UserId,
                Chart1 = layout.Charts[0],
                Chart2 = layout.Charts[1],
                Chart3 = layout.Charts[2],
                Chart4 = layout.Charts[3],
                Chart5 = layout.Charts[4],
                Chart6 = layout.Charts[5],
                Chart7 = layout.Charts[6],
                Chart8 = layout.Charts[7],
                Chart9 = layout.Charts[8]
            };

            var createdId = await _layoutRepo.CreateDetailed(detailedLayout);
            return createdId;
        }
        public async Task<AllLayoutsDto> GetAll(string userId)
        {
            var basicLayout = await _layoutRepo.GetBasic(userId);
            var scienticLayout = await _layoutRepo.GetScientic(userId);
            var detailedLayout = await _layoutRepo.GetDetailed(userId);

            AllLayoutsDto allLayoutsDto = new();

            if (basicLayout is not null)
            {
                allLayoutsDto.Basic.AddRange(new[] { basicLayout.Chart1, basicLayout.Chart2, basicLayout.Chart3, basicLayout.Chart4, basicLayout.Chart5, basicLayout.Chart6, basicLayout.Chart7 });
            }
            if (scienticLayout is not null)
            {
                allLayoutsDto.Scientic.AddRange(new[] { scienticLayout.Chart1, scienticLayout.Chart2, scienticLayout.Chart3, scienticLayout.Chart4, scienticLayout.Chart5, scienticLayout.Chart6, scienticLayout.Chart7, scienticLayout.Chart8 });
            }

            if (detailedLayout is not null)
            {
                allLayoutsDto.Detailed.AddRange(new[] { detailedLayout.Chart1, detailedLayout.Chart2, detailedLayout.Chart3, detailedLayout.Chart4, detailedLayout.Chart5, detailedLayout.Chart6, detailedLayout.Chart7, detailedLayout.Chart8, detailedLayout.Chart9 });
            }

            return allLayoutsDto;
        }

    }
}
