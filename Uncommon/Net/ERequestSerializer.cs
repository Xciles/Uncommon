﻿using System;

namespace Xciles.Uncommon.Net
{
    [Obsolete("This will be removed in a future version because this lib will start to use the HttpClient instead of just webrequests.")]
    public enum ERequestSerializer
    {
        Undefined,
        UseXmlDataContractSerializer,
        UseXmlSerializer,
        UseByteArray,
        UseJsonNet,
        UseStringUrlPost
    }
}