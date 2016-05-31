using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BlockChainMiner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Miner : IMiner
    {
        #region Private Members

        private ISha hash;
        private ISha Hash
        {
            get { return this.hash; }
            set { this.hash = value; }
        }

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

        public void ReceiveHash(ISha sha)
        {
            if (this.Hash.Value == sha.Value) return;
            BroadcastHash(sha);
        }

    }
}
