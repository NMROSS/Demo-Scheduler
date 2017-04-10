using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WindowsFormsApplication1
{
    public class Demo
    {
        // Details about the demo
        public String firstName = ""; // firstname - John
        public String familyName = ""; // lastname - Doe
        public String phone = ""; // phone number - 027 123 4567 (format not important as of yet)
        public String age = ""; // age - 18
        public String gender = "";// gender - includes Other (political correctness)
        public String username = ""; // username - jdo27 (again, format not important as of yet)
        public String ID = ""; // id number - 1234567
        public String summer = ""; // summer address, not really sure why this is important
        public String major = ""; // major - Computer Science
        public String degree = ""; // degree - BCMS
        public String year = "1"; // year of study - 3
        public String email = ""; // email address - jdoe@fakeemails.com
        public bool last = false; // last year of study? - true = yes
        public List<String> enrolled = new List<string>(); // papers enrolled in for this year
        public List<String> experience = new List<string>(); // demoing experience from previous years
        public List<String> prefer = new List<string>(); // preferered labs to demo
        public bool[,] freeTime = new bool[5,12]; // availabilty
        public int hours = 0;
       
        
        public Demo(String fn, String ln, String p, String a, String g, String u, String id, String s, String m, String d, String y, bool lst, String e, List<String> en, List<String> ex, List<String> pr, bool[,] ft)
        {
            firstName = fn;
            familyName = ln;
            phone = p;
            age = a;
            gender = g;
            username = u;
            ID = id;
            summer = s;
            major = m;
            degree = d;
            year = y;
            last = lst;
            email = e;
            enrolled = en;
            experience = ex;
            prefer = pr;
            freeTime = ft;
        }

        /// <summary>
        /// firstName = fn;familyName = ln;phone = p;age = a;gender = g;username = u;ID = id;summer = s;major = m;degree = d;year = y;last = lst;email = e;enrolled = en;experience = ex;prefer = pr;freeTime = ft;
        /// </summary>
        public Demo(String fn, String ln, String p, String a, String g, String u, String id, String s, String m, String d, String y, bool lst, String e)
        {
            firstName = fn;
            familyName = ln;
            phone = p;
            age = a;
            gender = g;
            username = u;
            ID = id;
            summer = s;
            major = m;
            degree = d;
            year = y;
            last = lst;
            email = e;
        }

        /// <summary>
        /// Updates demos attributes to database
        /// </summary>
        public void updateDB(){
            DatabaseQuery.DBInsert(String.Format(@"
            update DEMONSTRATOR SET name='{0}', lastName='{1}', phoneNo='{2}', gender='{3}', username='{4}', summerAddr='{5}', major='{6}', degree='{7}', studyYear={8}, email='{9}', lastYear={10}, age={11} where studentID = '{12}'",
            firstName, familyName, phone, gender, username, summer, major, degree, year, email, last.Equals("1") ? 1 : 0 , age, ID));


            // add enrolled, prefered and experience papers by removing old and inserting new
            DatabaseQuery.DBInsert(String.Format("delete from enrolled where demoID={0}", ID));
            foreach (String s in enrolled)
            {
                // prevents empty lines
                if (s != "")
                    DatabaseQuery.DBInsert(String.Format("insert into enrolled(paperCode, demoID) Values('{0}', {1})",s,ID));
            }

            DatabaseQuery.DBInsert(String.Format("delete from preferredPaper where demoID={0}", ID));
            foreach (String s in prefer)
            {
                // prevents empty lines
                if(s!="")
                    DatabaseQuery.DBInsert(String.Format("insert into preferredPaper Values('{0}', {1})", s , ID));
            }
            
            DatabaseQuery.DBInsert(String.Format("delete from previouslyDemoed where demoID={0}", ID));
            foreach (String s in experience)
            {
                // prevents empty lines
                if (s != "")
                    DatabaseQuery.DBInsert(String.Format("insert into previouslyDemoed Values('{0}', {1})", s, ID));
            }

            DatabaseQuery.DBInsert(String.Format("delete from demoTimeTable where demoID = {0}", ID));

            foreach (Tuple<String, String> t in mapBoolToTime(freeTime))
                DatabaseQuery.DBInsert(String.Format("insert into demoTimeTable Values('{0}','{1}', {2})", t.Item1, t.Item2, ID));

            // set non free demo time slots
            //for (int i = 0; i < freeTime.GetLength(0); i++)
            //{
            //    for (int j = 0; j < freeTime.GetLength(1); j++)
            //    {

            //        if (freeTime[i, j])
            //            DatabaseQuery.DBInsert(String.Format("insert into demoTimeTable Values({0},{1})", ID, intToDay(i), j)); //////////////////////////////////////////////////////////////////////////////// CHANGE i TO MON, TUE etc. j to 1000, 1100, 1200 etc
            //    }
            //}
        }

        /// <summary>
        /// Add demo to database
        /// </summary>
        public void addToDB()
        {
            DatabaseQuery.DBInsert(String.Format("INSERT INTO DEMONSTRATOR(studentID, name) VALUES('{0}', '{1}')", ID, firstName));
            updateDB();
        }

        /// <summary>
        /// removes demo from database
        /// </summary>
        public void removeFromDB()
        {
            // Remove demo from all tables
            DatabaseQuery.DBInsert(String.Format(@"delete from enrolled where demoID = {0}; delete from previouslyDemoed where demoID = {0};
                                                    delete from preferredPaper where demoID = {0}
                                                    delete from demoTimeTable where demoID = {0}", ID));
            DatabaseQuery.DBInsert(String.Format("delete from Demos where DemoID = {0}; delete from demonstrator where studentID='{0}'", ID));
        }


        /// <summary>
        /// Returns 
        /// </summary>
        /// <returns></returns>
        public int assignedHours()
        {
            DataTable dt = DatabaseQuery.DBQuery(String.Format("select * from demos where DemoID = {0}", ID));
            hours = dt.Rows.Count;
            return hours;
        }

        public int getAssignedHours
        {
            get
            {
                return assignedHours();
            }
        }

        public String getFName
        {
            get
            {
                return firstName + " " + familyName;
            }
        }
      
        public String getStudentID
        {
            get
            {
                return ID;
            }
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
    }
}
