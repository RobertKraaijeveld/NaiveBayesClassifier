using System;
using System.Collections.Generic;

namespace NaiveBayesClassifier
{
    public class Record
    {
        public string classification;

        public Dictionary<string, bool> predictors = new Dictionary<string, bool>();

        //Names/types are just for demonstrating purposes ese
        public Record(string classification, bool HateImmigrants, bool HighTaxes, bool FreedomEmphasis)
        {
            this.classification = classification;

            predictors.Add("HateImmigrants", HateImmigrants);
            predictors.Add("HighTaxes", HighTaxes);
            predictors.Add("FreedomEmphasis", FreedomEmphasis);
        }
    }
}
