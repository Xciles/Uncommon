﻿using System.Net;
using System.Net.Http.Headers;
using Xciles.Uncommon.Security;

namespace Xciles.Uncommon.Net
{
    public class UncommonRequestOptions
    {
        public bool Authorized { get; set; }
        public HttpHeaders Headers { get; set; }
        public int Timeout { get; set; }
        public CookieContainer CookieContainer { get; set; }
        public ERequestSerializer RequestSerializer { get; set; }
        public EResponseSerializer ResponseSerializer { get; set; }
        public ISecurityContext SecurityContext { get; set; }

        public UncommonRequestOptions()
        {
            Authorized = false;
            Timeout = 30000;
            RequestSerializer = ERequestSerializer.UseJsonNet;
            ResponseSerializer = EResponseSerializer.UseJsonNet;
        }
    }
}
