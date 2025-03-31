using Dapper;
using System.Text;

namespace Shared.Core.Extentions;

public static class SqlExtentions
{
    public static void ApplyPagination(
        this StringBuilder sqlBuilder,
        DynamicParameters dynamicParameters,
        int page,
        int pageSize)
    {
        dynamicParameters.Add("@PageSize", pageSize);
        dynamicParameters.Add("@Offset", (page - 1) * pageSize);

        sqlBuilder.Append(" LIMIT @PageSize OFFSET @Offset");
    }

    public static void ApplySorting(
        this StringBuilder sqlBuilder,
        DynamicParameters dynamicParameters,
        string? sortBy,
        string? sortDirection)
    {
        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            dynamicParameters.Add("@SortBy", sortBy);
            dynamicParameters.Add("@SortDirection", sortDirection ?? "asc");

            sqlBuilder.Append(" ORDER BY @SortBy @SortDirection");
        }
    }

    public static void ApplyFilterByColumn<T>(
        this StringBuilder sqlBuilder,
        string? columnName,
        T? queryFieldValue)
    {
        if (queryFieldValue is not null)
        {
            sqlBuilder = sqlBuilder.ToString().ToLower().Contains("where") ? sqlBuilder.Append(" AND ") : sqlBuilder.Append(" WHERE ");

            if (queryFieldValue.GetType() == typeof(string))
            {
                sqlBuilder.Append($"{columnName} = '{queryFieldValue}'");
            }
            else
            {
                sqlBuilder.Append($"{columnName} = {queryFieldValue}");
            }

        }
    }
}
