using System;
using System.Collections.Generic;

namespace NaiveBayesClassifier
{
    public class Record
    {
        public string classification;

        public Dictionary<string, bool> Factors = new Dictionary<string, bool>();

        //Names/types are just for demonstrating purposes ese
        public Record(string classification, bool HateImmigrants, bool HighTaxes, bool FreedomEmphasis)
        {
            this.classification = classification;


            Factors.Add("HateImmigrants", HateImmigrants);
            Factors.Add("HighTaxes", HighTaxes);
            Factors.Add("FreedomEmphasis", FreedomEmphasis);
        }
    }
}
