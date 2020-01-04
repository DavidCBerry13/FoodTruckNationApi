using DavidBerry.Framework.Domain;
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


        public SocialMediaPlatform(string name, string urlTemplate, string accountNameRegex) : base(ObjectState.NEW)
        {
            _name = name;
            _urlTemplate = urlTemplate;
            _accountNameRegex = accountNameRegex;
        }


        internal SocialMediaPlatform(int platformId, string name, string urlTemplate, string accountNameRegex) : base(ObjectState.UNCHANGED)
        {
            _platformId = platformId;
            _name = name;
            _urlTemplate = urlTemplate;
            _accountNameRegex = accountNameRegex;
        }

        private int _platformId;
        private string _name;
        private string _urlTemplate;
        private string _accountNameRegex;


        public int PlatformId
        {
            get { return _platformId; }
            private set { _platformId = value; }
        }


        public string Name
        {
            get { return _name; }
            private set { _name = value; }
        }


        public string UrlTemplate
        {
            get { return _urlTemplate; }
            private set { _urlTemplate = value; }
        }



        public string AccountNameRegex
        {
            get { return _accountNameRegex; }
            private set { _accountNameRegex = value; }

        }


        public bool IsValidAccountName(string accountName)
        {
            if (!string.IsNullOrWhiteSpace(AccountNameRegex))
                Regex.IsMatch(accountName, AccountNameRegex);

            return true;
        }

    }
}
