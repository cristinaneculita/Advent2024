// See https://aka.ms/new-console-template for more information

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;

string[] lines = File.ReadAllLines("input.txt");
var l = lines.Length;
char[][] m = new char[l][];
var c = lines[0].Length;
for (int i = 0; i < l; i++)
{
    m[i] = lines[i].ToCharArray();
}

int sum = 0;
for (int i = 0; i < l; i++)
{
    for (int j = 0; j < c; j++)
    {
        if (m[i][j]=='A')
        {
            if(FindPattern(i,j))
            { sum++; }

           
        }
    }
}


Console.WriteLine(sum);




List<int> LookAllDir(int i, int j, char car)
{
    var rez = new List<int>();
    if (i > 0 && j > 0)
        if (m[i - 1][j - 1] == car)
            rez.Add(1);
    if (i > 0)
        if (m[i - 1][j] == car)
            rez.Add(2);
    if (i > 0 && j < c - 1)
        if (m[i - 1][j + 1] == car)
            rez.Add(3);
    if (j < c - 1)
        if (m[i][j + 1] == car)
            rez.Add(4);
    if (i < l - 1 && j < c - 1)
        if (m[i + 1][j + 1] == car)
            rez.Add(5);
    if (i < l - 1)
        if (m[i + 1][j] == car)
            rez.Add(6);
    if (i < l - 1 && j > 0)
        if (m[i + 1][j - 1] == car)
            rez.Add(7);
    if (j > 0)
        if (m[i][j - 1] == car)
            rez.Add(8);
    return rez;
}

bool LookWord(int i, int j, int dir)
{
    switch (dir)
    {
        case 1:
            if (ms(i - 2, j - 2) == 'A' && ms(i - 3, j - 3) == 'S')
                return true;
            break;
        case 2:
            if (ms(i - 2, j) == 'A' && ms(i - 3, j) == 'S')
                return true;
            break;
        case 3:
            if(ms(i-2, j+2)=='A' && ms(i-3, j+3)=='S')
                return true;
            break;
        case 4: 
            if(ms(i, j+2)=='A' && ms(i, j+3)=='S')
                return true;
            break;
        case 5: if(ms(i+2, j+2)=='A' && ms(i+3, j+3)=='S')
            return true;
            break;
        case 6:if(ms(i+2, j)=='A' && ms(i+3, j)=='S')
            return true;
            break;
        case 7:if(ms(i+2, j-2)=='A' && ms(i+3, j-3)=='S')
            return true;
            break;
        case 8:
            if (ms(i, j - 2) == 'A' && ms(i, j - 3) == 'S')
                return true;
            break;
        default:
            return false;

    }

    return false;
}

char ms(int i, int j)
{
    if (i >= 0 && j >= 0 && i < l && j < c)
        return m[i][j];
    return 'Q';
}
bool FindPattern(int i, int j)
{
    if (ms(i - 1, j - 1) == 'M' && ms(i - 1, j + 1) == 'M' && ms(i + 1, j + 1) == 'S' && ms(i + 1, j - 1) == 'S')
        return true;
    if (ms(i - 1, j - 1) == 'M' && ms(i - 1, j + 1) == 'S' && ms(i + 1, j + 1) == 'S' && ms(i + 1, j - 1) == 'M')
        return true;
    if (ms(i - 1, j - 1) == 'S' && ms(i - 1, j + 1) == 'S' && ms(i + 1, j + 1) == 'M' && ms(i + 1, j - 1) == 'M')
        return true;
    if (ms(i - 1, j - 1) == 'S' && ms(i - 1, j + 1) == 'M' && ms(i + 1, j + 1) == 'M' && ms(i + 1, j - 1) == 'S')
        return true;
    return false;
}