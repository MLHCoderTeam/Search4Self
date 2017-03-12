import requests
import json
import sys
from multiprocessing.pool import ThreadPool
from bs4 import BeautifulSoup
from pprint import pprint
from tqdm import tqdm
from queue import Queue


categories = set()

def fetch_url(url):
    r = requests.get(url)
    soup = BeautifulSoup(r.text, 'html.parser')
    for cell in soup.html.findAll('h4'):
        if 'Category' in cell.text:
            cats = cell.next.next.next.text.strip().split('\n')
            for i in cats:
                categories.add(i)
            return cats


def worker(inQueue, outQueue):
    while True:
        entry = inQueue.get()
        entry['categories'] = fetch_url(entry['url'])
        if entry['categories']:
            outQueue.put(entry)
        inQueue.task_done()


if __name__ == '__main__':
    js = json.loads(open(sys.argv[1], encoding='utf8').read())

    l = []
    inQueue = Queue()
    outQueue = Queue()
    ThreadPool(64, worker, (inQueue, outQueue))

    for e in js:
        try:
            inQueue.put({'url': 'http://youtube.com/watch?v=' + e['contentDetails']['videoId'],
                          'time': e['contentDetails']['videoPublishedAt'].split('T')[0]})
        except KeyError:
            pass

    inQueue.join()

    t = {}
    while not outQueue.empty():
        e = outQueue.get()
        if e['time'] not in t:
            t[e['time']] = {cat: 0 for cat in categories}

        try:
            for cat in e['categories']:
                t[e['time']][cat] += 1
        except KeyError:
            print("passed")

    final = {'categories': list(categories),
             'histogram': t}
    print(json.dumps(final))

