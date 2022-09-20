using Pallet.Database.Entities.ProfileData.Profiles;

namespace Pallet.Services.Managers.Interfaces;

public interface IManagerProfiles
{
    IQueryable<Profile> Items { get; }

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

    public Profile GetActiveProfile();

    void SetSelectedProfile(Profile newprofile);

    void DeactivateProfile();

    public Profile ActiveProfile { get; }

    public event EventHandler? ActiveProfileChanged;
}