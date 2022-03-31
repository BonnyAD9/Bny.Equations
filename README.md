# Bny.Equations
Enables easier working with mathematical functions

## Example

### Code
```
using Bny.Equations;

Variable x = Identifier.X;

var function = 1 / (x ^ 2) + 6 * x; // this library

var de1egate = (double x) => 1 / Math.Pow(x, 2) + 6 * x; // delegate

for (int i = -10; i < 10; i++)
    Console.WriteLine($"f({i,3}) = {function.Eval(i),6:.##} = {de1egate(i):.##}");
```

### Output
```
f(-10) = -59.99 = -59.99
f( -9) = -53.99 = -53.99
f( -8) = -47.98 = -47.98
f( -7) = -41.98 = -41.98
f( -6) = -35.97 = -35.97
f( -5) = -29.96 = -29.96
f( -4) = -23.94 = -23.94
f( -3) = -17.89 = -17.89
f( -2) = -11.75 = -11.75
f( -1) =     -5 = -5
f(  0) =      ? = ?
f(  1) =      7 = 7
f(  2) =  12.25 = 12.25
f(  3) =  18.11 = 18.11
f(  4) =  24.06 = 24.06
f(  5) =  30.04 = 30.04
f(  6) =  36.03 = 36.03
f(  7) =  42.02 = 42.02
f(  8) =  48.02 = 48.02
f(  9) =  54.01 = 54.01
```
