using System;
using System.Linq;
using System.Collections.Generic;

namespace NaiveBayesClassifier
{
    public class NaiveBayesClassifier
    {
        public List<Record> classifiedSet;
        //add Dict of counts
        private Dictionary<string, int> classificationCounts = new Dictionary<string, int>();
        private Table<string, double> FrequencyTable = new Table<string, double>();
        private Table<string, double> LikelihoodTable = new Table<string, double>();

        public NaiveBayesClassifier(List<Record> classifiedSet)
        {
            this.classifiedSet = classifiedSet;

            CreateFrequencyTableAndCountClassifications();
            CreateLikelihoodTable();
        }

        public string GetClassification(Record recordToBeClassified)
        {
            var likelihoodsPerClassification = new Dictionary<string, double>();

            foreach (var possibleClassification in FrequencyTable.rowsPerColumn.Keys)
            {
                var thisClassificationsProbability = GetProbabilityOfGivenClassification(possibleClassification); 
                likelihoodsPerClassification.Add(possibleClassification, thisClassificationsProbability);
            }

            var sortedLikelihoodsPerClassification = likelihoodsPerClassification.OrderByDescending(kv => kv.Value).ToList();
            var mostLikelyClassification = sortedLikelihoodsPerClassification.First();

            Console.WriteLine("The most likely classification is " + mostLikelyClassification.Key + ", value is " + mostLikelyClassification.Value);
            return mostLikelyClassification.Key; 
        }

        private double GetProbabilityOfGivenClassification(string classification)
        {
            var priorClassProbability = GetClassPriorProbability(classification);    
            double finalProbability = priorClassProbability;

            foreach(var predictorLikelihood in LikelihoodTable.rowsPerColumn[classification])
            {
                finalProbability = finalProbability * predictorLikelihood.Value;
            }
            return finalProbability;
        }


        /**
        Table creation
        **/

        private void CreateFrequencyTableAndCountClassifications()
        {
            foreach (var record in classifiedSet)
            {
                //populating count dict
                if(!classificationCounts.ContainsKey(record.classification))
                    classificationCounts.Add(record.classification, 1);
                else
                    classificationCounts[record.classification] += 1;

                //populating frequency table
                FrequencyTable.PotentiallyAddClassification(record.classification);

                foreach (var predictor in record.predictors)
                {
                    if (!FrequencyTable.rowsPerColumn[record.classification].ContainsKey(predictor.Key))
                        FrequencyTable.rowsPerColumn[record.classification].Add(predictor.Key, 0);

                    if (predictor.Value == true)
                    {
                        FrequencyTable.rowsPerColumn[record.classification][predictor.Key] += 1;
                    }
                }
            }
        }

        private void CreateLikelihoodTable()
        {
            foreach (var record in FrequencyTable.rowsPerColumn)
            {
                var classificationOfItem = record.Key;
                LikelihoodTable.PotentiallyAddClassification(classificationOfItem);

                foreach (var predictor in record.Value)
                {
                    var likelihoodOfThisPredictor = predictor.Value / GetCountsOfClassification(FrequencyTable, classificationOfItem);

                    if (!LikelihoodTable.rowsPerColumn[classificationOfItem].ContainsKey(predictor.Key))
                        LikelihoodTable.rowsPerColumn[classificationOfItem].Add(predictor.Key, likelihoodOfThisPredictor);
                    else
                        LikelihoodTable.rowsPerColumn[classificationOfItem][predictor.Key] = likelihoodOfThisPredictor;
                }
            }
        }

        private int GetCountsOfClassification(Table<string, double> table, string classification)
        {
            return classificationCounts[classification];
        }


        /**
        Formula computation
        **/
        private double GetClassPriorProbability(string classification)
        {
            return (double) classificationCounts[classification] / classifiedSet.Count();
        }      
    }
}
