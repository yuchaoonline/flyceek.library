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
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Threading;
using System.Timers;


namespace ComLib.TaskScheduling
{
    
    /// <summary>
    /// ITask interface to represent a task  that can be scheduled.
    /// The task is implemented as a class.
    /// </summary>
    public interface ITask
    {
        void Execute();
    }



    /// <summary>
    /// This is a bit awkward, I don't want any reference to Quartz
    /// </summary>
    public abstract class BaseTask : ITask //,IJob
    {

        #region IJob Members

        //public void Execute(JobExecutionContext context)
        //{
        //    Execute();
        //}

        #endregion

        #region ITask Members

        public abstract void Execute();
        #endregion
    }



    /// <summary>
    /// A task that can be scheduled, instead of having to 
    /// represent a class, it can simply be a method.
    /// </summary>
    public delegate void OnTaskExecute();



    /// <summary>
    /// Status of the task scheduler.
    /// </summary>
    public enum TaskSchedulerStatus { Started, Shutdown, NotStarted };



    /// <summary>
    /// Runs the task
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITaskScheduler
    {
        bool IsStarted { get; }
        bool IsShutDown { get; }

        void ScheduleTask(string name, string group, TaskTrigger trigger, bool start, OnTaskExecute method);
        void ScheduleTask(string name, string group, TaskTrigger trigger, bool start, ITask task);        
        void PauseTask(string name, string group);
        void ResumeTask(string name, string group);
        void DeleteTask(string name, string group);
        void PauseAll();
        void ResumeAll();
        string[] GetTasksNames(string group);

        void ShutDown();
    }
    


    /// <summary>
    /// Task trigger
    /// </summary>
    public class TaskTrigger
    {
        private int _interval;
        private DateTime _startTime;


        public TaskTrigger(int interval, DateTime startTime)
        {
            _interval = interval;
            _startTime = startTime;
        }


        #region ITaskTrigger Members

