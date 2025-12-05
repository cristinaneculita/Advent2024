// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Globalization;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

string[] lines = File.ReadAllLines("input.txt");

var l = lines.Length;
int i = 0;
var intervals = new List<Interval>();
while (lines[i] != "")
{
    var x = lines[i].Split('-');
    intervals.Add(new Interval(){Low = long.Parse(x[0]), High = long.Parse(x[1])});

    i++;
}

i++;
var nr = new List<long>();
for (int j = i; j < l; j++)
{
    nr.Add(long.Parse(lines[j]));
}

long sum = 0;
foreach (var n in nr)
{
    foreach (var interval in intervals)
    {
        if (IsInInterval(n, interval))
        {
            sum++;
            break;
        }

      
    }
}

Console.WriteLine(sum);
bool IsInInterval(long l, Interval interval)
{
   if(l>=interval.Low &&l<=interval.High)
       return true;
   return false;
}

class Interval
{
    public long Low { get; set; }
    public long High { get; set; }
}