using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CzyjToKod.Model
{
    public class ResultsData
    {
        [JsonInclude]
        public float cosine;
        [JsonInclude]
        public float jaro_similarity;
        [JsonInclude]
        public float jaro_winkler_similarity;
        [JsonInclude]
        public float levenshtein_distance;
        [JsonInclude]
        public float damerau_levenshtein_distance;
        [JsonInclude]
        public float hamming_distance;

        public override string ToString()
        {
            return "Cosine: " + cosine + " jaro_similarity: " + jaro_similarity+ " jaro_winkler_similarity: " + jaro_winkler_similarity + " levenshtein_distance: " + levenshtein_distance + " damerau_levenshtein_distance: " + damerau_levenshtein_distance + " hamming_distance: " + hamming_distance;
        }
    }
}
