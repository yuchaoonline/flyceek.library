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


namespace ComLib.ImportExport
{

    /// <summary>
    /// Interface for an import/export service on objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IImportExportService<T>
    {
        /// <summary>
        /// The validator for validating items during import.
        /// </summary>
        IEntityValidator<T> Validator { get; set; }

        
        /// <summary>
        /// Determines whether this instance can import the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="errors">The errors.</param>
        /// <returns></returns>
        BoolMessageItem<IList<T>> CanImport(ImportExportActionContext<T> ctx);


        /// <summary>
        /// Determines whether this instance can import the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="errors">The errors.</param>
        /// <returns></returns>
        BoolMessageItem<IList<T>> CanImportFromText(ImportExportActionContext<T> ctx);


        /// <summary>
        /// Imports the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        BoolMessageItem<IList<T>> Import(ImportExportActionContext<T> ctx);


        /// <summary>
        /// Imports the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        BoolMessageItem<IList<T>> ImportFromText(ImportExportActionContext<T> ctx);


        /// <summary>
        /// Gets the total count of the items that can be exported.
        /// </summary>
        /// <returns></returns>
        int GetTotalExportCount();


        /// <summary>
        /// Exports a batch of items.
        /// </summary>
        /// <returns></returns>
        BoolMessageItem<IList<T>> ExportBatch(ImportExportActionContext<T> ctx);


        /// <summary>
        /// Exports all.
        /// </summary>
        /// <returns></returns>
        BoolMessageItem<IList<T>> ExportAll();

    }
}
