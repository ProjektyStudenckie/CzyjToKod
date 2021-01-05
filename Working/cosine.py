import math
import re
from collections import Counter

WORD = re.compile(r"\w+")


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


def text_to_vector(text):
    words = WORD.findall(text)
    return Counter(words)

second = open("reinterpreted_file_1.txt", "r")
first = open("reinterpreted_file_2.txt", "r")

text1 = second.read()
text2 = first.read()


vector1 = text_to_vector(text1)
vector2 = text_to_vector(text2)

cosine = calculateCosineSimilarity(vector1, vector2)

result = open("result.txt", "w")
result.write(str(cosine))