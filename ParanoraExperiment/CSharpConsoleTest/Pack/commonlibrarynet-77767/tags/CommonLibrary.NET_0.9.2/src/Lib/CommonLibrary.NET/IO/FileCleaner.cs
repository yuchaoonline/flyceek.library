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
using System.IO;
using System.Reflection;


namespace CommonLibrary
{
    /// <summary>
    /// Arguments for cleaning directories/files.
    /// </summary>
    public class FileCleanArgs
    {
        [Arg("recurse", "Recurse into subdirectories", typeof(bool), false, false, "true|false")]
        public bool Recurse { get; set; }


        [Arg("pattern", "Only process files with name matching pattern", typeof(string), false, ".svn", ".svn|.obj")]
        public string Pattern { get; set; }


        [Arg("filetype", "Indicate whether to handle only files/directories or both", typeof(string), false, "dir", "dir|file|all")]
        public string FileType { get; set; }


        [Arg("dryrun", "Indicate only showing what will happen without running.", typeof(bool), false, false, "true|false")]
        public bool DryRun { get; set; }


        [Arg("rootdir", "c:\\temp\" -Starting directory where cleaning should happen. If not specified, current directory is assumed.", typeof(string), false, ".", @".|..\|c:\temp")]
        public string RootDir { get; set; }


        [Arg("outfile", "fileclean.txt - Name of the file to write the output to.", typeof(string), false, "FileClean.txt", @"fileClean.txt|c:\temp\fileclean.txt")]
        public string OutputFile { get; set; }
    }


    
    /// <summary>
    /// Cleans directories / files.
    /// </summary>
    public class FileCleaner : ShellCommandBase, IShellCommand
    {
        private FileCleanArgs _args;
        private StringBuilder _buffer;


        /// <summary>
        /// File Cleaner.
        /// </summary>
        public FileCleaner()
        {
            Init();
        }


        /// <summary>
        /// Initialize
        /// </summary>
        public virtual void Init()
        {
            _programArgs = new FileCleanArgs();
            _args = _programArgs as FileCleanArgs;
            _buffer = new StringBuilder();
        }


        #region IShellCommand Members
        /// <summary>
        /// Execute cleaning of files/directories.
        /// Doesn't actually delete anything but generates a file
        /// containing the commands.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public BoolMessageItem Execute(string[] args)
        {
            // Parse the arguments.
            ArgsParser.Parse(args, "-", ":", _programArgs);
            
            // Initialize the starting directory.
            InitializeDir(_args.RootDir);

            StringBuilder buffer = new StringBuilder();
            bool handleFiles = true;
            if (_args.FileType == "dir")
                handleFiles = false;

            FileSearcher searcher = new FileSearcher(new Action<FileInfo>(HandleFile), new Action<DirectoryInfo>(HandleDirectory), "**/**", handleFiles);
            searcher.Search(_rootDirectory);
            File.WriteAllText(_args.OutputFile, _buffer.ToString());
            return new BoolMessageItem(null, true, string.Empty);
        }


        /// <summary>
        /// Handle the directory.
        /// </summary>
        /// <param name="directory"></param>
        protected virtual void HandleDirectory(DirectoryInfo directory)
        {
            if (!_args.DryRun)
            {
                _buffer.Append("rmdir \"" + directory.FullName + "\" /s /q" + Environment.NewLine);
            }
            else
            {
                _buffer.Append("Dry run - cleaning : " + directory.FullName + " /s /q " + Environment.NewLine);
            }
        }


        /// <summary>
        /// Handle the file.
        /// </summary>
        /// <param name="file"></param>
        protected virtual void HandleFile(FileInfo file)
        {
            if (!_args.DryRun)
            {
                _buffer.Append("del \"" + file.FullName + "\" /f /q" + Environment.NewLine);
            }
            else
            {
                _buffer.Append("Dry run - cleaning : " + file.FullName + " /f /q" + Environment.NewLine);
            }
        }
        #endregion
    }
}
