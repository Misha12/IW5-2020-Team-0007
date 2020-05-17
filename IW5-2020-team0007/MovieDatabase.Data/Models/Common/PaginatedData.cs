using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieDatabase.Data.Models.Common
{
    /// <summary>
    /// Paginated result.
    /// </summary>
    public class PaginatedData<TModel>
    {
        public List<TModel> Data { get; set; }

        /// <summary>
        /// Number of page.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Total count of items, independently of pagination.
        /// </summary>
        public long TotalItemsCount { get; set; }

        /// <summary>
        /// A flag that the user can request for next page.
        /// </summary>
        public bool CanNext { get; set; }

        /// <summary>
        /// A flag that the user can request for previous page.
        /// </summary>
        public bool CanPrev { get; set; }

        public static PaginatedData<TModel> Create<TEntity>(IQueryable<TEntity> query, PaginatedRequest request,
            Func<List<TEntity>, List<TModel>> mapFunc)
        {
            var result = CreateEmpty(request);
            result.TotalItemsCount = query.Count();

            var skip = (request.Page == 0 ? 0 : request.Page - 1) * request.Limit;
            result.SetFlags(skip, request.Limit);

            query = query.Skip(skip).Take(request.Limit);
            result.Data = mapFunc(query.ToList());

            return result;
        }

        public static PaginatedData<TModel> Create(List<TModel> data, PaginatedRequest request)
        {
            var result = CreateEmpty(request);
            result.TotalItemsCount = data.Count;

            var skip = (request.Page == 0 ? 0 : request.Page - 1) * request.Limit;

            result.SetFlags(skip, request.Limit);
            result.Data = data.Skip(skip).Take(request.Limit).ToList();

            return result;
        }

        public static PaginatedData<TModel> CreateEmpty(PaginatedRequest request)
        {
            if (request.Page <= 1)
                request.Page = 0;

            return new PaginatedData<TModel>()
            {
                Page = request.Page == 0 ? 1 : request.Page
            };
        }

        internal void SetFlags(int skip, int limit)
        {
            CanPrev = skip != 0;
            CanNext = skip + limit < TotalItemsCount;
        }
    }
}
