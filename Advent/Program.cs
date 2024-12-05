// See https://aka.ms/new-console-template for more information

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

string[] lines = File.ReadAllLines("input.txt");
var l = lines.Length;
var sum = 0;
var sum2 = 0;
var rules = new List<Rule>();
var prints = new List<List<int>>();
//citire date
for (int i = 0; i < l; i++)
{

    if (lines[i].Contains('|'))
    {
        
        var x = lines[i].Split('|');
        Rule r = new Rule(int.Parse(x[0]), int.Parse(x[1]));
        rules.Add(r);
    }

    if (lines[i].Contains(','))
    {
        var x = lines[i].Split(',');
        var li = new List<int>();
        foreach (var s in x)
        {
           li.Add(int.Parse(s));
        }
        prints.Add(li);
    }



}


//prelucrare date

foreach (var print in prints)
{

    var corect = Corect(print, rules);

    if (corect)
    {
        var mid = print.Count / 2;
        sum += print[mid];
    }
    else
    {
        var resultList = new List<int>();
        int first=0;
        while (print.Any())
        {
            foreach (var p in print)
            {
                //let's put p first
                var newList = new List<int>();
                newList.Add(p);
                foreach (var pp in print)
                {
                    if (pp != p)
                    {
                        newList.Add(pp);
                    }
                }

                if (CorectOnlyMe(newList, rules, p))
                {
                    resultList.Add(p);
                    first = p;
                    break;
                }
            }

            print.Remove(first);
        }
        Console.WriteLine();

        foreach (var i in resultList)
        {
            Console.Write($"{i} ");
        }

        var mid = resultList.Count / 2;
        sum2 += resultList[mid];
    }
}


int mostRulesBeforeLeastAfter(List<int> print, List<Rule> rules)
{
    var mostrules = int.MinValue;
    int val=0;
    foreach (var el in print)
    {
        var b = rules.Count(r => r.Before == el && print.Contains(r.After));
        if (b > mostrules)
        {
            mostrules = b;
            val = el;
        }
    }

    return val;
}

Console.WriteLine(sum);
Console.WriteLine(sum2);

bool Corect(List<int> ints, List<Rule> list)
{
    var b = true;
    foreach (var el in ints)
    {
        var ruleForBefore = list.Where(r => r.Before == el);
        var indcurent = ints.IndexOf(el);
        foreach (var rule in ruleForBefore)
        {
            var afternum = rule.After;
            var indr = ints.IndexOf(afternum);
            if (indr!=-1 && indr < indcurent)
            {
                b = false;
                break;
            }
        }
        if (b == false)
            break;
        var ruleForAfter = list.Where(r => r.After == el);
        foreach (var rule in ruleForAfter)
        {
            var beforeNr = rule.Before;
            var indr = ints.IndexOf(beforeNr);
            if (indr!=-1 && indr > indcurent)
            {
                b = false;
                break;
            }
        }
    }

    return b;
}

bool CorectOnlyMe(List<int> lis, List<Rule> rr, int r)
{
    var b = true;
    var el = r;
        var ruleForBefore = rr.Where(r => r.Before == el);
        var indcurent = lis.IndexOf(el);
        foreach (var rule in ruleForBefore)
        {
            var afternum = rule.After;
            var indr = lis.IndexOf(afternum);
            if (indr != -1 && indr < indcurent)
            {
                b = false;
                break;
            }
        }

        if (b == false)
            return false;
        var ruleForAfter = rr.Where(r => r.After == el);
        foreach (var rule in ruleForAfter)
        {
            var beforeNr = rule.Before;
            var indr = lis.IndexOf(beforeNr);
            if (indr != -1 && indr > indcurent)
            {
                b = false;
                break;
            }
        }
    

    return b;
}


class Rule
{
    public int Before { get; set; }
    public int After { get; set; }

    public Rule(int before, int after)
    {
        Before = before;
        After = after;
    }
}