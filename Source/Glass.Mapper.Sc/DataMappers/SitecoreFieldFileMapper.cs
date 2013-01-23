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
using System.Text;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Fields;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Resources.Media;

namespace Glass.Mapper.Sc.DataMappers
{
    public class SitecoreFieldFileMapper : AbstractSitecoreFieldMapper
    {
        public SitecoreFieldFileMapper() : base(typeof (File))
        {
        }

        public override object GetField(Field field, SitecoreFieldConfiguration config, SitecoreDataMappingContext context)
        {
            FileField fileField = new FileField(field);
            File file = new File();
            if (fileField.MediaItem != null)
                file.Src = MediaManager.GetMediaUrl(fileField.MediaItem);
            file.Id = fileField.MediaID.Guid;

            return file;
        }
    

        public override void SetField(Field field, object value, SitecoreFieldConfiguration config, SitecoreDataMappingContext context)
        {
            File file = value as File;

            var item = field.Item;

            FileField fileField = new FileField(field);

            if (file == null)
            {
                fileField.Clear();
                return;
            }

            if (fileField.MediaID.Guid != file.Id)
            {
                if (file.Id == Guid.Empty)
                {
                    ItemLink link = new ItemLink(item.Database.Name, item.ID, fileField.InnerField.ID, fileField.MediaItem.Database.Name, fileField.MediaID, fileField.MediaItem.Paths.FullPath);
                    fileField.RemoveLink(link);
                }
                else
                {
                    ID newId = new ID(file.Id);
                    Item target = item.Database.GetItem(newId);
                    if (target != null)
                    {
                        fileField.MediaID = newId;
                        ItemLink link = new ItemLink(item.Database.Name, item.ID, fileField.InnerField.ID, target.Database.Name, target.ID, target.Paths.FullPath);
                        fileField.UpdateLink(link);
                    }
                    else throw new MapperException("No item with ID {0}. Can not update File Item field".Formatted(newId));
                }
            }
        }
        public override string SetFieldValue(object value, SitecoreFieldConfiguration config, SitecoreDataMappingContext context)
        {
            throw new NotImplementedException();
        }
        public override object GetFieldValue(string fieldValue, SitecoreFieldConfiguration config, SitecoreDataMappingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
