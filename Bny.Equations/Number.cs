namespace Bny.Equations;

/// <summary>
/// Immutable structure indicating number
/// </summary>
public readonly struct Number : IComparable, IComparable<Number>, IEquatable<Number>, IFormattable, IEvaluatable
{
    /// <summary>
    /// Value of the number
    /// </summary>
    public double Value { get; init; }

    /// <summary>
    /// Indicates whether the value is number
    /// </summary>
    public bool IsNaN => double.IsNaN(Value);

    /// <summary>
    /// Initializes new number from double
    /// </summary>
    /// <param name="value"></param>
    public Number(double value) => Value = value;

    /// <summary>
    /// Number with value of 0
    /// </summary>
    public static readonly Number Zero = new(0);
    /// <summary>
    /// Number with number of 1
    /// </summary>
    public static readonly Number One = new(1);

    /// <summary>
    /// Creates invalid number
    /// </summary>
    public static readonly Number NaN = new(double.NaN);

    public Number Eval(Func<Variable, Number> _) => Value;

    public static Number Abs(Number value) => new(Math.Abs(value.Value));
    public static Number Pow(Number value) => new(Math.Pow(value.Value, 2));
    public static Number Pow(Number value, Number power) => new(Math.Pow(value.Value, power.Value));
    public static Number Sqrt(Number value) => new(Math.Sqrt(value.Value));

    public static Number operator +(Number a, Number b) => new(a.Value + b.Value);
    public static Number operator +(Number a, double b) => new(a.Value + b);
    public static Number operator +(Number a, int b) => new(a.Value + b);
    public static Number operator +(double a, Number b) => new(a + b.Value);
    public static Number operator +(int a, Number b) => new(a + b.Value);
    public static Number operator -(Number a, Number b) => new(a.Value - b.Value);
    public static Number operator -(Number a, double b) => new(a.Value - b);
    public static Number operator -(double a, Number b) => new(a + b.Value);
    public static Number operator -(Number a, int b) => new(a.Value - b);
    public static Number operator -(int a, Number b) => new(a + b.Value);
    public static Number operator -(Number n) => new(-n.Value);
    public static Number operator *(Number a, Number b) => new(a.Value * b.Value);
    public static Number operator *(Number a, double b) => new(a.Value * b);
    public static Number operator *(Number a, int b) => new(a.Value * b);
    public static Number operator *(double a, Number b) => new(a * b.Value);
    public static Number operator *(int a, Number b) => new(a * b.Value);
    public static Number operator /(Number a, Number b) => new(a.Value / b.Value);
    public static Number operator /(Number a, double b) => new(a.Value / b);
    public static Number operator /(double a, Number b) => new(a / b.Value);
    public static Number operator /(Number a, int b) => new(a.Value / b);
    public static Number operator /(int a, Number b) => new(a / b.Value);
    public static bool operator ==(Number a, Number b) => a.Value == b.Value;
    public static bool operator ==(Number a, double b) => a.Value == b;
    public static bool operator ==(Number a, int b) => a.Value == b;
    public static bool operator !=(Number a, Number b) => a.Value != b.Value;
    public static bool operator !=(Number a, double b) => a.Value != b;
    public static bool operator !=(Number a, int b) => a.Value != b;
    public static bool operator >(Number a, Number b) => a.Value > b.Value;
    public static bool operator >(Number a, double b) => a.Value > b;
    public static bool operator >(double a, Number b) => a > b.Value;
    public static bool operator >(Number a, int b) => a.Value > b;
    public static bool operator >(int a, Number b) => a > b.Value;
    public static bool operator <(Number a, Number b) => a.Value < b.Value;
    public static bool operator <(Number a, double b) => a.Value < b;
    public static bool operator <(double a, Number b) => a < b.Value;
    public static bool operator <(Number a, int b) => a.Value < b;
    public static bool operator <(int a, Number b) => a < b.Value;
    public static bool operator >=(Number a, Number b) => a.Value >= b.Value;
    public static bool operator >=(Number a, double b) => a.Value >= b;
    public static bool operator >=(double a, Number b) => a >= b.Value;
    public static bool operator >=(Number a, int b) => a.Value >= b;
    public static bool operator >=(int a, Number b) => a >= b.Value;
    public static bool operator <=(Number a, Number b) => a.Value <= b.Value;
    public static bool operator <=(Number a, double b) => a.Value <= b;
    public static bool operator <=(double a, Number b) => a <= b.Value;
    public static bool operator <=(Number a, int b) => a.Value <= b;
    public static bool operator <=(int a, Number b) => a <= b.Value;
    /// <summary>
    /// Raises number to the given power
    /// </summary>
    /// <param name="a">Number to raise</param>
    /// <param name="b">Power to which to raise</param>
    /// <returns>New number</returns>
    public static Number operator ^(Number a, Number b) => Pow(a, b);
    /// <summary>
    /// Raises number to the given power
    /// </summary>
    /// <param name="a">Number to raise</param>
    /// <param name="b">Power to which to raise</param>
    /// <returns>New number</returns>
    public static Number operator ^(Number a, double b) => Pow(a, b);
    /// <summary>
    /// Raises number to the given power
    /// </summary>
    /// <param name="a">Number to raise</param>
    /// <param name="b">Power to which to raise</param>
    /// <returns>New number</returns>
    public static Number operator ^(double a, Number b) => Pow(a, b);
    /// <summary>
    /// Raises number to the given power
    /// </summary>
    /// <param name="a">Number to raise</param>
    /// <param name="b">Power to which to raise</param>
    /// <returns>New number</returns>
    public static Number operator ^(Number a, int b) => Pow(a, b);
    /// <summary>
    /// Raises number to the given power
    /// </summary>
    /// <param name="a">Number to raise</param>
    /// <param name="b">Power to which to raise</param>
    /// <returns>New number</returns>
    public static Number operator ^(int a, Number b) => Pow(a, b);
    /// <summary>
    /// Calculates the inverse of the number (1 / a)
    /// </summary>
    /// <param name="a">Number which inverse will be calculated</param>
    /// <returns>Inverse of the number (1 / a)</returns>
    public static Number operator !(Number a) => new(1 / a.Value);
    /// <summary>
    /// Calculates the absolute value of the number
    /// </summary>
    /// <param name="a">Number which absolute value will be calculated</param>
    /// <returns>Absolute value of the given number</returns>
    public static Number operator ~(Number a) => Abs(a);
    public static Number operator ++(Number a) => new(a.Value + 1);
    public static Number operator --(Number a) => new(a.Value - 1);

    public static implicit operator Number(double value) => new(value);
    public static implicit operator double(Number value) => value.Value;
    public static implicit operator Number(int value) => new(value);
    public static explicit operator int(Number value) => (int)value.Value;

    public override string ToString() => Value.ToString();
    public string ToString(string? format, IFormatProvider? formatProvider) => Value.ToString(format, formatProvider);
    public string ToString(IFormatProvider formatProvider) => Value.ToString(formatProvider);
    public string ToString(string format) => Value.ToString(format);
    public override bool Equals(object? obj) => Value.Equals(obj);
    public override int GetHashCode() => Value.GetHashCode();
    public int CompareTo(object? obj) => obj is Number n ? CompareTo(n) : throw new ArgumentException();
    public bool Equals(Number other) => other == this;
    public int CompareTo(Number other) => Value.CompareTo(other.Value);
}
