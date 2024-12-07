// See https://aka.ms/new-console-template for more information

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Globalization;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

string[] lines = File.ReadAllLines("input.txt");
var l = lines.Length;

long sum = 0;
var equations = new List<Eq>(l);
//citire date
for (int i = 0; i < l; i++)
{
    var x = lines[i].Split(':');
    var eq = new Eq();
    eq.TestValue = long.Parse(x[0]);
    var y = x[1].Trim().Split(' ');
    foreach (var v in y)
    {
        eq.Nr.Add(long.Parse(v));
    }
    equations.Add(eq);
}


//prelucrare date
for (int i = 0; i < l; i++)
{
    Console.WriteLine(i);
    var b = CanBeTrue(equations[i]);


    if (b)
        sum += equations[i].TestValue;
}



Console.WriteLine(sum);

bool CanBeTrue(Eq equation)
{
    if (equation.Nr.Count == 1)
    {
        if (equation.Nr[0] == equation.TestValue)
            return true;
        else return false;
    }
    else
    {
        //resultforplus 
        var listForPlus = equation.Nr.ToList();
        long val = listForPlus[0] + listForPlus[1];
        //  if (val > equation.TestValue)
        //     return false;
        listForPlus.RemoveAt(0);
        listForPlus.RemoveAt(0);
        listForPlus.Insert(0, val);

        Eq e = new Eq(equation.TestValue, listForPlus);
        var resultForPlus = CanBeTrue(e);
        if (resultForPlus)
            return true;


        //resultformul
        var listForMul = equation.Nr.ToList();
        val = listForMul[0] * listForMul[1];
        // if (val > equation.TestValue)
        //    return false;
        listForMul.RemoveAt(0);
        listForMul.RemoveAt(0);
        listForMul.Insert(0, val);

        e = new Eq(equation.TestValue, listForMul);
        var resultForMul = CanBeTrue(e);
        if (resultForMul == true)
            return true;

        //result for concat
        var listForCon = equation.Nr.ToList();
        val = long.Parse(listForCon[0].ToString() + listForCon[1].ToString());

        // if (val > equation.TestValue)
        //    return false;
        listForCon.RemoveAt(0);
        listForCon.RemoveAt(0);
        listForCon.Insert(0, val);

        e = new Eq(equation.TestValue, listForCon);
        var resultForCon = CanBeTrue(e);

        return resultForCon;

    }

}

public class Eq
{
    public long TestValue { get; set; }
    public List<long> Nr { get; set; }

    public Eq()
    {
        Nr = new List<long>();
    }

    public Eq(long testValue, List<long> nr)
    {
        TestValue = testValue;
        Nr = nr;
    }
}