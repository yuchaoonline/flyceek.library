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
using System.Linq;
using System.Text;
using ComLib.Queue;
using ComLib.EmailSupport;


namespace ComLib.Notifications
{

    /// <summary>
    /// Notification services.
    /// </summary>
    public class NotificationService 
    {
        private static INotificationAccountService _accountNotifyService;
        private static INotificationMessagingService _messageNotifyService;
        private static NotificationDefinitions _messageDefinitions;
        private static QueueProcessor<NotificationMessage> _queue;
        private static NotificationSettings _settings = new NotificationSettings();
        private static object _syncroot = new object();


        /// <summary>
        /// Initialize using default settings.
        /// </summary>
        public static void Init(IEmailService emailService, NotificationSettings settings)
        {
            _settings = settings;
            _messageDefinitions = new NotificationDefinitions(); 
            _queue = new NotificationQueueInMemory(_settings, emailService, _messageDefinitions);            
            _accountNotifyService = new NotificationAccountService(_queue, _settings);
            _messageNotifyService = new NotificationMessagingService(_queue, _settings);            
        }


        #region INotificationService Members
        /// <summary>
        /// Get/Set the settings.
        /// </summary>
        public static NotificationSettings Settings
        {
            get { return _settings; }
            set { _settings = value; }
        }


        /// <summary>
        /// The account services.
        /// </summary>
        public static INotificationAccountService AccountService
        {
            get { return _accountNotifyService; }
            set { _accountNotifyService = value; }
        }


        /// <summary>
        /// The notification message queue.
        /// </summary>
        public static QueueProcessor<NotificationMessage> Queue
        {
            get { return _queue; }
            set { _queue = value; }
        }


        /// <summary>
        /// Message definitions.
        /// </summary>
        public static NotificationDefinitions MessageDefs
        {
            get { return _messageDefinitions; }
        }


        /// <summary>
        /// Messaging service for send feedback, post, links to site.
        /// </summary>
        public static INotificationMessagingService MessageService
        {
            get { return _messageNotifyService; }
            set { _messageNotifyService = value; }
        }

        #endregion


        #region private members
        private INotificationAccountService GetAccountService()
        {
            if (_accountNotifyService == null)
            {
                lock (_syncroot)
                {
                    _accountNotifyService = new NotificationAccountService(_queue, _settings);
                    _accountNotifyService.Settings = _settings;
                }
            }
            return _accountNotifyService;
        }


        private INotificationMessagingService GetMessageService()
        {
            if (_messageNotifyService == null)
            {
                lock (_syncroot)
                {
                    _messageNotifyService = new NotificationMessagingService(_queue, _settings);
                    _messageNotifyService.Settings = _settings;
                }
            }
            return _messageNotifyService;
        }
        #endregion
    }
}
