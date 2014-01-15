using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.IO;

using ComLib.Models;


namespace ComLib.CodeGeneration
{
    /// <summary>
    /// Base class for code builders.
    /// </summary>
    public class CodeBuilderBase
    {        
        protected int _indentLevel = 0;


        #region Indentation
        /// <summary>
        /// Gets the indentation level.
        /// </summary>
        /// <returns></returns>
        protected string GetIndent()
        {
            string indent = string.Empty;
            for (int ndx = 0; ndx < _indentLevel; ndx++)
            {
                indent += "\t";
            }
            return indent;
        }


        /// <summary>
        /// Increments the indentation by 1 level of spaces ( 4 ).
        /// </summary>
        protected void IncrementIndent()
        {
            _indentLevel++;
        }


        /// <summary>
        /// Increments the indentation by count number of spaces ( 4 )
        /// </summary>
        /// <param name="count"></param>
        protected void IncrementIndent(int count)
        {
            _indentLevel += count;
        }


        /// <summary>
        /// Decrements the indentation by 1 level of spaces ( 4 )
        /// </summary>
        protected void DecrementIndent()
        {
            _indentLevel--;
        }


        /// <summary>
        /// Decrements the indentation by count levels of spaces ( 4 ).
        /// </summary>
        /// <param name="count"></param>
        protected void DecrementIndent(int count)
        {
            _indentLevel -= count;
        }
        #endregion 
    }


    /// <summary>
    /// Handler for processing a specific model.
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="currentModel"></param>
    /// <returns></returns>
    public delegate void ModelHandler( ModelContext ctx, Model currentModel);


    /// <summary>
    /// Handler for processing properties of the model.
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="currentModel"></param>
    /// <param name="prop"></param>
    public delegate void PropertyHandler(ModelContext ctx, Model currentModel, PropertyInfo prop);


    /// <summary>
    /// Handler for processing an composite model.
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="currentModel"></param>
    /// <param name="compositeModel"></param>
    public delegate void CompositionHandler(ModelContext ctx, Model currentModel, Model compositeModel);


    /// <summary>
    /// Handler for processing an included model.
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="currentModel"></param>
    /// <param name="includedModel"></param>
    public delegate void IncludeHandler(ModelContext ctx, Model currentModel, Model includedModel);


    /// <summary>
    /// Handler for processing the UI for a model.
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="currentModel"></param>
    public delegate void UIHandler(ModelContext ctx, Model currentModel, UISpec uiSpec, PropertyInfo prop);



    /// <summary>
    /// Class to iterate over models to create and provide callbacks at key points to 
    /// handle certain events related to model code generation.
    /// </summary>
    public class CodeBuilderModelIterator
    {   
        #region Model Events
        /// <summary>
        /// Event to fire on model processing.
        /// </summary>
        public event ModelHandler OnModelProcess;


        /// <summary>
        /// Event to fire when a model has been processed.
        /// </summary>
        public event ModelHandler OnModelProcessCompleted;


        /// <summary>
        /// Event to fire when property should be processed for a specific model.
        /// </summary>
        public event PropertyHandler OnPropertyProcess;


        /// <summary>
        /// Event to fire when a composite model should be processed.
        /// </summary>
        public event CompositionHandler OnCompositeProcess;


        /// <summary>
        /// Event to fire when a included model should be processed.
        /// </summary>
        public event IncludeHandler OnIncludeProcess;


        /// <summary>
        /// Event to fire when an UI model should be processed.
        /// </summary>
        public event UIHandler OnUIProcess;
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
        /// Process the models one at a time.
        /// </summary>
        /// <param name="ctx"></param>
        public virtual void Process(ModelContext ctx)
        {
            foreach (Model currentModel in ctx.AllModels.AllModels)
            {   
                // Pre condition.
                if (FilterOnModel(currentModel))
                {
                    // Notify.
                    if (OnModelProcess != null)
                        OnModelProcess(ctx, currentModel);

                    // Create the database table for all the models.
                    List<Model> modelChain = ModelUtils.GetModelInheritancePath(ctx, currentModel.Name);

                    // Sort the models to create the columns/properties in a specific order.
                    // For the database, the inheritance chain doesn't really matter.
                    ModelUtils.Sort(modelChain);
                    
                    // Build the entity properties.
                    ProcessModel(ctx, currentModel);

                    if (OnModelProcessCompleted != null)
                        OnModelProcessCompleted(ctx, currentModel);
                }
            }
        }


        /// <summary>
        /// Build the properties.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual void ProcessModel(ModelContext ctx, Model model)
        {
            ProcessProperties(ctx, model);
            ProcessCompositions(ctx, model);
            ProcessIncludes(ctx, model);
            ProcessUI(ctx, model);
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
                if (FilterOnProperty == null || ( FilterOnProperty != null && FilterOnProperty(model, prop)))
                {
                    // Run event handler
                    if (this.OnPropertyProcess != null)
                        OnPropertyProcess(ctx, model, prop);
                }
            }
        }


        /// <summary>
        /// Process the UI for a specific model.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="model"></param>
        public virtual void ProcessUI(ModelContext ctx, Model model)
        {
            foreach (UISpec uiSpec in model.UI)
            {
                string propName = uiSpec.PropertyName;
                var props = from p in model.Properties where p.Name == propName select p;
                if (props != null && props.Count<PropertyInfo>() > 0)
                {
                    PropertyInfo prop = props.Single<PropertyInfo>();

                    if (OnUIProcess != null)
                    {
                        OnUIProcess(ctx, model, uiSpec, prop);
                    }
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
                    if (this.OnCompositeProcess != null)
                        OnCompositeProcess(ctx, model, compositeModel);
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
                    if (OnIncludeProcess != null)
                        OnIncludeProcess(ctx, model, includedModel);
                }
            }
        }
    }

}
