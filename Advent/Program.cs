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
var l = lines.Length;
//Hashtable memo = new Hashtable();

long sum = 0;
var ec = new List<Caz>();
//citire date
for (int i = 0; i < l; i+=4)
{
    var ln = lines[i];
    var x = new Caz();
    //A
    var rem= ln.Remove(0,12);
    var sx = rem.Split(',');
    x.AX = long.Parse(sx[0]);
    rem =sx[1].Remove(0, 3);
    x.AY=long.Parse(rem);
    
    //B
    ln = lines[i + 1];
    rem = ln.Remove(0, 12);
    sx = rem.Split(',');
    x.BX = int.Parse(sx[0]);
    rem =sx[1].Remove(0, 3);
    x.BY=int.Parse(rem);

    //Prize
    ln=lines[i + 2];
    rem = ln.Remove(0, 9);
    sx = rem.Split(',');
    x.PX = int.Parse(sx[0])+ 10000000000000;
    rem = sx[1].Remove(0, 3);
    x.PY=int.Parse(rem) + 10000000000000;
    
    ec.Add(x);
}

//procesare date
foreach (var e in ec)
{
    long t1 = e.PX * e.AY - e.PY * e.AX;
    long t2 = e.BX * e.AY - e.BY * e.AX;
    var rest = t1 % t2;
    if (rest == 0)
    {
        long m = t1 / t2;
        if (m >= 0)
        {
            e.rezB = m;
            t1 = e.PX - m * e.BX;
            t2 = e.AX;
            rest = t1 % t2;
            if (rest == 0)
            {
                long n = t1 / t2;
                if (n >= 0)
                {
                    e.rezA = n;
                }
                else
                {
                    e.rezA = -1;
                    e.rezB = -1;
                }
            }
            else
            {
                e.rezA = -1;
                e.rezB = -1;
            }
        }
        else
        {
            e.rezA = -1;
            e.rezB = -1;
        }
    }
    else
    {
        e.rezA = -1;
        e.rezB = -1;
    }
}

sum = 0;
foreach (var e in ec)
{
    //if (e.rezA <= 100 && e.rezB <= 100)
    //{
    if (e.rezA != -1 && e.rezB != -1)
    {
        long ad = e.rezA * 3 + e.rezB;
        sum += ad;


    }
}


Console.WriteLine(sum);

public class Caz
{
    public long AX { get; set; }
    public long AY { get; set; }
    public long BX { get; set; }
    public long BY { get; set; }
    public long PX { get; set; }
    public long PY { get; set; }

    public long rezA { get; set; }
    public long rezB { get; set; }

    public Caz(int ax, int ay, int bx, int by, int px, int py)
    {
        AX = ax;
        AY = ay;
        BX = bx;
        BY = by;
        PX = px;
        PY = py;
    }

    public Caz()
    {
        
    }
}

