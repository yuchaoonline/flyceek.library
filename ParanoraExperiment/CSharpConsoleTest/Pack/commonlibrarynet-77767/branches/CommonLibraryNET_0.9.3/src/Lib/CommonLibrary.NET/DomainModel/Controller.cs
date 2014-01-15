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

namespace ComLib.Entities
{
    /// <summary>
    /// Model controller.
    /// </summary>
    /// <typeparam name="TId">The type of the id.</typeparam>
    /// <typeparam name="T"></typeparam>
    public class EntityController<T> : IEntityController<T>
    {
        private IEntityView<T> _view;
        private IEntityService<T> _service;
        

        #region IEntityController<T> Members

        /// <summary>
        /// The view.
        /// </summary>
        /// <value></value>
        public IEntityView<T> View
        {
            get { return _view; }
            set { _view = value; }
        }


        /// <summary>
        /// The service.
        /// </summary>
        /// <value>The service.</value>
        public IEntityService<T> Service
        {
            get { return _service; }
            set { _service = value; }
        }

        #endregion
    }
}
