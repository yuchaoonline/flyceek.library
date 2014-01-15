﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// This is a static decorator( As opposed to using AOP ) to get cross-cutting behaviour.).
    /// </summary>
    public class ApplicationTemplateDecorationHelper
    {
        private Type _appType;
        private string _delimitedDecorators;
        private IDictionary<string, bool> _decorators;

        
        /// <summary>
        /// Initialize the decorators.
        /// </summary>
        /// <param name="delimitedDecorators"></param>
        public ApplicationTemplateDecorationHelper(Type appType, string delimitedDecorators)
        {
            Init(appType, delimitedDecorators);
        }


        /// <summary>
        /// Determine if there are any decorations specified.
        /// </summary>
        public void Init(Type appType, string delimitedDecorators)
        {
            _appType = appType;
            _delimitedDecorators = delimitedDecorators;
            _decorators = new Dictionary<string, bool>();
            
            // No decorators?
            if (string.IsNullOrEmpty(_delimitedDecorators))
                return;

            string[] decorators = _delimitedDecorators.Split(',');
            if (decorators == null || decorators.Length == 0)
                return;

            // Store the decorator in the dictionary.
            decorators.ForEach(decorator => _decorators[decorator.ToLower().Trim()] = true);
        }


        /// <summary>
        /// Determine if the decorator specified is enabled for the application being run.
        /// </summary>
        /// <param name="decoratorName"></param>
        /// <returns></returns>
        public bool IsDecoratedWith(string decoratorName)
        {
            return _decorators.ContainsKey(decoratorName.Trim().ToLower());
        }


        /// <summary>
        /// Executes the method action by wrapping it with
        /// 1. Logging of start / end time.
        /// 2. Status updates.
        /// </summary>
        /// <param name="methodName">The name of the method being executed.</param>
        /// <param name="taskName">Option name to use for the TaskName for StatusUpdates.</param>
        /// <param name="wrapTryCatch">Whether or not to wrap the call inside a try catch.</param>
        /// <param name="action">Action to execute.</param>
        public void Execute(string methodName, string taskName, bool wrapTryCatch, ActionVoid action)
        {
            Execute(methodName, taskName, () =>
            {
                if (!wrapTryCatch) action();
                else ExecuteHelper.TryCatchLog(action);
            });
        }


        /// <summary>
        /// Executes the method action by wrapping it with
        /// 1. Logging of start / end time.
        /// 2. Status updates.
        /// </summary>
        /// <param name="methodName">The name of the method being executed.</param>
        /// <param name="taskName">Option name to use for the TaskName for StatusUpdates.</param>
        /// <param name="wrapTryCatch">Whether or not to wrap the call inside a try catch.</param>
        /// <param name="action">Action to execute.</param>
        public void Execute<T>(string methodName, string taskName, bool wrapTryCatch, Func<T> action)
        {
            T result = default(T);
            Execute(methodName, taskName, () =>
            {
                if (!wrapTryCatch) result = action();
                else result = ExecuteHelper.TryCatchLog<T>("", action);
            });
        }


        /// <summary>
        /// Executes the method action by wrapping it with
        /// 1. Logging of start / end time.
        /// 2. Status updates.
        /// </summary>
        /// <param name="methodName">The name of the method being executed.</param>
        /// <param name="taskName">Option name to use for the TaskName for StatusUpdates.</param
        /// <param name="action">Action to execute.</param>
        public void Execute(string methodName, string taskName, ActionVoid action)
        {
            DateTime startTime = DateTime.Now;            
            HandleStart(methodName, startTime);
            action();
            HandleEnd(methodName, startTime);
        }


        /// <summary>
        /// Handle the "Logging" the starting of the method call before execution.
        /// </summary>
        /// <param name="methodName"></param>
        private void HandleStart(string methodName, DateTime startTime)
        {
            if(IsDecoratedWith("log"))
                Logger.Info(string.Format("{0}.{1} : {2} at {3}", _appType.Name, methodName, "Started", startTime));

            if(IsDecoratedWith("statusupdates"))
                StatusUpdates.Update(methodName, StatusUpdateConstants.Started, "", startTime, startTime);
        }


        /// <summary>
        /// Handle the "Logging" the starting of the method call before execution.
        /// </summary>
        /// <param name="methodName"></param>
        private void HandleEnd(string methodName, DateTime startTime)
        {
            // Time ended.
            DateTime endTime = DateTime.Now;
            
            if(IsDecoratedWith("log")) 
                Logger.Info(string.Format("{0}.{1} : {2} at {3}", _appType.Name, methodName, "Finished", endTime));

            if(IsDecoratedWith("statusupdates"))
                StatusUpdates.Update(methodName, StatusUpdateConstants.Completed, "", startTime, endTime);
        }
    }
}
