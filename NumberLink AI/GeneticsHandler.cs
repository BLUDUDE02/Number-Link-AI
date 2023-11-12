using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml.Linq;

namespace NumberLink_AI
{
    /// <summary>
    /// Class to store functionality for genetics and breeding
    /// </summary>
    public class GeneticsHandler
    {
        private int populationsize = 0;
        private int generations = 0;
        private int mutationRate = 0;
        private int fitness = 0;
        private Puzzle puzzle;
        private Random random = new Random();
        private Window Window = null;

        public GeneticsHandler(Window window)
        {
            this.Window = window;
            this.populationsize = window.population;
            this.generations = window.generations;
            this.mutationRate = window.mutationRate;
            this.fitness = window.fitness;
            this.puzzle = window.puzzle;
        }

        public async Task Start()
        {
            await Task.Run(() => { RunGenerations(GetInitialPopulation(puzzle)); });
        }

        public void runIndividual(Individual individual)
        {
            for (int j = 0; j < individual.Feelers.Count; j++)
            {
                individual.Feelers[j].FindEnd();
                int i = (individual.Feelers[j].Path.Count > 0 ? 1 : 0);
                if (j == 0 && i == 0)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR?");
                }
                individual.score += i;

                //Add Unique Blockers
                foreach (var f in individual.Feelers[j].Blockers.
                    Where(o => !individual.Blockers.Contains(o)).ToList())
                {
                    individual.Blockers.Add(f);
                }
            }
            //System.Diagnostics.Debug.WriteLine("Individual (" + individual.id + ") score = " + individual.score);
        }

        public void RunGenerations(List<Individual> population)
        {
            var clock = new Stopwatch();
            clock.Start();
            List<int> StagnationList = new List<int>();
            int successfulGenerations = -1;
            for (int i = 0; i < generations; i++)
            {
                population = BuildNewGeneration(population, i);
                foreach (Individual individual in population)
                {
                    Parallel.Invoke(() => { runIndividual(individual); });
                }
                population = Fitness(population);
                StagnationList.Add(population[0].score);

                int bestScore = StagnationList[StagnationList.Count - 1];
                int count = 0;
                for (int x = StagnationList.Count - 2; x >= 0; x--)
                {
                    if (bestScore == StagnationList[x])
                    {
                        count++;
                    }
                }
                if (count >= 5)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        population.Add(RandomIndividual(puzzle, i));
                    }
                }

                if (Window.useWOC)
                {
                    Individual WOCIndividual = WOC(population);
                    WOCIndividual.id = i + " -> WOC";
                    Parallel.Invoke(() => { runIndividual(WOCIndividual); });
                    if (WOCIndividual.score > population[0].score)
                    {
                        System.Diagnostics.Debug.WriteLine("WOC WON!");
                        Window.bestIndividual = WOCIndividual;
                    }
                }

                System.Diagnostics.Debug.WriteLine("GENERATION (" + i + ") AVERAGE SCORE: " + GetAverage(population));

