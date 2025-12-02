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
var info = lines[0];
var pairs = info.Split(",");
List<Par> pars = new List<Par>();
foreach (var pair in pairs)
{
    var x = pair.Split('-');
    pars.Add(new Par(){Start = long.Parse(x[0]), End = long.Parse(x[1])});
}


long sum = 0;
foreach (var par in pars)
{
    for(long i = par.Start; i <= par.End; i++)
    {
        if (Valid(i))
            sum += i;

       
    }
}

Console.WriteLine(sum);
bool Valid(long l)
{
    var str = l.ToString();
    var lung = str.Length;
    if (lung%2==1)
        return false;
    for (int i = 0; i < str.Length / 2; i++)
    {
        if (str[i] != str[i+lung/2])
            return false;
    }

    return true;
}

class Par
{
    public long Start { get; set; }
    public long End { get; set; }
}


