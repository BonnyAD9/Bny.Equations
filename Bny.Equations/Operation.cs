using System.Diagnostics.CodeAnalysis;

namespace Bny.Equations;

/// <summary>
/// Represents mathematical operation
/// </summary>
public abstract class Operation : IFormattable
{
    /// <summary>
    /// Multiplier of a value
    /// </summary>
    public virtual Number Coefficient { get; init; }
    /// <summary>
    /// Powe to which a value is raised
    /// </summary>
    public virtual Number Power { get; init; }

    /// <summary>
    /// Initializes operation
    /// </summary>
    /// <param name="coefficient">Multiplier of a value</param>
    /// <param name="power">Power to which a value is raised</param>
    public Operation(Number coefficient, Number power)
    {
        Coefficient = coefficient;
        Power = power;
    }

    /// <summary>
    /// Creates new Operation of the same class with changed values
    /// </summary>
    /// <param name="coefficient">New coefficient</param>
    /// <param name="power">New power</param>
    /// <returns>New Operation of the same type that derives it</returns>
    public abstract Operation With(Number coefficient, Number power);

    /// <summary>
    /// Evaluates this operation with the given value as variable
    /// </summary>
    /// <param name="n">Value for variables</param>
    /// <returns>Evaluated result</returns>
    public abstract Number Evaluate(Number n);
    /// <summary>
    /// Gets value indicating whether the body of this instance is same as the other
    /// </summary>
    /// <param name="other">Other instance</param>
    /// <returns>true if yes, otherwise false</returns>
    public abstract bool IsSame(Operation other);

    /// <summary>
    /// Creates new Operation of the same class with changed value
    /// </summary>
    /// <param name="coefficient">New coefficient</param>
    /// <returns>New Operation of the same type that derives it</returns>
    public Operation WithCoef(Number coefficient) => With(coefficient, Power);

    /// <summary>
    /// Creates new Operation of the same class with changed value
    /// </summary>
    /// <param name="power">New power</param>
    /// <returns>New Operation of the same type that derives it</returns>
    public Operation WithPower(Number power) => With(Coefficient, power);

    /// <summary>
    /// Tries to add two operations
    /// </summary>
    /// <param name="a">First operation</param>
    /// <param name="b">Second operation</param>
    /// <param name="res">Sum of operations</param>
    /// <returns>true if possible, otherwise false</returns>
    public static bool TryAdd(Operation a, Operation b, [NotNullWhen(true)] out Operation? res)
    {
        if (a.Power == b.Power && a.IsSame(b))
        {
            res = a.WithCoef(a.Coefficient + b.Coefficient);
            return true;
        }
        res = null;
        return false;
    }

    /// <summary>
    /// Tries to subtract two operations
    /// </summary>
    /// <param name="a">First operation</param>
    /// <param name="b">Second operation</param>
    /// <param name="res">Difference of operations</param>
    /// <returns>true if possible, otherwise false</returns>
    public static bool TrySubtract(Operation a, Operation b, [NotNullWhen(true)] out Operation? res)
    {
        if (a.Power == b.Power && a.IsSame(b))
        {
            res = a.WithCoef(a.Coefficient - b.Coefficient);
            return true;
        }

        res = null;
        return false;
    }

    /// <summary>
    /// Tries to multiply two operations
    /// </summary>
    /// <param name="a">First operation</param>
    /// <param name="b">Second operation</param>
    /// <param name="res">Product of operations</param>
    /// <returns>true if possible, otherwise false</returns>
    public static bool TryMultiply(Operation a, Operation b, [NotNullWhen(true)] out Operation? res)
    {
        if (a.IsSame(b))
        {
            res = a.With(a.Coefficient * b.Coefficient, a.Power + b.Power);
            return true;
        }

        res = null;
        return false;
    }

