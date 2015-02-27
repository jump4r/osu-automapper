using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace osu_automapper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        
       }
    }

    class Beatmap
    {
        private string[] text;

        public Beatmap(string[] text)
        {
            // TODO: Complete member initialization
            this.text = text;
        }

        public void AnalyzeBeatmap()
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.WriteLine(text[i]);
            }
        }
    }

    class HitCircle
    {
        //constructor
        public HitCircle() { }
        public HitCircle(int a, int b, int c, int d, int e)
        {
            x = a;
            y = b;
            time = c;
            type = d;
            hitsound = e;
            
        }

        //properties
        public int x { get; set; }
        public int y { get; set; }
        public int time { get; set; }
        public int type { get; set; }
        public int hitsound { get; set; }

        //funciton 
        public override string ToString()
        {
            return x + "," + y + "," + time + "," + type + "," + hitsound + ",0:0:0:0:";
        }

    }
}
