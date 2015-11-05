using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Win32;
using FileTransferClient.FileDownloader;
using System.IO;
using System.Threading;

namespace FileTransferClient.Model
{
    public class DownloadItem : INotifyPropertyChanged
    {

        private string filePath;
        public string FilePath
        {
            get { return this.filePath; }
            set
            {
                this.filePath = value;
                this.OnPropertyChanged("FilePath");
            }
        }

        private ICommand browser;
        public ICommand Browser
        {
            get
            {
                if (browser == null)
                {
                    browser = new DelegateCommand(OpenBrowser);
                }

                return browser;
            }
        }

        public void OpenBrowser()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Choose Files|*.*";
            bool? result = dlg.ShowDialog();
            if (result.HasValue && result.Value)
            {
                this.FilePath = dlg.FileName;
            }
        }
        private ICommand upload;
        public ICommand Upload
        {
            get
            {
                if (upload == null)
                {
                    upload = new DelegateCommand(StartUpload);
                }

                return upload;
            }
        }

        public void StartUpload()
        {
            try
            {

                FileInfo fileInfo = new FileInfo(this.FilePath);
                if (fileInfo.Exists)
                {

                    // open input stream
                    using (FileStream stream = new FileStream(this.FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {

                        using (StreamWithProgress uploadStreamWithProgress = new StreamWithProgress(stream))
                        {
                            uploadStreamWithProgress.ProgressChanged += uploadStreamWithProgress_ProgressChanged;
                            this.Client.UploadFile(fileInfo.Name, fileInfo.Length, 0, uploadStreamWithProgress);

                        }
                    }
                }
            }
            catch { }

        }
        private string filename;
        public string FileName
        {
            get { return this.filename; }
            set
            {
                this.filename = value;
                this.OnPropertyChanged("FileName");
            }
        }
        private ICommand download;
        public ICommand Download
        {
            get
            {
                if (download == null)
                {
                    download = new DelegateCommand(StartDownload);
                }

                return download;
            }
        }
        private ICommand pause;
        public ICommand Pause
        {
            get
            {
                if (pause == null)
                {
                    pause = new DelegateCommand(PauseDownload);
                }

                return pause;
            }
        }
        private ICommand resume;
        public ICommand Resume
        {
            get
            {
                if (resume == null)
                {
                    resume = new DelegateCommand(StartDownload);
                }

                return resume;
            }
        }

        public void PauseDownload()
        {
            if (this.CurrentClientThread != null && this.CurrentClientThread.IsAlive)
                this.CurrentClientThread.Abort(); //Abort will stop the current Thread but will put the downloaded file in the folder
        }



        private string contextStatus;
        public string ContextStatus
        {
            get { return this.contextStatus; }
            set
            {
                this.contextStatus = value;
                this.OnPropertyChanged("ContextStatus");
            }
        }
        public Thread CurrentClientThread { get; set; }
        public void StartDownload()
        {
            if (this.CurrentClientThread != null && this.CurrentClientThread.IsAlive) { this.ContextStatus = "Thread is Busy"; return; }
            this.CurrentClientThread = new Thread(this.StartDownloading);
            this.CurrentClientThread.Start();
        }
        
        private FileTransferLibraryClient oclient;
        public FileTransferLibraryClient Client
        {
            get
            {
                this.oclient = this.oclient ?? new FileTransferLibraryClient();
                return this.oclient;
            }
        }

        # region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        # endregion


        public void uploadStreamWithProgress_ProgressChanged(object sender, StreamWithProgress.ProgressChangedEventArgs e)
        {
            if (e.Length != 0)
                this.ProgressValue = (int)(e.BytesRead * 100 / e.Length);
        }

        private int progressvalue;
        public int ProgressValue { get { return this.progressvalue; } set { this.progressvalue = value; this.OnPropertyChanged("ProgressValue"); } }

        public void StartDownloading()
        {
            try
            {

                string filePath = System.IO.Path.Combine("Download", this.FileName);
                string fileName = this.FileName;

                Stream inputStream;

                long startlength = 0;
                FileInfo finfo = new FileInfo(filePath);
                if (finfo.Exists)
                    startlength = finfo.Length; // If File exists we need to send the Start length to the server

                long length = this.Client.DownloadFile(ref fileName, ref startlength, out inputStream);

                using (FileStream writeStream = new System.IO.FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                {

                    int chunkSize = 2048;
                    byte[] buffer = new byte[chunkSize];

                    do
                    {
    
                        int bytesRead = inputStream.Read(buffer, 0, chunkSize);
                        if (bytesRead == 0) break;

    
                        writeStream.Write(buffer, 0, bytesRead);

    
                        this.ProgressValue = (int)(writeStream.Position * 100 / length);
                    }
                    while (true);


                    writeStream.Close();
                }

                inputStream.Dispose();
            }
            catch
            {

            }
        }

        
    }
}