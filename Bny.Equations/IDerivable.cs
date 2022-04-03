namespace Bny.Equations;

/// <summary>
/// May be derived
/// </summary>
/// <typeparam name="T">Result of derivation</typeparam>
internal interface IDerivable<T> where T : class
{
    /// <summary>
    /// Derives the this
    /// </summary>
    /// <param name="v">Variable over which will be derived</param>
    /// <param name="derivative">Result of the derivation, null if the derivation cannot be done or if the result of derivation is 0</param>
    /// <returns>true on succes, otherwise false</returns>
    public bool TryDerive(Func<Variable, bool> v, out T? derivative);
}