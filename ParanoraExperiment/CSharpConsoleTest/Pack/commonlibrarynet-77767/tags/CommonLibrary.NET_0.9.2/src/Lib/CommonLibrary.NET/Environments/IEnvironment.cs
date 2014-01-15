using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace CommonLibrary
{
    public interface IEnvironment
    {
        void Change(string envName);
        EnvironmentContext Context { get; }
        EnvironmentType EnvType { get; }
        ReadOnlyCollection<CommonLibrary.EnvItem> GetAll();
        List<EnvItem> Inheritance { get; }
        
        /// <summary>
        /// Get the inheritance path, e.g. prod;qa;dev.
        /// </summary>
        string InheritancePath { get; }

        void Init(CommonLibrary.EnvironmentContext ctx, string envName);
        bool IsDev { get; }
        bool IsProd { get; }
        bool IsQa { get; }
        bool IsUat { get; }
        event EventHandler OnEnvironmentChange;
        EnvItem SelectedEnv { get; }
        List<string> SelectableEnvs { get; }        
    }
}
