import math
import re
import jellyfish
from collections import Counter

WORD = re.compile(r"\w+")

import json
def calculateCosineSimilarity(vec1, vec2):
    intersection = set(vec1.keys()) & set(vec2.keys())
    numerator = sum([vec1[x] * vec2[x] for x in intersection])

    sum1 = sum([vec1[x] ** 2 for x in list(vec1.keys())])
    sum2 = sum([vec2[x] ** 2 for x in list(vec2.keys())])
    denominator = math.sqrt(sum1) * math.sqrt(sum2)

    if not denominator:
        return 0.0
    else:
        return float(numerator) / denominator


def textToVector(text):
    words = WORD.findall(text)
    return Counter(words)

second = open("reinterpreted_file_1.txt", "r")
first = open("reinterpreted_file_2.txt", "r")

text1 = second.read()
text2 = first.read()


vector1 = textToVector(text1)
vector2 = textToVector(text2)

cosine = calculateCosineSimilarity(vector1, vector2)

data = {
    'cosine': cosine,
    'jaro_similarity': jellyfish.jaro_similarity(text1, text2),
    'jaro_winkler_similarity': jellyfish.jaro_winkler_similarity(text1, text2),
    'levenshtein_distance': jellyfish.levenshtein_distance(text1, text2),
    'damerau_levenshtein_distance': jellyfish.damerau_levenshtein_distance(text1, text2),
    'hamming_distance': jellyfish.hamming_distance(text1, text2)

}

with open('results.txt', 'w') as outfile:
    json.dump(data, outfile)