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
        private IService<T> _service = null;


        #region IActiveRecord<T> Members
        /// <summary>
        /// The underlying service that handles model actions.
        /// </summary>
        /// <value></value>
        public IService<T> Service
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
            return _service.Create(ctx);
        }


        /// <summary>
        /// Updates the model.
        /// </summary>
        /// <returns></returns>
        public virtual BoolMessage Update()
        {
            ActionContext ctx = CreateContext();
            return _service.Update(ctx);
        }


        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns></returns>
        public virtual BoolMessage Save()
        {
            ActionContext ctx = CreateContext();
            return _service.Save(ctx);
        }


        /// <summary>
        /// Deletes the model.
        /// </summary>
        /// <returns></returns>
        public virtual BoolMessage Delete()
        {
            ActionContext ctx = CreateContext();
            return _service.Delete(ctx);
        }


        private ActionContext CreateContext()
        {
            ActionContext ctx = new ActionContext(this);
            ctx.CombineMessageErrors = true;
            return ctx;
        }
        #endregion


        #region ActiveRecord Static Methods
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
            ActionContext ctx = new ActionContext();
            ctx.CombineMessageErrors = true;
            ctx.Item = entity;
            IService<T> service = ActiveRecordRegistration.GetService<T>();
            return service.Save(ctx);
        }


        /// <summary>
        /// Retrieve the model associated with the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BoolMessageItem<T> Retrieve(int id)
        {
            IService<T> service = ActiveRecordRegistration.GetService<T>();
            return service.Get(new ActionContext(id, true));
        }


        /// <summary>
        /// Retrieve all instances of model.
        /// </summary>
        /// <returns></returns>
        public static BoolMessageItem<IList<T>> RetrieveAll()
        {
            IService<T> service = ActiveRecordRegistration.GetService<T>();
            return service.GetAll(new ActionContext());
        }


        /// <summary>
        /// Delete the model associated with the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BoolMessage Delete(int id)
        {
            IService<T> service = ActiveRecordRegistration.GetService<T>();
            return service.Delete(new ActionContext(id, true));
        }
        #endregion
    }
}
