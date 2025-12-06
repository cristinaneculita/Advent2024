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
List<int> listacur = new List<int>();
int[][] nr = new int[4][];
for (int i = 0; i < 4; i++)
{
    nr[i]=new int[1000];
}

for (int i = 0; i < 4; i++)
{
    var j = 0;
    var x = lines[i].Split(" ");
    foreach (var s in x)
    {
        if (string.IsNullOrWhiteSpace(s))
        { continue; }
        else
        {
            nr[i][j] = int.Parse(s);
            j++;
        }
    }
}

char[] c = new char[1000];
var y = lines[4].Split(" ");
var k = 0;
foreach (var ss in y)
{
    if (string.IsNullOrWhiteSpace(ss))
    { continue; }
    else
    {
        c[k] = char.Parse(ss);
        k++;
    }
}

long[] rez = new long[1000];
long sum = 0;
long sc= 0;
for (int i = 0; i < rez.Length; i++)
{
    switch (c[i])
    {
        case '+':
            sc = 0;
            for (int j = 0; j < 4; j++)
            {
                sc += nr[j][i];
            }
            break;
        case '*':
            sc = 1;
            for (int j = 0; j < 4; j++)
            {
                sc *= nr[j][i];
            }
            break;
    }

    sum += sc;



}

Console.WriteLine(sum);
