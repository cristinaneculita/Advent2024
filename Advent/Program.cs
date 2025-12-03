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
int sum = 0;
foreach (var line in lines)
{
    int max = 0;
    var arr = line.ToCharArray();
    for (int i = 0; i < line.Length; i++)
    {
        for (int j = i+1; j < line.Length; j++)
        {

            char[] varc = new char[2];
            varc[0]= arr[i];
            varc[1]= arr[j];
            string str = new string(varc);
            int val = int.Parse(str);
            if (val > max)
            {
                max = val;
            }
        }
    }
    sum += max;
    Console.WriteLine(max);
}

Console.WriteLine(sum);