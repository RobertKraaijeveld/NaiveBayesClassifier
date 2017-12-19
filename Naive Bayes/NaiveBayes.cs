using System;
using System.Linq;
using System.Collections.Generic;

namespace NaiveBayesClassifier
{
    public class NaiveBayesClassifier
    {
        public List<Record> classifiedSet;
        private Table<string, double> FrequencyTable = new Table<string, double>();
        private Table<string, double> LikelihoodTable = new Table<string, double>();

        public NaiveBayesClassifier(List<Record> classifiedSet)
        {
            this.classifiedSet = classifiedSet;

            CreateFrequencyTable();
            CreateLikelihoodTable();
        }

        private void CreateFrequencyTable()
        {
            foreach (var record in classifiedSet)
            {
                /*
                //yay for containskey checks
                if (!FrequencyTable.rowsPerColumn.ContainsKey(record.classification))
                    FrequencyTable.rowsPerColumn.Add(record.classification, new Dictionary<string, double>());
                */

                
                foreach (var factor in record.Factors)
                {
                    if (!FrequencyTable[record.classification].ContainsKey(factor.Key))
                        FrequencyTable[record.classification].Add(factor.Key, 0);

                    if (factor.Value == true)
                    {
                        FrequencyTable[record.classification][factor.Key] += 1;
                    }
                }
            }

            foreach (var row in FrequencyTable["Conservative"])
                Console.WriteLine("Factor " + row.Key + " shows up " + row.Value + " times for conservatives");
        }

        private void CreateLikelihoodTable()
        {
            LikelihoodTable = FrequencyTable;
            foreach (var record in FrequencyTable.rowsPerColumn)
            {
                var classificationOfItem = record.Item1;
                Console.WriteLine("LIKELIHOOD: classificationOfItem = " + classificationOfItem);
                //The corresponding item in the likelihoodtable is the value / the amount with that same class

                foreach (var factor in record.Item2)
                {
                    LikelihoodTable[classificationOfItem][factor.Key] = factor.Value / 1;//GetCountsOfClassifications(LikelihoodTable);
                }
            }
        }

        private Dictionary<string, int> GetCountsOfClassifications(Table<string, double> table, string classification)
        {
            Dictionary<string, int> CountsForGivenClassification = new Dictionary<string, int>();

            //rowsPerColumn dictionary needs to be a list (?)
            //Nah, just make sure the AMOUNT of classifications of a certain kind is known somewhere
            foreach (var record in table.rowsPerColumn)
            {
                if (record.Item1 == classification)
                {
                    if (!CountsForGivenClassification.ContainsKey(record.Item1))
                        CountsForGivenClassification.Add(record.Item1, 1);
                    else
                        CountsForGivenClassification[record.Item1] += 1;
                }
            }
            return CountsForGivenClassification;
        }
    }
}
