using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class CsvFileParsing
    {
        public string[,] ParseTextAsset(TextAsset ft)
        {
            string fs = ft.text;
            string[] fLines = Regex.Split(fs, "\r\n");
            List<string> lines = new List<string>();
            foreach (string t in fLines)
            {
                if (t != "")
                {
                    lines.Add(t);
                }
            }
            string[,] values = new string[lines.Count, 3];
            int k = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                string valueLine = lines[i];
                string[] splitet = Regex.Split(valueLine, ",");
                int j = 1;
                values[k, 0] = k.ToString();
                foreach (string t in splitet)
                {
                    values[k, j] = t;
                    j++;
                }
                k++;
            }
            return values;
        }
    }
}
