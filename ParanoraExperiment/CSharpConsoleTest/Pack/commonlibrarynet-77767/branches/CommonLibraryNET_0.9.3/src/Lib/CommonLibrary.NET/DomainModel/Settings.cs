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
    public class EntitySettings<T> : IEntitySettings<T>
    {
        /// <summary>
        /// Whether authentication is required to edit the entity.
        /// </summary>
        public bool EnableAuthentication { get; set; }


        /// <summary>
        /// Whether or not to validate the entity.
        /// </summary>
        public bool EnableValidation { get; set; }
        

        /// <summary>
        /// Roles allowed to edit the entity.
        /// </summary>
        public string EditRoles { get; set; }



        /// <summary>
        /// Default construction.
        /// </summary>
        public EntitySettings()
        {
            Init();
        }


        /// <summary>
        /// Initalize settings.
        /// </summary>
        public virtual void Init()
        {
        }
    }
}
