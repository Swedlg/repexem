using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace examBusinessLogic.BindingModels
{
    [DataContract]
    public class ComponentBindingModel
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public double Price { get; set; }

        [DataMember]
        public DateTime DateCreate { get; set; }

        [DataMember]
        public string Firm { get; set; }

        [DataMember]
        public int? SystemBlockId { get; set; }

        [DataMember]
        public DateTime? DateFrom { get; set; }

        [DataMember]
        public DateTime? DateTo { get; set; }
    }
}
