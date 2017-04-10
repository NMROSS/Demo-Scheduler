using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class AddPaper : Form
    {
        public Main mainForm;
        public bool editing; // whether the paper is being edited or added, true for Edit, false for new paper
        public Paper paper; // the paper being edited (if any)
        public bool[,] times; // hold boolean values for whether a time was selected
        ListBox LB; // listbox from main form, for re focusing Selected Index

        public AddPaper(Form MainForm, ListBox lb)
        {
            InitializeComponent();
            mainForm = MainForm as Main;
            editing = false;
            times = new bool[5, 12];
            InitTimes();
            pictureBox1.Paint += pictureBoxPapers_Paint; // draws the gridlines in when first opened
            LB = lb;
        }

        public AddPaper(Form MainForm, Paper p, ListBox lb)
        {
            InitializeComponent();
            mainForm = MainForm as Main;
            paper = p;
            textBoxPaperCode.Text = p.Code; // set the paper's info in the textboxes
            textBoxPaperName.Text = p.Title;
            editing = true;
            textBoxPaperCode.Enabled = false;
            times = p.Timeslot; // draw the current timetable
            pictureBox1.Paint += pictureBoxPapers_Paint;
            LB = lb;
        }

        // Draws the timetable in the paint method of the form
        private void pictureBoxPapers_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            // Draw current timetbale of passed in paper (if any)
            Brush br = new SolidBrush(Color.Blue);
            for (int x = 0; x < 5; x++)
                for (int y = 0; y < 12; y++)
                    if (times[x, y])
                        canvas.FillRectangle(br, x * 60, y * 40, 60, 40);
            PictureBoxPapers_DrawLines(canvas); // draw gridlines
        }

        // Draws Gridlines on timetable
        private static void PictureBoxPapers_DrawLines(Graphics canvas)
        {
            Pen pene = new Pen(Color.Black);
            for (int x = 1; x < 5; x++)
                canvas.DrawLine(pene, x * 60, 0, x * 60, 480);   // vertical lines            
            for (int y = 1; y < 12; y++)
                canvas.DrawLine(pene, 0, y * 40, 300, y * 40);  // horizontal lines
        }

        private void AddPaper_Load(object sender, EventArgs e)
        {
            this.BackColor = mainForm.form_colour;
        }

        // for a new paper, all timeslots are initially set to false
        private void InitTimes()
        {
            for (int x = 0; x < 5; x++)
                for (int y = 0; y < 12; y++)
                    times[x, y] = false;
        }

        /// <summary>
        /// add paper to database.
        /// </summary>
        /// <param name="title">paper title e.g. infomation discovery</param>
        /// <param name="code">paper code e.g. COMP103</param>
        /// <param name="times">2D bool array where true indicates time</param>
        protected void addPaperToDB(String code, String title, bool[,] times)
        {
            if (code != "")
            {
                // add paper
                DatabaseQuery.DBInsert("insert into paper Values('" + code + "', '" + title + "')");

                // add time for paper
                foreach (Tuple<String, String> t in this.mainForm.mapBoolToTime(times))
                    DatabaseQuery.DBInsert(String.Format("insert into lab Values('{0}','{1}', {2}, 'R.G.07')", code, t.Item1, t.Item2));
            }
            else
                MessageBox.Show("Paper must have a paper code");
        }

        protected void updatePaperToDB(String code, String title, bool[,] times)
        {
            //Console.WriteLine(String.Format("delete from lab where code='{0}'; delete from paper where code='{0}'", code));
            //DatabaseQuery.DBInsert(String.Format("delete from lab where paper='{0}'; delete from paper where code='{0}'", paper.Code));
            // will remove assigned demos that are associated to a paper
            DatabaseQuery.DBInsert(String.Format(@"delete from Demos where LabID in (
                                                    select LabID
                                                    from demos d, lab l
                                                    where l.paper = '{0}'
                                                    and d.labID = l.id);delete from lab where paper='{0}';delete from paper where code='{0}';", paper.Code));

            addPaperToDB(code, title, times);
            this.mainForm.ListBoxPapers_Refresh();
        }

        private void buttonAddPaper_Click(object sender, EventArgs e)
        {
            if (editing) // if editing an old paper, delete the old copy            
                updatePaperToDB(textBoxPaperCode.Text, textBoxPaperName.Text, times);
            else
                addPaperToDB(textBoxPaperCode.Text, textBoxPaperName.Text, times);

            this.mainForm.ListBoxPapers_Refresh();
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e; // gets mouse location
            int x = me.Location.X / 60; // x and y coordinates of mouse, in relation to picture box regions
            int y = me.Location.Y / 40;

            Graphics canvas = pictureBox1.CreateGraphics();
            Brush b = new SolidBrush(Color.Blue);
            Brush b2 = new SolidBrush(Color.White);

            // (Un)Colour in region
            if (times[x, y])
                canvas.FillRectangle(b2, x * 60, y * 40, 60, 40);
            else
                canvas.FillRectangle(b, x * 60, y * 40, 60, 40);
            times[x, y] = !times[x, y]; // (Un)Select the box in the array

            PictureBoxPapers_DrawLines(canvas); // draw gridlines

            // Clean up
            b.Dispose();
            b2.Dispose();
            canvas.Dispose();
        }
    }
}
