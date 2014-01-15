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
    /// Base class for notifications.
    /// </summary>
    public class NotificationServiceBase
    {
        protected QueueProcessor<NotificationMessage> _queue = null;
        protected NotificationSettings _settings;


        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="settings"></param>
        public NotificationServiceBase(QueueProcessor<NotificationMessage> queue, NotificationSettings settings)
        {
            _settings = settings;
            _queue = queue;
        }


        /// <summary>
        /// Notification settings.
        /// </summary>
        public NotificationSettings Settings
        {
            get { return _settings; }
            set { _settings = value; }
        }


        /// <summary>
        /// The notification queue.
        /// </summary>
        public QueueProcessor<NotificationMessage> Queue
        {
            get { return _queue; }
            set { _queue = value; }
        }


        /// <summary>
        /// Queue the notification.
        /// Don't just send it directly.
        /// </summary>
        /// <param name="message"></param>
        protected void Notify(NotificationMessage message)
        {
            _queue.Enqueue(message);
        }
    }



    /// <summary>
    /// The notification service for sending feedback, sending to a friend.
    /// </summary>
    public class NotificationAccountService : NotificationServiceBase, INotificationAccountService
    {
        public NotificationAccountService(QueueProcessor<NotificationMessage> queue, NotificationSettings settings)
            : base(queue, settings)
        {
        }


        #region INotificationAccountService Members
        /// <summary>
        /// Queues the notification to email to a friend.
        /// ${message.to}
        /// ${message.from}
        /// ${message.subject}
        /// ${message.briefmessage}
        /// </summary>
        /// <param name="sendToFriendData"></param>
        /// <summary>
        /// Welcome a new user.
        /// </summary>
        /// <param name="ctx"></param>
        public void WelcomeNewUser(string to, string subject, string firstname, string username, string password)
        {
            NotificationContext ctx = new NotificationContext();
            ctx.Values["message.to"] = to;
            ctx.Values["message.subject"] = subject;
            ctx.Values["firstname"] = "kishore";
            ctx.Values["username"] = "kuser1";
            ctx.Values["password"] = "password";
            WelcomeNewUser(ctx);
        }


        /// <summary>
        /// Welcome new user using Notification context.
        /// </summary>
        /// <param name="ctx"></param>
        public void WelcomeNewUser(NotificationContext ctx)
        {
            Notify(new NotificationMessage(ctx.Values, ctx.Values["message.to"], string.Empty, 
                ctx.Values["message.subject"], "WelcomeNewUser"));            
        }


        /// <summary>
        /// Send remind account/password email to user.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="firstname"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void RemindUserPassword(string to, string subject, string firstname, string username, string password)
        {
            NotificationContext ctx = new NotificationContext();
            ctx.Values["message.to"] = to;
            ctx.Values["message.subject"] = subject;
            ctx.Values["firstname"] = "kishore";
            ctx.Values["username"] = "kuser1";
            ctx.Values["password"] = "password";
            RemindUserPassword(ctx);
        }

        /// <summary>
        /// Submits the feedback notification.
        /// ${message.to}
        /// ${message.from}
        /// ${message.subject}
        /// ${message.briefmessage}
        /// ${from.name}
        /// ${post.url}
        /// ${post.title}
        /// ${friend.name}
        /// ${friend.email}
        /// </summary>
        /// <param name="feedBack"></param>
        /// <summary>
        public void RemindUserPassword(NotificationContext ctx)
        {
            Notify(new NotificationMessage(ctx.Values, ctx.Values["message.to"], string.Empty,
                ctx.Values["message.subject"], "ForgotPassword"));             
        }
        #endregion
    }
    


    /// <summary>
    /// The notification service for sending feedback, sending to a friend.
    /// </summary>
    public class NotificationMessagingService : NotificationServiceBase, INotificationMessagingService
    {
        public NotificationMessagingService(QueueProcessor<NotificationMessage> queue, NotificationSettings settings ) 
            : base(queue, settings)
        {
        }


        #region INotificationMessagingService Members
        /// <summary>
        /// Send the website url to a friend.
        /// </summary>
        /// <param name="toEmail"></param>
        /// <param name="subject"></param>
        /// <param name="friendName"></param>
        /// <param name="fromName"></param>
        /// <param name="messageFromFriend"></param>
        public void SendToFriend(string toEmail, string subject, string friendName, string fromName, string messageFromFriend)
        {
            NotificationContext ctx = new NotificationContext();
            ctx.Values["message.to"] = toEmail;
            ctx.Values["message.subject"] = subject;
            ctx.Values["firstname"] = friendName;
            ctx.Values["from.name"] = fromName;
            ctx.Values["message.briefmessage"] = messageFromFriend;
            SendToFriend(ctx);
        }


        /// <summary>
        /// Queues the notification to email to a friend.
        /// ${message.to}
        /// ${message.from}
        /// ${message.subject}
        /// ${message.briefmessage}
        /// ${from.name}
        /// ${post.url}
        /// ${post.title}
        /// ${friend.name}
        /// ${friend.email}
        /// </summary>
        /// <param name="sendToFriendData"></param>
        public void SendToFriend(NotificationContext ctx)
        {
            Notify(new NotificationMessage(
                ctx.Values, ctx.Values["message.to"], string.Empty,
                ctx.Values["message.subject"], "SendWebSite"));
        }


        /// <summary>
        /// Submits the feedback notification.
        /// ${message.to}
        /// ${message.from}
        /// ${message.subject}
        /// ${message.briefmessage}
        /// ${from.name}
        /// ${post.url}
        /// ${post.title}
        /// ${friend.name}
        /// ${friend.email}
        /// </summary>
        /// <param name="feedBack"></param>
        public void SubmitFeedBack(NotificationContext ctx)
        {
            Notify(new NotificationMessage(
                ctx.Values, ctx.Values["message.to"], string.Empty,
                ctx.Values["message.subject"], "SubmitFeedback"));
        }


        /// <summary>
        /// Send an email to a friend with a link to a post on the page.
        /// </summary>
        /// <param name="toEmail"></param>
        /// <param name="subject"></param>
        /// <param name="toFirstname"></param>
        /// <param name="fromName"></param>
        /// <param name="messageToFriend"></param>
        /// <param name="postTitle"></param>
        /// <param name="postUrl"></param>
        public void SendToFriendPost(string toEmail, string subject, string toFirstname,
            string fromName, string messageToFriend, string postTitle, string postUrl)
        {
            NotificationContext ctx = new NotificationContext();            
            ctx.Values["firstname"] = toFirstname;
            ctx.Values["post.title"] = postTitle;
            ctx.Values["post.url"] = postUrl;
            ctx.Values["from.name"] = fromName;
            ctx.Values["message.briefmessage"] = messageToFriend;
            ctx.Values["message.subject"] = subject;
            ctx.Values["message.to"] = toEmail;
            SendToFriendPost(ctx);
        }


        /// <summary>
        /// Sends a link to a post to a friend.
        /// ${message.to}
        /// ${message.from}
        /// ${message.subject}
        /// ${message.briefmessage}
        /// ${from.name}
        /// ${post.url}
        /// ${post.title}
        /// ${friend.name}
        /// ${friend.email}
        /// </summary>
        /// <param name="placeHolderValues">Dictionary of keys representing the names of the 
        /// placeholders to replace in the content file.</param>
        public void SendToFriendPost(NotificationContext ctx)
        {
            Notify(new NotificationMessage(
                ctx.Values, ctx.Values["message.to"], string.Empty,
                ctx.Values["message.subject"], "SendWebSitePost"));
        }
        #endregion
    }
}
