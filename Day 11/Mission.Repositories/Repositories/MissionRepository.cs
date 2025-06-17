using Microsoft.EntityFrameworkCore;
using Mission.Entities;
using Mission.Entities.Context;
using Mission.Entities.Models;
using Mission.Repositories.IRepositories;

namespace Mission.Repositories.Repositories
{
    public class MissionRepository(MissionDbContext dbContext) : IMissionRepository
    {
        private readonly MissionDbContext _dbContext = dbContext;

        public Task<List<MissionRequestViewModel>> GetAllMissionAsync()
        {
            return _dbContext.Missions.Select(m => new MissionRequestViewModel()
            {
                Id = m.Id,
                CityId = m.CityId,
                CountryId = m.CountryId,
                EndDate = m.EndDate,
                MissionDescription = m.MissionDescription,
                MissionImages = m.MissionImages,
                MissionSkillId = m.MissionSkillId,
                MissionThemeId = m.MissionThemeId,
                MissionTitle = m.MissionTitle,
                StartDate = m.StartDate,
                TotalSeats = m.TotalSheets ?? 0,
            }).ToListAsync();
        }

        public async Task<MissionRequestViewModel?> GetMissionById(int id)
        {
            return await _dbContext.Missions.Where(m => m.Id == id).Select(m => new MissionRequestViewModel()
            {
                CityId = m.CityId,
                CountryId = m.CountryId,
                EndDate = m.EndDate,
                MissionDescription = m.MissionDescription,
                MissionImages = m.MissionImages,
                MissionSkillId = m.MissionSkillId,
                MissionThemeId = m.MissionThemeId,
                MissionTitle = m.MissionTitle,
                StartDate = m.StartDate,
                TotalSeats = m.TotalSheets ?? 0,
            }).FirstOrDefaultAsync();
        }

        public async Task<bool> AddMission(Missions mission)
        {
            try
            {
                _dbContext.Missions.Add(mission);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return true;
        }

        public async Task<List<MissionDetailResponseModel>> GetClientSideMissionList()
        {
            var dateToCompare = DateTime.Now.Date.AddDays(-1);
            return await _dbContext.Missions
                .Where(m => !m.IsDeleted)
                .Select(m => new MissionDetailResponseModel()
                {
                    Id = m.Id,
                    MissionTitle = m.MissionTitle,
                    MissionDescription = m.MissionDescription,
                    CountryId = m.CountryId,
                    CityId = m.CityId,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    TotalSheets = m.TotalSheets,
                    RegistrationDeadLine = m.RegistrationDeadLine,
                    MissionThemeId = m.MissionThemeId,
                    MissionSkillId = m.MissionSkillId,
                    MissionImages = m.MissionImages,
                    CountryName = m.Country.CountryName,
                    CityName = m.City.CityName,
                    MissionThemeName = m.MissionTheme.ThemeName,
                    MissionSkillName = string.Join(",", _dbContext.MissionSkills
                        .Where(ms => m.MissionSkillId.Contains(ms.Id.ToString()))
                        .Select(ms => ms.SkillName)
                        .ToList()),
                    MissionStatus = m.RegistrationDeadLine < dateToCompare ? "Closed" : "Available",
                    //MissionApplyStatus = m.MissionApplications.Any(ma => ma.UserId == userId) ? "Applied" : "Apply",
                    //MissionApproveStatus = m.MissionApplications.Any(ma => ma.UserId == userId && ma.Status == true) ? "Approved" : "Applied",
                    MissionDateStatus = m.EndDate <= dateToCompare ? "MissionEnd" : "MissionRunning",
                    MissionDeadLineStatus = m.RegistrationDeadLine <= dateToCompare ? "Closed" : "Running",
                    //MissionFavouriteStatus = m.MissionFavourites.Any(mf => mf.UserId == userId) ? "1" : "0",
                    //Rating = m.MissionRatings.Where(mr => mr.UserId == userId).Select(mr => mr.Rating).FirstOrDefault() ?? 0,
                }).ToListAsync();
        }
    }
}