        public int Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }


        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        #endregion
    }



    /// <summary>
    /// Class representing the task to execute, it can either
    /// be a class that derives from ITask or a delegate.
    /// </summary>
    internal class TaskDetail
    {
        public TaskDetail(string name, string group, ITask task, TaskTrigger trigger)
        {
            Name = name;
            GroupName = group;
            Task = task;
            Trigger = trigger;
        }


        public void onTimerElapsed(object sender, EventArgs e)
        {
            if (IsTaskBased)
            {
                Task.Execute();
            }
            else
            {
                TaskMethod();
            }
        }


        public string Name;
        public string GroupName;
        public ITask Task;
        public OnTaskExecute TaskMethod;
        public bool IsTaskBased = true;
        public TaskTrigger Trigger;
        public System.Timers.Timer TimerInfo;
    }



    /// <summary>
    /// Some utility methods for the Task Scheduler.
    /// </summary>
    public class TaskSchedulerUtils
    {
        public static string GetGroupName(string fullyQualifiedTaskName)
        {
            int indexOfGroupSeparator = fullyQualifiedTaskName.IndexOf(".");
            if (indexOfGroupSeparator >= 0)
                return fullyQualifiedTaskName.Substring(0, indexOfGroupSeparator);

            return string.Empty;
        }


        public static string GetName(string fullyQualifiedTaskName)
        {
            int indexOfGroupSeparator = fullyQualifiedTaskName.IndexOf(".");
            if (indexOfGroupSeparator >= 0)
                return fullyQualifiedTaskName.Substring(indexOfGroupSeparator + 1);

            return fullyQualifiedTaskName;
        }
    }



    /// <summary>
    /// This is the Light-Weight task scheduler modeled after 
    /// the Quartz scheduler.
    /// This one uses timers exclusively.
    /// </summary>
    public class TaskScheduler : ITaskScheduler
    {
        private object _syncObject = new object();
        private OrderedDictionary _tasks;
        private TaskSchedulerStatus _status = TaskSchedulerStatus.NotStarted;


        public TaskScheduler()
        {
            Init();
        }


        private void Init()
        {
            _tasks = new OrderedDictionary();
            _status = TaskSchedulerStatus.Started;
        }


        #region ITaskScheduler Members
        /// <summary>
        /// Is started.
        /// </summary>
        public bool IsStarted
        {
            get 
            {
                lock (_syncObject)
                {
                    return _status == TaskSchedulerStatus.Started;
                }
            }
        }


        /// <summary>
        /// Is shut down.
        /// </summary>
        public bool IsShutDown
        {
            get 
            {
                lock (_syncObject)
                {
                    return _status == TaskSchedulerStatus.Shutdown;
                }
            }
        }
        

        /// <summary>
        /// Schedules the specified task via a delegate.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <param name="trigger"></param>
        /// <param name="start"></param>
        /// <param name="method"></param>
        public void ScheduleTask(string name, string group, TaskTrigger trigger, bool start, OnTaskExecute method)
        {
            TaskDetail taskDetail = new TaskDetail(name, group, null, trigger);            
            taskDetail.IsTaskBased = false;
            taskDetail.TaskMethod = method;            

            lock (_syncObject)
            {
                InternalScheduleTask(taskDetail);
            }
        }


        /// <summary>
        /// Schedule task using ITask object.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <param name="trigger"></param>
        /// <param name="start"></param>
        /// <param name="task"></param>
        public void ScheduleTask(string name, string group, TaskTrigger trigger, bool start, ITask task)
        {
            TaskDetail taskDetail = new TaskDetail(name, group, task, trigger);
            
            lock (_syncObject)
            {
                InternalScheduleTask(taskDetail);
            }
        }
        

        /// <summary>
        /// Pauses the task
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        public void PauseTask(string name, string group)
        {
            lock (_syncObject)
            {
                InternalPauseTask(name, group);
            }
        }


        /// <summary>
        /// Resumes the task
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        public void ResumeTask(string name, string group)
        {
            lock (_syncObject)
            {
                InternalResumeTask(name, group);
            }
        }


        /// <summary>
        /// Delete task
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        public void DeleteTask(string name, string group)
        {
            lock (_syncObject)
            {
                InternalDeleteTask(name, group);
            }
        }


        /// <summary>
        /// Gets all the active tasks in the schedule.
        /// BUG: Currently does not return the task name that are associated 
        /// with the group name.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public string[] GetTasksNames(string group)
        {
            lock (_syncObject)
            {
                ICollection keyCollection = _tasks.Keys;
                string[] keys = new String[_tasks.Count];
                keyCollection.CopyTo(keys, 0);
                return keys;
            }
        }


        /// <summary>
        /// Pause all tasks
        /// </summary>
        public void PauseAll()
        {
            lock (_syncObject)
            {
                InternalPauseAll();
            }
        }


        /// <summary>
        /// Resume all tasks
        /// </summary>
        public void ResumeAll()
        {
            lock (_syncObject)
            {
                InternalResumeAll();
            }
        }


        /// <summary>
        /// Shuts down the scheduler.
        /// </summary>
        public void ShutDown()
        {
            lock (_syncObject)
            {
                InternalPauseAll();
                _status = TaskSchedulerStatus.Shutdown;
            }
        }
        #endregion


        private string GetFullyQualifiedTaskName(string name, string group)
        {
            if (string.IsNullOrEmpty(group))
                return name;

            return group + "." + name;
        }


        private void InternalScheduleTask(TaskDetail taskDetail)
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = taskDetail.Trigger.Interval;
            timer.Elapsed += taskDetail.onTimerElapsed;
            taskDetail.TimerInfo = timer;

            string fullName = GetFullyQualifiedTaskName(taskDetail.Name, taskDetail.GroupName);
            _tasks.Add(fullName, taskDetail);
            taskDetail.TimerInfo.Start();
        }


        private void InternalPauseTask(string name, string group)
        {
            TaskDetail taskDetail = _tasks[GetFullyQualifiedTaskName(name, group)] as TaskDetail;
            if (taskDetail != null)
            {
                taskDetail.TimerInfo.Stop();
            }
        }


        private void InternalResumeTask(string name, string group)
        {
            TaskDetail taskDetail = _tasks[GetFullyQualifiedTaskName(name, group)] as TaskDetail;
            if (taskDetail != null)
            {
                taskDetail.TimerInfo.Start();
            }
        }


        private void InternalDeleteTask(string name, string group)
        {
            string taskName = GetFullyQualifiedTaskName(name, group);
            TaskDetail taskDetail = _tasks[taskName] as TaskDetail;

            if (taskDetail != null)
            {
                taskDetail.TimerInfo.Stop();
                _tasks.Remove(taskName);
            }
        }


        private void InternalResumeAll()
        {
            for (int ndx = 0; ndx < _tasks.Count; ndx++)
            {
                TaskDetail taskDetail = _tasks[ndx] as TaskDetail;
                InternalResumeTask(taskDetail.Name, taskDetail.GroupName);
            }
        }


        private void InternalPauseAll()
        {
            for (int ndx = 0; ndx < _tasks.Count; ndx++)
            {
                TaskDetail taskDetail = _tasks[ndx] as TaskDetail;
                InternalPauseTask(taskDetail.Name, taskDetail.GroupName);
            }
        }
    }    
}
