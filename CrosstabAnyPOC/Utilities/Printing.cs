using Colorful;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace CrosstabAnyPOC
{
    internal static class Printing
    {

  

        internal static void BigPrint(string str)
        {
            //FigletFont font = FigletFont.Load("figlet/Stick Letters.flf");
            //FigletFont font = FigletFont.Load("figlet/JS Stick Letters.flf");
            //FigletFont font = FigletFont.Load("figlet/Cybermedium.flf");
            //FigletFont font = FigletFont.Load("figlet/Graceful.flf");
            FigletFont font = FigletFont.Load("figlet/Small.flf");

            Figlet figlet = new(font);

            Console.WriteLine(figlet.ToAscii(str), ColorTranslator.FromHtml("#8AFFEF"));

        }


        internal static string GetEnumDisplayName(Enum value)
        {
            return value.GetType().GetField(value.ToString())?
                       .GetCustomAttribute<DisplayAttribute>()?
                       .Name ?? value.ToString();
        }




    }
}
