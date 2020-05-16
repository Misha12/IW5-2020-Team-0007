using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieDatabase.Data.Models.Common
{
    public class PaginatedData<TModel>
    {
        public List<TModel> Data { get; set; }

        public int Page { get; set; }
        public long TotalItemsCount { get; set; }

        public bool CanNext { get; set; }
        public bool CanPrev { get; set; }

        public static PaginatedData<TModel> Create<TEntity>(IQueryable<TEntity> query, PaginatedRequest request,
            Func<List<TEntity>, List<TModel>> mapFunc)
        {
            if (request.Page <= 1)
                request.Page = 0;

            var result = new PaginatedData<TModel>()
            {
                Page = request.Page == 0 ? 1 : request.Page,
                TotalItemsCount = query.Count()
            };

            var skip = request.Page * request.Limit;
            query = query.Skip(skip).Take(request.Limit);

            result.CanNext = skip + request.Limit < result.TotalItemsCount;
            result.CanPrev = skip != 0;

            result.Data = mapFunc(query.ToList());
            return result;
        }
    }
}
