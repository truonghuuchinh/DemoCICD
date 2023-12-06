namespace DemoCICD.Contract.Extensions;
public static class ProductExtension
{
    public static string GetSortProductProperty(string sortColumn)
        => sortColumn.ToLower(System.Globalization.CultureInfo.CurrentCulture) switch
        {
            "name" => "Name",
            "price" => "Price",
            "description" => "Description",
            _ => "Id"
        };
}
