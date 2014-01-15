using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComLib.Models
{
    /// <summary>
    /// Class to iterate over models to create and provide callbacks at key points to 
    /// handle certain events related to model code generation.
    /// </summary>
    public class ModelIterator
    {
        #region Model Events
        /// <summary>
        /// Event to fire on model processing.
        /// </summary>
        public event Func<ModelContext, Model, bool> OnModelProcess;


        /// <summary>
        /// Event to fire when a model has been processed.
        /// </summary>
        public event Func<ModelContext, Model, bool> OnModelProcessCompleted;


        /// <summary>
        /// Event to fire when property should be processed for a specific model.
        /// </summary>
        public event Func<ModelContext, Model, PropertyInfo, bool> OnPropertyProcess;


        /// <summary>
        /// Event to fire when a composite model should be processed.
        /// </summary>
        public event Func<ModelContext, Model, Composition, bool> OnCompositeProcess;


        /// <summary>
        /// Event to fire when a included model should be processed.
        /// </summary>
        public event Func<ModelContext, Model, Include, bool> OnIncludeProcess;
        #endregion


        #region Filters
        /// <summary>
        /// Predicate to apply to model before processing it.
        /// </summary>
        public Func<Model, bool> FilterOnModel;


        /// <summary>
        /// Property filter.
        /// </summary>
        public Func<Model, PropertyInfo, bool> FilterOnProperty;
        #endregion


        /// <summary>
        /// Process all the models, which pass the filter, one at a time.
        /// </summary>
        /// <param name="ctx"></param>
        public virtual void Process(ModelContext ctx)
        {
            foreach (Model currentModel in ctx.AllModels.AllModels)
            {
                // Pre condition.
                if (FilterOnModel(currentModel))
                {
                    ProcessModel(ctx, currentModel);
                }
            }
        }


        /// <summary>
        /// Process the model with the specified name.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="modelName"></param>
        public virtual void Process(ModelContext ctx, string modelName)
        {
            Model current = ctx.AllModels.ModelMap[modelName];
            ProcessModel(ctx, current);
        }


        /// <summary>
        /// Proces given model
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="currentModel"></param>
        public virtual void ProcessModel(ModelContext ctx, Model currentModel)
        {   
            // Notify.
            if (OnModelProcess != null)
                OnModelProcess(ctx, currentModel);

            // Create the database table for all the models.
            List<Model> modelChain = ModelUtils.GetModelInheritancePath(ctx, currentModel.Name);

            // Sort the models to create the columns/properties in a specific order.
            // For the database, the inheritance chain doesn't really matter.
            ModelUtils.Sort(modelChain);

            foreach (Model model in modelChain)
            {
                // Build the entity properties.
                ProcessModelParts(ctx, model);
            }
            if (OnModelProcessCompleted != null)
                OnModelProcessCompleted(ctx, currentModel);               
        }


        /// <summary>
        /// Build the properties.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual void ProcessModelParts(ModelContext ctx, Model model)
        {
            ProcessProperties(ctx, model);
            ProcessCompositions(ctx, model);
            ProcessIncludes(ctx, model);            
        }


        /// <summary>
        /// Process all the properties of the model.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="model"></param>
        public virtual void ProcessProperties(ModelContext ctx, Model model)
        {
            // Handle properties of model.
            foreach (PropertyInfo prop in model.Properties)
            {
                if (FilterOnProperty == null || (FilterOnProperty != null && FilterOnProperty(model, prop)))
                {
                    // Run event handler
                    if (this.OnPropertyProcess != null)
                        OnPropertyProcess(ctx, model, prop);
                }
            }
        }


        /// <summary>
        /// Process all the compositions of the model.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="model"></param>
        public virtual void ProcessCompositions(ModelContext ctx, Model model)
        {
            // Handle compositions
            if (model.ComposedOf != null && model.ComposedOf.Count > 0)
            {
                // Now build mapping for composed objects.
                foreach (Composition composite in model.ComposedOf)
                {
                    Model compositeModel = ctx.AllModels.ModelMap[composite.Name];
                    composite.ModelRef = compositeModel;
                    if (this.OnCompositeProcess != null)
                        OnCompositeProcess(ctx, model, composite);
                }
            }
        }


        /// <summary>
        /// Process all the includes of the model.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="model"></param>
        public virtual void ProcessIncludes(ModelContext ctx, Model model)
        {
            // Handle includes
            if (model.Includes != null && model.Includes.Count > 0)
            {
                // Now build mapping for composed objects.
                foreach (Include include in model.Includes)
                {
                    Model includedModel = ctx.AllModels.ModelMap[include.Name];
                    include.ModelRef = includedModel;
                    if (OnIncludeProcess != null)
                        OnIncludeProcess(ctx, model, include);
                }
            }
        }


        /// <summary>
        /// Get all the applicable properties after running the filter.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<PropertyInfo> GetProperties(Model model)
        {
            List<PropertyInfo> props = new List<PropertyInfo>();
            // Handle properties of model.
            foreach (PropertyInfo prop in model.Properties)
            {
                if (FilterOnProperty == null || (FilterOnProperty != null && FilterOnProperty(model, prop)))
                {
                    props.Add(prop);
                }
            }
            return props;
        }
    }
}
