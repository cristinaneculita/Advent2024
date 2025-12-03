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
long sum = 0;
var rand = lines[0].Length;
foreach (var line in lines)
{
    var max = Max(line, 12);


    sum+=long.Parse(max);
}

Console.WriteLine(sum);


string Max(string s, int len)
{
    var maxnr = 9;
    if (len == 0)
        return "";
    while (maxnr > 0)
    {

        var firstOc = FirstOc(s, maxnr);
        if (firstOc >= 0 && firstOc <= s.Length-len)
        {
            //retin
            return maxnr + Max(s.Substring(firstOc + 1), len - 1);
        }
       

        maxnr--;
    }

    return "";
}

int FirstOc(string s, int maxnr)
{
    var arr = s.ToCharArray();
    for (int i = 0; i < arr.Length; i++)
    {
        if (int.Parse(arr[i].ToString()) == maxnr)
            return i;
    }

    return -1;
}

int GetMaxNr(string s1)
{
    throw new NotImplementedException();
}