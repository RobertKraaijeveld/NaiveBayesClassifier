using System;
using System.Collections.Generic;

namespace NaiveBayesClassifier
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Record> TrainingSet = new List<Record>()
            {
                new Record("Conservative", true, true, false),
                new Record("Socialist", false, true, true),
                new Record("Classic Liberal", false, false, true)
            };

            var toBeClassifiedRecord = new Record("Unknown", true, false, true);
            
            NaiveBayesClassifier bayesClassifier = new NaiveBayesClassifier(TrainingSet);
            toBeClassifiedRecord.classification = bayesClassifier.ClassifyNewRecord(toBeClassifiedRecord).classification;
        }
    }
}
