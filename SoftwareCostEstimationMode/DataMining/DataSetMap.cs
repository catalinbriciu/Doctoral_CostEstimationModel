using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareCostEstimationMode.Utility.IO;

namespace SoftwareCostEstimationMode.DataMining
{
    public class DataSetMap
    {
        IO_Operation handler;
        public  DataSetMap()
        {
            handler = new IO_Operation();
        }
        public void ReadFile(string filepath)
        {
            handler.SetFileToBeRead(filepath);
            bool _IsReadFinished = false;
            bool _IsAttr = true;
            do
            {
                string line = handler.ReadLine();
                _IsReadFinished = handler.IsInputFileEnd();
                if (_IsReadFinished == true)
                    break;
                /*in this point all attributes should be set*/
                if((_IsReadFinished != true) && line.Contains('%'))
                {
                     if(line.Contains("Attributes"))
                     {
                            _IsAttr = true;
                     }
                     else
                     {
                         if(line.Contains("Datasets")){_IsAttr = false;}
                     }
                }
                else
                {
                    if(_IsAttr)
                    {
                        /*we read an attribute*/
                        List<string> _listOfAttributes = new List<string>();
                        string[] splitText = line.Split(';');
                        for(int count = 0; count < splitText.Length; count++)
                        {
                            _listOfAttributes.Add(splitText[count]);
                        }
                        DatasetHandler.GetDataSetHandlerInstance().GetDatasetHandler().AddAttribute(_listOfAttributes);
                    }
                    else{
                        /*we read a record*/
                        List<string> _ListOfRecords = new List<string>();
                        string[] splitText = line.Split(',');
                        for (int count = 0; count < splitText.Length; count++)
                        {
                            _ListOfRecords.Add(splitText[count]);
                        }
                        DatasetHandler.GetDataSetHandlerInstance().GetDatasetHandler().AddRecord(_ListOfRecords);
                    }
                }
            } while (_IsReadFinished == false);
        }
    }
}
