// See https://aka.ms/new-console-template for more information

using System.Collections;
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

Hashtable memo= new Hashtable();

int sum = 0;
List<long> lst = new List<long>();
//citire date
var x = lines[0].Split(' ');
foreach (var s in x)
{
    lst.Add(long.Parse(s));
}

//procesare date

Console.WriteLine();
for (int i = 1; i < 77; i++)
{
    var count = LungimeLista(lst, i);
    Console.WriteLine(i + "--->"+count);
}







long LungimeLista(List<long> list, int blinks)
{

    var caz = new Case(list, blinks);
    if (memo.ContainsKey(caz))
        return (long)memo[caz];
    if (blinks == 0)
    {
        memo[caz] = (long)list.Count;
        return list.Count;
    }

    if (list.Count == 1 && list[0] == 0)
    {
        var x = LungimeLista(new List<long>() { 1 }, blinks - 1);
        memo[caz] = (long)x;
        return x;
    }

    if (list.Count == 1 && list[0] == 1)
    {
        var x= LungimeLista(new List<long>() { 2024 }, blinks - 1);
        memo[caz] = (long)x;
        return x;
    }
    if (list.Count == 1 && (list[0].ToString().Length % 2 == 0))
    {
        var strl = list[0].ToString();
        var firsthalf = strl.Substring(0, strl.Length / 2);
        var secondhalf = strl.Substring(strl.Length / 2);

        var x= LungimeLista(new List<long> { long.Parse(firsthalf) }, blinks - 1) +
               LungimeLista(new List<long>() { long.Parse(secondhalf) }, blinks - 1);
        memo[caz] = (long)x;
        return x;
    }

    if (list.Count == 1)
    {
        var x= LungimeLista(new List<long>() { list[0] * 2024 }, blinks - 1);
        memo[caz] = (long)x;
        return x;
    }
    long sum = 0;
    foreach (var l in list)
    {
        sum += LungimeLista(new List<long>() { l }, blinks - 1);
    }
    memo[caz] = (long)sum;
    return sum;
}


List<long> ProcesareLista()
{
    var newlist = new List<long>();
    foreach (var l in lst)
    {
        if (l == 0)
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
            newlist.Add(l * 2024);
        }
    }
    return newlist;
}

public class Case
{
    public List<long> Nr { get; set; }
    public int Blinks { get; set; }

    public Case(List<long> nr, int blinks)
    {
        Nr = nr;
        Blinks = blinks;
    }

    public override int GetHashCode()
    {
        return Blinks.GetHashCode();
    }

    public override bool Equals(object obj)
    {
   
        if (obj is Case other)
        {
            if (other.Blinks != Blinks)
                return false;
            if(other.Nr.Count != Nr.Count) return false;

            for (int i = 0; i < Nr.Count; i++)
            {
                if (Nr[i] != other.Nr[i])
                    return false;
            }
        }

        return true;
    }
}

public class Val
{
    public long Value { get; set; }

    public Val(long value)
    {
        Value = value;

    }
}

