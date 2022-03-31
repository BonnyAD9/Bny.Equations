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

    public Function(Element e1, Element e2) : this(e1) => Add(e2);

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
    /// Inserts element into others
    /// </summary>
    /// <param name="value">Element to insert</param>
    public void Add(Element value)
    {
        if (Element.TryAdd(value, Constant.Value, out Element r))
        {
            Constant.Value = r;
            return;
        }

        if (value < Constant.Value)
        {
            AddLeft(value, Constant);
            return;
        }

        AddRight(value, Constant);
    }

    protected void AddLeft(Element value, LinkedListNode<Element> node)
    {
        if (Element.TryAdd(value, node.Value, out Element r))
        {
            node.Value = r;
            return;
        }

        if (node.Previous is null)
        {
            Elements.AddBefore(node, value);
            return;
        }

        if (node.Value < value)
        {
            Elements.AddAfter(node, value);
            return;
        }

        AddLeft(value, node.Previous);
    }

    protected void AddRight(Element value, LinkedListNode<Element> node)
    {
        if (Element.TryAdd(value, node.Value, out Element r))
        {
            node.Value = r;
            return;
        }

        if (node.Next is null)
        {
            Elements.AddAfter(node, value);
            return;
        }

        if (node.Value > value)
        {
            Elements.AddBefore(node, value);
            return;
        }

        AddLeft(value, node.Next);
    }

    public static implicit operator Func<Number, Number>(Function f) => (n) => f.Eval(n);
    public static implicit operator Func<double, double>(Function f) => (n) => f.Eval(n);
}
