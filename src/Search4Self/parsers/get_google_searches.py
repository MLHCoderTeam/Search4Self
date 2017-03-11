import json
import os
import sys
import datetime
import string
import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
from collections import Counter

files = os.listdir(sys.argv[1])
nrSearches = int(sys.argv[2])

searches = []
dates = []
for file in files:
    with open("Data/Searches/%s"%(file)) as json_data:
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
top = freqs.most_common(nrSearches)

words = {k:v for (k,v) in freqs.most_common(nrSearches)}

print(json.dumps(words))
