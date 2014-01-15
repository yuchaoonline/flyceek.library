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

using System.Collections.Generic;

namespace CommonLibrary
{
    /// <summary>
    /// Encapsulate all the various inputs for performing validation on an object.
    /// </summary>
    public class ValidationEvent
    {
        /// <summary>
        /// The object to validate.
        /// </summary>
        public object Target;


        /// <summary>
        /// The results to store validation errors.
        /// </summary>
        public IValidationResults Results;


        /// <summary>
        /// Other contextual data that could be supplied.
        /// </summary>
        public object Context;


        /// <summary>
        /// Initialize data.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="useTarget"></param>
        /// <param name="results"></param>
        /// <param name="context"></param>
        public ValidationEvent(object target, IValidationResults results, object context)
        {
            Target = target;
            Results = results;
            Context = context;
        }


        /// <summary>
        /// Initialize data.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="useTarget"></param>
        /// <param name="results"></param>
        /// <param name="context"></param>
        public ValidationEvent(object target, IValidationResults results)
        {
            Target = target;
            Results = results;
        }
    }



    /// <summary>
    /// Base class for any validator.
    /// </summary>
    public abstract class ValidatorBase : IValidator
    {
        protected string _message;
        protected object _target;
        protected IValidationResults _lastValidationResults;


        #region IValidator Members
        /// <summary>
        /// The object to validate.
        /// </summary>
        public object Target
        {
            get { return _target; }
            set { _target = value; }
        }


        /// <summary>
        /// Message to use for the description of an error.
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }


        /// <summary>
        /// Simple true/false to indicate if validation passed.
        /// </summary>
        public bool IsValid
        {
            get { return Validate().IsValid; }
        }


        /// <summary>
        /// The results of the last validation.
        /// </summary>
        public IValidationResults Results
        {
            get { return _lastValidationResults; }
        }


        /// <summary>
        /// Validate data using data provided during initialization/construction.
        /// </summary>
        /// <returns></returns>
        public virtual IValidationResults Validate()
        {
            _lastValidationResults = new ValidationResults() as IValidationResults;
            Validate(Target, _lastValidationResults);
            return _lastValidationResults;
        }


        /// <summary>
        /// Validate using the object provided.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public virtual IValidationResults ValidateTarget(object target)
        {
            _lastValidationResults = new ValidationResults() as IValidationResults;
            Validate(new ValidationEvent(target, _lastValidationResults));
            return _lastValidationResults;
        }


        /// <summary>
        /// Validate using the results collection provided.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public virtual IValidationResults Validate(IValidationResults results)
        {
            Validate(new ValidationEvent(Target, results));
            return results;
        }


        /// <summary>
        /// Validate using the object provided, and add errors to the results list provided.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="results"></param>
        public bool Validate(object target, IValidationResults results)
        {
            return Validate(new ValidationEvent(target, results));
        }
        #endregion


        /// <summary>
        /// This Method will call the ValidateInternal method of this validator.
        /// </summary>
        /// <remarks>
        /// The reason that the ValidateInternal method is NOT called directly by the
        /// other Validate methods is because the CodeGenerator generates the Validation
        /// code inside of the ValidateInternal method.
        /// If a client wants to override the validation while sill leveraging the autogenerated
        /// validation code, it can be done by overrideing this method and calling the
        /// ValidateInternal method.
        /// This allows a lot of flexibility for codegeneration.
        /// </remarks>
        /// <param name="target"></param>
        /// <param name="useTarget"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        public virtual bool Validate(ValidationEvent validationEvent)
        {
            return ValidateInternal(validationEvent);
        }


        /// <summary>
        /// Implement this method.
        /// </summary>
        /// <param name="validationEvent"></param>
        /// <returns></returns>
        protected abstract bool ValidateInternal(ValidationEvent validationEvent);


        /// <summary>
        /// Add a new result to the list of errors.
        /// </summary>
        /// <param name="results"></param>
        /// <param name="key"></param>
        /// <param name="message"></param>
        /// <param name="target"></param>
        protected void AddResult(IValidationResults results, string key, string message, object target)
        {
            results.Add(key, message, target);
        }
    }
}
