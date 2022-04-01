using Bny.Equations;

// Creating function in this library
Variable x = Identifier.X;
var function = 2 + 1/(x^2) + 6*x + 6 - 10*(x^-3) + (x^1.0/4);

// Creating function using delegate
var de1egate = (double x) => 2 + 1 / Math.Pow(x, 2) + 6 * x + 6 - 10 * Math.Pow(x, -3) + Math.Pow(x, 1.0 / 4);

// Evaluating functions
Console.WriteLine("      function delegate");
for (int i = 0; i < 10; i++)
    Console.WriteLine($"f({i}) = {function.Eval(i),6:.##} = {de1egate(i):.##}");

// printing functions
Console.WriteLine();
Console.WriteLine("Printing funtion:");
Console.WriteLine(function); // Printing function from this library
Console.WriteLine("Printing delegate:");
Console.WriteLine(de1egate); // Printing delegate

// benchmariking
Console.WriteLine();
Console.WriteLine("Benchmark:");
const int count = 1000000;
Random r = new();
DateTime dt = DateTime.Now;
for (int i = 0; i < count; i++)
    function.Eval(r.NextDouble());
var res = DateTime.Now - dt;
Console.WriteLine($"Function: {res.TotalSeconds} s / {count} runs");
dt = DateTime.Now;
for (int i = 0; i < count; i++)
    de1egate(r.NextDouble());
var res2 = DateTime.Now - dt;
Console.WriteLine($"Delegate: {res2.TotalSeconds} s / {count} runs");

Console.WriteLine($"Functions are about {res.TotalSeconds / res2.TotalSeconds:0.##} times slower than delegates");

/* Console output:
      function delegate
f(0) =    NaN = NaN
f(1) =      6 = 6
f(2) =  20.19 = 20.19
f(3) =  27.06 = 27.06
f(4) =  33.32 = 33.32
f(5) =  39.46 = 39.46
f(6) =  45.55 = 45.55
f(7) =  51.62 = 51.62
f(8) =  57.68 = 57.68
f(9) =  63.73 = 63.73

Printing funtion:
-10x^-3 +1x^-2 +1x^0.25 8 +6x^1
Printing delegate:
System.Func`2[System.Double,System.Double]

Benchmark:
Function: 0.1917458 s / 1000000 runs
Delegate: 0.0917978 s / 1000000 runs
Functions are about 2.09 times slower than delegates
 */