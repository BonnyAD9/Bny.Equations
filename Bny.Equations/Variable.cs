namespace Bny.Equations;

/// <summary>
/// Variable
/// </summary>
public class Variable : IEvaluatable
{
    /// <summary>
    /// Name of the variable should be unique for the equation
    /// </summary>
    public Identifier ID { get; init; }
    /// <summary>
    /// Variables can behave as constants when they have value
    /// </summary>
    public Number Value { get; set; }

    /// <summary>
    /// Gets value indicating whether this variable is valid, if not it is ignored
    /// </summary>
    public bool IsValid => ID != Identifier.Invalid;

    /// <summary>
    /// Gets value indicating whether the variable has set value
    /// </summary>
    public bool HasValue => !Value.IsNaN;

    /// <summary>
    /// Invalid variable
    /// </summary>
    public static readonly Variable Invalid = new(Identifier.Invalid);

    /// <summary>
    /// Initializes new variable with the given id
    /// </summary>
    /// <param name="id">Unique id of the variable</param>
    public Variable(Identifier id) : this(id, Number.NaN) { }

    /// <summary>
    /// Initializes variable with value and id
    /// </summary>
    /// <param name="id">Unique id</param>
    /// <param name="value">Value of the variable, will act as constant</param>
    public Variable(Identifier id, Number value)
    {
        ID = id;
        Value = id == Identifier.Invalid ? Number.One : value;
    }

    public static bool operator ==(Variable a, Variable b) => a.ID == b.ID;
    public static bool operator !=(Variable a, Variable b) => a.ID != b.ID;
    public static Element operator -(Variable v) => new(-1, v);
    public static Element operator +(Variable a, Variable _) => new(2, a);
    public static Function operator +(Variable a, Number b) => new(a, b);
    public static Function operator +(Variable a, double b) => new(a, b);
    public static Function operator +(Variable a, int b) => new(a, b);
    public static Function operator +(Number a, Variable b) => new(b, a);
    public static Function operator +(double a, Variable b) => new(b, a);
    public static Function operator +(int a, Variable b) => new(b, a);
    public static Number operator -(Variable _, Variable __) => Number.Zero;
    public static Function operator -(Variable a, Number b) => new(a, -b);
    public static Function operator -(Variable a, double b) => new(a, -b);
    public static Function operator -(Variable a, int b) => new(a, -b);
    public static Function operator -(Number a, Variable b) => new(new Element(-1, b), a);
    public static Function operator -(double a, Variable b) => new(new Element(-1, b), a);
    public static Function operator -(int a, Variable b) => new(new Element(-1, b), a);
    public static Element operator *(Variable a, Variable _) => new(a, 2);
    public static Element operator *(Variable a, Number b) => new(a, b);
    public static Element operator *(Variable a, double b) => new(a, b);
    public static Element operator *(Variable a, int b) => new(a, b);
    public static Element operator *(Number a, Variable v) => new(a, v);
    public static Element operator *(double a, Variable v) => new(a, v);
    public static Element operator *(int a, Variable v) => new(a, v);
    public static Element operator /(Number a, Variable v) => new(a, v, -1);
    public static Element operator /(double a, Variable v) => new(a, v, -1);
    public static Element operator /(int a, Variable v) => new(a, v, -1);
    public static Element operator /(Variable v, Number a) => new(!a, v);
    public static Element operator /(Variable v, double a) => new(!new Number(a), v);
    public static Element operator /(Variable v, int a) => new(!new Number(a), v);
    /// <summary>
    /// Raises this variable to the given power
    /// </summary>
    /// <param name="v">Variable to raise</param>
    /// <param name="a">Number to which the variable will be raised</param>
    /// <returns></returns>
    public static Element operator ^(Variable v, Number a) => new(v, a);
    /// <summary>
    /// Raises this variable to the given power
    /// </summary>
    /// <param name="v">Variable to raise</param>
    /// <param name="a">Number to which the variable will be raised</param>
    /// <returns></returns>
    public static Element operator ^(Variable v, double a) => new(v, a);
    /// <summary>
    /// Raises this variable to the given power
    /// </summary>
    /// <param name="v">Variable to raise</param>
    /// <param name="a">Number to which the variable will be raised</param>
    /// <returns></returns>
    public static Element operator ^(Variable v, int a) => new(v, a);

    public static implicit operator Variable(Identifier i) => new(i);

    public override bool Equals(object? obj) => base.Equals(obj);
    public override int GetHashCode() => base.GetHashCode();
    public override string ToString() => ID.ToString().ToLower();

    public Number Eval(ValueGetter vg) => vg(this);
}
