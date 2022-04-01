using System.Text;

namespace Bny.Equations;

public class Function
{
    private LinkedList<Element> Elements { get; init; }
    private LinkedListNode<Element> Constant { get; init; }

    /// <summary>
    /// Creates empty function
    /// </summary>
    public Function()
    {
        Elements = new();
        Constant = Elements.AddFirst(new Element());
    }

    /// <summary>
    /// Creates function with one value
    /// </summary>
    /// <param name="value"></param>
    public Function(Element value)
    {
        Elements = new();
        if (value.IsConstant)
        {
            Constant = Elements.AddFirst(value);
            return;
        }
        Constant = Elements.AddFirst(new Element());
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
            Constant = Elements.AddFirst(new Element());
            return;
        }

        foreach (var e in f.Elements)
        {
            if (e.IsConstant)
            {
                Constant = Elements.AddLast(e);
                continue;
            }
            Elements.AddLast(e);
        }

        if (Constant is null)
            Constant = AddRight(new(), Elements.First!);
    }

    private Function(LinkedList<Element> elements, LinkedListNode<Element> constant)
    {
        Elements = elements;
        Constant = constant;
    }

    public Function(Variable v, Number n) : this(new Element(n), new Element(v)) { }

    /// <summary>
    /// Initializes function from two elements that will be added
    /// </summary>
    /// <param name="e1">First element</param>
    /// <param name="e2">Second element</param>
    public Function(Element e1, Element e2) : this(e1) => Add(e2);

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
    public Function(Function f, Element e) : this(f) => Add(e);

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
    public Function(Element e, Number n)
    {
        Elements = new();
        if (e.IsConstant)
        {
            Constant = Elements.AddFirst(e with { Coefficient = e.Coefficient + n });
            return;
        }
        Constant = Elements.AddFirst(new Element(n));
        Add(e);
    }

    /// <summary>
    /// Evaluates this function with the given value as value of variables
    /// </summary>
    /// <param name="n">Value of variables</param>
    /// <returns>Calculated result</returns>
    public Number Eval(Number n)
    {
        Number res = Number.Zero;
        foreach (var e in Elements)
            res += e.Evaluate(n);
        return res;
    }

    /// <summary>
    /// Adds function to this function
    /// </summary>
    /// <param name="value">Function to add to this function</param>
    /// <returns>Instance on which this method was called</returns>
    public Function Add(Function value)
    {
        LinkedListNode<Element>? ocur = value.Elements.First;
        LinkedListNode<Element> tcur = Elements.First!;

        while (ocur is not null)
        {
            if (Element.TryAdd(tcur.Value, ocur.Value, out Element e))
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
    public Function Add(Number value)
    {
        Constant.Value = Constant.Value with { Coefficient = Constant.Value.Coefficient + value };
        return this;
    }

    /// <summary>
    /// Inserts element into others
    /// </summary>
    /// <param name="value">Element to insert</param>
    /// <returns>Instance on which this method was called</returns>
    public Function Add(Element value)
    {
        if (Element.TryAdd(value, Constant.Value, out Element r))
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

    protected LinkedListNode<Element> AddLeft(Element value, LinkedListNode<Element> node) => AddLeft(value, node, Elements);
    protected static LinkedListNode<Element> AddLeft(Element value, LinkedListNode<Element> node, LinkedList<Element> elements)
    {
        if (Element.TryAdd(value, node.Value, out Element r))
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

    protected LinkedListNode<Element> AddRight(Element value, LinkedListNode<Element> node) => AddRight(value, node, Elements);
    protected static LinkedListNode<Element> AddRight(Element value, LinkedListNode<Element> node, LinkedList<Element> elements)
    {
        if (Element.TryAdd(value, node.Value, out Element r))
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
        LinkedList<Element> elements = new();

        if (a.Elements.Count == 0)
            return new(elements, elements.AddFirst(new Element()));

        LinkedListNode<Element>? constant = null;

        foreach (var e in a.Elements)
        {
            if (e.IsConstant)
            {
                constant = elements.AddLast(e);
                continue;
            }
            elements.AddLast(e);
        }

        if (constant is not null)
             return new(elements, constant);

        return new(elements, AddRight(new(), elements.First!, elements));
    }

    public static Function operator +(Function a, Function b) => new(a, b);
    public static Function operator +(Function a, Variable b) => new(a, b);
    public static Function operator +(Function a, Element b) => new(a, b);
    public static Function operator +(Function a, Number b) => new(a, b);
    public static Function operator +(Function a, double b) => new(a, b);
    public static Function operator +(Function a, int b) => new(a, b);
    public static Function operator +(Variable a, Function b) => new(b, a);
    public static Function operator +(Element a, Function b) => new(b, a);
    public static Function operator +(Number a, Function b) => new(b, a);
    public static Function operator +(double a, Function b) => new(b, a);
    public static Function operator +(int a, Function b) => new(b, a);
    public static Function operator -(Function a, Function b) => Negate(b).Add(a);
    public static Function operator -(Function a, Variable b) => new(a, -b);
    public static Function operator -(Function a, Element b) => new(a, -b);
    public static Function operator -(Function a, Number b) => new(a, -b);
    public static Function operator -(Function a, double b) => new(a, -b);
    public static Function operator -(Function a, int b) => new(a, -b);
    public static Function operator -(Function a) => Negate(a);
    public static Function operator -(Variable a, Function b) => Negate(b).Add(new Element(a));
    public static Function operator -(Element a, Function b) => Negate(b).Add(a);
    public static Function operator -(Number a, Function b) => Negate(b).Add(a);
    public static Function operator -(double a, Function b) => Negate(b).Add(a);
    public static Function operator -(int a, Function b) => Negate(b).Add(a);

    public static implicit operator Func<Number, Number>(Function f) => (n) => f.Eval(n);
    public static implicit operator Func<double, double>(Function f) => (n) => f.Eval(n);

    public override string ToString() => string.Join(' ', Elements);
}
