using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DavidBerry.Framework.Domain;

namespace FoodTruckNation.Core.Domain
{
    public class Locality : BaseEntity
    {

        /// <summary>
        /// Parameterless constructor for Entity Framework to use
        /// </summary>
        internal Locality() : base(ObjectState.UNCHANGED)
        {

        }


        private Locality(string code, string name, ObjectState objectState)
            : base(objectState)
        {
            _localityCode = code;
            _localityName = name;

        }


        private string _localityCode;
        private string _localityName;


        #region Validation Constants

        public const string CODE_VALIDATION = @"^[A-Z]{2,5}$";


        public const string NAME_VALIDATION = @"^\w[\w ?,!\.'\-\(\)]{1,29}$";

        #endregion

        [Required]
        public string LocalityCode
        {
            get { return _localityCode;  }
            set
            {
                _localityCode = value;
                SetObjectModified();
            }
        }



        [Required]
        public string Name
        {
            get { return _localityName; }
            set
            {
                _localityName = value;
                SetObjectModified();
            }
        }


        #region Static Creator Methods

        /// <summary>
        /// Creates a new Locality object
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Locality CreateNewLocality(string code, string name)
        {
            return new Locality(code, name, ObjectState.NEW);
        }


        internal static Locality CreateExistingLocality(string code, string name)
        {
            return new Locality(code, name, ObjectState.UNCHANGED);
        }

        #endregion
    }
}
