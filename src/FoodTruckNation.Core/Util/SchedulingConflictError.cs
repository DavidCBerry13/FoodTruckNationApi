using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DavidBerry.Framework.Functional;
using FoodTruckNation.Core.Domain;

namespace FoodTruckNation.Core.Util
{
    public class SchedulingConflictError : BusinessRuleViolationError
    {
        public SchedulingConflictError(string message, IEnumerable<Schedule> conflictingSchedules) : base(message)
        {
            _conflictingSchedules = conflictingSchedules;
        }


        private readonly IEnumerable<Schedule> _conflictingSchedules;

        public List<Schedule> ConflictingSchedules
        {
            get { return _conflictingSchedules.ToList(); }
        }

    }
}
