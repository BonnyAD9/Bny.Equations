namespace Bny.Equations;

/// <summary>
/// Variable raised to a power and multiplied by a coefficient
/// </summary>
public class Element : Operation, IEquatable<Element>
{
    /// <summary>
    /// Variable that is in this element
    /// </summary>
    public Variable Variable { get; init; }

    /// <summary>
    /// Returns value indicating whether this is constant
    /// </summary>
    public bool IsConstant => !Variable.IsValid;

    /// <summary>
    /// Returns value indicating whether the variable has set value and this element can be evaluated
    /// </summary>
    public bool IsEvaluatable => Variable.HasValue;

    /// <summary>
    /// Creates zero
    /// </summary>
    public Element() : this(Number.Zero, Variable.Invalid, Number.One) { }

    /// <summary>
    /// Creates the given number
    /// </summary>
    /// <param name="coefficient">Value of this number</param>
    public Element(Number coefficient) : this(coefficient, Variable.Invalid, Number.One) { }

    /// <summary>
    /// Creates element with variable multiplied by a number
    /// </summary>
    /// <param name="coefficient">Multiplier of the variable</param>
    /// <param name="variable">Variable</param>
    public Element(Number coefficient, Variable variable) : this(coefficient, variable, Number.One) { }

    /// <summary>
    /// Creates element with variable
    /// </summary>
    /// <param name="variable">variable to create element with</param>
    public Element(Variable variable) : this(Number.One, variable, Number.One) { }

    /// <summary>
    /// Creates element with variable raised to a power
    /// </summary>
    /// <param name="variable">Variable</param>
    /// <param name="power">Power to which the variable will be raised</param>
    public Element(Variable variable, Number power) : this(Number.One, variable, power) { }

    /// <summary>
    /// Creates element with variable raised to a power and multiplied
    /// </summary>
    /// <param name="coefficient">Multiplier</param>
    /// <param name="variable">Variable</param>
    /// <param name="power">Power to which the variable will be raised</param>
    public Element(Number coefficient, Variable variable, Number power) : base(coefficient, power)
    {
        Variable = Coefficient == Number.Zero || power == Number.Zero ? Variable.Invalid : variable;
    }

    /// <summary>
    /// Gets value indicating whether the given element can be added to this element
    /// </summary>
    /// <param name="el">Element that would be added</param>
    /// <returns>True if the element can be added to this one, otherwise false</returns>
    public bool CanAdd(Element el) => Variable == el.Variable && Power == el.Power;

    /// <summary>
    /// Adds the given element to this if it is possible
    /// </summary>
    /// <param name="el">Element that might be added</param>
    /// <returns>Value indicating whether the element was added or not</returns>
    public static bool TryAdd(Element e1, Element e2, out Element res)
    {
        res = new();
        if (!e1.CanAdd(e2))
            return false;

        res = new(e1.Coefficient + e2.Coefficient, e1.Variable, e1.Power);
        return true;
    }

    public override Number Eval(ValueGetter vg) => IsConstant ? Coefficient : Coefficient * (vg(Variable) ^ Power);

