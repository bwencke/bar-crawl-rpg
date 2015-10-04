import json
import os.path
import sys

print

# Check number of arguments
if (len(sys.argv) < 2):
    print('Usage: python dialogue.py ../game/Assets/Conversations/[Level]/[Character].json')
    print
    sys.exit()

# Open file
filename = sys.argv[1]
if (not os.path.isfile(filename)):
    print('Creating file: ' + filename)
    print('Opening file: ' + filename)
    file = open(filename, 'w+')
else:
    print('Opening file: ' + filename)
    file = open(filename, 'r+')
print

# Read file
filetext = file.read()
tree = json.loads(filetext)
print(tree)
print

# Close file
print('Closing file: ' + filename)
print
file.close()
