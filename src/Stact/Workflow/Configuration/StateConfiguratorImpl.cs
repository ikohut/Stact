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
namespace Stact.Workflow.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using Internal;
	using Magnum.Extensions;


	public class StateConfiguratorImpl<TWorkflow, TInstance> :
		StateConfigurator<TWorkflow, TInstance>,
		StateMachineBuilderConfigurator<TWorkflow, TInstance>
		where TWorkflow : class
		where TInstance : class
	{
		readonly IList<StateBuilderConfigurator<TWorkflow, TInstance>> _configurators;
		readonly StateMachineConfigurator<TWorkflow, TInstance> _stateMachineConfigurator;
		readonly Func<StateMachineBuilder<TWorkflow,TInstance>,StateMachineState<TInstance>> _getState;

		public StateConfiguratorImpl(StateMachineConfigurator<TWorkflow, TInstance> stateMachineConfigurator,
		                             Expression<Func<TWorkflow, State>> stateExpression)
			: this(stateMachineConfigurator)
		{
			_getState = builder => builder.Model.GetState(stateExpression);
		}

		public StateConfiguratorImpl(StateMachineConfigurator<TWorkflow, TInstance> stateMachineConfigurator,
		                             string stateName)
			: this(stateMachineConfigurator)
		{
			_getState = builder => builder.Model.GetState(stateName);
		}

		StateConfiguratorImpl(StateMachineConfigurator<TWorkflow, TInstance> stateMachineConfigurator)
		{
			_stateMachineConfigurator = stateMachineConfigurator;
			_configurators = new List<StateBuilderConfigurator<TWorkflow, TInstance>>();
		}

		public void AddConfigurator(StateBuilderConfigurator<TWorkflow, TInstance> configurator)
		{
			_configurators.Add(configurator);
		}

		public void Configure(StateMachineBuilder<TWorkflow, TInstance> stateMachineBuilder)
		{
			var state = _getState(stateMachineBuilder);

			var stateBuilder = new StateBuilderImpl<TWorkflow, TInstance>(stateMachineBuilder, state);

			_configurators.Each(x => x.Configure(stateBuilder));
		}

		public void ValidateConfiguration()
		{
			if (_getState == null)
				throw new StateMachineConfigurationException("Null state expression specified");
		}
	}
}