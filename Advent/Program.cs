// See https://aka.ms/new-console-template for more information

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Globalization;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

string[] lines = File.ReadAllLines("input.txt");


int sum = 0;
List<long> lst = new List<long>();
//citire date
var x = lines[0].Split(' ');
foreach (var s in x)
{
    lst.Add(long.Parse(s));
}

//procesare date
for (int i = 0; i < 25; i++)
{

    Console.WriteLine();
    lst = ProcesareLista();
    Console.Write(i +"->" +lst.Count+"--->");
    //foreach (var l in lst)
    //{
    //    Console.Write(l + " ");
    //}
}


Console.WriteLine(lst.Count);
List<long> ProcesareLista()
{
    var newlist = new List<long>();
    foreach (var l in lst)
    {
        if(l==0)
            newlist.Add(1);
        else if (l.ToString().Length % 2 == 0)
        {
            var strl = l.ToString();
            var firsthalf = strl.Substring(0, strl.Length / 2);
            var secondhalf = strl.Substring(strl.Length / 2);
            newlist.Add(long.Parse(firsthalf));
            newlist.Add(long.Parse(secondhalf));
        }
        else
        {
            newlist.Add(l*2024);
        }
    }
    return newlist;
}