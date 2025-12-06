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

//order intervals
//keep sum
var orderedInt = intervals.OrderBy(v => v.Low).ToList();

var result = new Result()
    { Low = orderedInt[0].Low, High = orderedInt[0].High, Numbers = orderedInt[0].High - orderedInt[0].Low + 1 };

for (int j = 1; j < orderedInt.Count; j++)
{
    if (orderedInt[j].Low <= result.High)
    {

        if (orderedInt[j].High <= result.High)
        {
            //nothing
        }
        else
        {
            result.Numbers += orderedInt[j].High - result.High;
            result.High = orderedInt[j].High;
        }
    }
    else if (orderedInt[j].Low > result.High)
    {
        result.Low = orderedInt[j].Low;
        result.High = orderedInt[j].High;
        result.Numbers += orderedInt[j].High - orderedInt[j].Low + 1;
    }
}

Console.WriteLine(result.Numbers);


class Interval
{
    public long Low { get; set; }
    public long High { get; set; }
}

class Result
{
    public long Low { get; set; }
    public long High { get; set; }
    public long Numbers { get; set; }
}