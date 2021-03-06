// Copyright 2010 Chris Patterson
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Stact.Routing.Contexts
{
    using System;


    public class ResponseProxy<TInput, TOutput> :
        Response<TOutput>
        where TInput : TOutput
    {
        readonly Response<TInput> _response;

        public ResponseProxy(Response<TInput> response)
        {
            _response = response;
        }

        public Uri BodyType
        {
            get { return _response.BodyType; }
        }

        public string MessageId
        {
            get { return _response.MessageId; }
        }

        public string CorrelationId
        {
            get { return _response.CorrelationId; }
        }

        public Uri SenderAddress
        {
            get { return _response.SenderAddress; }
        }

        public Uri DestinationAddress
        {
            get { return _response.DestinationAddress; }
        }

        public Uri FaultAddress
        {
            get { return _response.FaultAddress; }
        }

        public Headers Headers
        {
            get { return _response.Headers; }
        }

        public TOutput Body
        {
            get { return _response.Body; }
        }

        public string RequestId
        {
            get { return _response.RequestId; }
        }
    }
}