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
//Hashtable memo = new Hashtable();
var l = lines.Length;
List<string> flags = new List<string>(); 
List<string> patterns = new List<string>();
List<string> notpos = new List<string>();
//citire date
for (int i = 0; i < l; i++)
{
    if (lines[i].Contains(','))
    {
        flags = lines[i].Split(", ").ToList();
    }
    else if (!string.IsNullOrEmpty(lines[i]))
    {
        patterns.Add(lines[i]);
    }
}

var count = 0;
foreach (var pattern in patterns)
{
    Console.WriteLine(patterns.IndexOf(pattern));
    var isp = Solve(pattern);
    
    if (isp)
        count++;
}
Console.WriteLine(count);

bool Solve(string s)
{
    if(flags.Contains(s))
        return true;
    if(notpos.Contains(s))
        return false;
    var solved = false;
    foreach (var flag in flags)
    {
        if (s.StartsWith(flag))
        {
            var r = s.Remove(0, flag.Length);
            if (Solve(r))
                return true;
        }
    }
    notpos.Add(s);
    return false;
}