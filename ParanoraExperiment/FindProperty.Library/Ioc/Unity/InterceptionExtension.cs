using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace FindProperty.Lib.Ioc.Unity
{
    public class InterceptionExtension: UnityContainerExtension
    {
        protected override void Initialize()
        {
            Context.Strategies.AddNew<InterceptionStrategy>(UnityBuildStage.PreCreation);
        }
    }
}
