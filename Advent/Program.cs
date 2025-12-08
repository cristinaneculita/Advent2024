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
        Y = int.Parse(x[1]),
        Z = int.Parse(x[2]),
        pos = i
    });
}

List<List<int>> lists = new List<List<int>>();
var dist = new List<Dist>();

for (int i = 0; i < l; i++)
{
    for (int j = i+1; j < l; j++)
    {
        dist.Add(new Dist(){P1 = points[i], P2 = points[j],Distance = GetDist(points[i], points[j])});
    }
}
dist = dist.OrderBy(d => d.Distance).ToList();
int indexOfDist = 0;

for (int i = 0; i < 1000000; i++)
{
    var cl = FindClosest();

    bool found= false;
    foreach (var lista in lists)
    {
        if (lista.Contains(cl.Item1.pos)&&!found)
        {
            if (!lista.Contains(cl.Item2.pos))
            {
                lista.Add(cl.Item2.pos);
            }
            found = true;
        }
        else if (lista.Contains(cl.Item2.pos)&&!found)
        {
            if (!lista.Contains(cl.Item1.pos))
                lista.Add(cl.Item1.pos);
            found = true;
        }
    }

    if (!found)
    {
        var y = new List<int>() { cl.Item1.pos, cl.Item2.pos };
        lists.Add(y);
    }
    else
    {
        //check if there is a common number in the queues, merge them
        for (int j = 0; j < lists.Count; j++)
        {
            for (int k = j+1; k < lists.Count; k++)
            {
                foreach (var number in lists[j])
                {
                    if (lists[k].Contains(number))
                    {
                        //merge
                        lists[k].Remove(number);
                        lists[j].AddRange(lists[k]);

                        lists.RemoveAt(k);
                        break;
                    }
                }
            }
        }
    }

    if (lists[0].Count == 1000)
    {
        Console.WriteLine(cl.Item1.X*cl.Item2.X);
        break;
    }
}



//lists.Sort((a, b) => a.Count.CompareTo(b.Count));
//lists.Reverse();
//Console.WriteLine(lists[0].Count * lists[1].Count*lists[2].Count);


(Point,Point) FindClosest()
{
    dist[indexOfDist].Taken = true;
    var x= (dist[indexOfDist].P1, dist[indexOfDist].P2);
    indexOfDist++;
    return x;
}

double GetDist(Point point1, Point point2)
{
    long sum = (point1.X - point2.X) * (point1.X - point2.X) +
                     (point1.Y - point2.Y) * (point1.Y - point2.Y)+
                     (point1.Z - point2.Z) * (point1.Z - point2.Z);
    return Math.Sqrt(sum);

}

class Point
{
    public long X { get; set; }
    public long Y { get; set; }
    public long Z { get; set; }
    public int pos { get; set; }
}

class Dist
{
    public Point P1 { get; set; }
    public Point P2 { get; set; }
    public double Distance { get; set; }
    public bool Taken { get; set; }
}