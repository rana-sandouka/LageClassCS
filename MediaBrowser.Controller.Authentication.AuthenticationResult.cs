    public class AuthenticationResult
    {
        public UserDto User { get; set; }

        public SessionInfo SessionInfo { get; set; }

        public string AccessToken { get; set; }

        public string ServerId { get; set; }
    }