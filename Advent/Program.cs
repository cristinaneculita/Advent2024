// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.ComponentModel.Design;
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
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

string[] lines = File.ReadAllLines("input.txt");

var l= lines.Length;
var points = new List<Point>();
for (int i = 0; i < l; i++)
{
    var x = lines[i].Split(',');
    points.Add(new Point()
    {
        X= int.Parse(x[0]),
        Y = int.Parse(x[1])
    });
}

long areamax  = 0;
for (int i = 0; i < points.Count; i++)
{
    for (int j = i+1; j < points.Count; j++)
    {
        var area = (Math.Abs(points[i].X - points[j].X) + 1) * (Math.Abs(points[i].Y - points[j].Y)+1);
        if(area>areamax)
            areamax = area;
    }
}

Console.WriteLine(areamax);



class Point
{
    public long X { get; set; }
    public long Y { get; set; }

}
