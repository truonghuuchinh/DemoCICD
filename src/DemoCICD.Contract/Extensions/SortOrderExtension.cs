using DemoCICD.Contract.Enumerations;

namespace DemoCICD.Contract.Extensions;
public static class SortOrderExtension
{
    public static SortOrder ConvertStringToSortOrder(string? order)
        => !string.IsNullOrWhiteSpace(order) && order.Trim().Equals("ASC", StringComparison.OrdinalIgnoreCase)
        ? SortOrder.Ascending
        : SortOrder.Descending;

    public static IDictionary<string, SortOrder> ConvertStringToSortOrderAndColumns(string? sortOrder)
    {
        var result = new Dictionary<string, SortOrder>();
        if (!string.IsNullOrWhiteSpace(sortOrder))
        {
            var splitSortOrder = sortOrder.Trim().Split(',');
            if (splitSortOrder.Length > 1)
            {
                foreach (var item in splitSortOrder)
                {
                    var (key, value) = ProcessSortAndColumn(item);
                    result.TryAdd(key, value);
                }
            }
            else
            {
                var (key, value) = ProcessSortAndColumn(sortOrder);
                result.Add(key, value);
            }
        }

        return result;
    }

    private static (string, string) ProcessSortAndColumn(string item)
    {
        if (!item.Contains('-', StringComparison.OrdinalIgnoreCase))
        {
            throw new FormatException("Sort condition should be follow by format: Column1-ASC, Column2-DESC,...");
        }

        var property = item.Trim().Split('-');
        var key = ProductExtension.GetSortProductProperty(property[0]);
        var value = ConvertStringToSortOrder(property[1]);
        return (key!, value!);
    }
}
