using System;
using System.Management.Automation;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;
using Redeploy.Azure.Storage.Models;
using Redeploy.Azure.Storage.Blob;

namespace Redeploy.Azure.Storage.Commands
{
    [OutputType(typeof(CloudBlob))]
    [Cmdlet("Set", "AZStorageBlobContent", SupportsShouldProcess = true)]
    public class SetAZStorageBlobContent : Cmdlet
    {
        [ValidateNotNull]
        [Parameter(
            Mandatory = true,
            Position = 0,
            HelpMessage = "A Storage Context object. Create one with 'New-AZStorageContext'."
        )]
        public StorageContext Context
        {
            get { return _context; }
            set { _context = value; }
        }
        private StorageContext _context;

        [ValidateNotNull]
        [Parameter(
            Mandatory = true,
            Position = 1,
            HelpMessage = "Path as a string to a file to upload."
        )]
        public string File
        {
            get { return _file; }
            set { _file = value; }
        }       
        private string _file;

        [ValidateNotNull]
        [Parameter(
            Mandatory = true,
            Position = 2,
            HelpMessage = "Name of blob in the Storage Account Container."
        )]
        public string Blob
        {
            get { return _blob; }
            set { _blob = value; }
        }
        private string _blob;

        [ValidateNotNull]
        [Parameter(
            Mandatory = true,
            Position = 3,
            HelpMessage = "Name of container in Storage Account."
        )]
        public string Container
        {
            get { return _container; }
            set { _container = value; }
        }
        private string _container;

        [Parameter()]
        public SwitchParameter Force
        {
            get { return _force; }
            set { _force = value; }
        }
        private bool _force;

        protected override void ProcessRecord()
        {
            
            var resolvedFilePath = resolveFilePath(_file);

            StorageContext context = _context;
            BlobHelper storageHelper = new BlobHelper(context);
            
            CloudBlob blob;
            bool exists;

            try 
            {
                exists = storageHelper.BlobExists(Container, Blob).Result;
            }
            catch (System.AggregateException exception)
            {
                throw new System.AggregateException(exception.InnerException.GetBaseException().Message);
            }
            
            try
            {
                if (exists)
                {
                    if (ShouldProcess(Blob))
                    {
                        var message = "Are you sure to overwrite '" + Context.BlobEndpoint + Container + "/" + Blob + "'?";

                        if (Force || ShouldContinue(message, "Confirm"))
                        {
                            blob = storageHelper.UploadBlob(Container, resolvedFilePath, Blob).Result;
                            WriteObject(blob);
                        }
                        else
                        {
                            throw new System.Management.Automation.HaltCommandException("Blob " + Blob + " already exists.");
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    blob = storageHelper.UploadBlob(Container, resolvedFilePath, Blob).Result;
                    WriteObject(blob);
                }
                
            }
            catch (System.AggregateException exception)
            {
                throw new System.AggregateException(exception.InnerException.GetBaseException().Message);
            }
        }

        private string resolveFilePath(string filePath)
        {
            string resolvedFilePath;

            if (!System.IO.Path.IsPathRooted(filePath))
            {
                // Work around to get the path from where the command is run.
                SessionState state = new SessionState();
                var currentFsLocation = state.Path.CurrentFileSystemLocation.ToString();
                resolvedFilePath = Path.GetFullPath(Path.Combine(currentFsLocation, filePath));
            } 
            else
            {
                resolvedFilePath = Path.GetFullPath(filePath);
            }

            if (!System.IO.File.Exists(resolvedFilePath))
            {
                throw new IOException("File does not exist.");
            }

            return resolvedFilePath;
        }
    }
}