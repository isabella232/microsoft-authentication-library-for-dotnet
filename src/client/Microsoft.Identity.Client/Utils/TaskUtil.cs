// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading.Tasks;

#if HAVE_METHOD_IMPL_ATTRIBUTE
#endif

namespace Microsoft.Identity.Client.Utils
{
    internal static class TaskUtil
    {
        public static Task CompletedTask
        {
            get
            {
#if !NET45
                    return Task.CompletedTask; 
#else
                return Task.WhenAll();
#endif
            }
        }
    }
}
