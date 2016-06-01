using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainMiner
{
    class Hasher : IHasher
    {
        public Hasher(byte[] data)
        {
            Data = (byte[])data.Clone();
            nonceOffset = Data.Length - 4;
            hasher = new SHA256Managed();
        }
        
        private SHA256Managed hasher;
        private long nonceOffset;
        public byte[] Data { get; set; }
        
        internal bool FindShare(ref uint nonce, uint batchSize)
        {
            for(;batchSize > 0; batchSize--)
            {
                
                //count trailing bytes that are zero
                int zeroBytes = 0;
                for (int i = 31; i >= 28; i--, zeroBytes++)
                    if(doubleHash[i] > 0)
                        break;

                //standard share difficulty matched! (target:ffffffffffffffffffffffffffffffffffffffffffffffffffffffff00000000)
                if(zeroBytes == 4)
                    return true;

                //increase
                if(++nonce == uint.MaxValue)
                    nonce = 0;
            }
            return false;
        }

        private byte[] Sha256(byte[] input)
        {
            byte[] crypto = hasher.ComputeHash(input);
            return crypto;
        }

        public byte[] Hash
        {
            get { return Sha256(Sha256(Data)); }
        }

        public ISha Compute(int nonce)
        {
            BitConverter.GetBytes(nonce).CopyTo(Data, nonceOffset);
            byte[] doubleHash = Sha256(Sha256(Data));
            return new Sha(doubleHash);
        }
    }
}
