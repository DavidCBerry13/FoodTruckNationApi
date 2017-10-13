using Framework;
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
        internal Tag (int id, String text) : base(ObjectState.UNCHANGED)
        {
            this.tagId = id;
            this.tagText = text;
        }


        public Tag(String text) : base(ObjectState.NEW)
        {
            this.tagText = text;
        }


        #region Constants

        public const int TAG_TEXT_MIN_LENGTH = 3;

        public const int TAG_TEXT_MAX_LENGTH = 30;

        public const String TAG_TEXT_REGEX = "^[A-Za-z][\\w -]{2,29}$";

        #endregion


        private int tagId;
        private String tagText;

        /// <summary>
        /// Gets the unique id number of this tag
        /// </summary>
        public int TagId
        {
            get { return this.tagId; }
            private set { this.tagId = value; }
        }


        /// <summary>
        /// Gets the text of this tag (e.g. 'Chinese' or 'Pizza')
        /// </summary>
        [Required]
        [MinLength(TAG_TEXT_MIN_LENGTH)]
        [MaxLength(TAG_TEXT_MAX_LENGTH)]
        [RegularExpression(TAG_TEXT_REGEX)]
        public String Text
        {
            get { return this.tagText; }
            set
            {
                this.tagText = value;
                this.SetObjectModified();
            }
        }

    }
}
