using System.Linq.Expressions;

namespace Bny.Equations;

/// <summary>
/// Represents a constant
/// </summary>
public class Num : Operation
{
    /// <summary>
    /// Creates zero
    /// </summary>
    public Num() : this(Number.Zero) { }
    /// <summary>
    /// Creates constant with the given value
    /// </summary>
    /// <param name="n"></param>
    public Num(Number n) : base(n, Number.One) { }

    public override Number Eval(Func<Variable, Number> _) => Coefficient;
    public override bool IsSame(Operation other) => other is Num;
    public override Operation With(Number coeffitient, Number power) => new Num(coeffitient);

    public override Expression ToExpression(Func<Variable, Expression> _) => Expression.Constant(Coefficient.Value, typeof(double));

    public override bool TryDerive(Func<Variable, bool> _, out Operation? derivative)
    {
        derivative = null;
        return true;
    }

    public override string ToString() => $"{(Coefficient > 0 ? "+" : "")}{Coefficient}";
    public override string ToString(string? format, IFormatProvider? formatProvider) => $"{(Coefficient > 0 ? "+" : "")}{Coefficient.ToString(format, formatProvider)}";

    public static Num operator +(Num a, Num b) => new(a.Coefficient + b.Coefficient);
    public static Num operator +(Num a, Number b) => new(a.Coefficient + b);
    public static Num operator +(Num a, double b) => new(a.Coefficient + b);
    public static Num operator +(Num a, int b) => new(a.Coefficient + b);
    public static Num operator +(Number a, Num b) => new(a + b.Coefficient);
    public static Num operator +(double a, Num b) => new(a + b.Coefficient);
    public static Num operator +(int a, Num b) => new(a + b.Coefficient);
    public static Num operator -(Num a, Num b) => new(a.Coefficient - b.Coefficient);
    public static Num operator -(Num a, Number b) => new(a.Coefficient - b);
    public static Num operator -(Num a, double b) => new(a.Coefficient - b);
    public static Num operator -(Num a, int b) => new(a.Coefficient - b);
    public static Num operator -(Number a, Num b) => new(a - b.Coefficient);
    public static Num operator -(double a, Num b) => new(a - b.Coefficient);
    public static Num operator -(int a, Num b) => new(a - b.Coefficient);
    public static Num operator -(Num a) => new(-a.Coefficient);
    public static Num operator *(Num a, Num b) => new(a.Coefficient * b.Coefficient);
    public static Num operator *(Num a, Number b) => new(a.Coefficient * b);
    public static Num operator *(Num a, double b) => new(a.Coefficient * b);
    public static Num operator *(Num a, int b) => new(a.Coefficient * b);
    public static Num operator *(Number a, Num b) => new(a * b.Coefficient);
    public static Num operator *(double a, Num b) => new(a * b.Coefficient);
    public static Num operator *(int a, Num b) => new(a * b.Coefficient);
    public static Num operator /(Num a, Num b) => new(a.Coefficient / b.Coefficient);
    public static Num operator /(Num a, Number b) => new(a.Coefficient / b);
    public static Num operator /(Num a, double b) => new(a.Coefficient / b);
    public static Num operator /(Num a, int b) => new(a.Coefficient / b);
    public static Num operator /(Number a, Num b) => new(a / b.Coefficient);
    public static Num operator /(double a, Num b) => new(a / b.Coefficient);
    public static Num operator /(int a, Num b) => new(a / b.Coefficient);
    public static Num operator ^(Num a, Num b) => new(a.Coefficient ^ b.Coefficient);
    public static Num operator ^(Num a, Number b) => new(a.Coefficient ^ b);
    public static Num operator ^(Num a, double b) => new(a.Coefficient ^ b);
    public static Num operator ^(Num a, int b) => new(a.Coefficient ^ b);
    public static Num operator ^(Number a, Num b) => new(a ^ b.Coefficient);
    public static Num operator ^(double a, Num b) => new(a ^ b.Coefficient);
    public static Num operator ^(int a, Num b) => new(a ^ b.Coefficient);

}
