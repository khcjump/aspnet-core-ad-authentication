using System.Security.Principal;

namespace Demo213 {
    public class MyIdentity : IIdentity {
        public MyIdentity (string name) {
            this.AuthenticationType = "ad";
            IsAuthenticated = true;
            Name = name;
        }
        public string AuthenticationType { get; }
        public bool IsAuthenticated { get; }
        public string Name { get; }
    }
}