import json
import os
import sys
import datetime
import string
import numpy as np
from collections import Counter

folder = sys.argv[1]
files = os.listdir(folder)

searches = []
dates = []
for _file in files:
    with open(folder + "/" + _file, encoding='utf-8') as json_data:
        d = json.load(json_data)

    for i in range(len(d['event'])):
        for j in range(len((d['event'][i][u'query'][u'id']))):
            searches.append(d['event'][i][u'query'][u'query_text'])
            dates.append(d['event'][i][u'query'][u'id'][j][u'timestamp_usec'])

dates = [datetime.datetime.fromtimestamp(int(i)/1000000).strftime('%Y-%m-%d %H:%M:%S')
         for i in dates]
hours = [datetime.datetime.strptime(i, '%Y-%m-%d %H:%M:%S').hour for i in dates]

combo = [entry.lower().translate(str.maketrans("", "", string.punctuation)) for entry in searches if len(entry) > 3]
freqs = Counter(combo)

if len(sys.argv) > 2:
    words = {k:v for (k,v) in freqs.most_common(sys.argv[2])}
    freqs = words

print(json.dumps(freqs))
