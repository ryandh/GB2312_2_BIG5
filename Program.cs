using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Gb2312toBig5
{
    class Program
    {
        static void Main(string[] args)
        {
             String fileinput = "insim.xml";
            StreamWriter sw = File.CreateText("big.xml");
            StreamWriter sw2 = File.CreateText("sim.xml");
            String[] input = File.ReadAllLines(fileinput);
            Regex reg = new Regex(">([^<]+)<");
            foreach (var item in input)
            {
                var ns = item;
                var ns2 = item;
                //only translate the >content here<
                if (reg.IsMatch(item))
                {
                    var newcontent = ConvertToBig5(reg.Match(item).Groups[1].Value);
                    ns = ns.Replace(reg.Match(item).Groups[1].Value, newcontent);

                    var newcontent2 = ConvertoSime(reg.Match(item).Groups[1].Value);
                    ns2 = ns2.Replace(reg.Match(item).Groups[1].Value, newcontent2);
                }
                sw.WriteLine(ns);
                sw2.WriteLine(ns2);
            }
            sw.Close();
            sw2.Close();
            Process.Start(".");
        }

        private static string ConvertToBig5(string s)
        {
            return   Strings.StrConv(s, VbStrConv.TraditionalChinese, 0);
        }

        private static string ConvertoSime(string s)
        {
            return Strings.StrConv(s, VbStrConv.SimplifiedChinese, 0);
        }
    }
}
