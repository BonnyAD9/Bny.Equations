using System.Linq.Expressions;
using System.Text;

namespace Bny.Equations;

public class Function : IEvaluatable, IExpressable
{
    private LinkedList<Operation> Elements { get; init; }
    private LinkedListNode<Operation> Constant { get; init; }
    private Num CNum => (Constant.Value as Num)!;

    /// <summary>
    /// Creates empty function
    /// </summary>
    public Function()
    {
        Elements = new();
        Constant = Elements.AddFirst(new Num());
    }

    /// <summary>
    /// Creates function with one value
    /// </summary>
    /// <param name="value"></param>
    public Function(Operation value)
    {
        Elements = new();
        if (value is Num)
        {
            Constant = Elements.AddFirst(value);
            return;
        }
        Constant = Elements.AddFirst(new Num());
        Add(value);
    }

    /// <summary>
    /// Copies the given function
    /// </summary>
    /// <param name="f">Function to copy</param>
    public Function(Function f)
    {
        Elements = new();

        if (f.Elements.Count == 0)
        {
            Constant = Elements.AddFirst(new Num());
            return;
        }

        foreach (var e in f.Elements)
        {
            if (e is Num)
            {
                Constant = Elements.AddLast(e);
                continue;
            }
            Elements.AddLast(e);
        }

        if (Constant is null)
            Constant = AddRight(new Num(), Elements.First!);
    }

    private Function(LinkedList<Operation> elements, LinkedListNode<Operation> constant)
    {
        Elements = elements;
        Constant = constant;
    }

    public Function(Variable v, Number n) : this(new Num(n), new Element(v)) { }

    /// <summary>
    /// Initializes function from two elements that will be added
    /// </summary>
    /// <param name="e1">First element</param>
    /// <param name="e2">Second element</param>
    public Function(Operation e1, Operation e2) : this(e1) => Add(e2);

    /// <summary>
    /// Adds element and variable
    /// </summary>
    /// <param name="e">Element</param>
    /// <param name="v">Variable</param>
    public Function(Element e, Variable v) : this(e) => Add(new Element(v));

    /// <summary>
    /// Creates new function from other function with added element
    /// </summary>
    /// <param name="f">Function that will be copied</param>
    /// <param name="e">New element</param>
    public Function(Function f, Operation e) : this(f) => Add(e);

    /// <summary>
    /// Creates new function from other function with added variable
    /// </summary>
    /// <param name="f">Function that will be copied</param>
    /// <param name="v">Variable that will be added</param>
    public Function(Function f, Variable v) : this(f) => Add(new Element(v));

    /// <summary>
    /// Creates new function from other function with added number
    /// </summary>
    /// <param name="f">Function to copy</param>
    /// <param name="n">Number to add</param>
    public Function(Function f, Number n) : this(f) => Add(n);

    /// <summary>
    /// Creates function from sum two functions
    /// </summary>
    /// <param name="f1">First function</param>
    /// <param name="f2">Second function</param>
    public Function(Function f1, Function f2) : this(f1) => Add(f2);

    /// <summary>
    /// Creates function from adding number and element
    /// </summary>
    /// <param name="e">Element</param>
    /// <param name="n">Number to add</param>
    public Function(Operation e, Number n)
    {
        Elements = new();
        if (e is Num num)
        {
            Constant = Elements.AddFirst(num + CNum);
            return;
        }
        Constant = Elements.AddFirst(new Num(n));
        Add(e);
    }

    public Number Eval(ValueGetter vg)
    {
        Number res = Number.Zero;
        foreach (var e in Elements)
            res += e.Eval(vg);
        return res;
    }

    public Expression ToExpression(VariableGetter p)
    {
        Expression e = CNum.ToExpression(p);
        foreach (var el in Elements)
        {
            if (el is Num)
                continue;
            e = Expression.Add(e, el.ToExpression(p));
        }
        return e;
    }

    /// <summary>
    /// Adds function to this function
    /// </summary>
    /// <param name="value">Function to add to this function</param>
    /// <returns>Instance on which this method was called</returns>
    internal Function Add(Function value)
    {
        LinkedListNode<Operation>? ocur = value.Elements.First;
        LinkedListNode<Operation> tcur = Elements.First!;

        while (ocur is not null)
        {
            if (Operation.TryAdd(tcur.Value, ocur.Value, out Operation? e))
            {
                tcur.Value = e;
                ocur = ocur.Next;
                continue;
            }

            if (ocur.Value < tcur.Value)
            {
                Elements.AddBefore(tcur, ocur.Value);
                continue;
            }

            if (tcur.Next is null)
            {
                tcur = Elements.AddAfter(tcur, ocur.Value);
                continue;
            }

            tcur = tcur.Next;
        }

        return this;
    }

