using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.IDPDrv
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Message
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }

        private List<Field> fieldsField;

        private string nameField;

        private int sINField;

        private int mINField;

        private string isForwardField;

        /// <remarks/>
        public virtual List<Field> Fields
        {
            get
            {
                return this.fieldsField;
            }
            set
            {
                this.fieldsField = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
 
        /// <summary>
        /// 
        /// </summary>
        public int SIN
        {
            get
            {
                return this.sINField;
            }
            set
            {
                this.sINField = value;
            }
        }
         /// <summary>
         /// 
         /// </summary>
        public int MIN
        {
            get
            {
                return this.mINField;
            }
            set
            {
                this.mINField = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsForward
        {
            get
            {
                return this.isForwardField;
            }
            set
            {
                this.isForwardField = value;
            }
        }
    }
}
