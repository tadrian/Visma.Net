﻿using ONIT.VismaNetApi.Models;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ONIT.VismaNetApi.Lib.Data
{
    public class JournalTransactionData : BasePaginatedCrudDataClass<JournalTransaction>
    {
        public JournalTransactionData(VismaNetAuthorization auth) : base(auth)
        {
            ApiControllerUri = VismaNetControllers.JournalTransaction;
        }

        public async Task<string> AddAttachmentToJournalTransaction(string batchNumber, string content, string fileName)
        {
            return await AddAttachmentToJournalTransaction(batchNumber, Encoding.UTF8.GetBytes(content), fileName);
        }

        public async Task<string> AddAttachmentToJournalTransaction(string batchNumber, byte[] byteArray, string fileName)
        {
            if (byteArray == default(byte[]))
                throw new ArgumentNullException(nameof(byteArray), "ByteArray is missing");
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(Path.GetExtension(fileName)))
                throw new ArgumentNullException(nameof(fileName), "File name must be provided and have an extention");
            return await VismaNetApiHelper.AddAttachmentToJournalTransaction(Authorization, batchNumber, byteArray, fileName);
        }

        public async Task<VismaActionResult> Release(JournalTransaction transaction)
        {
            return await VismaNetApiHelper.Action(Authorization, ApiControllerUri, transaction.GetIdentificator(), "release");
        }

        public async Task<VismaActionResult> Release(string transactionNumber)
        {
            return await VismaNetApiHelper.Action(Authorization, ApiControllerUri, transactionNumber, "release");
        }
    }
}
