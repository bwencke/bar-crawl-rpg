import json
import os.path
import sys

print

# Check number of arguments
if (len(sys.argv) < 3):
    print('Usage: python dialogue.py [Character].txt [Character].json')
    print
    sys.exit()

# Open text file
filename = sys.argv[1]
if (not os.path.isfile(filename)):
    print('Error: File not found.')
    print 
    sys.exit()
print('Opening file: ' + filename)
file = open(filename, 'r')
print

# Create data
data = {}
snippets = []
data['snippets'] = snippets

# Read file
line = file.readline()
while (line):
    # Skip empty lines
    if (line.isspace()):
        line = file.readline()
        continue
    line = line.lstrip().rstrip()
        
    # Read snippet
    if (not line == 'SNIPPET'):
        print('Error: Expected SNIPPET.')
        print
        sys.exit()
    
    line = file.readline().lstrip().rstrip()
    
    # Create snippet
    snippet = {}
    snippets.append(snippet)
    
    # Read ID
    if (not line):
        print('Error: Expected snippet ID.')
        print
        sys.exit()
    
    # Create ID
    snippet['id'] = line
    
    line = file.readline().lstrip().rstrip()
    
    # Create assignments
    assignments = []
    snippet['assignments'] = assignments;
    
    # Read assignments
    if (line == 'ASSIGNMENTS'):
        line = file.readline().lstrip().rstrip()
        while (line != 'ENDASSIGNMENTS'):
            if (not line):
                print('Error: Expected assignment.')
                print
                sys.exit()
            
            assignment = line.split('=', 1)
            if (len(assignment) < 2):
                print('Error: Expected equal sign in assignment.')
                print
                sys.exit()
            
            # Create assignment
            assignments.append([assignment[0], assignment[1]])
            
            line = file.readline().lstrip().rstrip()
        
        line = file.readline().lstrip().rstrip()
    
    # Create statements
    statements = []
    snippet['statements'] = statements
    
    # Read statements
    if (line == 'STATEMENTS'):
        line = file.readline().lstrip().rstrip()
        while (line != 'ENDSTATEMENTS'):
            if (not line):
                print('Error: Expected character name.')
                print
                sys.exit()
            
            # Create statement
            statement = []
            statements.append(statement)
            
            # Create name
            statement.append(line)
            
            line = file.readline().lstrip().rstrip()
            
            if (not line):
                print('Error: Expected character text.')
                print
                sys.exit()
            
            # Create text
            statement.append(line)
            
            line = file.readline().lstrip().rstrip()
            
        line = file.readline().lstrip().rstrip()
    
    # Create options
    options = []
    snippet['options'] = options
    
    # Read options
    while (line == 'OPTION'):
        line = file.readline().lstrip().rstrip()
        
        # Create option
        option = {}
        options.append(option)
        
        # Create conditions
        conditions = []
        option['conditions'] = conditions
        
        if (line == 'CONDITIONS'):
            line = file.readline().lstrip().rstrip()
            while (line != 'ENDCONDITIONS'):
                if (not line):
                    print('Error: Expected condition.')
                    print
                    sys.exit()
                
                condition = line.split('=', 1)
                if (len(condition) < 2):
                    print('Error: Expected equal sign in condition.')
                    print
                    sys.exit()
            
                # Create condition
                conditions.append([condition[0], condition[1]])
                
                line = file.readline().lstrip().rstrip()
                
            line = file.readline().lstrip().rstrip()
        
        if (not line):
            print('Error: Expected option text.')
            print
            sys.exit()
            
        # Create text
        option['text'] = line
        
        line = file.readline().lstrip().rstrip()
        
        if (not line):
            print('Error: Expected option ID.')
            print
            sys.exit()
        
        # Create ID
        option['id'] = line
        
        line = file.readline().lstrip().rstrip() # ENDOPTION
        line = file.readline().lstrip().rstrip() # OPTION or ENDSNIPPET
        
    line = file.readline()

# Close text file
print('Closing file: ' + filename)
print
file.close()

# Open JSON file
filename = sys.argv[2]
if (not os.path.isfile(filename)):
    print('Creating file: ' + filename)
print('Opening file: ' + filename)
print
file = open(filename, 'w+')

# Write JSON
json.dump(data, file)

# Close JSON file
print('Closing file: ' + filename)
print
file.close()
