using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComLib.Database;


namespace ComLib.Models
{
    public struct StringClob
    {
        public override string ToString()
        {
            return "StringClob";
        }
    }



    public class CustomType
    {
        public CustomType(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }



    public enum DbCreateType
    {
        DropCreate,
        Create,
        Update
    }



    public class ModelBuilderSettings
    {
        /// <summary>
        /// Database connectio used to create the tables associated with a model.
        /// </summary>
        public ConnectionInfo Connection { get; set; }


        /// <summary>
        /// Assembly name.
        /// </summary>
        public string AssemblyName { get; set; }


        /// <summary>
        /// Location of the generated code.
        /// </summary>
        public string ModelCodeLocation { get; set; }


        /// <summary>
        /// Location of the templates for code generation
        /// </summary>
        public string ModelCodeLocationTemplate { get; set; }


        /// <summary>
        /// Location where the sql schema files are created.
        /// </summary>
        public string ModelInstallLocation { get; set; }


        /// <summary>
        /// Location where the sql schema files are created.
        /// </summary>
        public string ModelCodeLocationUI { get; set; }


        /// <summary>
        /// Location where the UI templates are located.
        /// </summary>
        public string ModelCodeLocationUITemplate { get; set; }


        /// <summary>
        /// Location where orm mapping file should be created.
        /// </summary>
        public string ModelOrmLocation { get; set; }


        /// <summary>
        /// Location of the stored procedure templates.
        /// </summary>
        public string ModelDbStoredProcTemplates { get; set; }


        /// <summary>
        /// Location where orm mapping file should be created.
        /// </summary>
        public DbCreateType DbAction_Create { get; set; }


        /// <summary>
        /// Definition of how to generate ORM mappings.
        /// </summary>
        public OrmGeneration OrmGenerationDef { get; set; }
    }


    /// <summary>
    /// Collection of models.
    /// </summary>
    public class ModelContainer
    {
        private List<Model> _modelList = new List<Model>();
        private Dictionary<string, Model> _modelMap = new Dictionary<string, Model>();

        /// <summary>
        /// Initalize 
        /// </summary>
        public ModelContainer() { }


        /// <summary>
        /// Map of all the models.
        /// </summary>
        public Dictionary<string, Model> ModelMap { get { return _modelMap; } }


        


        /// <summary>
        /// Used to assign a collection of properties at once.
        /// </summary>
        public List<Model> AllModels
        {
            get { return _modelList; }
            set
            {
                Add(value);
            }
        }


        private ModelBuilderSettings _settings = new ModelBuilderSettings();
        public ModelBuilderSettings Settings
        {
            get { return _settings; }
            set { _settings = value; }
        }


        /// <summary>
        /// Additional settings to make it easy to add new settings dynamically.
        /// Also allows for inheritance.
        /// </summary>
        public Dictionary<string, object> ExtendedSettings { get; set; }


        /// <summary>
        /// Add a single model to collection.
        /// </summary>
        /// <param name="model"></param>
        public void Add(Model model)
        {
            Add(new List<Model>(){ model });
        }


        /// <summary>
        /// Add a collection of models.
        /// </summary>
        /// <param name="models"></param>
        public void Add(List<Model> models)
        {
            foreach (Model model in models)
            {
                _modelMap[model.Name] = model;
                _modelList.Add(model);
            }
        }
    }



    /// <summary>
    /// DomainModel representing class/table mappings.
    /// </summary>
    public class Model
    {
        public Model()
        {
        }


        public Model(string name)
        {
            Name = name;
        }


        public string Name { get; set; }
        public string TableName { get; set; }
        public string NameSpace { get; set; }
        public string Inherits { get; set; }
        public bool GenerateTable { get; set; }
        public bool GenerateOrMap { get; set; } 
        public bool GenerateCode { get; set; }
        public bool GenerateTests { get; set; }
        public bool GenerateUI { get; set; }
        public bool GenerateRestApi { get; set; }
        public bool GenerateFeeds { get; set; }
        public bool IsWebUI { get; set; }
        public List<PropertyInfo> Properties { get; set; }
        public int PropertiesSortOrder { get; set; }
        public string ExcludeFiles { get; set; }

        /// <summary>
        /// Get /set the repository type.
        /// </summary>
        public string RepositoryType { get; set; }


        /// <summary>
        /// List of names of model whose properties to include.
        /// </summary>
        public List<Include> Includes { get; set; }


        /// <summary>
        /// List of objects that compose this model.
        /// </summary>
        public List<Composition> ComposedOf { get; set; }


