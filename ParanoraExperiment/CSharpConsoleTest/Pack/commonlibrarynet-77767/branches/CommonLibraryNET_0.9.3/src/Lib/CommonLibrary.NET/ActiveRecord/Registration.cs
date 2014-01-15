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
    /// Static class used to register the creation of the components of 
    /// the domain model.
    /// </summary>
    public class ActiveRecordRegistration : EntityRegistration
    {

        /// <summary>
        /// Inits the DomainModelActiveRecord with it's corresponding services.
        /// 1. Service.
        /// 2. Validator
        /// 3. ServiceSettings
        /// 4. Repository.
        /// </summary>
        /// <typeparam name="TId">The type of the id.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">The model.</param>
        public static void InitModel<T>(T model) where T : IActiveRecordDomainObject<T>
        {            
            IEntityService<T> service = GetService<T>();
            model.Service = service;
            model.Validator = service.Validator;
        }


        /// <summary>
        /// Inits model with the validator.
        /// </summary>
        /// <typeparam name="TId">The type of the id.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">The model.</param>
        public static void Init<T>(T model, IEntityService<T> service) where T : IDomainObject<T>
        {
            if (model != null)
            {
                model.Validator = service.Validator;
            }
        }


        /// <summary>
        /// Inits model with the validator.
        /// </summary>
        /// <typeparam name="TId">The type of the id.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">The model.</param>
        public static void Init<T>(T model) where T : IDomainObject<T>
        {
            IEntityService<T> service = GetService<T>();
            model.Validator = service.Validator;
        }


        /// <summary>
        /// Inits the DomainModelActiveRecord with it's corresponding services.
        /// 1. Service.
        /// 2. Validator
        /// 3. ServiceSettings
        /// 4. Repository.
        /// </summary>
        /// <typeparam name="TId">The type of the id.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">The model.</param>
        public static void InitModel<T>(T model, IEntityService<T> service) where T : IActiveRecordDomainObject<T>
        {            
            model.Service = service;
            model.Validator = service.Validator;
        }
    }
}
