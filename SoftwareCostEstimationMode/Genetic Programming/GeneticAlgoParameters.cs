using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAF;
using GAF.Operators;

namespace SoftwareCostEstimationMode.Genetic_Programming
{
    class GeneticAlgoParameters
    {
        private static GeneticAlgoParameters instance;
        private static IGeneticAlgoOperators IGeneticOperatorObject;
        private static double CrossoverProbability = 0.65;
        private static double MutationProbability = 0.08;
        private static int ElitismPercentage = 5;
        private static int PopulationSize = 0;
        private static bool ReEvaluateAllChilds = true;
        private static CrossoverType _crossoverType = CrossoverType.SinglePoint;
        private static int ChromosomeLength = 0;
        private static bool ApplyLinearNormalisationOnFitness = true;
        private GeneticAlgoParameters() { }
        public static GeneticAlgoParameters Instance
        {
            get {
                if (instance == null)
                {
                    instance = new GeneticAlgoParameters();
                }
                return instance;
            }
        }
        public static double _CrossoverProbability
        {
            get { return CrossoverProbability; }
            set
            {
                if (value < 1)
                {
                    CrossoverProbability = value;
                }
                else
                {
                    //do nothing
                }
            }
        }
        public static double _MutationProbability
        {
            get { return MutationProbability; }
            set {
                if (value < 1)
                {
                    MutationProbability = value;
                }
                else
                {
                    //do nothing
                }
                  }
        }
        public static int _ElitismPercentage
        {
            get { return ElitismPercentage; }
            set
            {
                    ElitismPercentage = value;
            }
        }
        public static int _PopulationSize
        {
            get { return PopulationSize; }
            set { PopulationSize = value; }
        }
        public static int _ChromosomeLength
        {
            get { return _ChromosomeLength; }
            set { ChromosomeLength = value; }
        }
        private static  GeneticAlgorithm _ga;
        public static CrossoverType _CrossOverType
        {
            get { return _crossoverType; }
            set {
                  _crossoverType = value;
            }
        }
        public static IGeneticAlgoOperators _IGeneticOperatorObject
        {
            //get { return IGeneticOperatorObject; }
            set {
                    IGeneticOperatorObject = value;
            }
        }
        public static void CreateGeneticAlgorithm()
        {
            if (instance != null)
            {
                var population = new Population(_PopulationSize, _ChromosomeLength, ReEvaluateAllChilds, ApplyLinearNormalisationOnFitness);
                var elite = new Elite(_ElitismPercentage);
                var crossover = new Crossover(_CrossoverProbability, true,_CrossOverType);
                var mutation = new BinaryMutate(_MutationProbability,true);
                _ga = new GeneticAlgorithm(population,IGeneticOperatorObject.EvaluateFitness);
                _ga.OnGenerationComplete+=IGeneticOperatorObject.OnGenerationCompleteEvent;
                _ga.OnRunComplete += IGeneticOperatorObject.OnTerminateAlgorithmEvent;
                _ga.OnRunException+= IGeneticOperatorObject.OnRunExceptionHandlerEvent;
                _ga.Operators.Add(elite);
                _ga.Operators.Add(crossover);
                _ga.Operators.Add(mutation);
            }
            else
            {
                //do nothing
            }
        }
        public static void RunAlgorithm()
        {
            _ga.RunAsync(IGeneticOperatorObject.TerminateFunctionEvaluation);
        }
    }
}
