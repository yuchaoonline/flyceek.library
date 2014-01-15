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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ComLib;

using ComLib.Entities;


namespace ComLib.NamedQueries
{
    /// <summary>
    /// Active record functionality for NamedQuery
    /// </summary>
    public partial class NamedQuerys : ActiveRecord<NamedQuery> 
    {   /*
        /// <summary>
        /// 1. Initialize static components.
        /// 2. Registrer with ActiveRecordRegistration so services
        /// can be created at runtime dynamically for any entity.
        /// </summary>        
        static NamedQuerys()
        {
            Init();
        }

        
        /// <summary>
        /// Register Article type with the EntityManager so that
        /// 1. Entity manager makes callbacks for creating the service.
        /// 2. Entity manager makes callbacks for creating the repository.
        /// 3. Setup wiring for ActiveRecord functionality.
        /// </summary>        
        public static void Init()
        {
            Init(null);
        }


        /// <summary>
        /// Register Article type with the EntityManager so that
        /// 1. Entity manager makes callbacks for creating the service.
        /// 2. Entity manager makes callbacks for creating the repository.
        /// 3. Setup wiring for ActiveRecord functionality.
        /// </summary>        
        /// <param name="repositoryCreator"></param>
        public static void Init(Func<object> repositoryCreator)
        {
            // Reset the created components to null so they can be initialized again.
            Reset();

            if (repositoryCreator == null)
                repositoryCreator = new Func<object>(CreateRepository);

            EntityRegistration.Register(typeof(NamedQuery), new Func<object, object>(CreateService), repositoryCreator);
            InitComponents();            
        }


        /// <summary>
        /// Creates a new instance of BlogPost and 
        /// initializes it with a validator and settings.
        /// </summary>
        /// <returns></returns>
        public static NamedQuery New()
        {
            NamedQuery entity = new NamedQuery();
            entity.Validator = new NamedQueryValidator();
            entity.Settings = _settings;
            return entity;
        }


        /// <summary>
        /// Create new Instance of NamedQueryService.
        /// </summary>
        /// <returns></returns>
        public static IEntityService<NamedQuery> CreateService(object repository)
        {
            if (!_useSingletonService)
                return CreateServiceInternal(repository);

            return Create<IEntityService<NamedQuery>>(ref _service, _syncroot, () => CreateServiceInternal(repository));
        }        


        /// <summary>
        /// Create new instance of NamedQueryService
        /// </summary>
        /// <returns></returns>
        protected static void InitComponents()
        {
            Create<IEntityRepository<NamedQuery>>(ref _repository, _syncroot, () => EntityRegistration.GetRepository<NamedQuery>());
            Create<IEntityResources>(ref _resources, _syncroot, () => new NamedQueryResources());
            Create<IEntitySettings<NamedQuery>>(ref _settings, _syncroot, () => new NamedQuerySettings());
        }


        /// <summary>
        /// Create a new NamedQueryService.
        /// </summary>
        /// <returns></returns>
        protected static IEntityService<NamedQuery> CreateServiceInternal(object repository)
        {
            NamedQueryService service = new NamedQueryService();
            service.Validator = new NamedQueryValidator();
            service.Repository = repository == null ? (IEntityRepository<NamedQuery>)CreateRepository() : (IEntityRepository<NamedQuery>)repository;            
            service.Settings = _settings;
            service.Resources = _resources;
            return service;
        }


        /// <summary>
        /// Create instance of repository.
        /// </summary>
        /// <returns></returns>
        public static IEntityRepository<NamedQuery> CreateRepository()
        {
            IEntityRepository<NamedQuery> repository = null;

            if(!_useSingletonRepository)
            {
                repository = RepositoryFactory.Create<NamedQuery, IEntityRepository<NamedQuery>>(() => new NamedQueryRepository());                
            }

            repository = Create<IEntityRepository<NamedQuery>>(ref _repository, _syncroot, () => RepositoryFactory.Create<NamedQuery, IEntityRepository<NamedQuery>>(() => new NamedQueryRepository()));
            repository.RowMapper = new NamedQueryRowMapper();
            return repository;
        }


        #region Private methods
        /// <summary>
        /// Reset the components.
        /// </summary>
        private static void Reset()
        {
            lock (_syncroot)
            {
                _service = null;
                _repository = null;
                _resources = null;
                _settings = null;
            }
        }
        #endregion



        #region Private data 
        private static bool _useSingletonService = false;
        private static bool _useSingletonRepository = true;
        private static IEntityService<NamedQuery> _service = null;
        private static IEntityRepository<NamedQuery> _repository = null;
        private static IEntitySettings<NamedQuery> _settings = null;
        private static IEntityResources _resources = null;
        private static object _syncroot = new object();
        #endregion
        */
    }
}
