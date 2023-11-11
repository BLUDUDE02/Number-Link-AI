using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLink_AI
{
    /// <summary>
    /// Class defining an individual set of genomes to be implemented on a puzzle
    /// </summary>
    public class Individual
    {
        /// <summary>
        /// Feeler 1 = Blocker Feeler, Feeler 2 = Blocked Feeler.
        /// </summary>
        public List<(int, int)> Blockers = new List<(int, int)>();
        public List<Feeler> Feelers = new List<Feeler>();
        public List<Individual> parents = new List<Individual>();
        public int score = 0;
        public string id;
    }
}
