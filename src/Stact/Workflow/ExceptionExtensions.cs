﻿// Copyright 2010 Chris Patterson
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
namespace Stact.Workflow
{
	using Configuration;


	public static class ExceptionExtensions
	{
		public static ExceptionConfigurator<TWorkflow, TInstance> InCaseOf<TWorkflow, TInstance>(
			this ActivityConfigurator<TWorkflow, TInstance> activityConfigurator)
			where TWorkflow : class
			where TInstance : class
		{
			var configurator = new ExceptionConfiguratorImpl<TWorkflow, TInstance>(activityConfigurator);

			activityConfigurator.AddConfigurator(configurator);

			return configurator;
		}

		public static MessageExceptionConfigurator<TWorkflow, TInstance, TBody> InCaseOf<TWorkflow, TInstance, TBody>(
			this ActivityConfigurator<TWorkflow, TInstance, TBody> activityConfigurator)
			where TWorkflow : class
			where TInstance : class
		{
			var configurator = new MessageExceptionConfiguratorImpl<TWorkflow, TInstance, TBody>(activityConfigurator);

			activityConfigurator.AddConfigurator(configurator);

			return configurator;
		}
	}
}