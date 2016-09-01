//
// Copyright 2014-2015 Amazon.com, 
// Inc. or its affiliates. All Rights Reserved.
// 
// Licensed under the Amazon Software License (the "License"). 
// You may not use this file except in compliance with the 
// License. A copy of the License is located at
// 
//     http://aws.amazon.com/asl/
// 
// or in the "license" file accompanying this file. This file is 
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, express or implied. See the License 
// for the specific language governing permissions and 
// limitations under the License.
//
using System;
using System.IO;

using Amazon.Runtime;
using Amazon.CognitoIdentity;
using Amazon.CognitoSync.Model;

namespace Amazon.CognitoSync.SyncManager.Internal
{
    /// <summary>
    /// Remote data storage using Cognito Sync service on which we can invoke
    /// actions like creating a dataset or record.
    /// </summary>
    public partial class CognitoSyncStorage : IDisposable
    {
        private readonly string identityPoolId;

#if UNITY
        private readonly AmazonCognitoSyncClient client;
#else
        private readonly IAmazonCognitoSync client;
#endif

        private readonly CognitoAWSCredentials cognitoCredentials;

        #region Dispose

        /// <summary>
        /// Dispose this Object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose this Object
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (client != null)
                    client.Dispose();
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an insance of CognitoSyncStorage. 
        /// </summary>
        /// <param name="cognitoCredentials"><see cref="Amazon.CognitoIdentity.CognitoAWSCredentials"/></param>
        /// <param name="config"><see cref="Amazon.CognitoSync.AmazonCognitoSyncConfig"/></param>
        public CognitoSyncStorage(CognitoAWSCredentials cognitoCredentials, AmazonCognitoSyncConfig config)
        {
            if (cognitoCredentials == null)
            {
                throw new ArgumentNullException("cognitoCredentials");
            }
            this.identityPoolId = cognitoCredentials.IdentityPoolId;
            this.cognitoCredentials = cognitoCredentials;
            this.client = new AmazonCognitoSyncClient(cognitoCredentials, config);
        }

        #endregion

        #region Private Methods

        private string GetCurrentIdentityId()
        {
            return cognitoCredentials.GetIdentityId();
        }

        private static RecordPatch RecordToPatch(Record record)
        {
            RecordPatch patch = new RecordPatch();
            patch.DeviceLastModifiedDate = record.DeviceLastModifiedDate.Value;
            patch.Key = record.Key;
            patch.Value = record.Value;
            patch.SyncCount = record.SyncCount;
            patch.Op = (record.Value == null ? Operation.Remove : Operation.Replace);
            return patch;
        }

        private static DatasetMetadata ModelToDatasetMetadata(Amazon.CognitoSync.Model.Dataset model)
        {
            return new DatasetMetadata(
                model.DatasetName,
                model.CreationDate,
                model.LastModifiedDate,
                model.LastModifiedBy,
                model.DataStorage,
                model.NumRecords
                );
        }

        private static Record ModelToRecord(Amazon.CognitoSync.Model.Record model)
        {
            return new Record(
                model.Key,
                model.Value,
                model.SyncCount,
                model.LastModifiedDate,
                model.LastModifiedBy,
                model.DeviceLastModifiedDate,
                false);
        }

        private static SyncManagerException HandleException(Exception e, string message)
        {
            var ase = e as AmazonServiceException;

            if (ase == null) ase = new AmazonServiceException(e);

            if (ase.GetType() == typeof(ResourceNotFoundException))
            {
                return new DatasetNotFoundException(message);
            }
            else if (ase.GetType() == typeof(ResourceConflictException)
                     || ase.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                return new DataConflictException(message);
            }
            else if (ase.GetType() == typeof(LimitExceededException))
            {
                return new DataLimitExceededException(message);
            }
            else if (IsNetworkException(ase))
            {
                return new NetworkException(message);
            }
            // HACK FIX by PMF
            else if (ase.GetType() == typeof(AmazonCognitoSyncException)
                     && ase.Message != null 
                     && ase.Message.StartsWith("Current SyncCount for:"))
            {
                return new DataConflictException(message);
            }
            // END HACK FIX by PMF
            else
            {
                return new DataStorageException(message, ase);
            }
        }

        private static bool IsNetworkException(AmazonServiceException ase)
        {
            return ase.InnerException != null && ase.InnerException.GetType() == typeof(IOException);
        }

        #endregion
    }
}

