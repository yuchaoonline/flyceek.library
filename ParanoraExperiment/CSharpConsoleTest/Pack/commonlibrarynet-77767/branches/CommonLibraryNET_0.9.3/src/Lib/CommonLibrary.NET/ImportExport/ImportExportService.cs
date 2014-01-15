/*
 * Author: Kishore Reddy
 * Url: http://commonlibrarynet.codeplex.com/
 * Title: CommonLibrary.NET
 * Copyright: � 2009 Kishore Reddy
 * License: LGPL License
 * LicenseUrl: http://commonlibrarynet.codeplex.com/license
 * Description: A C# based .NET 3.5 Open-Source collection of reusable components.
 * Usage: Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Text;
using ComLib;
using ComLib.Entities;
using ComLib.Database;
using ComLib.IO;
using ComLib.Entities;



namespace ComLib.ImportExport
{

    /// <summary>
    /// Interface for an import/export service on objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ImportExportServiceIni<T> : IImportExportService<T>
    {
        private IEntityValidator<T> _validator;
        private RowMapperContextual<IniDocument, T, string> _mapper;


        /// <summary>
        /// Initialize.
        /// </summary>
        public ImportExportServiceIni(RowMapperContextual<IniDocument, T, string> mapper)
        {
            _mapper = mapper;
        }


        /// <summary>
        /// The validator to use when importing.
        /// </summary>
        public IEntityValidator<T> Validator
        {
            get { return _validator; }
            set { _validator = value; }
        }


        /// <summary>
        /// Map each node in the ini document into the item T.
        /// </summary>
        public RowMapperContextual<IniDocument, T, string> Mapper
        {
            get { return _mapper; }
            set { _mapper = value; }
        }

       
        /// <summary>
        /// Determines whether this instance can import the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="errors">The errors.</param>
        /// <returns></returns>
        public BoolMessageItem<IList<T>> CanImport(ImportExportActionContext<T> ctx)
        {
            ValidationResults results = new ValidationResults();

            foreach (T item in ctx.ItemList)
            {
                _validator.Validate(item, results);
            }
            return new BoolMessageItem<IList<T>>(ctx.ItemList, results.IsValid, string.Empty);
        }


        /// <summary>
        /// Determines whether this instance can import the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="errors">The errors.</param>
        /// <returns></returns>
        public BoolMessageItem<IList<T>> CanImportFromText(ImportExportActionContext<T> ctx)
        {
            BoolMessageItem<IList<T>> canImport = null;
            IList<T> items = new List<T>();               
                
            // For each section.
            try
            {
                IniDocument iniDoc = new IniDocument(ctx.ImportText, false, false);
                ICollection<string> sectionNames = iniDoc.Sections;
                RowMappingContext<IniDocument, T, string> rowContext = new RowMappingContext<IniDocument, T, string>();
                rowContext.ValidationResults = ctx.Errors;
                rowContext.Source = iniDoc;
                rowContext.IsRowIdStringBased = false;

                // Get each section.
                for (int ndx = 0; ndx < iniDoc.Count; ndx++)
                {
                    // Set the current section being parsed.
                    rowContext.RowId = ndx.ToString();

                    // Now map the row to item T.
                    BoolMessageItem<T> result = _mapper.MapRow(rowContext);

                    // Check for both success and the item is not null.
                    if (result.Item != null)
                    {
                        T item = result.Item;

                        // Validate.
                        bool isValid = _validator.Validate(item, rowContext.ValidationResults);

                        // Only add to list if validation passed.
                        if (isValid) items.Add(item);
                    }
                }
                canImport = new BoolMessageItem<IList<T>>(items, rowContext.ValidationResults.IsValid, string.Empty);
            }
            catch (Exception ex)
            {
                canImport = new BoolMessageItem<IList<T>>(items, false, "An error occurred during the import process: " + ex.Message);
            }
            return canImport;
        }


        /// <summary>
        /// Imports the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public BoolMessageItem<IList<T>> Import(ImportExportActionContext<T> ctx)
        {
            // Check if nodes or import text was supplied.
            if (ctx.ItemList == null )
                return new BoolMessageItem<IList<T>>(null, false, "Nodes to import was not supplied.");

            return InternalImport(ctx);
        }


        /// <summary>
        /// Imports the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public BoolMessageItem<IList<T>> ImportFromText(ImportExportActionContext<T> ctx)
        {
            // If the items are null, and import text is provided, parse it.
            if (string.IsNullOrEmpty(ctx.ImportText))
                return new BoolMessageItem<IList<T>>(null, false, "Import content is empty.");
            
            BoolMessageItem<IList<T>> result = CanImportFromText(ctx);
            
            // Unable to import from text ?
            if (!result.Success) return result;

            // Set the item list on the context as internal method only handles parsed nodes.
            ctx.ItemList = result.Item;
            return InternalImport(ctx);
        }


        /// <summary>
        /// Gets the total count of the items that can be exported.
        /// </summary>
        /// <returns></returns>
        public int GetTotalExportCount()
        {
            throw new NotImplementedException("Not yet implemented.");
        }


        /// <summary>
        /// Exports a batch of items.
        /// </summary>
        /// <returns></returns>
        public BoolMessageItem<IList<T>> ExportBatch(ImportExportActionContext<T> ctx)
        {
            throw new NotImplementedException("Not yet implemented.");
        }


        /// <summary>
        /// Exports all.
        /// </summary>
        /// <returns></returns>
        public BoolMessageItem<IList<T>> ExportAll()
        {
            throw new NotImplementedException("Not yet implemented.");
        }


        #region Private members
        private BoolMessageItem<IList<T>> InternalImport(ImportExportActionContext<T> ctx)
        {
            IEntityService<T> service = EntityRegistration.GetService<T>();
            IActionContext actionContext = EntityRegistration.GetContext(typeof(T).FullName);
            actionContext.Errors = ctx.Errors;
            actionContext.Messages = ctx.Messages;
            actionContext.CombineMessageErrors = false;
            actionContext.Items = ctx.ItemList;
            actionContext.Args["isImporting"] = true;
            BoolMessage createResult = service.Create(actionContext);
            return new BoolMessageItem<IList<T>>(ctx.ItemList, createResult.Success, createResult.Message);
        }

        #endregion

    }
}