    /// <summary>
    /// Adds number to this function
    /// </summary>
    /// <param name="value"></param>
    /// <returns>Instance on which this method was called</returns>
    internal Function Add(Number value)
    {
        Constant.Value = CNum + value;
        return this;
    }

    /// <summary>
    /// Inserts element into others
    /// </summary>
    /// <param name="value">Element to insert</param>
    /// <returns>Instance on which this method was called</returns>
    internal Function Add(Operation value)
    {
        if (Operation.TryAdd(value, Constant.Value, out Operation? r))
        {
            Constant.Value = r;
            return this;
        }

        if (value < Constant.Value)
        {
            AddLeft(value, Constant);
            return this;
        }

        AddRight(value, Constant);

        return this;
    }

    protected LinkedListNode<Operation> AddLeft(Operation value, LinkedListNode<Operation> node) => AddLeft(value, node, Elements);
    protected static LinkedListNode<Operation> AddLeft(Operation value, LinkedListNode<Operation> node, LinkedList<Operation> elements)
    {
        if (Operation.TryAdd(value, node.Value, out Operation? r))
        {
            node.Value = r;
            return node;
        }

        if (node.Previous is null)
            return elements.AddBefore(node, value);

        if (node.Value < value)
            return elements.AddAfter(node, value);

        return AddLeft(value, node.Previous, elements);
    }

    protected LinkedListNode<Operation> AddRight(Operation value, LinkedListNode<Operation> node) => AddRight(value, node, Elements);
    protected static LinkedListNode<Operation> AddRight(Operation value, LinkedListNode<Operation> node, LinkedList<Operation> elements)
    {
        if (Operation.TryAdd(value, node.Value, out Operation? r))
        {
            node.Value = r;
            return node;
        }

        if (node.Next is null)
            return elements.AddAfter(node, value);

        if (node.Value > value)
            return elements.AddBefore(node, value);

        return AddLeft(value, node.Next, elements);
    }

    /// <summary>
    /// Creates negated function
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static Function Negate(Function a)
    {
        LinkedList<Operation> elements = new();

        if (a.Elements.Count == 0)
            return new(elements, elements.AddFirst(new Num()));

        LinkedListNode<Operation>? constant = null;

        foreach (var e in a.Elements)
        {
            if (e is Num)
            {
                constant = elements.AddLast(e);
                continue;
            }
            elements.AddLast(e);
        }

        if (constant is not null)
             return new(elements, constant);

        return new(elements, AddRight(new Num(), elements.First!, elements));
    }

    public static Function operator +(Function a, Function b) => new(a, b);
    public static Function operator +(Function a, Variable b) => new(a, b);
    public static Function operator +(Function a, Operation b) => new(a, b);
    public static Function operator +(Function a, Number b) => new(a, b);
    public static Function operator +(Function a, double b) => new(a, b);
    public static Function operator +(Function a, int b) => new(a, b);
    public static Function operator +(Variable a, Function b) => new(b, a);
    public static Function operator +(Operation a, Function b) => new(b, a);
    public static Function operator +(Number a, Function b) => new(b, a);
    public static Function operator +(double a, Function b) => new(b, a);
    public static Function operator +(int a, Function b) => new(b, a);
    public static Function operator -(Function a, Function b) => Negate(b).Add(a);
    public static Function operator -(Function a, Variable b) => new(a, -b);
    public static Function operator -(Function a, Operation b) => new(a, -b);
    public static Function operator -(Function a, Number b) => new(a, -b);
    public static Function operator -(Function a, double b) => new(a, -b);
    public static Function operator -(Function a, int b) => new(a, -b);
    public static Function operator -(Function a) => Negate(a);
    public static Function operator -(Variable a, Function b) => Negate(b).Add(new Element(a));
    public static Function operator -(Operation a, Function b) => Negate(b).Add(a);
    public static Function operator -(Number a, Function b) => Negate(b).Add(a);
    public static Function operator -(double a, Function b) => Negate(b).Add(a);
    public static Function operator -(int a, Function b) => Negate(b).Add(a);

    public static implicit operator Func<Number, Number>(Function f) => (n) => f.Eval(n);
    public static implicit operator Func<double, double>(Function f) => (n) => f.Eval(n);

    public override string ToString() => string.Join(' ', Elements.Where(p => p.Coefficient != Number.Zero));
}
