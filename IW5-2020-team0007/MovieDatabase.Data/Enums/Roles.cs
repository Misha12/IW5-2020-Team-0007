namespace MovieDatabase.Data.Enums
{
    /// <summary>
    /// User roles
    /// </summary>
    public enum Roles
    {
        /// <summary>
        /// Registered user (unverified).
        /// </summary>
        Registered,

        /// <summary>
        /// Basic user (verified). Can add/edit/remove with their ratings.
        /// </summary>
        User,

        /// <summary>
        /// Content manager. Can work with movies, genres, ratings.
        /// </summary>
        ContentManager,

        /// <summary>
        /// Administrator of system. Can do all actions on API.
        /// </summary>
        Administrator
    }
}
