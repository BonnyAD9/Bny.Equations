using System.Linq.Expressions;

namespace Bny.Equations;

/// <summary>
/// Indicates that this is able to be expressed using System.Linq.Expressions.Expression
/// </summary>
public interface IExpressable
{
    /// <summary>
    /// Creates expression
    /// </summary>
    /// <param name="p">Gets the expression for variables</param>
    /// <returns>Expression of this</returns>
    public Expression ToExpression(VariableGetter p);
}

/// <summary>
/// Gets the correct expression for this variable
/// </summary>
/// <param name="v">Variable for which the expression will be get</param>
/// <returns>Expression representing that variable</returns>
public delegate Expression VariableGetter(Variable v);

/// <summary>
/// Provides useful extensions for IExpressable
/// </summary>
public static class ExpressableExtensions
{
    /// <summary>
    /// Compiles this into lambda expression that takes single argument that will be used for all variables
    /// </summary>
    /// <param name="ex">What will be compiled</param>
    /// <returns>Compiled function</returns>
    public static Func<double, double> Compile(this IExpressable ex)
    {
        var x = Expression.Parameter(typeof(double), "x");
        return Expression.Lambda<Func<double, double>>(ex.ToExpression(p => x), x).Compile();
    }

    /// <summary>
    /// Compiles this into lambda expression that takes single argument that will be used for all unset variables
    /// </summary>
    /// <param name="ex">What will be compiled</param>
    /// <returns>Compiled function</returns>
    public static Func<double, double> CompileUnset(this IExpressable ex)
    {
        var x = Expression.Parameter(typeof(double), "x");
        return Expression.Lambda<Func<double, double>>(ex.ToExpression(p => p.HasValue ? Expression.Constant(p.Value.Value, typeof(double)) : x)).Compile();
    }
}