using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainMiner
{
    interface IHasher
    {
        ISha Compute(int nonce);
    }
}
