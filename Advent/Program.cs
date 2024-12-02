// See https://aka.ms/new-console-template for more information

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;

string[] lines = File.ReadAllLines("input.txt");
int[] left= new int[lines.Length];
int[] right = new int[lines.Length];
long sim = 0;
for (int i = 0; i < lines.Length; i++)
{
    var x = lines[i].Split(' ');
    left[i] = int.Parse(x[0]);
    right[i] = int.Parse(x[3]);
}
Array.Sort(left);
Array.Sort(right);

for (int i = 0; i < lines.Length; i++)
{
    var a = left[i];
    var count = 0;
    for (int j = 0; j < lines.Length; j++)
    {
        if (a == right[j])
            count++;
    }

    sim += a * count;
}
long s = 0;
for (int i = 0; i < lines.Length; i++)
{
    s += Math.Abs(left[i] - right[i]);
}

Console.WriteLine(s);
Console.WriteLine(sim);