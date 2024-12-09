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
var l = lines.Length;
long sum = 0;
int[] val = new int[lines[0].Length];
//citire date
for (int i = 0; i < l; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        val[j] = int.Parse(lines[i][j].ToString());
    }
}

long indmic = 0;
long indmare = 0;
long indmicdr = val.Length-1;
long indmaredr = val.Length / 2;
long pusiDinDr = 0;
long many = HowManyInBigArray();
long manyTotal = HowManyTotal();
long[] valori = new long[manyTotal];

for (int i = 0; i < val.Length; i++)
{
    if (i % 2 == 0)
    {
        for (int j = 0; j < val[i]; j++)
        {
            valori[indmare] = i/2;
            indmare++;
        }

        
    }
    else
    {
        for (int j = 0; j < val[i]; j++)
        {
            valori[indmare] = -1;
            indmare++;
        }
    }
}

indmaredr = valori.Length - 1;
indmicdr = val.Length - 1;
var toMove = val[indmicdr];
while (true)
{
    var moved = Move();
    indmaredr -= val[indmicdr - 1];
    if (!moved)
        indmaredr -= toMove;
    indmicdr -=2;
    toMove = val[indmicdr];
    if (indmicdr == 0)
        break;
}

for (int i = 0; i < manyTotal; i++)
{
    if (valori[i] != -1)
        sum += i * valori[i];
}

bool Move()
{
    var laRand = 0;
    for (int i = 0; i < indmaredr; i++)
    {
        if (valori[i] == -1)
        {
            laRand++;
            if (laRand == toMove)
            {
                for (int j = 0; j<toMove; j++)
                {
                    valori[indmaredr] = -1;
                    indmaredr--;
                    valori[i] = indmicdr/2;
                    i--;
                }

                return true;
            }
        }
        else
        {
            laRand = 0;
        }
    }

    return false;
}
Console.WriteLine(sum);


long HowManyTotal()
{
    long s = 0;
    for (int i = 0; i < val.Length; i += 1)
    {
        s += val[i];
    }
    return s;
}

long HowManyInBigArray()
{
    long s = 0;
    for (int i = 0; i < val.Length; i+=2)
    {
        s += val[i];
    }
    return s;
}

//prelucrare date
//for (int i = 0; i < val.Length; i++)
//{
//    if (i % 2 == 0)
//    {
//        for (int j = 0; j < val[i]; j++)
//        {
//            sum += indmare * indmic;
//            indmare++;
//            if (indmare == many)
//                break;
//        }
//        indmic++;
//    }
//    else
//    {
//        for (int j = 0; j < val[i]; j++)
//        {
//            //var rm = FindRightmost();
//            sum += indmaredr * indmare;
//            pusiDinDr++;
//            if (pusiDinDr == val[indmicdr])
//            {
//                pusiDinDr = 0;
//                indmicdr -= 2;
//                indmaredr -= 1;
//            }

//            indmare++;
          
//        }
//    }

//    if (indmare == many)
//        break;
//}

Console.WriteLine(sum);
long FindRightmost()
{
    return indmaredr;

}