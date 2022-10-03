using Pallet.Database.Entities.ProfileData.Profiles;
using Pallet.Database.Repositories.Interfaces;
using Pallet.Services.Managers.Interfaces;

namespace Pallet.Services.Managers
{
    /// <summary>
    /// Manager profiles.
    /// </summary>
    public class ManagerProfiles : IManagerProfiles
    {
        private Profile _ChosenProfile;
        private Profile _ActiveProfile;

        /// <summary>
        /// Activated profile.
        /// </summary>
        public Profile ActiveProfile
        {
            get => _ActiveProfile;
            private set
            {
                _ActiveProfile = value;

                _ActiveProfile.DateLastUse = DateTime.Now;
                OnActiveProfileChanged();
            }
        }

        private readonly IDbRepository<Profile> _RepositoryProfiles;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerProfiles"/> class.
        /// </summary>
        /// <param name="RepositoryProfiles">The repository profiles.</param>
        public ManagerProfiles(IDbRepository<Profile> RepositoryProfiles)
        {
            _RepositoryProfiles = RepositoryProfiles;
            "ManagerProfiles init --------------".CheckStage();
        }

        /// <summary>
        ///All items.
        /// </summary>
        public IQueryable<Profile> Items => _RepositoryProfiles.Items;

        /// <summary>
        /// Add item.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>A Profile.</returns>
        public Profile Add(Profile profile) => _RepositoryProfiles.Add(profile);

        /// <summary>
        /// Add query of profiles.
        /// </summary>
        /// <param name="profiles">The profiles.</param>
        /// <returns>An IQueryable.</returns>
        public IQueryable<Profile> Add(IQueryable<Profile> profiles)
        {
            foreach (var profile in profiles)
                Add(profile);
            return profiles;
        }

        /// <summary>
        /// Update profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        public void Update(Profile profile) => _RepositoryProfiles.Update(profile);

        /// <summary>
        /// Get profile by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Profile.</returns>
        public Profile Get(int id) => _RepositoryProfiles.Get(id);

        /// <summary>
        /// Add profile async .
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>A Task.</returns>
        public async Task<Profile> AddAsync(Profile profile) => await _RepositoryProfiles.AddAsync(profile).ConfigureAwait(false);

        /// <summary>
        /// Add query profiles async.
        /// </summary>
        /// <param name="profiles">The profiles.</param>
        /// <returns>A Task.</returns>
        public async Task<IQueryable<Profile>> AddAsync(IQueryable<Profile> profiles)
        {
            foreach (var profile in profiles)
                await AddAsync(profile).ConfigureAwait(false);
            return profiles;
        }

        /// <summary>
        /// Update profile the async.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>A Task.</returns>
        public async Task UpdateAsync(Profile profile) => await _RepositoryProfiles.UpdateAsync(profile).ConfigureAwait(false);

        /// <summary>
        /// Get Profile async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        public async Task<Profile> GetAsync(int id) =>
            await _RepositoryProfiles
            .GetAsync(id)
            .ConfigureAwait(false);

        /// <summary>
        /// Set new selected profile (not activate).
        /// </summary>
        /// <param name="newprofile">The newprofile.</param>
        public void SetSelectedProfile(Profile newprofile) => _ChosenProfile =
            _RepositoryProfiles.Items?
            .FirstOrDefault(profile => profile.Name == newprofile.Name);

        /// <summary>
        /// Get selected profile (not active).
        /// </summary>
        /// <returns>A Profile.</returns>
        public Profile GetSelectedProfile() => _ChosenProfile;

        /// <summary>
        /// Activate selected profile.
        /// </summary>
        public void ActivateSelectedProfile()
        {
            ActiveProfile = _RepositoryProfiles.Items?
                .FirstOrDefault(profile => profile.Name == _ChosenProfile.Name);

            _RepositoryProfiles.UpdateAsync(ActiveProfile);
        }

        /// <summary>
        /// Get active profile.
        /// </summary>
        /// <returns>A Profile.</returns>
        public Profile GetActiveProfile() => ActiveProfile;

        public event EventHandler? ActiveProfileChanged;

        /// <summary>
        /// Active profile changed events executer.
        /// </summary>
        private void OnActiveProfileChanged() => ActiveProfileChanged?.Invoke(this, new());

        public void DeactivateProfile()
        {
            _ChosenProfile = null;
            _ActiveProfile = null;
            OnActiveProfileChanged();
        }
    }
}