namespace Bny.Equations;

public interface IEvaluatable
{
    /// <summary>
    /// Evaluates this with variables evaluated trough getter
    /// </summary>
    /// <param name="getter">Function that gets value from variable</param>
    /// <returns>Evaluated result</returns>
    public Number Eval(ValueGetter getter);
}

public delegate Number ValueGetter(Variable v);

public static class EvaluatableExtensions
{
    /// <summary>
    /// Evaluates this with variables having their values
    /// </summary>
    /// <param name="ie">What will be evaluated</param>
    /// <returns>Evaluated result</returns>
    /// <exception cref="ArgumentException">Thrown when any variable that will try to be evaluated has no value</exception>
    public static Number Eval(this IEvaluatable ie) => ie.Eval(v => v.HasValue ? v.Value : throw new ArgumentException($"Variable {v} had no value."));

    /// <summary>
    /// Evaluates this with all variables having the given value
    /// </summary>
    /// <param name="ie">What will be evaluated</param>
    /// <param name="n">Value of variables</param>
    /// <returns>Evaluated result</returns>
    public static Number Eval(this IEvaluatable ie, Number n) => ie.Eval(_ => n);

    /// <summary>
    /// Tries to evaluate this
    /// </summary>
    /// <param name="ie">What will be evaluated</param>
    /// <param name="res">Result of evaluation</param>
    /// <returns>True on success, otherwise false</returns>
    public static bool TryEval(this IEvaluatable ie, out Number res)
    {
        try
        {
            res = ie.Eval();
            return true;
        }
        catch (ArgumentException)
        {
            res = Number.NaN;
            return false;
        }
    }

    /// <summary>
    /// Evaluates this with variables having their value if their have it, otherwise default value n is used
    /// </summary>
    /// <param name="ie">What will be evaluated</param>
    /// <param name="n">Default value for variables without value</param>
    /// <returns>Evaluated result</returns>
    public static Number EvalUnset(this IEvaluatable ie, Number n) => ie.Eval(v => v.HasValue ? v.Value : n);
}
