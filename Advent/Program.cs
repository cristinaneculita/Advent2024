// See https://aka.ms/new-console-template for more information

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Linq.Expressions;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;

string[] lines = File.ReadAllLines("input.txt");
var l = lines.Length;
int[][]data= new int[l][];

for (int i = 0; i < lines.Length; i++)
{
    var x = lines[i].Split(' ');
    data[i]= new int[x.Length];
    for (int j = 0; j < x.Length; j++)
    {
        data[i][j] = int.Parse(x[j]);
    }
}

int count = 0;

for (int i = 0; i < l; i++)

{
    bool safe = true;
    safe = Safe(data[i]);

    if (safe)
    {
        count++;
        continue;
    }
    else
    {
        for (int j = 0; j < data[i].Length; j++)
        {
            //remove j element
            var x = data[i].ToList();
            x.RemoveAt(j);
            var y = x.ToArray();
            safe = Safe(y);
            if (safe)
            {
                count++;
                break;
            }
        }
    }
}

Console.WriteLine(count);
Console.WriteLine(l-count);

bool Safe(int[] ints)
{
    if (ints[0] == ints[1])
    {
        return false;
    }

    bool cresc = !(ints[0] > ints[1]);
    if (cresc)
    {
        for (int j = 0; j < ints.Length-1; j++)
        {
            if (ints[j + 1] - ints[j] < 1 || ints[j + 1] - ints[j] > 3)
            {
                return false;
            }
        }
    }
    else
    {
        for (int j = 0; j < ints.Length - 1; j++)
        {
            if (ints[j + 1] - ints[j] < -3 || ints[j + 1] - ints[j] > -1)
            {
                return false;
            }
        }
    }

    return true;
}