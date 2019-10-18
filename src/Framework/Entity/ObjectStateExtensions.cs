using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Entity
{

    /// <summary>
    /// Class containing extension methods on the ObjectState enum
    /// </summary>
    public static class ObjectStateExtensions
    {

        /// <summary>
        /// Extension method to check if the object is considered active (that is, not deleted)
        /// </summary>
        /// <param name="objectState">An ObjectState enum value</param>
        /// <returns>True if the object is in NEW, UNCHANGED or MODIFIED status.  False otherwise</returns>
        public static bool IsActiveState(this ObjectState objectState)
        {
            if (objectState == ObjectState.NEW
                || objectState == ObjectState.UNCHANGED
                || objectState == ObjectState.MODIFIED)
            {
                return true;
            }

            return false;
        }

    }
}
