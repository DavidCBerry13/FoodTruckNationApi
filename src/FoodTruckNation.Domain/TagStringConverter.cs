using FoodTruckNation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodTruckNation.Domain
{

    /// <summary>
    /// A Worker class that converts raw strings into Tag objects
    /// </summary>
    public class TagStringConverter
    {


        /// <summary>
        /// Takes the list of tag strings and converts them into Tag objects.  
        /// </summary>
        /// <remarks>
        /// This method helps for the situation where you have an incoming list of tag strings that you need
        /// to make into a list of tag objects.  Some of the strings may already have corresponding tag objects
        /// and some strings may represent completely new tags.  The idea is to match the 
        /// </remarks>
        /// <param name="tagStrings"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static List<Tag> ConvertStringsToTags(IEnumerable<String> tagStrings, IEnumerable<Tag> tags)
        {
            List<Tag> tagObjects = new List<Tag>();

            foreach (var tagString in tagStrings)
            {
                var tagObject = tags.Where(t => t.Text == tagString).SingleOrDefault();
                if (tagObject != null)
                {
                    // Tag already exists
                    tagObjects.Add(tagObject);
                }
                else
                {
                    // This is a completely new tag
                    Tag newTag = new Tag(tagString);
                    tagObjects.Add(newTag);
                }
            }
            return tagObjects;
        }


    }
}
