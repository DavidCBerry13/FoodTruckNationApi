using Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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


        public SocialMediaPlatform(String name, String urlTemplate, String accountNameRegex) : base(ObjectState.NEW)
        {
            _name = name;
            _urlTemplate = urlTemplate;
            _accountNameRegex = accountNameRegex;
        }


        internal SocialMediaPlatform(int platformId, String name, String urlTemplate, String accountNameRegex) : base(ObjectState.UNCHANGED)
        {
            _platformId = platformId;
            _name = name;
            _urlTemplate = urlTemplate;
            _accountNameRegex = accountNameRegex;
        }

        private int _platformId;
        private String _name;
        private String _urlTemplate;
        private String _accountNameRegex;


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



        public String AccountNameRegex
        {
            get { return _accountNameRegex; }
            private set { _accountNameRegex = value; }

        }


        public bool IsValidAccountName(String accountName)
        {
            if (!String.IsNullOrWhiteSpace(this.AccountNameRegex))
                Regex.IsMatch(accountName, this.AccountNameRegex);

            return true;
        }

    }
}
