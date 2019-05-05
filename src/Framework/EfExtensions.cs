using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{

    /// <summary>
    /// Helper classes related to Entity Framework
    /// </summary>
    public static class EfExtensions
    {

        /// <summary>
        /// Converts the state of an object that self tracks changes to an the appropriate state for Entity Framework
        /// </summary>
        /// <remarks>
        /// This code is from the Julie Lerman article in MSDN Magazine at 
        /// https://msdn.microsoft.com/magazine/mt767693
        /// </remarks>
        /// <param name="node"></param>
        public static void ConvertStateOfNode(EntityEntryGraphNode node)
        {
            IObjectState entity = (IObjectState)node.Entry.Entity;
            node.Entry.State = ConvertToEFState(entity.ObjectState);
        }


        internal static EntityState ConvertToEFState(ObjectState objectState)
        {
            EntityState efState = EntityState.Unchanged;
            switch (objectState)
            {
                case ObjectState.NEW:
                    efState = EntityState.Added;
                    break;
                case ObjectState.MODIFIED:
                    efState = EntityState.Modified;
                    break;
                case ObjectState.DELETED:
                    efState = EntityState.Deleted;
                    break;
                case ObjectState.UNCHANGED:
                    efState = EntityState.Unchanged;
                    break;
            }
            return efState;
        }


    }
}
