using Bny.Equations;

// Creating function in this library
Variable x = VID.X, y = VID.Y;
var function = -5*(x^-1) + (x^2) + 100*y;

// Creating function using delegate
var de1egate = (double x, double y) => -5 * Math.Pow(x, -1) + Math.Pow(x, 2) + 100 * y;

// Evaluating functions
Console.WriteLine("      function delegate");
for (int i = 0; i < 10; i++)
    Console.WriteLine($"f({i}) = {function.Eval(v => v.ID switch { VID.X => i, VID.Y => i * 2, _ => Number.NaN}),7:.##} = {de1egate(i, i * 2):.##}");

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
{
    var num = r.NextDouble();
    de1egate(num, num);
}
var res2 = DateTime.Now - dt;
Console.WriteLine($"Delegate: {res2.TotalSeconds} s / {count} runs");

Console.WriteLine($"Functions are about {res.TotalSeconds / res2.TotalSeconds:0.##} times slower than delegates");

/* Console output:
      function delegate
f(0) =      -? = -?
f(1) =     196 = 196
f(2) =   401.5 = 401.5
f(3) =  607.33 = 607.33
f(4) =  814.75 = 814.75
f(5) =    1024 = 1024
f(6) = 1235.17 = 1235.17
f(7) = 1448.29 = 1448.29
f(8) = 1663.38 = 1663.38
f(9) = 1880.44 = 1880.44

Printing funtion:
+100y -5/x +x^2
Printing delegate:
System.Func`3[System.Double,System.Double,System.Double]

Benchmark:
Function: 0.254189 s / 1000000 runs
Delegate: 0.0639918 s / 1000000 runs
Functions are about 3.97 times slower than delegates
 */