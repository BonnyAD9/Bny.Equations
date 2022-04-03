using Bny.Equations;

// Creating function in this library
Variable x = VID.X, y = VID.Y;
var function = -5*(x^-1) + (x^2) + 100*x + 1/(x^0.25);

// Creating function using delegate
var de1egate = (double x) => -5 * Math.Pow(x, -1) + Math.Pow(x, 2) + 100 * x + 1 / Math.Pow(x, 0.25);

var compiled = function.Compile();

// Evaluating functions
Console.WriteLine("      function delegate compiled");
for (int i = 0; i < 10; i++)
    Console.WriteLine($"f({i}) = {function.Eval(i),6:0.##} = {de1egate(i),6:0.##} = {compiled(i):0.##}");

// printing functions
Console.WriteLine();
Console.WriteLine("Printing funtion:");
Console.WriteLine(function); // Printing function from this library
Console.WriteLine("Printing delegate:");
Console.WriteLine(de1egate); // Printing delegate

// benchmarking
Console.WriteLine();
Console.WriteLine("Benchmark:");
const int count = 10_000_000;
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
dt = DateTime.Now;
for (int i = 0; i < count; i++)
    compiled(r.NextDouble());
var res3 = DateTime.Now - dt;
Console.WriteLine($"Compiled: {res3.TotalSeconds} s / {count} runs");

Console.WriteLine($"Functions are about {res.TotalSeconds / res2.TotalSeconds:0.##} times slower than delegates");
Console.WriteLine($"Compiled functions are about {res.TotalSeconds / res3.TotalSeconds:0.##} times faster than normal functions");

/* Console output:
      function delegate compiled
f(0) =    NaN =    NaN = NaN
f(1) =     97 =     97 = 97
f(2) = 202.34 = 202.34 = 202.34
f(3) = 308.09 = 308.09 = 308.09
f(4) = 415.46 = 415.46 = 415.46
f(5) = 524.67 = 524.67 = 524.67
f(6) = 635.81 = 635.81 = 635.81
f(7) =  748.9 =  748.9 = 748.9
f(8) = 863.97 = 863.97 = 863.97
f(9) = 981.02 = 981.02 = 981.02

Printing funtion:
+100x -5/x +1/x^-0.25 +x^2
Printing delegate:
System.Func`2[System.Double,System.Double]

Benchmark:
Function: 2.4282064 s / 10000000 runs
Delegate: 0.8340107 s / 10000000 runs
Compiled: 0.8261073 s / 10000000 runs
Functions are about 2.91 times slower than delegates
Compiled functions are about 2.94 times faster than normal functions
 */