using System;
using System.Linq;
using System.Collections.Generic;

namespace NaiveBayesClassifier
{
    public class NaiveBayesClassifier
    {
        public List<Record> classifiedSet;
        private Table<string, int> FrequencyTable = new Table<string, int>();
        private Table<string, float> LikelihoodTable;

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
                //yay for containskey checks
                if (!FrequencyTable.rowsPerColumn.ContainsKey(record.classification))
                    FrequencyTable.rowsPerColumn.Add(record.classification, new Dictionary<string, int>());

                foreach (var factor in record.Factors)
                {
                    if (!FrequencyTable.rowsPerColumn[record.classification].ContainsKey(factor.Key))
                        FrequencyTable.rowsPerColumn[record.classification].Add(factor.Key, 0);

                    if (factor.Value == true)
                    {
                        FrequencyTable.rowsPerColumn[record.classification][factor.Key] += 1;
                    }
                }
            }

            foreach (var row in FrequencyTable.rowsPerColumn["Conservative"])
            {
                Console.WriteLine("Factor " + row.Key + " shows up " + row.Value + " times for conservatives");
            }
        }

        private void CreateLikelihoodTable()
        {

        }
    }
}
