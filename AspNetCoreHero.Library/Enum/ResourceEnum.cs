namespace AspNetCoreHero.Library.Enum
{
    public static class ResourceEnum
    {
        public const string AssemblyLoaded = "Assembly \"{0}\" - Version {1} has been loaded.";
        public const string AssemblyNotFound = "Could not load Assembly \"{0}\" - File not found.";
        public const string AssemblyWrongVersion = "Could not load Assembly \"{0}\" - Wrong GUID or Version.";
        public const string AssemblySettingNotFound = "Could not load Setting of Assembly \"{0}\" - Not found any record.";


        public const string CacheEmpty = "Cache \"{0}\" is empty.";
        public const string CacheLoadedException = "Could not load Cache \"{0}\" - Exception.";
        public const string CacheDuplicated = "Could not load Cache \"{0}\" - Duplicated.";
        public const string CacheInUsed = "Could not load Cache \"{0}\" to Redis - Because database {1} has been used by \"{2}\".";


        public const string QueueConsumerEnabled = "ENABLE listen request come from Queue \"{0}\".";
        public const string QueueConsumerDisabled = "DISABLE listen request come from Queue.";
        public const string QueueConsumerInvalid = "Queue Name is null or empty.";
        public const string QueueConsumerDuplicated = "Consumer \"{0}\" can not register on Queue \"{1}\", because this Queue has been registered by Consumer \"{2}\".";
        public const string QueueConsumerRegistered = "Consumer \"{0}\" has been registered on Queue \"{1}\".";
        public const string QueuePublishFailure = "Could not send message to Queue name \"{0}\" because it does not exist.";


        public const string FunctionLoaded = "Function \"{0}\" has been loaded.";
        public const string ServiceLoaded = "Service  \"{0}\" has been loaded.";


        public static readonly string BasicAuthenticationFail =
            $"Basic authentication failed. (UserName: {LogTagEnum.OpenEmbossTag}{{0}}{LogTagEnum.CloseEmbossTag})";


        public static readonly string RequestDuplicated =
            $"Request {LogTagEnum.OpenEmbossTag}{{0}}{LogTagEnum.CloseEmbossTag} has been duplicated on date {LogTagEnum.OpenEmbossTag}{{1}}{LogTagEnum.CloseEmbossTag}";


        public const string InvalidField = "Field \"{0}\" is required.";
        public const string InvalidFieldValue = "Field \"{0}\" is invalid.";

        public static readonly string InvalidFunction =
            $"Could not find function handler for keyword {LogTagEnum.OpenEmbossTag}{{0}}{LogTagEnum.CloseEmbossTag}";

        public static readonly string InvalidService =
            $"Could not find service handler for keyword {LogTagEnum.OpenEmbossTag}{{0}}{LogTagEnum.CloseEmbossTag}";

        public static readonly string InvalidSignature
            = $"Signature is invalid. Sign method {LogTagEnum.OpenEmbossTag}{{0}}{LogTagEnum.CloseEmbossTag}";


        public const string InvalidNumber = "Field \"{0}\" is a invalid number {1}. It must be has value between {2} and {3}.";
        public const string InvalidValue = "Field \"{0}\" is a invalid value {1}. Accepted value are: {2}.";


        public const string LineSeparator = "======================================================================";

        public const string NotFoundConnection = "The given connection \"{0}\" was not present in configuration.";
        public const string NotFoundKey = "The given key \"{0}\" was not present in dictionary.";


        public const string MessageRequest = "Send request to {0}:";
        public const string MessageResponse = "Receive response from {0}:";
    }

}
