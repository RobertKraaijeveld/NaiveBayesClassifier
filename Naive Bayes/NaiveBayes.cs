using System;
using System.Linq;
using System.Collections.Generic;

namespace NaiveBayesClassifier
{
    public class NaiveBayesClassifier
    {
        public List<Record> classifiedSet;

        public NaiveBayesClassifier(List<Record> classifiedSet)
        {
            this.classifiedSet = classifiedSet;
        }


        /*
      
       - Calculate the sum of the probabilities of possesing F1..FN

        - Foreach possible classification C:
            - Calculate the prior probability of being C
            - Foreach factor Cf
                - Calculate 'being in class Cj , causes to have feature f with some probability'. IE: The probability of being tough on immigratans knowing that C is conservative

            - Finally, fill in the Bayes Theorem formula for this and return the value.

        - Order C1..CN by descending likelihood
        - Return the first element of that list
        */

        public Record ClassifyNewRecord(Record newRecord)
        {
            Dictionary<string, double> classificationLikelihoods = new Dictionary<string, double>();

            //the sum of the probability of having the same value for a given factor as the newrecord 
            var SummedFactorProbabilities = CalculateSummedFactorProbabilities(newRecord);

            foreach(var possibleClassification in classifiedSet) //todo make this so that it goes per UNIQUE classification only, not per row
            {
                //The numerator starts with the simple prior probability of having this classification
                var numerator = CalculatePriorOfRecord(possibleClassification);

                //the likelihood of having a (true) value for each factor when having this classification
                var likelihoodOfAllFactorsForThisClassification = CalculateLikelihoodOfAllFactors(possibleClassification);

                foreach (var factor in possibleClassification.Factors)
                {
                    numerator = numerator *  likelihoodOfAllFactorsForThisClassification[factor.Key];
                }
                var finalPosteriorLikelihoodOfHavingThisClassification = numerator / SummedFactorProbabilities;
                classificationLikelihoods.Add(possibleClassification.classification, finalPosteriorLikelihoodOfHavingThisClassification);
            }

            //getting most likely classification
            var mostLikely = classificationLikelihoods.OrderByDescending(kv => kv.Value).First();
            Console.WriteLine("The most likely classification  is " + mostLikely.Key + ". Likelihood is " + mostLikely.Value);

            newRecord.classification = mostLikely.Key;
            return newRecord;
        }

        public double CalculateSummedFactorProbabilities(Record newRecord)
        {
            double summedFactorProbabilities = 0;

            foreach(var factor in newRecord.Factors)
            {
                var factorName = factor.Key;
                var factorValue = factor.Value;

                summedFactorProbabilities += classifiedSet.Where(r => r.Factors[factorName].Equals(factorValue)).Count() / classifiedSet.Count();
            }
            return summedFactorProbabilities;
        }

        public double CalculatePriorOfRecord(Record record)
        {
            return (double) classifiedSet.Where(r => r.classification.Equals(record.classification)).Count() / classifiedSet.Count;
        }

        public Dictionary<string, double> CalculateLikelihoodOfAllFactors(Record record)
        {
            // IE: The probability of being tough on immigratans knowing that the record is conservative
            Dictionary<string, double> likelihoodPerFactorWhenHavingThisClassification = new Dictionary<string, double>();

            //Get all records of the same classification
            //Find out how likely you are per factor to have that factor be true
            foreach (var recordWithSameClassification in classifiedSet.Where(r => r.classification.Equals(record.classification)))
            {
                
            }


            //RETURN
        }
    }
}
