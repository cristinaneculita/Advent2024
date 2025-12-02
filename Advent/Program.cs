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
    for (int i = 1; i <= lung / 2; i++)
    {
        if (B(str, i, lung)) return true;
    }

    return false;
}

bool B(string s, int i1, int lung1)
{
    //grupuri de i
    var mostra = s[0..i1];
    if (lung1 % i1 != 0)
        return false;
    var groups = lung1 / i1;
    for (int j = 0; j < groups; j++)
    {
        for (int k = 0; k < i1; k++)
        {
            if (s[j * i1 + k] != mostra[k])
                return false;
        }
    }

    return true;
}

class Par
{
    public long Start { get; set; }
    public long End { get; set; }
}


