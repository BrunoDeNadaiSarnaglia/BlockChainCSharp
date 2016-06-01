using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainMiner
{
    class EndZerosVerifier : IVerifier
    {
        private int NumZeros { get; set; }
        public EndZerosVerifier(int numZeros)
        {
            this.NumZeros = numZeros;
        }

        public bool SatifyCriterium(byte[] hash)
        {
            int count = 0;
            foreach (byte b in hash)
            {
                if (b == 0) count++;
            }
            return count >= this.NumZeros;
        }
    }
}
