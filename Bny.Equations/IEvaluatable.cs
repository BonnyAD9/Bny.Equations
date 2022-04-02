namespace Bny.Equations;

internal interface IEvaluatable
{
    /// <summary>
    /// Tries to evaluate this with variables that are set internally
    /// </summary>
    /// <param name="res">Result of the evaluation</param>
    /// <returns>True on success, otherwise false</returns>
    public bool TryEval(out Number res);

    /// <summary>
    /// Evaluates this with the given value of variables
    /// </summary>
    /// <param name="var">Value of variables</param>
    /// <returns>Evaluated result</returns>
    public Number Eval(Number var);

    /// <summary>
    /// Evaluates this with the given value of unset variables
    /// </summary>
    /// <param name="var">Value of variables</param>
    /// <returns>Evaluated result</returns>
    public Number EvalUnset(Number var);
}
