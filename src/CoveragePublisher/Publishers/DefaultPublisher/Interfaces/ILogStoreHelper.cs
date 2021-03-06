﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.TestManagement.WebApi;

namespace Microsoft.Azure.Pipelines.CoveragePublisher.Publishers.DefaultPublisher
{
    public interface ILogStoreHelper
    {
        Task<TestLogStatus> UploadTestBuildLogAsync(Guid projectId, int buildId, TestLogType logType, string logFileSourcePath, Dictionary<string, string> metaData, string destDirectoryPath, bool allowDuplicate, CancellationToken cancellationToken);
    }
}
