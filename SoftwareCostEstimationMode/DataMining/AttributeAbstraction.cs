using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareCostEstimationMode.DataMining
{
    public class AttributeAbstraction
    {
        public static int NbOfMinimumAttributes = 3; 
        private string Attribute_FullName;
        private string Hint;
        private string Attribute_ShortName;
        private Dictionary<string, float> Explanation;
        public string GetAttribute_FullName() {return this.Attribute_FullName;}
        public void SetAttribute_FullName(string FullName) {this.Attribute_FullName = FullName;}
        public string GetAttribute_Hint() {return this.Hint;}
        public void SetAttribute_Hint(string Hint) { this.Hint = Hint; }
        public string GetAttribute_ShortName() { return this.Attribute_ShortName; }
        public void SetAttribute_ShortName(string ShortName) { this.Attribute_ShortName = ShortName; }
        public Dictionary<string, float> GetListOfExplanations() { return this.Explanation; }
        public void AddExplanation(string name, float value)
        {
            if (this.Explanation == null)
            {
                this.Explanation = new Dictionary<string,float>();
            }
            this.Explanation.Add(name, value);
        }
        public float GetValueOfExplanation(string name)
        {
            return this.Explanation[name];
        }
    }
}