    public static bool operator ==(Element a, Element b) => a.Variable == b.Variable && a.Coefficient == b.Coefficient && a.Power == b.Power;
    public static bool operator !=(Element a, Element b) => a.Variable != b.Variable || a.Coefficient != b.Coefficient || a.Power != b.Power;
    public static bool operator <(Element a, Element b) => a.Power < b.Power;
    public static bool operator >(Element a, Element b) => a.Power > b.Power;
    public static bool operator <=(Element a, Element b) => a.Power <= b.Power;
    public static bool operator >=(Element a, Element b) => a.Power >= b.Power;
    public static Function operator +(Element a, Element b) => new(a, b);
    public static Function operator +(Element a, Number b) => new(a, b);
    public static Function operator +(Element a, Variable b) => new(a, b);
    public static Function operator +(Element a, double b) => new(a, b);
    public static Function operator +(Element a, int b) => new(a, b);
    public static Function operator +(Variable a, Element b) => new(b, a);
    public static Function operator +(Number a, Element b) => new(b, a);
    public static Function operator +(double a, Element b) => new(b, a);
    public static Function operator +(int a, Element b) => new(b, a);
    public static Element operator -(Element a) => new(-a.Coefficient, a.Variable, a.Power);
    public static Function operator -(Element a, Element b) => new(a, -b);
    public static Function operator -(Element a, Number b) => new(a, -b);
    public static Function operator -(Element a, Variable b) => new(a, -b);
    public static Function operator -(Element a, double b) => new(a, -b);
    public static Function operator -(Element a, int b) => new(a, -b);
    public static Function operator -(Number a, Element b) => new(-b, a);
    public static Function operator -(Variable a, Element b) => new(-b, a);
    public static Function operator -(double a, Element b) => new(-b, a);
    public static Function operator -(int a, Element b) => new(-b, a);
    public static Element operator *(Element a, Element b) => new(a.Coefficient * b.Coefficient, a.Variable, a.Power + b.Power);
    public static Element operator *(Element a, Number b) => new(a.Coefficient * b, a.Variable, a.Power);
    public static Element operator *(Element a, Variable _) => new(a.Coefficient, a.Variable, a.Power + 1);
    public static Element operator *(Element a, double b) => new(a.Coefficient * b, a.Variable, a.Power);
    public static Element operator *(Element a, int b) => new(a.Coefficient * b, a.Variable, a.Power);
    public static Element operator *(Variable _, Element b) => new(b.Coefficient, b.Variable, b.Power + 1);
    public static Element operator *(Number a, Element b) => new(a * b.Coefficient, b.Variable, b.Power);
    public static Element operator *(double a, Element b) => new(a * b.Coefficient, b.Variable, b.Power);
    public static Element operator *(int a, Element b) => new(a * b.Coefficient, b.Variable, b.Power);
    public static Element operator /(Element a, Element b) => new(a.Coefficient / b.Coefficient, a.Variable, a.Power - b.Power);
    public static Element operator /(Element a, Number b) => new(a.Coefficient / b, a.Variable, a.Power);
    public static Element operator /(Element a, Variable _) => new(a.Coefficient, a.Variable, a.Power - 1);
    public static Element operator /(Element a, double b) => new(a.Coefficient / b, a.Variable, a.Power);
    public static Element operator /(Element a, int b) => new(a.Coefficient / b, a.Variable, a.Power);
    public static Element operator /(Number a, Element b) => new(a / b.Coefficient, b.Variable, -b.Power);
    public static Element operator /(Variable _, Element b) => new(b.Coefficient, b.Variable, 1 - b.Power);
    public static Element operator /(double a, Element b) => new(a / b.Coefficient, b.Variable, -b.Power);
    public static Element operator /(int a, Element b) => new(a / b.Coefficient, b.Variable, -b.Power);

    public bool Equals(Element? other) => other is not null && this == other;
    public override bool Equals(object? obj) => base.Equals(obj);
    public override int GetHashCode() => base.GetHashCode();

    public override string ToString() => ToString("0.##", null);

    public override Operation With(Number coeffitient, Number power) => new Element(coeffitient, Variable, power);

    public override bool IsSame(Operation other) => other is Element o && o.Variable == Variable;

    public override string ToString(string? format, IFormatProvider? formatProvider) => (IsConstant, Power.Value, Coefficient.Value) switch
    {
        (_, _, 0) => $"+{0.ToString(format, formatProvider)}",
        (true, _, > 0) or (_, 0, > 0) => $"+{Coefficient.ToString(format, formatProvider)}",
        (true, _, _) or (_, 0, _) => Coefficient.ToString(format, formatProvider),
        (_, 1, 1) => $"+{Variable}",
        (_, 1, -1) => $"-{Variable}",
        (_, 1, > 0) => $"+{Coefficient.ToString(format, formatProvider)}{Variable}",
        (_, 1, _) => $"{Coefficient.ToString(format, formatProvider)}{Variable}",
        (_, -1, > 0) => $"+{Coefficient.ToString(format, formatProvider)}/{Variable}",
        (_, -1, _) => $"{Coefficient.ToString(format, formatProvider)}/{Variable}",
        (_, < 0, > 0) => $"+{Coefficient.ToString(format, formatProvider)}/{Variable}^{Power.ToString(format, formatProvider)}",
        (_, < 0, _) => $"{Coefficient.ToString(format, formatProvider)}/{Variable}^{Power.ToString(format, formatProvider)}",
        (_, _, 1) => $"+{Variable}^{Power.ToString(format, formatProvider)}",
        (_, _, -1) => $"-{Variable}^{Power.ToString(format, formatProvider)}",
        (_, _, > 0) => $"+{Coefficient.ToString(format, formatProvider)}{Variable}^{Power.ToString(format, formatProvider)}",
        _ => $"{Coefficient.ToString(format, formatProvider)}{Variable}^{Power.ToString(format, formatProvider)}",
    };
}
