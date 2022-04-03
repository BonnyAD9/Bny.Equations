using System.Linq.Expressions;

namespace Bny.Equations;

public interface IExpressable
{
    public Expression ToExpression(VariableGetter p);
}

public delegate Expression VariableGetter(Variable v);

public static class ExpressableExtensions
{
    public static Func<double, double> Compile(this IExpressable ex)
    {
        var x = Expression.Parameter(typeof(double), "x");
        return Expression.Lambda<Func<double, double>>(ex.ToExpression(p => x), x).Compile();
    }
}