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
Hashtable memo = new Hashtable();
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

flags = flags.OrderBy(c=>c.Length).ToList();
var count = 0;
long pos = 0;
//brwrr can be made in two different ways: b, r, wr, r or br, wr, r
foreach (var pattern in patterns)
{
    long posc = 0;
   // Console.WriteLine(patterns.IndexOf(pattern));
    posc = Solve(pattern);
    pos += posc;
    Console.WriteLine($"{pattern}: {posc}");
    //if (isp)
    //    count++;
}
Console.WriteLine(count);

long Solve(string s)
{
    long sol = 0;
    var solved = false;
    if (memo.Contains(s))
    {
       // pos+= (int)memo[s];
         sol = (long)memo[s];
        return sol;
    }
    if (flags.Contains(s))
    {
        //pos++;
        sol++;
        solved = true;
      //  memo.Add(s,sol);
        // return true;
    }
    if(notpos.Contains(s))
        return 0;
    foreach (var flag in flags)
    {
        if (s.StartsWith(flag))
        {
            var r = s.Remove(0, flag.Length);
            var rez =Solve(r);
            if(rez>0) solved=true;
            sol += rez;
        }
    }

    if (!solved)
    {
        notpos.Add(s);
        return 0;
    }
    memo.Add(s,sol);
    return sol;
}

Console.WriteLine("pos "+pos);