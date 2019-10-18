using Framework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FoodTruckNation.Core.Domain
{

    /// <summary>
    /// Represents a tag (short descriptive phrase) that can be attached to a food truck to help describe the food truck
    /// </summary>
    public class Tag : BaseEntity
    {
        /// <summary>
        /// Constructor for use by Entity Framework (since EF requires you to have a parameterless constructor
        /// </summary>
        /// <remarks>
        /// As these objects are being created by EF, we know that they must exist and their
        /// state should be set to UNCHAGED as they are just getting loaded out of the database
        /// </remarks>
        private Tag() : base(ObjectState.UNCHANGED)
        {

        }


        /// <summary>
        /// Constructor used when creating a Tag object that already exists
        /// </summary>
        /// <param name="id">An int of the tag id number</param>
        /// <param name="text">A String of the tag text</param>
        internal Tag (int id, string text) : base(ObjectState.UNCHANGED)
        {
            _tagId = id;
            _tagText = text;
        }


        public Tag(string text) : base(ObjectState.NEW)
        {
            _tagText = text;
        }


        #region Constants

        public const int TAG_TEXT_MIN_LENGTH = 3;

        public const int TAG_TEXT_MAX_LENGTH = 30;

        public const string TAG_TEXT_REGEX = "^[A-Za-z][\\w -]{2,29}$";

        #endregion


        private int _tagId;
        private string _tagText;

        /// <summary>
        /// Gets the unique id number of this tag
        /// </summary>
        public int TagId
        {
            get { return _tagId; }
            private set { _tagId = value; }
        }


        /// <summary>
        /// Gets the text of this tag (e.g. 'Chinese' or 'Pizza')
        /// </summary>
        [Required]
        [MinLength(TAG_TEXT_MIN_LENGTH)]
        [MaxLength(TAG_TEXT_MAX_LENGTH)]
        [RegularExpression(TAG_TEXT_REGEX)]
        public string Text
        {
            get { return _tagText; }
            set
            {
                _tagText = value;
                SetObjectModified();
            }
        }


        public override bool Equals(object obj)
        {
            Tag other = obj as Tag;

            if (other == null)
                return false;

            return _tagText == other._tagText;
        }


        public override int GetHashCode()
        {
            return _tagText.GetHashCode();
        }

    }
}
