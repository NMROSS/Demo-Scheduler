from bs4 import BeautifulSoup
import httplib2

papers = []

try:
    file = open('papers.txt', 'r')
    for line in file:
        if '[END]' in line:
            break
        papers.append(line.rstrip()) # add to list and remove /n characters
except IOError:
    print('Can not open file')
file.close()

httpObj = httplib2.Http('.cache')


def extract_labs(table):
    '''
    Extracts lab day and time from html table
    :returns None probably mean page is invalid(incorrect paper or no info given)
    '''

    if table is None:
        return

    timetable = [ map(str, row.findAll('td')) for row in table.findAll('tr') ] # extract table elements
    lab = []
    for item in timetable:
        row = list(item)
        if(len(row)>0 and 'Laboratory' in row[0]): # get only labs no lecture times etc..
            day, start, finish = [row[1][4:-5], int(row[2][4:-5].replace(':','')), int(row[3][4:-5].replace(':',''))]
            while finish-start >= 100: # create new entry for each hour of lab
                if [day, start] not in lab:
                    lab.append( [day, start] )
                start += 100
    return lab


def get_lab_info(papers):
    '''
    Extracts Laboratory times from Waikato University site given a paper in form COMP103-16A
    '''
    labs = []
    for paper in papers:
        response, content = httpObj.request('http://timetable.waikato.ac.nz/{0}%20%28HAM%29&year=2016'.format(paper))
        soup = BeautifulSoup(content, 'html.parser')
        description = soup.find('caption')
        description = description.get_text().rsplit(')')[-1].strip() # extracts name from <caption> tag
        table = soup.find('table')
        labs.append([paper, description, extract_labs(table)])
    return labs


def create_sql(lab_info):
    '''
    Creates SQL Insert statements to insert into DB
    Creates file populateLabs.sql
    '''
    try:
        sql_file = open('populateLabs.sql','w')

        for paper in lab_info:
          sql_file.write('INSERT INTO Paper (code, name) VALUES (\'{0}\', \'{1}\');\n'.format(paper[0],paper[1]))
          for lab_session in paper[2]:
              sql_file.write('INSERT INTO Lab (paper, labday, labtime) VALUES (\'{0}\', \'{1}\', {2});\n'.format(paper[0], lab_session[0], lab_session[1] ))
    except IOError:
        print('Error Writing to file')
    sql_file.close()

create_sql(get_lab_info(papers))