using Bny.Equations;

Variable x = Identifier.X;

var function = 1 / (x ^ 2) + 6 * x; // knihovna co jsem udělal

var de1egate = (double x) => 1 / Math.Pow(x, 2) + 6 * x; // delegate

for (int i = -10; i < 10; i++)
    Console.WriteLine($"f({i,3}) = {function.Eval(i),6:.##} = {de1egate(i):.##}");
// výpis je jen důkaz že to má stejný výsledky