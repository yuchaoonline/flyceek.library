using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.IO;

using ComLib.Models;
using ComLib;


namespace ComLib.CodeGeneration
{
    /// <summary>
    /// Builds the validation for the model, this includes it's properties and
    /// it's composite objects.
    /// </summary>
    public class CodeBuilderWebUI : CodeBuilderBase, ICodeBuilder
    {
        private StringBuilder _createEditCode = new StringBuilder();
        private StringBuilder _detailsCode = new StringBuilder();
        private StringBuilder _indexCode = new StringBuilder();
        private StringBuilder _indexColumnCode = new StringBuilder();
        private int _indexColumnCount = 0;
        CodeBuilderModelIterator _iterator;
        private int _modelsProcessed = 0;


        #region ICodeBuilder Members
        /// <summary>
        /// Create the Web UI.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public BoolMessageItem<ModelContainer> Process(ModelContext ctx)
        {
            _iterator = new CodeBuilderModelIterator();
            _iterator.FilterOnModel = model => model.GenerateUI;
            Init();

            // Build the property
            _iterator.OnModelProcessCompleted += new ModelHandler(ModelProcessCompleted);
            _iterator.OnCompositeProcess += new CompositionHandler(CompositeBuilder);
            _iterator.OnIncludeProcess += new IncludeHandler(IncludeBuilder);
            _iterator.OnUIProcess += new UIHandler(UIBuilder);
            _iterator.Process(ctx);
            return null;
        }

        
        /// <summary>
        /// Builds the model.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="model"></param>
        public void ModelProcessCompleted(ModelContext ctx, Model model)
        {
            // Write out the files.
            // 1. Controller.
            // 2. Views ( create.aspx, update.aspx, index.aspx
            Dictionary<string, CodeFile> files = CodeBuilderUtils.GetFiles(ctx, "*.cs,*.ascx,*.aspx", ctx.AllModels.Settings.ModelCodeLocationUITemplate);
            string manageColumnsUI = _indexColumnCode.ToString() + "<td><div class=\"field\">manage</div></td>" + Environment.NewLine;

            List<KeyValuePair<string, string>> subs = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("<%= model.NameSpace %>", model.NameSpace),
                new KeyValuePair<string, string>("<%= model.Name %>", model.Name),
                new KeyValuePair<string, string>("<%= model.PropertiesUI %>", _createEditCode.ToString()),
                new KeyValuePair<string, string>("<%= model.DetailsUI %>", _detailsCode.ToString()),
                new KeyValuePair<string, string>("<%= model.IndexUI %>", _indexCode.ToString()),
                new KeyValuePair<string, string>("<%= model.IndexColumnsUI %>", _indexColumnCode.ToString()),
                new KeyValuePair<string, string>("<%= model.IndexColumnCount %>", _indexColumnCount.ToString()),
                new KeyValuePair<string, string>("<%= model.ManageColumnsUI %>", manageColumnsUI),
                new KeyValuePair<string, string>("<%= model.ManageColumnCount %>", (_indexColumnCount + 1).ToString())
            };

            string rootUIFolder = ctx.AllModels.Settings.ModelCodeLocationUI;

            foreach(string key in files.Keys)
            {
                CodeFile file = files[key];
                if (file.Name == "Controller.cs")
                {
                    file.QualifiedName = model.Name + file.Name;
                    file.OutputFolder = rootUIFolder + "\\" + file.Folder;
                }
                else 
                {
                    file.QualifiedName = file.Name;
                    file.OutputFolder = rootUIFolder + "\\" + file.Folder + "\\" + model.Name + "s";
                }
            }
            CodeBuilderUtils.WriteFiles(files, subs, ctx.AllModels.Settings.ModelCodeLocationUI);
            Init();
        }


        /// <summary>
        /// Builds the code the entity create/edit form.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="model"></param>
        /// <param name="prop"></param>
        public void UIBuilder(ModelContext ctx, Model model, UISpec uiSpec, PropertyInfo prop)
        {
            if (uiSpec.CreateEditUI)
            {
                string controlType = GetUIControlType(prop);

                // UI Code for building create/edit form.
                string createEditCode = "<tr><td><div class=\"field\">{0}:</div></td><td><div class=\"fieldval\">"
                                 + "<%= Html.{1}(\"{2}\", Model.{3}) %>"
                                 + "<%= Html.ValidationMessage(\"{4}\", \"*\")%>"
                                 + "</div></td></tr>" + Environment.NewLine;
                createEditCode = string.Format(createEditCode, prop.Name, controlType, prop.Name, prop.Name, prop.Name);
                _createEditCode.Append(createEditCode);
            }
            if( uiSpec.DetailsUI)
            {

                // UI Code for building the details page.
                // <tr><td><div class="field">Description:</div></td><td><div class="fieldval"> <%= Html.Encode(Model.Description) %></div></td></tr>    
                string detailsCode = "<tr><td><div class=\"field\">{0}:</div></td><td><div class=\"fieldval\">"
                                + "<%= Html.Encode(Model.{1}) %></div></td></tr>" + Environment.NewLine;
                detailsCode = string.Format(detailsCode, prop.Name, prop.Name);
                _detailsCode.Append(detailsCode);
            }
            if (uiSpec.SummaryUI)
            {
                // UI Code for building the details page.
                // <tr><td><div class="field">Description:</div></td><td><div class="fieldval"> <%= Html.Encode(Model.Description) %></div></td></tr>    
                string indexCode = "<td><div class=\"fieldval\">" + "<%= Html.Encode(entity.{1}) %></div></td>";
                indexCode = string.Format(indexCode, prop.Name, prop.Name);
                _indexCode.Append(indexCode);

                string colHeader = "<td><div class=\"field\">{0}</div></td>" + Environment.NewLine;
                colHeader = string.Format(colHeader, prop.Name);
                _indexColumnCode.Append(colHeader);
                _indexColumnCount++;
            }
        }



        /// <summary>
        /// Build the UI properties for the composite model.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="model"></param>
        /// <param name="composite"></param>
        public void CompositeBuilder(ModelContext ctx, Model model, Model composite)
        {
            if (composite.GenerateUI)
            {
                _createEditCode.Append("<div class=\"h3\">" + composite.Name + "</div>");
                _detailsCode.Append("<div class=\"h3\">" + composite.Name + "</div>");
                _iterator.ProcessProperties(ctx, composite);
            }
        }


        /// <summary>
        /// Builds the UI properties for the included model.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="model"></param>
        /// <param name="included"></param>
        public void IncludeBuilder(ModelContext ctx, Model model, Model included)
        {
            _iterator.ProcessProperties(ctx, included);
        }
        #endregion


        private void Init()
        {
            _modelsProcessed = 0;
            _indexColumnCount = 0;
            ClearBuffers();
        }


        private void ClearBuffers()
        {
            _createEditCode = new StringBuilder();
            _detailsCode = new StringBuilder();
            _indexCode = new StringBuilder();
            _indexColumnCode = new StringBuilder();            
        }


        private string GetUIControlType(PropertyInfo prop)
        {
            if (prop.DataType == typeof(StringClob))
                return "TextArea";

            if (prop.DataType == typeof(bool) || prop.DataType == typeof(Boolean))
                return "CheckBox";

            return "TextBox";
        }
    }
}