using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;


namespace ComLib.FTP
{

    /// <summary>
    /// Ftp Interface
    /// </summary>
    public interface IFtp
    {
        /// <summary>
        /// Open an ftp connection.
        /// </summary>
        void Open();


        /// <summary>
        /// Close the ftp connection but keeps session.
        /// </summary>
        void Close();


        /// <summary>
        /// Quit the ftp connection.
        /// </summary>
        void Quit(bool waitForReply);
        

        /// <summary>
        /// Get the file from the ftp server.
        /// </summary>
        /// <param name="remoteFile">Name of the remote file.</param>
        /// <param name="localFile">Name of the local file.</param>
        void Get(string remoteFile, string localFile);


        /// <summary>
        /// Put the local file onto the remote ftp server.
        /// </summary>
        /// <param name="localfileName">Local file path</param>
        /// <param name="remoteFileName">Remove file path.</param>
        /// <param name="overwrite">Flat to overwrite the file.</param>
        void Put(string localfileName, string remoteFileName, bool overwrite);


        /// <summary>
        /// Set the file type.
        /// </summary>
        /// <param name="p"></param>
        void SetType(string p);


        /// <summary>
        /// Delete the file.
        /// </summary>
        /// <param name="filepath"></param>
        void Delete(string filepath);


        /// <summary>
        /// Determine if the file exists on server.
        /// </summary>
        /// <param name="p"></param>
        bool Exists(string filepath);


        /// <summary>
        /// Makes the directory.
        /// </summary>
        /// <param name="p"></param>
        void MakeDir(string dirname);


        /// <summary>
        /// Removes the directory on the server.
        /// </summary>
        /// <param name="dirname"></param>
        void RemoveDir(string dirname);
    }



    /// <summary>
    /// Ftp implementation.
    /// </summary>
    public class Ftp : IFtp
    {
        #region IFtp Members

        /// <summary>
        /// Open an ftp connection.
        /// </summary>
        public void Open()
        {
            Connect();
        }


        /// <summary>
        /// Close the ftp connection but keeps session.
        /// </summary>
        public void Close()
        {
        }


        /// <summary>
        /// Quit the ftp connection.
        /// </summary>
        public void Quit(bool waitForReply)
        {
        }


        /// <summary>
        /// Get the file from the ftp server.
        /// </summary>
        /// <param name="remoteFile">Name of the remote file.</param>
        /// <param name="localFile">Name of the local file.</param>
        public void Get(string remoteFile, string localFile)
        {
        }


        /// <summary>
        /// Put the local file onto the remote ftp server.
        /// </summary>
        /// <param name="localfileName">Local file path</param>
        /// <param name="remoteFileName">Remove file path.</param>
        /// <param name="overwrite">Flat to overwrite the file.</param>
        public void Put(string localfileName, string remoteFileName, bool overwrite)
        {
        }


        /// <summary>
        /// Set the file type.
        /// </summary>
        /// <param name="p"></param>
        public void SetType(string p)
        {
        }


        /// <summary>
        /// Delete the file.
        /// </summary>
        /// <param name="filepath"></param>
        public void Delete(string filepath)
        {
        }


        /// <summary>
        /// Determine if the file exists on server.
        /// </summary>
        /// <param name="p"></param>
        public bool Exists(string filepath)
        {
            return false;
        }


        /// <summary>
        /// Makes the directory.
        /// </summary>
        /// <param name="p"></param>
        public void MakeDir(string dirname)
        {
        }


        /// <summary>
        /// Removes the directory on the server.
        /// </summary>
        /// <param name="dirname"></param>
        public void RemoveDir(string dirname)
        {
        }
        #endregion

        
        
        /// <summary>
        /// Create new ftp connection.
        /// </summary>
        /// <param name="server">server name</param>
        /// <param name="port">port number</param>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public static IFtp New(string server, int port, string username, string password, bool connect)
        {
            return new Ftp(server, port, username, password, connect);
        }


        /// <summary>
        /// Create new ftp connection.
        /// </summary>
        /// <param name="server">server name</param>
        /// <param name="port">port number</param>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public Ftp(string server, int port, string username, string password, bool connect)
        {
            Init(server, port, username, password, connect);
        }


        // <summary>
        /// Create new ftp connection.
        /// </summary>
        /// <param name="server">server name</param>
        /// <param name="port">port number</param>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public void Init(string server, int port, string username, string password, bool connect)
        {
            _settings = new FtpSettings(server, port, username, password);
        }


        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="args"></param>
        public void Exec(string cmd)
        {

        }


        #region Private Methods
        private void Connect()
        {
            Connect(_settings.Server, _settings.Port, Encoding.ASCII);
        }


        private void Connect(string hostname, int port, Encoding textEncoding)
        {
            _tcp = new TcpClient(hostname, port);
            Stream s = _tcp.GetStream();
            s.ReadTimeout = _settings.TimeOut;
            s.WriteTimeout = _settings.TimeOut;
            SetupStream(s);
        }


        private void Disconnect(string hostname, int port, Encoding textEncoding)
        {
            if (_tcp != null)
            {
                // Be polite
                try
                {
                    Quit(false);
                }
                catch (Exception)
                {
                }

                _reader.Close();
                _writer.Close();

                _tcp.Close();
                _tcp = null;
            }
        }


        private void SetupStream(Stream s)
        {
            if (_writer != null)
                _writer.Flush();

            // SreamWriter's doc states that the default encoding is UTF8 without BOM.
            // Mono 2.0 seems to have a BOM anyway. Create it explicitly as a workaround
            Encoding encoding = new UTF8Encoding(false);

            _reader = new StreamReader(s, encoding);
            _writer = new StreamWriter(s, encoding);
            _writer.NewLine = Environment.NewLine;
        }

        #endregion


        #region Private Data
        private FtpSettings _settings;
        private TcpClient _tcp = null;
        private StreamReader _reader;
        private StreamWriter _writer;

        #endregion
    }



    public class FtpSettings
    {
        public readonly string Server;
        public readonly int Port;
        public readonly string User;
        public readonly string Password;
        public int TimeOut = 120000;


        /// <summary>
        /// Create new ftp connection.
        /// </summary>
        /// <param name="server">server name</param>
        /// <param name="port">port number</param>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public FtpSettings(string server, int port, string username, string password)
        {
            Server = server;
            Port = port;
            User = username;
            Password = password;           
        }
    }
}
