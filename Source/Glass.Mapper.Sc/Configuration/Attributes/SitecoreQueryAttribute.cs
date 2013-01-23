/*
   Copyright 2011 Michael Edwards
 
   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 
*/ 
//CRE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Glass.Mapper.Configuration;
using Glass.Mapper.Configuration.Attributes;

namespace Glass.Mapper.Sc.Configuration.Attributes
{
    public class SitecoreQueryAttribute : QueryAttribute
    {
        public SitecoreQueryAttribute(string query):base(query)
        {
        }

        /// <summary>
        /// Use the Sitecore.Data.Query.QueryContext when querying for data
        /// </summary>
        public bool UseQueryContext { get; set; }

        public override AbstractPropertyConfiguration Configure(PropertyInfo propertyInfo)
        {
            var config = new SitecoreQueryConfiguration();
            Configure(propertyInfo, config);
            return config;
        }
        public void Configure(PropertyInfo propertyInfo, SitecoreQueryConfiguration config)
        {
            config.UseQueryContext = this.UseQueryContext;
            base.Configure(propertyInfo, (QueryConfiguration) config);
        }
    }
}
