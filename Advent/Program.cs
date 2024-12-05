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
var sum = 0;
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
    var corect = true;
    foreach (var el in print)
    {
        var ruleForBefore = rules.Where(r => r.Before == el);
        var indcurent = print.IndexOf(el);
        foreach (var rule in ruleForBefore)
        {
            var afternum = rule.After;
            var indr = print.IndexOf(afternum);
            if (indr!=-1 && indr < indcurent)
            {
                corect = false;
                break;
            }
        }
        if (corect == false)
            break;
        var ruleForAfter = rules.Where(r => r.After == el);
        foreach (var rule in ruleForAfter)
        {
            var beforeNr = rule.Before;
            var indr = print.IndexOf(beforeNr);
            if (indr!=-1 && indr > indcurent)
            {
                corect = false;
                break;
            }
        }
    }

    if (corect)
    {
        var mid = print.Count / 2;
        sum += print[mid];
    }


}



Console.WriteLine(sum);



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