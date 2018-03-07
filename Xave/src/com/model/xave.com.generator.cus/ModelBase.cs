using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
//using HIE.Framework.DataTransferObject;

namespace xave.com.generator.cus
{
    /// <summary>
    /// Model Base Class
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    public abstract class ModelBase : INotifyPropertyChanged // : HIEDTOBaseObject
    {
        /// <summary>
        /// Narrative Block Table
        /// </summary>
        public virtual string[] TableBodyArray { get; set; }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        #endregion
    }
}
