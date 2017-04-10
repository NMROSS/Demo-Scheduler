Description:
 Scraps Waikato uni timetable page for lab times given a list of papers returns an sql file for use with the Demo Scheduler program.

Required:
 Python 3		'https://www.python.org/downloads/'
 BeautifulSoup  'pip install beautifulsoup4'
 httplib2		'pip install httplib2'
 
Use:
  Run 'python main.py' from command line
  
Input:
 papers.txt containing Valid Waikato uni paper names each on new lines e.g. COMP103-15A
  
Output:
  Creates a sql file called 'populateLabs.sql' containing valid sql insert statements

  