    /// <summary>
    /// Tries to divide two operations
    /// </summary>
    /// <param name="a">First operation</param>
    /// <param name="b">Second operation</param>
    /// <param name="res">Result of division of operations</param>
    /// <returns>true if possible, otherwise false</returns>
    public static bool TryDivide(Operation a, Operation b, [NotNullWhen(true)] out Operation? res)
    {
        if (a.IsSame(b))
        {
            res = a.With(a.Coefficient / b.Coefficient, a.Power - b.Power);
            return true;
        }

        res = null;
        return false;
    }

    /// <summary>
    /// Multiplies operation with number
    /// </summary>
    /// <param name="a">Operation to multiply</param>
    /// <param name="b">Multiplier</param>
    /// <returns>New operation</returns>
    public static Operation Multiply(Operation a, Number b) => a.WithCoef(a.Coefficient * b);

    /// <summary>
    /// Divides operation with number
    /// </summary>
    /// <param name="a">Operation to divide</param>
    /// <param name="b">Divisor</param>
    /// <returns>New operation</returns>
    public static Operation Divide(Operation a, Number b) => a.WithCoef(a.Coefficient / b);

    /// <summary>
    /// Divides number with operation
    /// </summary>
    /// <param name="a">Operation to divide with</param>
    /// <param name="b">Number to divide</param>
    /// <returns>New operation</returns>
    public static Operation Divide(Number a, Operation b) => b.With(a / b.Coefficient, -b.Power);

    /// <summary>
    /// Raises operation to the given power
    /// </summary>
    /// <param name="a">Operation to raise</param>
    /// <param name="b">Power</param>
    /// <returns>New operation</returns>
    public static Operation Raise(Operation a, Number b) => a.With(a.Coefficient ^ b, a.Power * b);

    public static bool operator <(Operation a, Operation b) => a.Power < b.Power;
    public static bool operator >(Operation a, Operation b) => a.Power > b.Power;
    public static bool operator <=(Operation a, Operation b) => a.Power >= b.Power;
    public static bool operator >=(Operation a, Operation b) => a.Power <= b.Power;
    public static Function operator +(Operation a, Operation b) => new(a, b);
    public static Function operator +(Operation a, Number b) => new(a, b);
    public static Function operator +(Operation a, double b) => new(a, b);
    public static Function operator +(Operation a, int b) => new(a, b);
    public static Function operator +(Number a, Operation b) => new(b, a);
    public static Function operator +(double a, Operation b) => new(b, a);
    public static Function operator +(int a, Operation b) => new(b, a);
    public static Function operator -(Operation a, Operation b) => new(a, -b);
    public static Function operator -(Operation a, Number b) => new(a, -b);
    public static Function operator -(Operation a, double b) => new(a, -b);
    public static Function operator -(Operation a, int b) => new(a, -b);
    public static Function operator -(Number a, Operation b) => new(-b, a);
    public static Function operator -(double a, Operation b) => new(-b, a);
    public static Function operator -(int a, Operation b) => new(-b, a);
    public static Operation operator -(Operation a) => a.WithCoef(-a.Coefficient);
    public static Operation operator *(Operation a, Number b) => Multiply(a, b);
    public static Operation operator *(Operation a, double b) => Multiply(a, b);
    public static Operation operator *(Operation a, int b) => Multiply(a, b);
    public static Operation operator *(Number a, Operation b) => Multiply(b, a);
    public static Operation operator *(double a, Operation b) => Multiply(b, a);
    public static Operation operator *(int a, Operation b) => Multiply(b, a);
    public static Operation operator /(Operation a, Number b) => Divide(a, b);
    public static Operation operator /(Operation a, double b) => Divide(a, b);
    public static Operation operator /(Operation a, int b) => Divide(a, b);
    public static Operation operator /(Number a, Operation b) => Divide(a, b);
    public static Operation operator /(double a, Operation b) => Divide(a, b);
    public static Operation operator /(int a, Operation b) => Divide(a, b);
    public static Operation operator ^(Operation a, Number b) => Raise(a, b);
    public static Operation operator ^(Operation a, double b) => Raise(a, b);
    public static Operation operator ^(Operation a, int b) => Raise(a, b);

    public abstract string ToString(string? format, IFormatProvider? formatProvider);
}
