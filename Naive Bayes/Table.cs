using System;
using System.Linq;
using System.Collections.Generic;

namespace NaiveBayesClassifier
{
    public class Table<T, J>
    {
        //Key is classification name, tablenetry key is factors name, tableentry value is factors count for this classification. 
        //This is a list because we need to be able to store duplicate classificationss
        public List<Tuple<T, Dictionary<T, J>>> rowsPerColumn { get; set; }


        //Overloading square bracket operator 
        public Dictionary<T, J> this[string i]
        {
            get { return rowsPerColumn.Where(r => r.Item1.Equals(i)).First().Item2; }
        }

        public Table()
        {
            rowsPerColumn = new List<Tuple<T, Dictionary<T, J>>>();
        }
    }
}
