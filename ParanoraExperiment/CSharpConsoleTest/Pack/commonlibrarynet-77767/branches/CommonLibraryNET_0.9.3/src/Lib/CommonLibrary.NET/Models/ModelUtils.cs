using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;


namespace ComLib.Models
{
    public class ModelUtils
    {
        /// <summary>
        /// Get the inheritance path of a model as list of models.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="modelName"></param>
        /// <returns></returns>
        public static List<Model> GetModelInheritancePath(ModelContext ctx, string modelName, bool sortOnProperties)
        {
            List<Model> chain = GetModelInheritancePath(ctx, modelName);
            if (sortOnProperties)
                Sort(chain);

            return chain;
        }


        /// <summary>
        /// Get the inheritance path of a model as list of models.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="modelName"></param>
        /// <returns></returns>
        public static List<Model> GetModelInheritancePath(ModelContext ctx, string modelName)
        {
            Model currentModel = ctx.AllModels.ModelMap[modelName];
            string inheritancePath = ConvertNestedToFlatInheritance(currentModel, ctx);
            
            // No parents?
            if( inheritancePath.IndexOf(",") < 0 )
            {
                return new List<Model>() { ctx.AllModels.ModelMap[inheritancePath] };
            }

            // Delimited.
            List<Model> modelChain = new List<Model>();
            string[] parents = inheritancePath.Split(',');
            foreach (string parent in parents)
            {
                Model model = ctx.AllModels.ModelMap[parent];
                modelChain.Add(model);
            }
            return modelChain;
        }


        /// <summary>
        /// Traverses the nodes inheritance path to build a single flat delimeted line of 
        /// inheritance paths.
        /// e.g. returns "Job,Post,EntityBase"
        /// </summary>
        /// <returns></returns>
        public static string ConvertNestedToFlatInheritance(Model model, ModelContext ctx)
        {
            // Return name of environment provided if it doesn't have 
            // any inheritance chain.
            if (string.IsNullOrEmpty(model.Inherits))
                return model.Name;

            // Single parent.
            if (model.Inherits.IndexOf(",") < 0)
            {
                // Get the parent.
                Model parent = ctx.AllModels.ModelMap[model.Inherits.Trim()];
                return model.Name + "," + ConvertNestedToFlatInheritance(parent, ctx);
            }

            // Multiple parents.
            string[] parents = model.Inherits.Split(',');
            string path = model.Name;
            foreach (string parent in parents)
            {
                Model parentModel = ctx.AllModels.ModelMap[model.Inherits.Trim()];
                path += "," + ConvertNestedToFlatInheritance(parentModel, ctx);
            }
            return path;
        }


        /// <summary>
        /// Sort the 
        /// </summary>
        /// <param name="modelChain"></param>
        public static void Sort(List<Model> modelChain)
        {
            modelChain.Sort(delegate(Model m1, Model m2) 
            {
                if (m1.PropertiesSortOrder > m2.PropertiesSortOrder)
                    return 1;
                if (m1.PropertiesSortOrder < m2.PropertiesSortOrder)
                    return -1;
                return 0;
            }
            );
        }
    }
}
