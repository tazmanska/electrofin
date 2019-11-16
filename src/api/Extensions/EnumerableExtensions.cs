using System;
using System.Collections.Generic;
using System.Linq;
using api.Dtos;

namespace api.Extensions
{
    public static class EnumerableExtensions
    {
        public static DataListDto<T> ToPagedData<T>(this IEnumerable<T> enumerable, int page = 1, int pageSize = int.MaxValue) where T: BaseDto
        {
            if (page < 1)
            {
                page = 1;
            }

            if (pageSize < 1)
            {
                pageSize = int.MaxValue;
            }

            var data = enumerable.Skip((page - 1) * pageSize).Take(pageSize).ToArray();

            var total = enumerable.Count();

            return new Dtos.DataListDto<T>()
            {
                Count = data.Length,
                Data = data,
                Page = page,
                Pages = (int)Math.Ceiling(total / (double)pageSize),
                Total = total
            };
        }
    }
}