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

namespace GenericCode.DomainModel
{
    /// <summary>
    /// Hybrid of Domain object with active record support.
    /// </summary>
    /// <remarks>
    /// If .NET supported multiple inheritance, this class would extend from
    /// both DomainObject, and ActiveRecord, however the IActiveRecord interface
    /// has to be implemented in this class.
    /// 1. Possible alternatives are extension methods 
    /// 2. delegation.
    /// </remarks>
    /// <typeparam name="TId">The type of the id.</typeparam>
    /// <typeparam name="T"></typeparam>
    public abstract class DomainObjectActiveRecord<TId, T> : DomainObject<TId, T>, IDomainObjectActiveRecord<TId, T> where T : IDomainObjectActiveRecord<TId, T>, new()
    {
        private IService<TId, T> _service = null;
        protected string _modelName = string.Empty;


        #region IActiveRecord<TId,T> Members
        /// <summary>
        /// Gets the name of the model.
        /// This is used when creating an new intance of this DomainObjectActiveRecord.
        /// This model name MAY be used as the key for getting all the neccessary
        /// components ( Service, Repository, etc ) from the <see cref="GenericCode.DomainModel.ActiveRecordRegistration"/>
        /// </summary>
        /// <value>The name of the model.</value>
        public abstract string ModelName { get; }


        /// <summary>
        /// The underlying service that handles model actions.
        /// </summary>
        /// <value></value>
        public IService<TId, T> Service
        {
            get { return _service; }
            set { _service = value; }
        }


        /// <summary>
        /// Creates the model
        /// </summary>
        /// <returns></returns>
        public virtual BooleanMessage Create()
        {
            ActionContext<TId, T> ctx = CreateContext();   
            return _service.Create(ctx);
        }


        /// <summary>
        /// Retrieves the model.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual BooleanMessageItem<T> Retrieve(TId id)
        {
            return _service.Retrieve(id);
        }


        /// <summary>
        /// Retrieves all the items.
        /// </summary>
        /// <returns></returns>
        public virtual BooleanMessageItem<IList<T>> RetrieveAll()
        {
            return _service.RetrieveAll();
        }


        /// <summary>
        /// Updates the model.
        /// </summary>
        /// <returns></returns>
        public virtual BooleanMessage Update()
        {
            ActionContext<TId, T> ctx = CreateContext();            
            return _service.Update(ctx);
        }


        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns></returns>
        public virtual BooleanMessage Save()
        {
            ActionContext<TId, T> ctx = CreateContext();
            return _service.Save(ctx);
        }


        /// <summary>
        /// Deletes the model.
        /// </summary>
        /// <returns></returns>
        public virtual BooleanMessage Delete()
        {
            ActionContext<TId, T> ctx = CreateContext();   
            return _service.Delete(ctx);
        }


        #endregion


        /// <summary>
        /// Creates a new instance of this DomainModelActiveRecord and 
        /// initializes it so that all of its necessary components are configured.
        /// Service, Repository, Settings, Validator etc.
        /// </summary>
        /// <returns></returns>
        public static T New()
        {
            T model = new T();
            ActiveRecordRegistration.InitModel<TId, T>(model, model.ModelName);
            return model;
        }
        

        private ActionContext<TId, T> CreateContext()
        {
            ActionContext<TId, T> ctx = new ActionContext<TId, T>(this);
            ctx.CombineMessageErrors = true;
            return ctx;
        }
    }

}
