using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAF;
using GAF.Operators;

namespace SoftwareCostEstimationMode.Genetic_Programming
{
    interface IGeneticAlgoOperators
    {
        double EvaluateFitness(Chromosome _chromosome);
        void OnTerminateAlgorithmEvent(object sender, GaEventArgs e);
        void OnGenerationCompleteEvent(object sender, GaEventArgs e);
        void OnRunExceptionHandlerEvent(object sender, GaExceptionArgs e);
        bool TerminateFunctionEvaluation(Population population, int currentGeneration, long currentEvaluation);
    }
}
