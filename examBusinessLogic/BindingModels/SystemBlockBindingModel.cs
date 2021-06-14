using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace examBusinessLogic.BindingModels
{
    [DataContract]
    public class SystemBlockBindingModel
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public string Brand { get; set; }

        [DataMember]
        public string BlockType { get; set; }

        [DataMember]
        public DateTime DateCreate { get; set; }

        [DataMember]
        public DateTime? DateFrom { get; set; }

        [DataMember]
        public DateTime? DateTo { get; set; }
    }
}
