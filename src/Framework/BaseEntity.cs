using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Framework
{
    /// <summary>
    /// Serves as a base object for all entity objects.  Specifically, it contains a property to trck
    /// object state and some helper methods to manage object state
    /// </summary>
    public class BaseEntity : IObjectState
    {

        public BaseEntity(ObjectState initialState)
        {
            this.ObjectState = initialState;
        }


        public ObjectState ObjectState { get; private set; }


        protected internal void SetObjectModified()
        {
            if (this.ObjectState == ObjectState.UNCHANGED)
                this.ObjectState = ObjectState.MODIFIED;           
        }

        protected internal void SetObjectDeleted()
        {
            this.ObjectState = ObjectState.DELETED;
        }
    }
}
