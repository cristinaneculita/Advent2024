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
int s = 50;
int t = 0;
foreach (var line in lines)
{
    var dir = line[0];
    var val = int.Parse(line[1..]);
    if (dir == 'R')
    {
        s+= val;
        while (s>99)
            s-=100;
    }
    else if (dir == 'L')
    {
        s-= val;
        while (s<0)
            s+=100;
    }
    if (s==0)
        t+=1;
    Console.WriteLine(s);
}
Console.WriteLine(t);
  


