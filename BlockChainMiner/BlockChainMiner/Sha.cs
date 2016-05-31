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
        public Sha(string value)
        {
            this.value = value;
        }

        #endregion


        [DataMember]
        private string value;
        public string Value {
            get { return this.value; }
        }
    }
}
