using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareCostEstimationMode.DataMining
{
    public class DataSetModel
    {
        List<AttributeAbstraction> DataSetAttributesList;
        List<Dictionary<string, float>> DataSetValue;
        public void AddAttribute(List<string> attributeValues)
        {
            if (this.DataSetAttributesList == null)
            {
                DataSetAttributesList = new List<AttributeAbstraction>();
            }
            if (attributeValues.Count >= AttributeAbstraction.NbOfMinimumAttributes)
            {
                AttributeAbstraction _attAbstr = new AttributeAbstraction();
                /*this is okay, number of attributes are ok*/
                _attAbstr.SetAttribute_FullName(attributeValues[0]);
                _attAbstr.SetAttribute_Hint(attributeValues[1]);
                _attAbstr.SetAttribute_ShortName(attributeValues[2]);
                if (attributeValues.Count > AttributeAbstraction.NbOfMinimumAttributes)
                {
                    /*in this point we have at least an explanation*/
                    for (int count = AttributeAbstraction.NbOfMinimumAttributes; count < attributeValues.Count; count += 2)
                    {
                        _attAbstr.AddExplanation(attributeValues[count],Convert.ToSingle(attributeValues[count+1]));
                    }
                }
                DataSetAttributesList.Add(_attAbstr);
            }
            else
            {
                /*do nothing; record not completed*/
            }
        }
        public void AddRecord(List<string> _recordList)
        {
            if (DataSetValue == null)
            {
                DataSetValue = new List<Dictionary<string, float>>();
            }
            if (_recordList.Count != DataSetAttributesList.Count)
            {
                /*nothing to be done in this case, record not ok*/
                /*display the error in the error log*/
            }
            else
            {
                int count = 0;
                Dictionary<string, float> _newDict = new Dictionary<string, float>();
                foreach(string _CurrentRecord in _recordList)
                {
                    _newDict.Add(DataSetAttributesList[count].GetAttribute_ShortName(),Convert.ToSingle(_CurrentRecord));
                    count++;
                }
                DataSetValue.Add(_newDict);
            }
        }
    }
}
