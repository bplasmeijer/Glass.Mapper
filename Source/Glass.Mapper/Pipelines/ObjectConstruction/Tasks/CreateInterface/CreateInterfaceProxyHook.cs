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
using Castle.DynamicProxy;

namespace Glass.Mapper.Pipelines.ObjectConstruction.Tasks.CreateInterface
{
    public class CreateInterfaceProxyHook : IProxyGenerationHook
    {

        public void MethodsInspected()
        {
            //no idea what this method is used for
        }

        public void NonProxyableMemberNotification(Type type, System.Reflection.MemberInfo memberInfo)
        {
            throw new ObjectConstructionException("Can not proxy method {0} on interface {1}. Only properties of interface can be proxied".Formatted(memberInfo.Name, type.FullName));
        }

        public bool ShouldInterceptMethod(Type type, System.Reflection.MethodInfo methodInfo)
        {
            //for interface we only support properties
            return type.IsInterface && methodInfo.MemberType == System.Reflection.MemberTypes.Property;
        }
    }
}
