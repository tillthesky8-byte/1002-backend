using System.Data;
using Dapper;
namespace _1002_backend.Infrastructure.Data;

public class DateOnlyHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        parameter.Value = value.ToString("yyyy-MM-dd");
    }

    public override DateOnly Parse(object value)
    {
        return DateOnly.Parse(value.ToString() ?? throw new InvalidOperationException("Cannot parse null to DateOnly."));
    }
}

public class NullableDateOnlyHandler : SqlMapper.TypeHandler<DateOnly?>
{
    public override void SetValue(IDbDataParameter parameter, DateOnly? value)
    {
        parameter.Value = value?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value;
    }

    public override DateOnly? Parse(object value)
    {
        if (value == null || value is DBNull)
        {
            return null;
        }
        return DateOnly.Parse(value.ToString() ?? throw new InvalidOperationException("Cannot parse null to DateOnly."));
    }
}