/*
 * Copyright 2010-2014 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 * 
 *  http://aws.amazon.com/apache2.0
 * 
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

using Amazon.AWSSupport.Model;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;
using ThirdParty.Json.LitJson;

namespace Amazon.AWSSupport.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Response Unmarshaller for RecentCaseCommunications Object
    /// </summary>  
    public class RecentCaseCommunicationsUnmarshaller : IUnmarshaller<RecentCaseCommunications, XmlUnmarshallerContext>, IUnmarshaller<RecentCaseCommunications, JsonUnmarshallerContext>
    {
        RecentCaseCommunications IUnmarshaller<RecentCaseCommunications, XmlUnmarshallerContext>.Unmarshall(XmlUnmarshallerContext context)
        {
            throw new NotImplementedException();
        }

        public RecentCaseCommunications Unmarshall(JsonUnmarshallerContext context)
        {
            context.Read();
            if (context.CurrentTokenType == JsonToken.Null) 
                return null;

            RecentCaseCommunications unmarshalledObject = new RecentCaseCommunications();
        
            int targetDepth = context.CurrentDepth;
            while (context.ReadAtDepth(targetDepth))
            {
                if (context.TestExpression("communications", targetDepth))
                {
                    var unmarshaller = new ListUnmarshaller<Communication, CommunicationUnmarshaller>(CommunicationUnmarshaller.Instance);
                    unmarshalledObject.Communications = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("nextToken", targetDepth))
                {
                    var unmarshaller = StringUnmarshaller.Instance;
                    unmarshalledObject.NextToken = unmarshaller.Unmarshall(context);
                    continue;
                }
            }
          
            return unmarshalledObject;
        }


        private static RecentCaseCommunicationsUnmarshaller _instance = new RecentCaseCommunicationsUnmarshaller();        

        public static RecentCaseCommunicationsUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}