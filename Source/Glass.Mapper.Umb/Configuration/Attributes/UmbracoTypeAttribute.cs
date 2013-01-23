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
using Glass.Mapper.Configuration.Attributes;
using Glass.Mapper.Configuration;

namespace Glass.Mapper.Umb.Configuration.Attributes
{
    public class UmbracoTypeAttribute : AbstractTypeAttribute
    {
        /// <summary>
        /// Indicates the document type to use when trying to create an item
        /// </summary>
        public int DocumentTypeId { get; set; }

        public override void Configure(Type type, AbstractTypeConfiguration config)
        {
            var umbConfig = config as UmbracoTypeConfiguration;

            if (umbConfig == null)
                throw new ConfigurationException(
                    "Type configuration is not of type {0}".Formatted(typeof(UmbracoTypeConfiguration).FullName));

            umbConfig.DocumentTypeId = this.DocumentTypeId;

            base.Configure(type, config);
        }
    }
}
