using System;

namespace MovieDatabase.Data.Repository
{
    public class RepositoryBase : IDisposable
    {
        protected AppDbContext Context { get; }

        public RepositoryBase(AppDbContext context)
        {
            Context = context;
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
