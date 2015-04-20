using System.Collections.Generic;

namespace AngularAspNetMvc.Data.ExtendedModels
{
    public class PagedResult<T>
    {
        public PagedResult(IEnumerable<T> result, int totalRows, int totalPages)
        {
            Result = result;
            TotalRows = totalRows;
            TotalPages = totalPages;
        }
        public int TotalRows { get; set; }
        public IEnumerable<T> Result { get; set; }
        public int TotalPages { get; set; }
    }
}
