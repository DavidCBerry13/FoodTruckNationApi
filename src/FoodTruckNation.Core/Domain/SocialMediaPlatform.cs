using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.Domain
{

    /// <summary>
    /// Represents a Social Media platform like Facebook or Twitter
    /// </summary>
    public class SocialMediaPlatform : BaseEntity
    {

        public SocialMediaPlatform() : base(ObjectState.UNCHANGED)
        {

        }


        public SocialMediaPlatform(String name, String urlTemplate) : base(ObjectState.NEW)
        {
            _name = name;
            _urlTemplate = urlTemplate;
        }


        internal SocialMediaPlatform(int platformId, String name, String urlTemplate) : base(ObjectState.UNCHANGED)
        {
            _platformId = platformId;
            _name = name;
            _urlTemplate = urlTemplate;
        }

        private int _platformId;
        private String _name;
        private String _urlTemplate;


        public int PlatformId
        {
            get { return _platformId; }
            private set { _platformId = value; }
        }


        public String Name
        {
            get { return _name; }
            private set { _name = value; }
        }


        public String UrlTemplate
        {
            get { return _urlTemplate; }
            private set { _urlTemplate = value; }
        }

    }
}
