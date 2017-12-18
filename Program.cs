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
                new Record("Conservative", true, false, false),
            };

            var toBeClassifiedRecord = new Record("Unknown", true, true, false);

            NaiveBayesClassifier bayesClassifier = new NaiveBayesClassifier(TrainingSet);
        }
    }
}
