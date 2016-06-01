using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainMiner
{
    [DataContract]
    public class Sha : ISha
    {
        #region Constructor
        public Sha(byte[] value)
        {
            this.value = value;
        }

        #endregion


        [DataMember]
        private byte[] value;
        public byte[] Value
        {
            get { return this.value; }
        }
    }
}
