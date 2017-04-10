using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1
{
    public partial class AddDemo : Form
    {
        public Main mainForm;
        public bool editing; // whether the demo's details are being edited/viewed (true) or a new demo is being added (false)
        public bool[,] times; // hold boolean values for whether a time was selected
        ListBox LB; // listbox from main form, for re focusing Selected Index
        public Demo demo;

        public AddDemo(Form MainForm, ListBox lb)
        {
            InitializeComponent();
            mainForm = MainForm as Main;
            LB = lb;
            times = new bool[5, 12];
            InitTimes(); // initate the timeslots as false
            editing = false;
            pictureBoxFreeTime.Paint += pictureBoxFreeTime_Paint; // draw the gridlines onto the picturebox when form is opened
        }

        public AddDemo(Form MainForm, Demo d, ListBox lb)
        {
            InitializeComponent();
            mainForm = MainForm as Main;
            demo = d;
            LB = lb;
            times = demo.freeTime;
            InitTexts(); // fill in the demo's details
            editing = true;
            pictureBoxFreeTime.Paint += pictureBoxFreeTime_Paint;
        }

        // Time slots are initiallised as false
        public void InitTimes()
        {
            for (int x = 0; x < 5; x++)
                for (int y = 0; y < 12; y++)
                    times[x, y] = false;
        }

        // If there is an easier way, please fix this
        public void InitTexts()
        {
            try
            {
                // check that there is no null values before trying to add information into the textboxes
                // When a Demo is added through the form, any blanks are filled (empty strings etc) to prevent issues, the database entries/webpage might not have this protection though
                if (demo.firstName != null)
                    textBoxFirstName.Text = demo.firstName;
                if (demo.familyName != null)
                    textBoxFamilyName.Text = demo.familyName;
                if (demo.phone != null)
                    textBoxPhone.Text = demo.phone;
                if (demo.age != null)
                    textBoxAge.Text = demo.age;
                if (demo.gender == "Male")
                    comboBoxGender.SelectedIndex = 0;
                else if (demo.gender == "Female")
                    comboBoxGender.SelectedIndex = 1;
                else if (demo.gender != null)
                    comboBoxGender.SelectedIndex = 2;
                if (demo.username != null)
                    textBoxUsername.Text = demo.username;
                if (demo.ID != null)
                    textBoxID.Text = demo.ID;
                if (demo.summer != null)
                    textBoxSummer.Text = demo.summer;
                if (demo.major != null)
                    textBoxMajor.Text = demo.major;
                if (demo.degree != null)
                    textBoxDegree.Text = demo.degree;
                if (demo.year != null)
                {
                    if (demo.year == "0")
                        comboBoxYear.SelectedIndex = 0;
                    else if (int.Parse(demo.year) > 5) // in case a user enters a year grater than 5 in the web app
                        comboBoxYear.SelectedIndex = 4;
                    else
                        comboBoxYear.SelectedIndex = int.Parse(demo.year) - 1;
                }
                if (demo.last)
                    comboBoxLast.SelectedIndex = 0;
                else
                    comboBoxLast.SelectedIndex = 1;
                if (demo.email != null)
                    textBoxEmail.Text = demo.email;

                textBoxEnrolled.Text = ""; // clear the text, but add an empty string to allow +=
                textBoxEnrolled.ForeColor = Color.Black; // set text colour to black
                foreach (String s in demo.enrolled)
                    textBoxEnrolled.Text += s + "\r\n";
                textBoxPrevious.Text = ""; // clear the text, but add an empty string to allow +=
                textBoxPrevious.ForeColor = Color.Black; // set text colour to black
                foreach (String s in demo.experience)
                    textBoxPrevious.Text += s + "\r\n";
                textBoxPreffered.Text = ""; // clear the text, but add an empty string to allow +=
                textBoxPreffered.ForeColor = Color.Black; // set text colour to black
                foreach (String s in demo.prefer)
                    textBoxPreffered.Text += s + "\r\n";
            }
            catch (Exception exc)
            {
                MessageBox.Show("Demonstrator information is corrupt. /nSaving the form will overwrite any corrupted data"); // this will stop the form from crashing, instead just stopping at invalid data, the user will then be able to save what was already loaded, overwritting the corrupted data
            }
        }

        private void pictureBoxFreeTime_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            Brush br = new SolidBrush(Color.Blue);
            for (int x = 0; x < 5; x++)
                for (int y = 0; y < 12; y++)
                    if (times[x, y])
                        canvas.FillRectangle(br, x * 60, y * 40, 60, 40); // draw the inital timeslots in, if any

            PictureBoxFreeTime_DrawLines(canvas); // draw the gridlines 
        }

        private void buttonAddDemo_Click(object sender, EventArgs e)
        {
            bool last = false; // inital values for comboboxes
            String gend = "Male";
            String yr = "5";

            // check if last year
            if (comboBoxLast.SelectedIndex == 0)
                last = true;

            // check gender
            if (comboBoxGender.SelectedIndex == 1)
                gend = "Female";
            else if (comboBoxGender.SelectedIndex == 2)
                gend = "Other";

            // year of study
            if (comboBoxYear.SelectedIndex < 4) // else it is 5+ (inital value)            
                yr = (comboBoxYear.SelectedIndex + 1).ToString();

            // fill in multiline textboxes
            List<String> en = new List<string>();
            en.AddRange(textBoxEnrolled.Lines);
            en.RemoveAll(String.IsNullOrWhiteSpace);
            List<String> ex = new List<string>();
            ex.AddRange(textBoxPrevious.Lines);
            List<String> pr = new List<string>();
            pr.AddRange(textBoxPreffered.Lines);

            bool fail = false;

            foreach (string s in en)
            {
                if (!Regex.Match(s.ToUpper(), "^(CGRD|COMP|STAT|MATH)\\d{3}-\\d{2}(A|B|T|S|C)").Success || s.Equals("") || s.Equals("\n"))
                {
                    MessageBox.Show("Invalid Course: " + s + "\nFORMAT: \"COMP103-14A\"");
                    fail = true;
                }
            }
            if (fail == false && (textBoxFirstName.Text == "" || textBoxFamilyName.Text == ""))
            {
                MessageBox.Show("Name fields cannot be blank");
                fail = true;
            }

            if (fail == false && (textBoxEmail.Text == "" && textBoxPhone.Text == ""))
            {
                MessageBox.Show("At least one form of contact is required, either email or phone");
                fail = true;
            }

            if (fail == false)
            {
                try
                {
                    int.Parse(textBoxAge.Text);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Age needs to be a number");
                    fail = true;
                }
            }

            // If invalid Paper Don't save
            if (fail)
                return;

            // Create new demo
            Demo d = new Demo(textBoxFirstName.Text, textBoxFamilyName.Text, textBoxPhone.Text, textBoxAge.Text, gend, textBoxUsername.Text, textBoxID.Text, textBoxSummer.Text, textBoxMajor.Text, textBoxDegree.Text, yr, last, textBoxEmail.Text, en, ex, pr, times);

            if (editing) // if the demo is being edited            
                d.updateDB(); // update demos attribute to database            
            else
            {
                Main.potentialDemos.Add(d); // add in demo to main form's list
                d.addToDB(); // add demo to database
            }

            mainForm.ListBoxPotentialDemos_Refresh();
            mainForm.ListBoxPDHours_Refresh();
            // update list box
            this.Close();
        }

        private void AddDemo_Load(object sender, EventArgs e)
        {
            this.BackColor = mainForm.form_colour;
        }

        // Draws gridlines on timetable
        private static void PictureBoxFreeTime_DrawLines(Graphics canvas)
        {
            Pen pene = new Pen(Color.Black);
            for (int x = 1; x < 5; x++)
                canvas.DrawLine(pene, x * 60, 0, x * 60, 480);   // vertical lines            
            for (int y = 1; y < 12; y++)
                canvas.DrawLine(pene, 0, y * 40, 300, y * 40);  // horizontal lines
        }


        private void pictureBoxFreeTime_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e; // gets mouse location
            int x = me.Location.X / 60; // x and y coordinates of mouse, in relation to picture box regions
            int y = me.Location.Y / 40;

            Graphics canvas = pictureBoxFreeTime.CreateGraphics();
            Brush b = new SolidBrush(Color.Blue);
            Brush b2 = new SolidBrush(Color.White);

            // (Un)Colour in region
            if (times[x, y])
                canvas.FillRectangle(b2, x * 60, y * 40, 60, 40); // MATHEMATICAL!! - Aaaaaaaddddddventure Time
            else
                canvas.FillRectangle(b, x * 60, y * 40, 60, 40);
            times[x, y] = !times[x, y]; // (Un)Select the box in the array

            PictureBoxFreeTime_DrawLines(canvas);

            // Clean up
            b.Dispose();
            b2.Dispose();
            canvas.Dispose();
        }

        private void textBoxEnrolled_Click(object sender, EventArgs e)
        {
            if (textBoxEnrolled.Text.StartsWith("e.g.")) // on first click
            {
                textBoxEnrolled.Clear(); // clear the example text
                textBoxEnrolled.ForeColor = Color.Black; // set text colour to black
            }
        }

        private void textBoxPrevious_Click(object sender, EventArgs e)
        {
            if (textBoxPrevious.Text.StartsWith("e.g.")) // on first click
            {
                textBoxPrevious.Clear(); // clear example text
                textBoxPrevious.ForeColor = Color.Black; // set text colour to black
            }
        }

        private void textBoxPreffered_Click(object sender, EventArgs e)
        {
            if (textBoxPreffered.Text.StartsWith("e.g.")) // on first click
            {
                textBoxPreffered.Clear(); // clear example text
                textBoxPreffered.ForeColor = Color.Black; // set text colour to black
            }
        }
        /// <summary>
        /// enables the deletion of a demo from the list
        /// Alexander Steel 1179339 5/8/15
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeleteDemo_Click(object sender, EventArgs e)
        {
            //checks that the click was intentional
            if (MessageBox.Show("Do you wish to delete " + demo.firstName + " " + demo.familyName + " from the Demo pool?", "Delete demo?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //remove the demo
                demo.removeFromDB();
                Main.potentialDemos.Remove(demo);
                //reset the listbox
                this.mainForm.ListBoxPotentialDemos_Refresh();
                this.mainForm.ListBoxPDHours_Refresh();
                LB.SelectedIndex = LB.Items.Count - 1;
                //close the edit demo dialog
                this.Close();
            }
        }
    }
}
