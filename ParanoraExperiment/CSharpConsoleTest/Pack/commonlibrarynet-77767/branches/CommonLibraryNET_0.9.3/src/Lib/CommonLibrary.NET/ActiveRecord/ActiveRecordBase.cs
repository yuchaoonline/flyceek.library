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

using CommonLibrary;


namespace CommonLibrary.DomainModel
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
    public class ActiveRecordDomainObject<T> : DomainObject<T>, IActiveRecordDomainObject<T> where T : IActiveRecordDomainObject<T>, new()
    {
        private IEntityService<T> _service = null;


        #region IActiveRecord<T> Instance Members
        /// <summary>
        /// The underlying service that handles model actions.
        /// </summary>
        /// <value></value>
        public IEntityService<T> Service
        {
            get { return _service; }
            set { _service = value; }
        }


        /// <summary>
        /// Creates the model
        /// </summary>
        /// <returns></returns>
        public virtual BoolMessage Create()
        {
            ActionContext ctx = CreateContext();
            ctx.Item = this;
            return _service.Create(ctx);
        }


        /// <summary>
        /// Updates the model.
        /// </summary>
        /// <returns></returns>
        public virtual BoolMessage Update()
        {
            ActionContext ctx = CreateContext();
            ctx.Item = this;
            return _service.Update(ctx);
        }


        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns></returns>
        public virtual BoolMessage Save()
        {
            ActionContext ctx = CreateContext();
            ctx.Item = this;
            return _service.Save(ctx);
        }


        /// <summary>
        /// Deletes the model.
        /// </summary>
        /// <returns></returns>
        public virtual BoolMessage Delete()
        {
            ActionContext ctx = CreateContext();
            ctx.Item = this;
            return _service.Delete(ctx);
        }        
        #endregion


        #region ActiveRecord<T> Static Create/Update/Delete/Get Methods
        /// <summary>
        /// Creates a new instance of this DomainModelActiveRecord and 
        /// initializes it so that all of its necessary components are configured.
        /// Service, Repository, Settings, Validator etc.
        /// </summary>
        /// <returns></returns>
        public static T New()
        {
            T model = new T();
            ActiveRecordRegistration.InitModel<T>(model);
            return model;
        }


        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns></returns>
        public static BoolMessage Save(T entity)
        {
            ActionContext ctx = CreateContext();
            ctx.CombineMessageErrors = true;
            ctx.Item = entity;
            IEntityService<T> service = ActiveRecordRegistration.GetService<T>();
            return service.Save(ctx);
        }


        /// <summary>
        /// Retrieve the model associated with the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BoolMessageItem<T> Get(int id)
        {
            IEntityService<T> service = ActiveRecordRegistration.GetService<T>();
            ActionContext context = CreateContext();
            context.Id = id;
            BoolMessageItem<T> result = service.Get(context);
            ActiveRecordRegistration.InitModel<T>(result.Item, service);
            return result;
        }


        /// <summary>
        /// Retrieve all instances of model.
        /// </summary>
        /// <returns></returns>
        public static BoolMessageItem<IList<T>> GetAll()
        {
            IEntityService<T> service = ActiveRecordRegistration.GetService<T>();
            BoolMessageItem<IList<T>> result = service.GetAll(CreateContext());
            foreach (T item in result.Item)
            {
                ActiveRecordRegistration.InitModel<T>(item, service);
            }
            return result;
        }


        /// <summary>
        /// Delete the model associated with the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BoolMessage Delete(int id)
        {
            IEntityService<T> service = ActiveRecordRegistration.GetService<T>();
            ActionContext context = CreateContext();
            context.Id = id;
            return service.Delete(context);
        }
        #endregion


        #region ActiveRecord<T> Static Find Methods
        #endregion


        #region Private methods
        private static ActionContext CreateContext()
        {
            IActionContext ctx = ActiveRecordRegistration.GetContext(typeof(T).FullName);
            ctx.CombineMessageErrors = true;
            return ctx as ActionContext;
        }
        #endregion
    }
}
