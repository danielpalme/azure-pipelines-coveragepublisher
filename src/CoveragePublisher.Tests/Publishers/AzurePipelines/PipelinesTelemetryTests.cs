﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Pipelines.CoveragePublisher;
using Microsoft.Azure.Pipelines.CoveragePublisher.Publishers.DefaultPublisher;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.CustomerIntelligence.WebApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CoveragePublisher.Tests
{
    [TestClass]
    public class PipelinesTelemetryTests
    {
        [TestMethod]
        public void PublishTelemetryAsyncTest()
        {
            var clientFactory = new Mock<IClientFactory>();
            var telemetryDataCollector = new PipelinesTelemetry(clientFactory.Object, true);
            var ciHttpClient =
                new Mock<CustomerIntelligenceHttpClient>(new Uri("https://somename.Visualstudio.com"), new VssCredentials());

            clientFactory
                .Setup(x => x.GetClient<CustomerIntelligenceHttpClient>())
                .Returns(ciHttpClient.Object);

            telemetryDataCollector.PublishTelemetryAsync("Feature", new Dictionary<string, object>());
        }

        [TestMethod]
        public void PublishCumulativeTelemetryAsyncTest()
        {
            var clientFactory = new Mock<IClientFactory>();
            var telemetryDataCollector = new PipelinesTelemetry(clientFactory.Object, true);

            telemetryDataCollector.AddAndAggregate("Property", 1.1);
            telemetryDataCollector.AddAndAggregate("Property", 1.1);

            Assert.IsTrue((double)telemetryDataCollector.Properties["Property"] == 2.2);

            telemetryDataCollector.PublishCumulativeTelemetryAsync();

            Assert.IsTrue(telemetryDataCollector.Properties.Count == 0);
        }
    }
}
