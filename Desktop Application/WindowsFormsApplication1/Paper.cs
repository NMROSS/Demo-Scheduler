using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class Paper
    {
        // Paper represents a paper/course available through the university

        public String Title; // paper title - Intro to Computer Science
        public String Code; // paper code - COMP103
        public bool[,] Timeslot; // lab times of the paper

        public Paper(String t, String c, bool[,] times)
        {
            this.Title = t;
            this.Code = c;
            this.Timeslot = times;
        }
    }
}
