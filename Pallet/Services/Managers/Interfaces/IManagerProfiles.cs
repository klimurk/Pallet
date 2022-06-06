using Pallet.Database.Entities.Change.Profiles;
using Pallet.Models.Interfaces;

namespace Pallet.Services.Managers.Interfaces;

public interface IManagerProfiles
{
    IQueryable<Profile> Items { get; }

    IProfileInfoData GetActiveProfileInfoData();

    IQueryable<Profile> Add(IQueryable<Profile> profiles);

    Profile Add(Profile profile);

    Task<IQueryable<Profile>> AddAsync(IQueryable<Profile> profiles);

    Task<Profile> AddAsync(Profile profile);

    Profile Get(int id);

    Task<Profile> GetAsync(int id);

    void ActivateSelectedProfile();

    void Update(Profile profile);

    Task UpdateAsync(Profile profile);

    Profile GetSelectedProfile();

    void SetSelectedProfile(IProfileInfoData newprofile);

    public Profile ActiveProfile { get; }

    public event EventHandler? ActiveProfileChanged;
}