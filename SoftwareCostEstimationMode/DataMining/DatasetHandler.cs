using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareCostEstimationMode.DataMining
{
    public class DatasetHandler
    {
        private static DatasetHandler Instance;
        private DataSetModel _DataSetModel;
        private DatasetHandler() { _DataSetModel = new DataSetModel(); }
        public static DatasetHandler GetDataSetHandlerInstance()
        {
             if (Instance == null)
            {
                Instance = new DatasetHandler();
            }
            else
            {

            }
             return Instance;
        }
        public DataSetModel GetDatasetHandler()
        {
            return this._DataSetModel;  
        }
    }
}
