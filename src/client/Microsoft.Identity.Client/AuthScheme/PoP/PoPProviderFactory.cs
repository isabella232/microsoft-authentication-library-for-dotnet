﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.AuthScheme.PoP
{
    /// <summary>
    /// This factory ensures key rotation every 8h
    /// </summary>
    internal class PoPProviderFactory
    {
        private static InMemoryCryptoProvider s_currentProvider;
        private static DateTime s_providerExpiration;
        private static readonly TimeSpan s_expirationTimespan = TimeSpan.FromHours(8);
        private static object s_lock = new object();

        internal static ITimeService TimeService { get; set; } = new TimeService();

        public static InMemoryCryptoProvider GetOrCreateProvider()
        {
            lock (s_lock)
            {
                if (s_currentProvider != null && s_providerExpiration > TimeService.GetUtcNow())
                {
                    return s_currentProvider;
                }

                s_currentProvider = new InMemoryCryptoProvider();
                s_providerExpiration = TimeService.GetUtcNow() + s_expirationTimespan;
                return s_currentProvider;
            }
        }
    }
}
