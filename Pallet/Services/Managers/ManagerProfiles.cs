using Pallet.Database.Entities.Change.Profiles;
using Pallet.Database.Repositories.Interfaces;
using Pallet.Models;
using Pallet.Models.Interfaces;
using Pallet.Services.Managers.Interfaces;

namespace Pallet.Services.Managers
{
    public class ManagerProfiles : IManagerProfiles
    {
        private Profile _ChosenProfile;
        private Profile _ActiveProfile;

        public Profile ActiveProfile
        {
            get => _ActiveProfile;
            private set
            {
                _ActiveProfile = value;
                _ActiveProfile.DateLastUse = DateTime.Now;
                ProfileData.Name = _ActiveProfile.Name;
                ProfileData.DateLastUse = _ActiveProfile.DateLastUse;
            }
        }

        public ProfileInfoData ProfileData;
        private readonly IDbRepository<Profile> _RepositoryProfiles;

        public ManagerProfiles(
            IDbRepository<Profile> RepositoryProfiles
            )
        {
            _RepositoryProfiles = RepositoryProfiles;
            ProfileData = new();
        }

        public IQueryable<Profile> Items => _RepositoryProfiles.Items;

        public Profile Add(Profile profile) => _RepositoryProfiles.Items.Append(profile).First();

        public IQueryable<Profile> Add(IQueryable<Profile> profiles)
        {
            foreach (var profile in profiles)
                Add(profile);
            return profiles;
        }

        public void Update(Profile profile) => _RepositoryProfiles.Update(profile);

        public Profile Get(int id) => _RepositoryProfiles.Get(id);

        public async Task<Profile> AddAsync(Profile profile) => await _RepositoryProfiles.AddAsync(profile).ConfigureAwait(false);

        public async Task<IQueryable<Profile>> AddAsync(IQueryable<Profile> profiles)
        {
            foreach (var profile in profiles)
                await AddAsync(profile).ConfigureAwait(false);
            return profiles;
        }

        public async Task UpdateAsync(Profile profile) => await _RepositoryProfiles.UpdateAsync(profile).ConfigureAwait(false);

        public async Task<Profile> GetAsync(int id) =>
            await _RepositoryProfiles
            .GetAsync(id)
            .ConfigureAwait(false);

        public void SetSelectedProfile(IProfileInfoData newprofile) => _ChosenProfile =
            _RepositoryProfiles.Items.
            FirstOrDefault(profile => profile.Name == newprofile.Name);

        public Profile GetSelectedProfile() => _ChosenProfile;

        public void ActivateSelectedProfile()
        {
            ActiveProfile = _RepositoryProfiles.Items.FirstOrDefault(profile => profile.Name == _ChosenProfile.Name);

            OnActiveProfileChanged();

            _RepositoryProfiles.UpdateAsync(ActiveProfile);
        }

        public Profile GetActiveProfile() => ActiveProfile;

        public IProfileInfoData GetActiveProfileInfoData() => ProfileData;

        public event EventHandler? ActiveProfileChanged;

        private void OnActiveProfileChanged() => ActiveProfileChanged?.Invoke(this, new());
    }
}