                //If the best individual succeded
                if (population[0].score == puzzle.Pairs.Count)
                {
                    successfulGenerations = i;
                    break;
                }
            }
            if (successfulGenerations >= 0)
            {
                System.Diagnostics.Debug.WriteLine("PROBLEM SOLVED IN " + successfulGenerations + " GENERATIONS.");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("NO SOLUTION FOUND IN " + generations + " GENERATIONS.");
            }

            clock.Stop();
            System.Diagnostics.Debug.WriteLine($"Execution time: {clock.Elapsed}");
        }

        public float GetAverage(List<Individual> pop)
        {
            float avg = 0;
            foreach (Individual i in pop)
            {
                avg += i.score;
            }
            avg = avg / pop.Count();
            avg = avg / puzzle.Pairs.Count();
            avg *= 100;
            return avg;
        }

        public List<Individual> GetInitialPopulation(Puzzle puzzle)
        {
            List<Individual> population = new List<Individual>();
            for (int i = 0; i < populationsize; i++)
            {
                Individual individual = new Individual();

                List<Feeler> feelers = new List<Feeler>();
                int numfeelers = puzzle.Pairs.Count();

                for (int j = 0; j < numfeelers; j++)
                {
                    feelers.Add(new Feeler(puzzle, j));
                }

                foreach (Feeler f in feelers)
                {
                    f.feelers = feelers;
                }

                feelers = ShuffleList(feelers);

                individual.Feelers = feelers;
                individual.id = "0 -> " + i;

                population.Add(individual);
            }
            return population;
        }

        public Individual RandomIndividual(Puzzle puzzle, int Generation)
        {
            Individual individual = new Individual();

            List<Feeler> feelers = new List<Feeler>();
            int numfeelers = puzzle.Pairs.Count();

            for (int j = 0; j < numfeelers; j++)
            {
                feelers.Add(new Feeler(puzzle, j));
            }

            foreach (Feeler f in feelers)
            {
                f.feelers = feelers;
            }

            feelers = ShuffleList(feelers);

            individual.Feelers = feelers;
            individual.id = Generation + " -> x";
            return individual;
        }

        /// <summary>
        /// Sort By number of connected nodes, then by least amount of blocker nodes and return
        /// </summary>
        /// <param name="population"></param>
        /// <returns></returns>
        public List<Individual> Fitness(List<Individual> population)
        {
            List<Individual> retList = new List<Individual>();
            population = population.OrderByDescending(o => o.score).
                ThenBy(n => n.Blockers.Count()).ToList();

            for (int i = 0; i < fitness; i++)
            {
                retList.Add(population[i]);
            }

            Window.bestIndividual = retList[0];

            return retList;
        }

        /// <summary>
        /// Breed each in a population with every other node once.
        /// </summary>
        /// <param name="population"></param>
        /// <returns></returns>
        public List<Individual> BuildNewGeneration(List<Individual> population, int generation)
        {
            List<Individual> retList = new List<Individual>();

            for (int i = 0; i < population.Count(); i++)
            {
                for (int j = i + 1; j < population.Count(); j++)
                {
                    //Cross over
                    Individual individual = Crossover(population[i], population[j]);
                    //Handle Mutation
                    if (generation > 0)
                    {
                        if (random.Next(1, 100) <= mutationRate)
                        {
                            int rnd = random.Next(3);
                            switch (rnd)
                            {
                                case 0:
                                    individual = Mutate(individual);
                                    break;
                                case 1:
                                    individual = Mutate1(individual);
                                    break;
                                case 2:
                                    individual = Mutate2(individual);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    retList.Add(individual);
                }
            }

            for (int i = 0; i < retList.Count; i++)
            {
                retList[i].id = generation + " -> " + i;
            }

            return retList;
        }

        /// <summary>
        /// Perform an aggregation on two parents, determine 
        /// which nodes should come after eachother using in-list analysis.
        /// Get the index of each feeler and order the list based on those averages.
        /// In the case of duplicates, numeric superiority
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public Individual Crossover(Individual A, Individual B)
        {
            Individual child = new Individual();
            child.Feelers = new List<Feeler>();
            List<(float, int)> Objects = new List<(float, int)>();

            for (int i = 0; i < A.Feelers.Count(); i++)
            {
                float rank = (float)(A.Feelers.IndexOf(A.Feelers.Select(o => o).
                            Where(o => o.id == A.Feelers[i].id).ToList()[0]) +
                    B.Feelers.IndexOf(B.Feelers.Select(o => o).
                            Where(o => o.id == A.Feelers[i].id).ToList()[0])) / 2;
                Objects.Add((rank, A.Feelers[i].id));
            }
            Objects = Objects.OrderBy(o => o.Item1).ToList();
            List<int> ids = Objects.Select(o => o.Item2).Distinct().ToList();

            foreach (int i in ids)
            {
                child.Feelers.Add(new Feeler(puzzle, i));
            }

            foreach (Feeler f in child.Feelers)
            {
                f.feelers = child.Feelers;
            }

            child.parents.Add(A);
            child.parents.Add(B);

            if (child.Feelers.Count > puzzle.Pairs.Count)
            {
                System.Diagnostics.Debug.WriteLine("ERROR?");
            }

            return child;
        }

        /// <summary>
        /// In order crossover
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public Individual Crossover2(Individual A, Individual B)
        {
            Individual child = new Individual();
            child.Feelers = new List<Feeler>();
            for (int i = 0; i < (int)Math.Ceiling((double)A.Feelers.Count / 2); i++)
            {
                child.Feelers.Add(new Feeler(puzzle, A.Feelers[i].id));
            }
            foreach (Feeler f in B.Feelers)
            {
                if (!child.Feelers.Select(o => o.id).ToList().Contains(f.id))
                {
                    child.Feelers.Add(new Feeler(puzzle, f.id));
                }
            }

            foreach (Feeler f in child.Feelers)
            {
                f.feelers = child.Feelers;
            }

            child.parents.Add(A);
            child.parents.Add(B);

            return child;
        }

        public Individual WOC(List<Individual> population)
        {
            Individual child = new Individual();
            child.Feelers = new List<Feeler>();
            List<(float, int)> Objects = new List<(float, int)>();

            for (int i = 0; i < population[0].Feelers.Count; i++)
            {
                float rank = 0;
                foreach (Individual ind in population)
                {
                    rank += ind.Feelers.IndexOf(ind.Feelers.Select(o => o).
                            Where(o => o.id == population[0].Feelers[i].id).ToList()[0]);
                }
                Objects.Add((rank, population[0].Feelers[i].id));
            }
            Objects = Objects.OrderBy(o => o.Item1).ToList();
            List<int> ids = Objects.Select(o => o.Item2).Distinct().ToList();

            foreach (int i in ids)
            {
                child.Feelers.Add(new Feeler(puzzle, i));
            }

            foreach (Feeler f in child.Feelers)
            {
                f.feelers = child.Feelers;
            }

            if (child.Feelers.Count > puzzle.Pairs.Count)
            {
                System.Diagnostics.Debug.WriteLine("ERROR?");
            }

            return child;
        }

        /// <summary>
        /// Unintelligent Mutation - randomly reinsert 50% of nodes
        /// </summary>
        /// <param name="individual"></param>
        /// <returns></returns>
        public Individual Mutate(Individual child)
        {
            Individual individual = child;
            int quantity = (int)Math.Ceiling(0.5 * individual.Feelers.Count);
            for (int x = 0; x < quantity; x++)
            {
                Feeler f = individual.Feelers[random.Next(individual.Feelers.Count)];
                individual.Feelers.Remove(f);
                int i = random.Next(individual.Feelers.Count);
                individual.Feelers.Insert(i, f);
            }
            if (individual.Feelers.Count > puzzle.Pairs.Count)
            {
                System.Diagnostics.Debug.WriteLine("ERROR?");
            }
            return individual;
        }

        /// <summary>
        /// Intelligent Mutation. Add the parent's blockers, move the most common blockee up past the blockers
        /// </summary>
        /// <param name="individual"></param>
        /// <returns></returns>
        public Individual Mutate1(Individual child)
        {
            Individual individual = child;
            List<(int, int)> Blockers = new List<(int, int)>();
            foreach (Individual parent in individual.parents)
            {
                foreach ((int, int) Item in parent.Blockers)
                {
                    Blockers.Add(Item);
                }
            }

            if (Blockers.Count > 0)
            {
                List<int> f = Blockers.Select(o => o.Item1).ToList();
                int weakestLink = f.GroupBy(value => value)
                .OrderByDescending(group => group.Count())
                .Select(group => group.Key)
                .First();

                List<int> NodesToPush = Blockers.Select(o => o).Where(o => o.Item1 == weakestLink).Select(o => o.Item1).ToList();

                //insert weakest link at first instance of blocker nodes
                for (int i = 0; i < individual.Feelers.Count; i++)
                {
                    if (NodesToPush.Contains(individual.Feelers[i].id))
                    {
                        individual.Feelers.Remove(individual.Feelers.Select(o => o).
                            Where(o => o.id == weakestLink).ToList()[0]);
                        individual.Feelers.Insert(i, new Feeler(puzzle, weakestLink));
                        break;
                    }
                }
            }

            foreach (Feeler f in child.Feelers)
            {
                f.feelers = child.Feelers;
            }

            if (individual.Feelers.Count > puzzle.Pairs.Count)
            {
                System.Diagnostics.Debug.WriteLine("ERROR?");
            }
            return individual;
        }

        /// <summary>
        /// Intelligent Mutation. Move a blocked node to the top.
        /// </summary>
        /// <param name="individual"></param>
        /// <returns></returns>
        public Individual Mutate2(Individual child)
        {
            List<int> checkedFeelers = new List<int>();
            Individual individual = child;
            foreach (Individual parent in individual.parents)
            {
                foreach (Feeler f in parent.Feelers)
                {
                    if (f.Path.Count == 0 && !checkedFeelers.Contains(f.id))
                    {
                        individual.Feelers.Remove(individual.Feelers.Select(o => o).
                            Where(o => o.id == f.id).ToList()[0]);
                        individual.Feelers.Insert(0, new Feeler(puzzle, f.id));
                        checkedFeelers.Add(f.id);
                        break;
                    }
                }
            }
            foreach (Feeler f in child.Feelers)
            {
                f.feelers = child.Feelers;
            }

            if (individual.Feelers.Count > puzzle.Pairs.Count)
            {
                System.Diagnostics.Debug.WriteLine("ERROR?");
            }

            return individual;
        }

        private List<Feeler> ShuffleList(List<Feeler> list)
        {
            Random rnd = new Random();
            List<Feeler> retList = new List<Feeler>();
            while (list.Count > 0)
            {
                int index = rnd.Next(0, list.Count());
                Feeler F = list[index];
                list.RemoveAt(index);
                retList.Add(F);
            }
            return retList;
        }

    }
}
