using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace WindowsFormsApplication1
{

    public partial class Main : Form
    {
        /* *********************************************************************************************************************************** 
         Use this space to write up any majpor changes that have happened between versions
         * //////////////////////////// Changes made from VC 19
         * Tidied up code
         * Added a new variable 'form_colour' to hold the backcolor for all the forms, allowing easier changing
         * Added .pdf to end of files
         * fixed deleting demoes
         * fixed timetable printing issue (THU instead of THUR)
         * Added Try Catches
         * Added ability to change back colour
         * Switched round Family name and First name in Add Demo
         * Changed Demo timetables to save differently in database
         * Demos now only show up when they are available  
         ********************************************************************************************************************************** */

        public Color form_colour = ColorTranslator.FromHtml("#999999"); // each form assigns back color to this variable, allowing quick change of color. All forms except Login can access this

        public static List<Paper> papers = new List<Paper>(); // will be replaced by the database
        public static List<Demo> potentialDemos = new List<Demo>();    // will be replaced by the database
        public static List<Demo> assignedDemo = new List<Demo>();
        public int timeX, timeY; // global variables for remembering the timeslot that was selected
        public String day = "";
        public String time = "";
        public String code = ""; // paper code

        public Main()
        {            
            InitializeComponent();
            pictureBoxTimes.Paint += PictureBoxTimes_Paint; // paints initial Timetable Grid
        }

        // Clicking on a paper will highlight all the designated timeslots. - DONE
        // Adding Demos to Papers
        // Clicking one of the highlighted slots will update the two demo listboxes.
        // The potential demos listbox will be sorted by capability, and won't show those who can't attend or demo it.
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.BackColor = form_colour;
            listBoxPapers.SelectedIndex = 0; // set inital focus to << New Paper >>

            updatePaperListFromDB();
            ListBoxPapers_Refresh(); // resets the list box
            ListBoxPotentialDemos_Refresh();
        }

        /// <summary>
        /// Convert strings day, time to boolean format
        /// </summary>
        /// <param name="day">String "MON", "TUE" ... </param>
        /// <param name="time">String 1800, 0900, ...</param>
        /// <returns>Tuple containing x,y position in array bool[,]</returns>
        public Tuple<int, int> mapTimeToBool(String day, String time)
        {
            int iDay, iTime;
            day = day.ToUpper();
            // convert day to array position in Bool[,]
            switch (day)
            {
                case "MON": iDay = 0; break;
                case "TUE": iDay = 1; break;
                case "WED": iDay = 2; break;
                case "THU": iDay = 3; break;
                case "FRI": iDay = 4; break;
                default: Console.Error.WriteLine("INVALID DAY GiVEN: " + day);
                    iDay = -1;
                    return null;
            }

            // convert time to array position in Bool[,]
            iTime = Int32.Parse(time) / 100 - 8;
            return new Tuple<int, int>(iDay, iTime);
        }



        /// <summary>
        /// Get all papers in databse and add them to Paper List
        /// </summary>
        private void updatePaperListFromDB()
        {
            // Delete papers in list as we getting the current list of paper from database.
            papers.Clear();

            // Get all papers from database and create paper object for each entrie in DB
            DataTable paperTable = DatabaseQuery.DBQuery("select * from paper");
            // add new paper for each row in db result
            for (int i = 0; i < paperTable.Rows.Count; i++)
            {
                
                Paper newPaper = new Paper(paperTable.Rows[i]["name"].ToString(), paperTable.Rows[i]["code"].ToString(), new bool[5, 12]);
                // get time this papers labs occour
                DataTable timeTable = DatabaseQuery.DBQuery("select labday, labtime from lab where lab.paper = '" + paperTable.Rows[i]["code"].ToString() + "'");
                for (int j = 0; j < timeTable.Rows.Count; j++)
                {
                    // convert time to boolean array format and update paper object.
                    Tuple<int, int> timeday = mapTimeToBool(timeTable.Rows[j]["labday"].ToString(), timeTable.Rows[j]["labtime"].ToString());
                    newPaper.Timeslot[timeday.Item1, timeday.Item2] = true;
                }
                papers.Add(newPaper);
            }
        }

        public void updatePotentialDemosListFromDB(String day, String time, String code)
        {
            // Delete papers in list as we getting the current list of paper from database.
            potentialDemos.Clear();

            // Get all demo from database and create paper object for each entrie in DB
            DataTable demoTable = DatabaseQuery.DBQuery(String.Format(@"select *
                                                                        from demoTimeTable dt, demonstrator demon
                                                                        where dt.labday = '{0}'
                                                                        and demon.studentID = dt.demoID
                                                                        and dt.labtime = '{1}'
                                                                        and dt.demoID in (  select studentID
                                                                                  from demonstrator
                                                                                  where studentID NOT IN (select ds.studentID
					                                                                                    from demos d, demonstrator ds, lab l
					                                                                                    where d.demoid = ds.studentID
					                                                                                    and l.id = d.labid
					                                                                                    and d.labid IN (select id
									                                                                                    from lab
									                                                                                    where labday = '{0}'
									                                                                                    and labtime = '{1}')))", day, time));



            // add new demo for each row in db result
            for (int i = 0; i < demoTable.Rows.Count; i++)
            {
                Demo newDemo = new Demo(                    
                        demoTable.Rows[i]["name"].ToString(), //firstName
                        demoTable.Rows[i]["lastName"].ToString(),//familyName
                        demoTable.Rows[i]["phoneNo"].ToString(), //phone
                        demoTable.Rows[i]["age"].ToString(), //age
                        demoTable.Rows[i]["gender"].ToString(),// gender
                        demoTable.Rows[i]["username"].ToString(),//username
                        demoTable.Rows[i]["studentID"].ToString(),// id
                        demoTable.Rows[i]["summerAddr"].ToString(),//summer
                        demoTable.Rows[i]["major"].ToString(),//major
                        demoTable.Rows[i]["degree"].ToString(),// degree
                        demoTable.Rows[i]["studyYear"].ToString(),//year
                        true,                  
                        demoTable.Rows[i]["email"].ToString()//email
                    );

                potentialDemos.Add(newDemo);

                // add enrolled papers
                DataTable enrolledTable = DatabaseQuery.DBQuery(String.Format("select * from enrolled where demoID={0}", newDemo.ID));
                if (enrolledTable != null)
                {
                    if (enrolledTable != null)
                    {
                        for (int j = 0; j < enrolledTable.Rows.Count; j++)
                        {
                            newDemo.enrolled.Add(enrolledTable.Rows[j]["paperCode"].ToString());
                        }
                    }
                }
                // add preferred Papers
                DataTable preferredPaperTable = DatabaseQuery.DBQuery(String.Format("select * from preferredPaper where demoID={0}", newDemo.ID));
                if (preferredPaperTable != null)
                {
                    for (int j = 0; j < preferredPaperTable.Rows.Count; j++)
                    {
                        newDemo.prefer.Add(preferredPaperTable.Rows[j]["paperCode"].ToString());
                    }
                }
                
                // add previously Demoed papers
                DataTable previouslyDemoedTable = DatabaseQuery.DBQuery(String.Format("select * from previouslyDemoed where demoID={0}", newDemo.ID));
                if (previouslyDemoedTable != null)
                {
                    for (int j = 0; j < previouslyDemoedTable.Rows.Count; j++)
                    {
                        newDemo.experience.Add(previouslyDemoedTable.Rows[j]["paperCode"].ToString());
                    }
                }

                DataTable freeTimeTable = DatabaseQuery.DBQuery(String.Format("select * from demoTimetable where demoID = {0}", newDemo.ID));
                if (freeTimeTable != null)
                {
                    for (int j = 0; j < freeTimeTable.Rows.Count; j++)
                    {
                        string lDay = freeTimeTable.Rows[j]["labday"].ToString();
                        string lTime = freeTimeTable.Rows[j]["labtime"].ToString();

                        Tuple<int, int> freeTimeTuple = mapTimeToBool(lDay, lTime);

                        newDemo.freeTime[freeTimeTuple.Item1, freeTimeTuple.Item2] = true;
                    }
                }
                
            }
            ListBoxPotentialDemos_Refresh();
            ListBoxPDHours_Refresh();
        }
        
        public void updateAssignedDemosListFromDB(String day, String time, String code)
        {
            assignedDemo.Clear();
            DataTable assignedTable = DatabaseQuery.DBQuery(String.Format(@"select * 
                                                                            from demos d, demonstrator ds, lab l
                                                                            where d.demoid = ds.studentID
                                                                            and l.id = d.labid
                                                                            and d.labid IN (select id
				                                                                            from lab
				                                                                            where labday = '{0}'
				                                                                            and labtime = '{1}'
                                                                                            and paper = '{2}')", day, time, code));
           
            // add new demo for each row in db result
            for (int i = 0; i < assignedTable.Rows.Count; i++)
            {
                Demo newDemo = new Demo(
                        assignedTable.Rows[i]["name"].ToString(), //firstName
                        assignedTable.Rows[i]["lastName"].ToString(),//familyName
                        assignedTable.Rows[i]["phoneNo"].ToString(), //phone
                        assignedTable.Rows[i]["age"].ToString(), //age
                        assignedTable.Rows[i]["gender"].ToString(),// gender
                        assignedTable.Rows[i]["username"].ToString(),//username
                        assignedTable.Rows[i]["studentID"].ToString(),// id
                        assignedTable.Rows[i]["summerAddr"].ToString(),//summer
                        assignedTable.Rows[i]["major"].ToString(),//major
                        assignedTable.Rows[i]["degree"].ToString(),// degree
                        assignedTable.Rows[i]["studyYear"].ToString(),//year
                        true,
                        assignedTable.Rows[i]["email"].ToString()//email
                    );
                assignedDemo.Add(newDemo);
            }
            ListBoxAssigned_Refresh();
            ListBoxADHours_Refresh();
        }

        private bool hasAssignedDemos(String day, String time, String code)
        {
            DataTable assignedTable = DatabaseQuery.DBQuery(String.Format(@"select ds.name,ds.studentID, l.paper 
                                                                            from demos d, demonstrator ds, lab l
                                                                            where d.demoid = ds.studentID
                                                                            and l.id = d.labid
                                                                            and d.labid IN (select id
				                                                                            from lab
				                                                                            where labday = '{0}'
				                                                                            and labtime = '{1}'
                                                                                            and paper = '{2}')", day, time, code));

            // add new demo for each row in db result
            return (assignedTable.Rows.Count > 0);
        }

        private String intToDay(int i)
        {
            String sDay = "";
            switch (i)
            {
                case 0: sDay = "MON"; break;
                case 1: sDay = "TUE"; break;
                case 2: sDay = "WED"; break;
                case 3: sDay = "THU"; break;
                case 4: sDay = "FRI"; break;
                default: Console.Error.WriteLine("INVALID DAY GiVEN: " + i);
                    return null;
            }
            return sDay;
        }

        public List<Tuple<String, String>> mapBoolToTime(bool[,] timeArray)
        {
            List<Tuple<String, String>> timesList = new List<Tuple<String, String>>();

            // loop through timearray find true element indicating a time
            for (int i = 0; i < timeArray.GetLength(0); i++)
            {
                for (int j = 0; j < timeArray.GetLength(1); j++)
                {
                    if (timeArray[i, j])
                    {
                        // convert position to day, time string add to list of times
                        timesList.Add(new Tuple<String, String>(intToDay(i), ((j + 8) * 100).ToString()));
                    }
                }
            }
            return timesList;
        }

        // Draws the timetable in the paint method of the form
        private void PictureBoxTimes_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            PictureBoxTimes_DrawLines(canvas);
        }

        // Based on the index selected, display the code and title above the timetable
        private void listBoxPapers_SelectedIndexChanged(object sender, EventArgs e)
        {
            code = listBoxPapers.SelectedItem.ToString();
            clearLists();

            if (listBoxPapers.SelectedItem != null) // apparantly you can select nothing in a list box
            {
                Graphics canvas = pictureBoxTimes.CreateGraphics();

                timeX = timeY = 0; // forgets the last selected time slot as paper has changed
                
                if (listBoxPapers.SelectedItem.ToString().StartsWith("<< ")) // if the New Paper Item is selected
                {
                    buttonNewPaper.Text = "New Paper"; // Change the button text to New Paper
                    canvas.Clear(Color.White); // redraw the timetable (no times selected for New Paper)
                    PictureBoxTimes_DrawLines(canvas);
                    buttonRemove.Enabled = false;
                }
                else // if a paper is selected
                {
                    buttonRemove.Enabled = true;
                    SolidBrush br = new SolidBrush(Color.Blue);
                    buttonNewPaper.Text = "Edit Paper"; // Change button to Edit rather than New
                    foreach (Paper p in papers) // replace with database
                    {
                        if (p.Code == listBoxPapers.SelectedItem.ToString()) // find the paper we selected
                        {
                            lblTitle.Text = p.Code + " - " + p.Title; // edit the label above the timetable

                            // Clear the timetable and draw the selected paper's timetable
                            canvas.Clear(Color.White);
                            for (int x = 0; x < 5; x++)
                            {
                                day = intToDay(x);
                                for (int y = 0; y < 12; y++)
                                {
                                    time = ((y + 8) * 100).ToString();
                                    if (p.Timeslot[x, y])
                                    {
                                        if (hasAssignedDemos(day, time, code))
                                            br.Color = Color.Green;
                                        else
                                            br.Color = Color.Blue;
                                        canvas.FillRectangle(br, x * 60, y * 40, 60, 40);
                                    }
                                }
                            }
                            PictureBoxTimes_DrawLines(canvas); // Draw the gridlines
                            break;
                        }
                    }
                }
            }
            // set time and day to null values if the paper is changed
            day = "";
            time = "";
        }

        
        // Draws the Gridlines for the timetable over the picturebox
        private static void PictureBoxTimes_DrawLines(Graphics canvas)
        {
            Pen pene = new Pen(Color.Black);        
            for (int x = 1; x < 5; x++)
                canvas.DrawLine(pene, x * 60, 0, x * 60, 480);  // vertical lines            
            for (int y = 1; y < 12; y++)
                canvas.DrawLine(pene, 0, y * 40, 300, y * 40);  // horizontal lines
            pene.Dispose(); // tidy Kiwi
        }

        // Opens a new window to either Add a new paper or Edit an existing one
        private void buttonNewPaper_Click(object sender, EventArgs e)
        {
            if (listBoxPapers.SelectedItem.ToString().StartsWith("<< ")) // If << New Paper >> is selected, add a new paper
            {
                AddPaper addP = new AddPaper(this, listBoxPapers); // Adds a new paper to the list
                addP.Show();
            }
            else // Otherwise, edit an existing one
            {
                foreach (Paper p in papers) // find the selected paper
                {
                    if (p.Code == listBoxPapers.SelectedItem.ToString()) 
                    {
                        AddPaper addP = new AddPaper(this, p, listBoxPapers); // pass it in for editing
                        addP.Show();
                        break;
                    }
                }                
            }
        }



        public void ListBoxPapers_Refresh() // refreshes the listbox, then adds << New Paper >> to the bottom
        {
            updatePaperListFromDB();
            listBoxPapers.Items.Clear(); // clear out Items
            foreach (Paper p in papers)
                listBoxPapers.Items.Add(p.Code);    // refill the list box
            listBoxPapers.Items.Add("<< New Paper >>"); // Add in the new paper option
            listBoxPapers.SelectedIndex = listBoxPapers.Items.Count - 1;
        }

       /// <summary>
        /// Clears potential demo listbox and refreshes with current data.
       /// </summary>
        public void ListBoxPotentialDemos_Refresh() // refreshes the listbox, then adds << New Demo >> to the bottom
        {
            //setting null and assigned data source get new data from db          
            listBoxPotentialDemos.DataSource = null;
            listBoxPotentialDemos.DataSource = potentialDemos;
            listBoxPotentialDemos.DisplayMember = "getFName";
            listBoxPotentialDemos.ValueMember = "getStudentID";
            listBoxPotentialDemos.Refresh();
        }

        public void ListBoxPDHours_Refresh() // refreshes the listbox, then adds << New Demo >> to the bottom
        {       
            listBoxPDHours.DataSource = null;
            listBoxPDHours.DataSource = potentialDemos;
            listBoxPDHours.DisplayMember = "getAssignedHours";
            listBoxPDHours.ValueMember = "getStudentID";
            listBoxPDHours.Refresh();
        }

        /// <summary>
        /// Clears assigned demo listbox and refreshes with current data.
        /// </summary>
        public void ListBoxAssigned_Refresh()
        {
            listBoxDemos.DataSource = null;
            listBoxDemos.DataSource = assignedDemo;
            listBoxDemos.DisplayMember = "getFName";
            listBoxDemos.ValueMember = "getStudentID";
            listBoxDemos.Refresh();
        }

        public void ListBoxADHours_Refresh() // refreshes the listbox, then adds << New Demo >> to the bottom
        {
     
            listBoxADHours.DataSource = null;
            listBoxADHours.DataSource = assignedDemo;
            listBoxADHours.DisplayMember = "getAssignedHours";
            listBoxADHours.ValueMember = "getStudentID";
            listBoxADHours.Refresh();
        }

        // ███████████████████████████████████████████████MOVE TO DEMO.cs When refarctoring█████████████████████████████████████████████████████████████████████
        /// <summary>
        /// Remove given paper from database
        /// </summary>
        /// <param name="code"></param>
        protected void deletePaperFromDB(String code)
        {
            DatabaseQuery.DBInsert((String.Format("delete from lab where paper='{0}'; delete from paper where code='{0}'", code)));
            ListBoxPapers_Refresh();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you wish to delete " + listBoxPapers.SelectedItem.ToString() + "?", "Delete " + listBoxPapers.SelectedItem.ToString() + "?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (listBoxPapers.SelectedItem != null) // check to make sure something is selected
                {

                    deletePaperFromDB(listBoxPapers.SelectedItem.ToString());
                }
            }
        }

        private void PictureBoxTimes_Click(object sender, EventArgs e)
        {
            if (listBoxPapers.SelectedIndex != listBoxPapers.Items.Count - 1)
            {
                string tmpDay, tmpTime;
                MouseEventArgs me = (MouseEventArgs)e; // gets mouse location
                int X = me.Location.X / 60; // x and y coordinates of mouse, in relation to picture box regions
                int Y = me.Location.Y / 40;
                Paper paper = papers.ElementAt(0);

                day = intToDay(X);
                tmpDay = day;
                time = ((Y + 8) * 100).ToString();
                tmpTime = time;
                code = listBoxPapers.SelectedItem.ToString();

                Graphics canvas = pictureBoxTimes.CreateGraphics();
                Brush b = new SolidBrush(Color.Blue);
                Brush g = new SolidBrush(Color.Green);
                Brush c = new SolidBrush(Color.Cyan);

                clearLists();

                foreach (Paper p in papers)
                {
                    if (p.Code == listBoxPapers.SelectedItem.ToString())
                    {
                        paper = p;
                    }
                }

                // Show loading image
                if (paper.Timeslot[X, Y])
                {
                    pictureBoxLoading.Visible = true;
                    pictureBoxLoading.Refresh();
                }

                //{
                // remember the timeslot selected, this will be forgotten when a new paper is selected
                timeX = X;
                timeY = Y;

                for (int x = 0; x < 5; x++)
                {
                    day = intToDay(x);
                    for (int y = 0; y < 12; y++)
                    {
                        time = ((y + 8) * 100).ToString();
                        if (paper.Timeslot[x, y] && X == x && Y == y) canvas.FillRectangle(c, x * 60, y * 40, 60, 40);
                        else if (paper.Timeslot[x, y])
                        {
                            if (hasAssignedDemos(day, time, code))
                                canvas.FillRectangle(g, x * 60, y * 40, 60, 40);
                            else
                                canvas.FillRectangle(b, x * 60, y * 40, 60, 40);
                        }
                    }
                }
                PictureBoxTimes_DrawLines(canvas); // draws the gridlines on the timetable
                //}

                day = tmpDay;
                time = tmpTime;

                if (paper.Timeslot[X, Y]) // removed to make it possible to unselect papers
                {
                    updatePotentialDemosListFromDB(day, time, code);
                    updateAssignedDemosListFromDB(day, time, code);
                    timerLoading.Start(); // hide loading image after 1 second
                }

                // Clean up
                g.Dispose();
                b.Dispose();
                canvas.Dispose();
            }
        }

        /*
         * Clears both demo lists
         */
        private void clearLists()
        {
            potentialDemos.Clear();
            assignedDemo.Clear();
            ListBoxAssigned_Refresh();
            ListBoxADHours_Refresh();
            ListBoxPotentialDemos_Refresh();
            ListBoxPDHours_Refresh();
        }

        private void buttonNewDemo_Click(object sender, EventArgs e)
        {
            AddDemo addD = new AddDemo(this, listBoxPotentialDemos); // pass in the demo being edited
            addD.Show();         
        }

        private void buttonDemoAssign_Click(object sender, EventArgs e)
        {
            if (listBoxPotentialDemos.SelectedItem != null)
            {
                // Get lab ID
                DataTable labTable = DatabaseQuery.DBQuery(String.Format(@"select id
                                                                from lab
                                                                where paper = '{0}'
                                                                and labday = '{1}'
                                                                and labtime = '{2}'", code, day, time));

                if (labTable.Rows.Count > 0)
                {
                    String demoID = listBoxPotentialDemos.SelectedValue.ToString();
                    String labID = labTable.Rows[0]["id"].ToString();

                    // Assign demo to new lab
                    DatabaseQuery.DBInsert(String.Format("INSERT INTO DEMOS VALUES({0}, {1})", labID, demoID));
                    updatePotentialDemosListFromDB(day, time, code);
                    updateAssignedDemosListFromDB(day, time, code);
                }
                else
                {
                    No no = new No(this); // pass in the demo being edited
                    no.Show();  
                }
                
            }
        }

        private void buttonDemoUnassign_Click(object sender, EventArgs e)
        {
            // check something is actually selected
            if (listBoxDemos.SelectedItem != null)
            {
                // Get lab ID
                DataTable labTable = DatabaseQuery.DBQuery(String.Format(@"select id
                                                                from lab
                                                                where paper = '{0}'
                                                                and labday = '{1}'
                                                                and labtime = '{2}'", code, day, time));

                String demoID = listBoxDemos.SelectedValue.ToString();
                String labID = labTable.Rows[0]["id"].ToString();

                //r unassign a demo from lab
                DatabaseQuery.DBInsert(String.Format(@"Delete from demos 
                                        where demoid = '{0}'
                                        and labid = '{1}'", demoID, labID));

                updatePotentialDemosListFromDB(day, time, code);
                updateAssignedDemosListFromDB(day, time, code);
            }
        }

        private void buttonEditDemo_Click(object sender, EventArgs e)
        {
            if (listBoxPotentialDemos.SelectedItem != null)
            {
                foreach (Demo d in potentialDemos)
                {
                    // get studentid of select demo
                    if ((d.ID) == listBoxPotentialDemos.SelectedValue.ToString())
                    {
                        AddDemo addD = new AddDemo(this, d, listBoxPotentialDemos); // pass in the demo being edited
                        addD.Show();
                        break;
                    }
                }
            }
            else if (listBoxDemos.SelectedItem != null)
            {
                foreach (Demo d in assignedDemo)
                {
                    // get studentid of select demo
                    if ((d.ID) == listBoxDemos.SelectedValue.ToString())
                    {
                        AddDemo addD = new AddDemo(this, d, listBoxDemos); // pass in the demo being edited
                        addD.Show();
                        break;
                    }
                }
            }
        }

        /*
         * Enable/disable buttons, select/unselected list items in potential demos
         */
        private void listBoxPotentialDemos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPotentialDemos.SelectedIndex > -1)
            {
                listBoxDemos.ClearSelected();
                buttonDemoAssign.Enabled = true;
                buttonEditDemo.Enabled = true;
                buttonDemoUnassign.Enabled = false;
            }
            if (listBoxPDHours.Items.Count > 0 && (listBoxPotentialDemos.Items.Count == listBoxPDHours.Items.Count))
                listBoxPDHours.SelectedIndex = listBoxPotentialDemos.SelectedIndex;
            if (listBoxDemos.SelectedIndex < 0 && listBoxPotentialDemos.SelectedIndex < 0)
            {
                buttonDemoAssign.Enabled = false;
                buttonEditDemo.Enabled = false;
                buttonDemoUnassign.Enabled = false;
            }
        }

        /*
         * Enable/disable buttons, select/unselected list items in assigned demos
         */
        private void listBoxDemos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxDemos.SelectedIndex > -1)
            {
                listBoxPotentialDemos.ClearSelected();
                buttonDemoAssign.Enabled = false;
                buttonEditDemo.Enabled = true;
                buttonDemoUnassign.Enabled = true;
            }
            if (listBoxADHours.Items.Count > 0 && (listBoxDemos.Items.Count == listBoxADHours.Items.Count))
                listBoxADHours.SelectedIndex = listBoxDemos.SelectedIndex;
            if (listBoxDemos.SelectedIndex < 0 && listBoxPotentialDemos.SelectedIndex < 0)
            {
                buttonDemoAssign.Enabled = false;
                buttonEditDemo.Enabled = false;
                buttonDemoUnassign.Enabled = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// using pdf creatin library found at http://pdfsharp.codeplex.com/releases/view/37054
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPrintPaper_Click(object sender, EventArgs e)
        {
            // print out a timetable
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "PDF (.pdf)|*.pdf";
            if (save.ShowDialog() == DialogResult.OK)
            {
                PdfDocument pdf = new PdfDocument();
                PdfPage page = pdf.AddPage();
                PageSize[] pageSizes = (PageSize[])Enum.GetValues(typeof(PageSize));
                page.Size = pageSizes[4];
                XGraphics graph = XGraphics.FromPdfPage(page);
                //create fonts
                XFont title = new XFont("Verdana", 15, XFontStyle.Bold);
                XFont paper = new XFont("Verdana", 10, XFontStyle.Bold);
                XFont demo = new XFont("Verdana", 10);

                //column locations
                int colWidth = (int)(page.Width - 200) / 11;
                int[] col = new int[6];
                col[0] = colWidth + 100;
                col[1] = 3 * colWidth + 100;
                col[2] = 5 * colWidth + 100;
                col[3] = 7 * colWidth + 100;
                col[4] = 9 * colWidth + 100;
                col[5] = (int)page.Width - 100;

                int YBase = 100;
                int YCurr = YBase + 25;
                int YMax = 140;

                int YStart = 120;
                int YEnd = (int)page.Height - 100;

                //set up table
                graph.DrawString("Mon", title, XBrushes.Black, new XRect(col[0], YBase, colWidth * 2, page.Height.Point), XStringFormats.TopCenter);
                graph.DrawString("Tue", title, XBrushes.Black, new XRect(col[1], YBase, colWidth * 2, page.Height.Point), XStringFormats.TopCenter);
                graph.DrawString("Wed", title, XBrushes.Black, new XRect(col[2], YBase, colWidth * 2, page.Height.Point), XStringFormats.TopCenter);
                graph.DrawString("Thur", title, XBrushes.Black, new XRect(col[3], YBase, colWidth * 2, page.Height.Point), XStringFormats.TopCenter);
                graph.DrawString("Fri", title, XBrushes.Black, new XRect(col[4], YBase, colWidth * 2, page.Height.Point), XStringFormats.TopCenter);
                graph.DrawString("0800", title, XBrushes.Black, new XRect(100, 125, colWidth, page.Height.Point), XStringFormats.TopCenter);

                Point[] x = new Point[2];
                //  topBorder
                x[0] = new Point(col[0], YStart);
                x[1] = new Point(col[5], YStart);
                graph.DrawLines(XPens.Black, x);



                DataTable timetable = DatabaseQuery.DBQuery(@"select l.labtime, l.labday, l.paper, dem.name
                                                            from lab l, demos d, DEMONSTRATOR dem
                                                            where l.id = d.LabID
                                                            and dem.studentID = d.DemoID
                                                            order by l.labtime, l.labday,l.paper,dem.name ASC");
                String[] times = new String[12];
                times[0] = "800";
                times[1] = "900";
                times[2] = "1000";
                times[3] = "1100";
                times[4] = "1200";
                times[5] = "1300";
                times[6] = "1400";
                times[7] = "1500";
                times[8] = "1600";
                times[9] = "1700";
                times[10] = "1800";
                times[11] = "1900";

                String[] days = new String[5];
                days[0] = "MON";
                days[1] = "TUE";
                days[2] = "WED";
                days[3] = "THU";
                days[4] = "FRI";

                int cTime = 0;
                int cDay = 0;
                String cPaper = "";
                int pad = 2;
                int nl = 10;

                for (int i = 0; i < timetable.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        bool finding = true;
                        while (finding)
                        {
                            //if the time is the next hour
                            if (times[cTime].Equals(timetable.Rows[i]["labtime"].ToString().ToUpper()))
                            {

                                //set day to Monday
                                cDay = 0;
                                while (finding)
                                {
                                    //if it is on the current day
                                    if (days[cDay].Equals(timetable.Rows[i]["labday"].ToString().ToUpper()))
                                    {
                                        //update the paper
                                        cPaper = timetable.Rows[i]["paper"].ToString().ToUpper();
                                        //write the paper code and demo name
                                        graph.DrawString(cPaper, paper, XBrushes.Black, new XRect(col[cDay] + pad, YCurr, colWidth * 2, page.Height.Point), XStringFormats.TopLeft);
                                        YCurr = YCurr + nl + pad;
                                        graph.DrawString(timetable.Rows[i]["name"].ToString(), demo, XBrushes.Black, new XRect(col[cDay] + pad, YCurr, colWidth * 2, page.Height.Point), XStringFormats.TopLeft);
                                        YCurr = YCurr + nl + pad;
                                        finding = false;
                                    }
                                    else
                                    {
                                        if (cDay < 5) cDay++;
                                        else
                                        {
                                            MessageBox.Show("Database error: Returned out of order");
                                            break;
                                        }
                                    }
                                }
                            }
                            //if there is nothing in the next hour
                            else
                            {
                                //update the time
                                cTime++;
                                if (YCurr + pad > YMax) YMax = YCurr;
                                YBase = YMax;
                                YCurr = YBase + pad;
                                YMax = YBase + 20;
                                //draw the baseline
                                x[0] = new Point(col[0], YBase);
                                x[1] = new Point(col[5], YBase);
                                graph.DrawLines(XPens.Black, x);
                                //draw teh time label
                                if (times[cTime].Equals("900")) graph.DrawString("0900", title, XBrushes.Black, new XRect(100, YCurr, colWidth, page.Height.Point), XStringFormats.TopCenter);
                                else graph.DrawString(times[cTime], title, XBrushes.Black, new XRect(100, YCurr, colWidth, page.Height.Point), XStringFormats.TopCenter);
                                //move to the next hour
                            }

                        }
                    }
                    else
                    {
                        //if teh time hasn't changed
                        if (timetable.Rows[i]["labtime"].ToString().ToUpper().Equals(times[cTime]))
                        {
                            //if the day hasnt changed
                            if (timetable.Rows[i]["labday"].ToString().ToUpper().Equals(days[cDay]))
                            {
                                //if the paper hasn't changed
                                if (timetable.Rows[i]["paper"].ToString().ToUpper().Equals(cPaper))
                                {
                                    //write the demo name
                                    graph.DrawString(timetable.Rows[i]["name"].ToString(), demo, XBrushes.Black, new XRect(col[cDay] + pad, YCurr, colWidth * 2, page.Height.Point), XStringFormats.TopLeft);
                                    YCurr = YCurr + nl + pad;
                                }
                                //if the paper has changed
                                else
                                {
                                    //update the paper
                                    cPaper = timetable.Rows[i]["paper"].ToString().ToUpper();
                                    //write the paper code
                                    graph.DrawString(cPaper, paper, XBrushes.Black, new XRect(col[cDay] + pad, YCurr, colWidth * 2, page.Height.Point), XStringFormats.TopLeft);
                                    //write the demo name
                                    YCurr = YCurr + nl + pad;
                                    graph.DrawString(timetable.Rows[i]["name"].ToString(), demo, XBrushes.Black, new XRect(col[cDay] + pad, YCurr, colWidth * 2, page.Height.Point), XStringFormats.TopLeft);
                                    YCurr = YCurr + nl + pad;
                                }
                            }
                            //if the day has Changed
                            else
                            {
                                //see if the floor needs moving
                                if (YCurr + pad > YMax) YMax = YCurr;
                                YCurr = YBase + pad;
                                bool finding = true;
                                //update the day and paper
                                cDay = 0;
                                while (finding)
                                {
                                    //if it is on the current day
                                    if (days[cDay].Equals(timetable.Rows[i]["labday"].ToString().ToUpper()))
                                    {
                                        //update the paper
                                        cPaper = timetable.Rows[i]["paper"].ToString().ToUpper();
                                        //write the paper code and demo name
                                        graph.DrawString(cPaper, paper, XBrushes.Black, new XRect(col[cDay] + pad, YCurr, colWidth * 2, page.Height.Point), XStringFormats.TopLeft);
                                        YCurr = YCurr + nl + pad;
                                        graph.DrawString(timetable.Rows[i]["name"].ToString(), demo, XBrushes.Black, new XRect(col[cDay] + pad, YCurr, colWidth * 2, page.Height.Point), XStringFormats.TopLeft);
                                        YCurr = YCurr + nl + pad;
                                        finding = false;
                                    }
                                    else
                                    {
                                        //update the day
                                        if (cDay != 5) cDay++;
                                        else
                                        {
                                            MessageBox.Show("Database error: Returned out of order");
                                            finding = false;
                                            break;
                                        }
                                    }
                                }

                            }
                        }
                        //if the time has changed
                        else
                        {
                            bool finding = true;
                            while (finding)
                            {
                                //if the time is the next hour
                                if (times[cTime].Equals(timetable.Rows[i]["labtime"].ToString().ToUpper()))
                                {

                                    //set day to Monday
                                    cDay = 0;
                                    while (finding)
                                    {
                                        //if it is on the current day
                                        if (days[cDay].Equals(timetable.Rows[i]["labday"].ToString().ToUpper()))
                                        {
                                            //update the paper
                                            cPaper = timetable.Rows[i]["paper"].ToString().ToUpper();
                                            //write the paper code and demo name
                                            graph.DrawString(cPaper, paper, XBrushes.Black, new XRect(col[cDay] + pad, YCurr, colWidth * 2, page.Height.Point), XStringFormats.TopLeft);
                                            YCurr = YCurr + nl + pad;
                                            graph.DrawString(timetable.Rows[i]["name"].ToString(), demo, XBrushes.Black, new XRect(col[cDay] + pad, YCurr, colWidth * 2, page.Height.Point), XStringFormats.TopLeft);
                                            YCurr = YCurr + nl + pad;
                                            finding = false;
                                        }
                                        else
                                        {
                                            if (cDay != 5) cDay++;
                                            else
                                            {
                                                MessageBox.Show("Database error: Returned out of order");
                                                break;
                                            }
                                        }
                                    }
                                }
                                //if there is nothing in the next hour
                                else
                                {
                                    //update the time
                                    cTime++;
                                    if (YCurr + pad > YMax) YMax = YCurr;
                                    YBase = YMax;
                                    YCurr = YBase + pad;
                                    YMax = YBase + 20;
                                    //draw the baseline
                                    x[0] = new Point(col[0], YBase);
                                    x[1] = new Point(col[5], YBase);
                                    graph.DrawLines(XPens.Black, x);
                                    //draw teh time label
                                    if (times[cTime].Equals("900")) graph.DrawString("0900", title, XBrushes.Black, new XRect(100, YCurr, colWidth, page.Height.Point), XStringFormats.TopCenter);
                                    else graph.DrawString(times[cTime], title, XBrushes.Black, new XRect(100, YCurr, colWidth, page.Height.Point), XStringFormats.TopCenter);
                                    //move to the next hour
                                }

                            }
                        }
                    }
                }

                if (YCurr + pad > YMax) YMax = YCurr;
                YBase = YMax;
                x[0] = new Point(col[0], YBase);
                x[1] = new Point(col[5], YBase);
                graph.DrawLines(XPens.Black, x);

                //Colomn Borders
                x[0] = new Point(col[0], YStart);
                x[1] = new Point(col[0], YBase);
                graph.DrawLines(XPens.Black, x);
                x[0] = new Point(col[1], YStart);
                x[1] = new Point(col[1], YBase);
                graph.DrawLines(XPens.Black, x);
                x[0] = new Point(col[2], YStart);
                x[1] = new Point(col[2], YBase);
                graph.DrawLines(XPens.Black, x);
                x[0] = new Point(col[3], YStart);
                x[1] = new Point(col[3], YBase);
                graph.DrawLines(XPens.Black, x);
                x[0] = new Point(col[4], YStart);
                x[1] = new Point(col[4], YBase);
                graph.DrawLines(XPens.Black, x);
                x[0] = new Point(col[5], YStart);
                x[1] = new Point(col[5], YBase);
                graph.DrawLines(XPens.Black, x);


                //export the pdf
                try
                {
                    pdf.Save(save.FileName);
                }
                catch (Exception e1)
                {
                    MessageBox.Show("please make sure that " + save.FileName + " is not already being used by another program" + e1.Message);
                }

            }
        }

        /*
         * Delay loading picture disappearing
         */
        private void timerLoading_Tick(object sender, EventArgs e)
        {
            pictureBoxLoading.Visible = false;
            pictureBoxLoading.Refresh();
            timerLoading.Stop();
        }

        private void buttonColour_Click(object sender, EventArgs e)
        {
            if (colorPicker.ShowDialog() == DialogResult.OK)
            {
                form_colour = colorPicker.Color;
                this.BackColor = form_colour;
            }
        }

    }
}
