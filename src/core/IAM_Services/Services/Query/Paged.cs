using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.Helper;

public class PagedList<T>
{
    public List<T> Items { get; }
    public int TotalCount { get; }
    public int PageIndex { get; }
    public int PageSize { get; }

    public PagedList(List<T> items, int totalCount, int pageIndex, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        PageIndex = pageIndex;
        PageSize = pageSize;
    }

    public bool HasPreviousPage
    {
        get { return (PageIndex > 0); }
    }

    public bool HasNextPage
    {
        get { return ((PageIndex + 1) * PageSize) < TotalCount; }
    }
}