using System;
using System.Xml.Serialization;
using BlauwtipjeApp.Core.Examples.Helpers;
using BlauwtipjeApp.Core.Examples.Interfaces;

namespace BlauwtipjeApp.Core.Examples.Models
{
    public class BaseDataObject : ObservableObject, IBaseDataObject
    {
        public BaseDataObject()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Id for item
        /// </summary>
        [XmlIgnore]
        public string Id { get; set; }

        /// <summary>
        /// Azure created at time stamp
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Azure UpdateAt timestamp for online/offline sync
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Azure version for online/offline sync
        /// </summary>
        [XmlIgnore]
        public string AzureVersion { get; set; }
    }
}
