using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace cwegineeringedgehtmlstage.App_Code
{
    public class Storage
    {
        private readonly string _accountName = "corsivacdncontent";
        private readonly string _accessKey = "b6ANjJuW0M/DVk4/EPyarz2yhFCnnY4wmPremMMQGf5h58VcA6U5LchCazdqBiAuOwKDtInclYorwyo1dKUqog==";
        private readonly string _containerReference = "csee/POFiles";

        public string UploadToAzureCDN(Stream fileStream, string fileName)
        {
            string extension = fileName.Substring(fileName.LastIndexOf(".") + 1).ToLower();

            StorageCredentials credentials = new StorageCredentials(_accountName, _accessKey);
            CloudStorageAccount account = new CloudStorageAccount(credentials, true);
            CloudBlobClient client = account.CreateCloudBlobClient();
            CloudBlobContainer container = client.GetContainerReference(_containerReference);
            CloudBlockBlob blob = container.GetBlockBlobReference(fileName);

            switch (extension)
            {
                case "pdf":
                    blob.Properties.ContentType = "application/pdf";
                    break;
                case "docx":
                    blob.Properties.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                default:
                    throw new NotSupportedException();
            }

            blob.UploadFromStream(fileStream);
            return blob.StorageUri.PrimaryUri.AbsoluteUri;
        }

        internal string UploadFileToStorageSection(Stream inputStream, string fileName, string extension)
        {
            throw new NotImplementedException();
        }

        //public string StoreDocumentAndReturnURL(HttpPostedFile uploadedFile)
        //{
        //    string extension = uploadedFile.FileName.Substring(uploadedFile.FileName.LastIndexOf(".") + 1).ToLower();
        //    if (extension == "pdf" || extension == "docx")
        //    {
        //        Storage storage = new Storage();
        //        string fileName = HttpUtility.UrlEncode(Hash.hashwithtime(uploadedFile.FileName) + "." + extension);

        //        return storage.UploadToAzureCDN(uploadedFile.InputStream, fileName);
        //    }

        //    return string.Empty;
        //}
    }
}