        public List<UISpec> UI { get; set; }


        /// <summary>
        /// One-to-many relationships.
        /// </summary>
        public List<Relation> HasMany { get; set; }


        /// <summary>
        /// Validations to perform on entity.
        /// </summary>
        public List<ValidationItem> Validations { get; set; }


        /// <summary>
        /// List of data massage items to apply.
        /// </summary>
        public List<DataMassageItem> DataMassages { get; set; }


        /// <summary>
        /// List of roles that can manage ( delete ) instances of this model.
        /// </summary>
        /// <example>ManagedBy = new List(string){ "Owner", "Moderator", "Admin" }; </example>
        public List<string> ManagedBy { get; set; }


        /// <summary>
        /// List of properties that can be used to lookup up an entity.
        /// e.g. These should typically be the Id ( integer ) and "Name" ( string )
        /// </summary>
        /// <example>LookupOn = new List(string){ "Id", "Name" };</example>
        public List<string> LookupOn { get; set; }


        /// <summary>
        /// Assembly name.
        /// </summary>
        public string AssemblyName { get; set; }


        /// <summary>
        /// Additional settings to make it easy to add new settings dynamically.
        /// Also allows for inheritance.
        /// </summary>
        public Dictionary<string, string> Settings { get; set; }
    }



    /// <summary>
    /// Composition information.
    /// </summary>
    public class Include
    {
        public Include(string refModel)
        {
            Name = refModel;
            GenerateOrMap = true;
            GenerateCode = true;
            GenerateUI = true;
        }


        /// <summary>
        /// Name of the model that in the <see cref="ModelContainer"/> that
        /// represents this composition.
        /// </summary>
        public string Name { get; set; }
        public bool GenerateOrMap { get; set; }
        public bool GenerateCode { get; set; }
        public bool GenerateUI { get; set; }
        public Model ModelRef { get; set; }
    }



    /// <summary>
    /// Composition information.
    /// </summary>
    public class Composition : Include
    {
        public Composition(string refModel) : base(refModel) { }
    }



    /// <summary>
    /// Validation definition for a specific property.
    /// </summary>
    public class ValidationItem
    {
        /// <summary>
        /// Initialize the validator the property.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="validator"></param>
        public ValidationItem(string property, Type validator)
        {
            PropertyToValidate = property;
            PropertyValidator = validator;
        }


        /// <summary>
        /// The name of the property on the entity to validate.
        /// </summary>
        public string PropertyToValidate { get; set; }


        /// <summary>
        /// The datatype of the validator to use for validating this property.
        /// </summary>
        public Type PropertyValidator { get; set; }


        /// <summary>
        /// Whether or not the validator is instance based or can be statically called.
        /// </summary>
        public bool IsStatic { get; set; }
    }



    /// <summary>
    /// Specification for a data masager.
    /// </summary>
    public class DataMassageItem
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public DataMassageItem(string propertyName, Type dataMassagerType, Massage sequence)
        {
            PropertyToMassage = propertyName;
            DataMassager = dataMassagerType;
            Sequence = sequence;
        }


        /// <summary>
        /// A data massager for a specific entity for CRUD operations.
        /// </summary>
        public Type DataMassager { get; set; }


        /// <summary>
        /// The property to massage.
        /// </summary>
        public string PropertyToMassage { get; set; }


        /// <summary>
        /// Whether or not the data massager is instance based or can be statically called.
        /// </summary>
        public bool IsStatic { get; set; }


        /// <summary>
        /// When to massage.
        /// </summary>
        public Massage Sequence { get; set; }
    }



    public enum Massage
    {
        /// <summary>
        /// Indicates to massage data before validation.
        /// </summary>
        BeforeValidation,


        /// <summary>
        /// Indicates to massage data after validation.
        /// </summary>
        AfterValidation
    }



    /// <summary>
    /// Orm generation instruction.
    /// </summary>
    public class OrmGeneration
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="replace"></param>
        /// <param name="startTag"></param>
        /// <param name="endTag"></param>
        public OrmGeneration(bool replace, string startTag, string endTag)
        {
            Replace = replace;
            StartTag = startTag;
            EndTag = endTag;
        }


        /// <summary>
        /// Whether or not to replace the orm file or generate it.
        /// </summary>
        public bool Replace { get; set; }


        /// <summary>
        /// Starting tag so codegeneration knows where to start the replacement.
        /// </summary>
        public string StartTag { get; set; }


        /// <summary>
        /// Ending tag so code generation knows where to stop the replacement.
        /// </summary>
        public string EndTag { get; set; }
    }
}
