using System;
namespace Fss.Rasp
{
    public enum ApiStatus
    {
        Unknown = -1,
        Ok = 0,
        Error = 1,
        RemoteApiClientError,
        Initializing,
        Initialized,
        FileNotFound,
        ProcessNotFound,
        LibraryError,
        ConfigurationError
    }
}
