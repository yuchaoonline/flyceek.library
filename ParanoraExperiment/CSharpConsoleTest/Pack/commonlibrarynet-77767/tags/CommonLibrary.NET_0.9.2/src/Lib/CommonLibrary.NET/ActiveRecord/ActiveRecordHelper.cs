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

using CommonLibrary.DomainModel;


namespace CommonLibrary
{
    /// <summary>
    /// This is a simple STATIC class to provide active record (CRUD) functionality
    /// on ANY IEntityPersistant
    /// NOTE: 
    /// You can also use the ActiveRecordDomainObject base class
    /// and derive from that to have a more robust ActiveRecord implemetation
    /// on your domain objects.
    /// <see cref="CommonLibrary.DomainModel.ActiveRecordDomainObject"/>
    /// </summary>
    /// <example>
    ///     
    ///     // Using static class.
    ///     
    ///     BlogPost post = ActiveRecord<BlogPost>.Retrieve(22);
    ///     ActiveRecordHelper<BlogPost>.Save(post);
    ///     ActiveRecordHelper<BlogPost>.Delete(22);
    ///     
    /// 
    ///     // Using Derived class.
    ///     
    ///     BlogPost post = BlogPost.New();
    ///     post.Title = "New movie just came out tonight.";
    ///     post.Details = "Movie just opened tonight with rave reviews.";
    ///     post.Save();
    ///     
    /// </example>
    /// <typeparam name="TId">The type of the id.</typeparam>
    /// <typeparam name="T"></typeparam>
    public class ActiveRecordHelper<T> where T : IEntity
    {

        #region ActiveRecord CRUD functionality        
        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns></returns>
        public static BoolMessage Create(T entity)
        {
            ActionContext ctx = new ActionContext(entity, true);
            IEntityService<T> service = ActiveRecordRegistration.GetService<T>();
            return service.Save(ctx);
        }


        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns></returns>
        public static BoolMessage Update(T entity)
        {
            ActionContext ctx = new ActionContext(entity, true);
            IEntityService<T> service = ActiveRecordRegistration.GetService<T>();
            return service.Update(ctx);
        }


        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns></returns>
        public static BoolMessage Save(T entity)
        {
            ActionContext ctx = new ActionContext(entity, true);
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
            return service.Get(new ActionContext(id, true));            
        }


        /// <summary>
        /// Retrieve all instances of model.
        /// </summary>
        /// <returns></returns>
        public static BoolMessageItem<IList<T>> GetAll()
        {
            IEntityService<T> service = ActiveRecordRegistration.GetService<T>();
            return service.GetAll(new ActionContext());
        }


        /// <summary>
        /// Delete the model associated with the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BoolMessage Delete(int id)
        {
            IEntityService<T> service = ActiveRecordRegistration.GetService<T>();            
            return service.Delete(new ActionContext(id, true));
        }
        #endregion
    }
}