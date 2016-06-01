using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace BlockChainMiner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Miner : IMiner
    {
        #region Private Members

        private ISha Hash { get; set; }

        private IVerifier Verifier { get; set; }

        private IHasher hasher { get; set; }

        private Thread worker { get; set; }

        private IEnumerable<IMiner> links;
        private IEnumerable<IMiner> Links 
        {
            get { return this.links; } 
        }

        #endregion

        #region Constructor

        public Miner(IEnumerable<IMiner> links)
        {
            this.links = links;
        }

        #endregion

        public void BroadcastHash(ISha sha)
        {
            foreach (IMiner miner in this.Links)
            {
                miner.ReceiveHash(sha);
            }
        }

        private int work()
        {
            int nonce = 0;
            while (true)
            {
                ISha sha = hasher.Compute(++nonce);
                if (Verifier.SatifyCriterium(sha)) return nonce;
            }
        }

        public void ReceiveHash(ISha sha)
        {
            if (this.Hash.Value == sha.Value) return;
            worker.Abort();
            worker = new Thread(delegate(){
                this.work();
            });
            worker.Start();
            BroadcastHash(sha);
        }

    }
}
