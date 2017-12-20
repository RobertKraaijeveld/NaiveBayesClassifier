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
                new Record("Conservative", false, false, true),
                new Record("Conservative", true, true, false),
                new Record("Conservative", true, false, false),

                new Record("Socialist", false, true, false),
                new Record("Socialist", true, true, true),

                new Record("Libertarian", true, true, true),
            };

            var toBeClassifiedRecord = new Record("Unknown", true, true, true);

            NaiveBayesClassifier bayesClassifier = new NaiveBayesClassifier(TrainingSet);
            toBeClassifiedRecord.classification = bayesClassifier.GetClassification(toBeClassifiedRecord);
        }
    }
}
