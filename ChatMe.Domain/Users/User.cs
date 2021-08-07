namespace ChatMe.Domain.Users
{
    using System;

    public class User
    {
        private readonly Username username;

        private readonly Password password;

        public User(Username username)
        {
            this.Id = Guid.NewGuid();
            this.username = username;
        }

        public User(Username username, Password password) 
            : this(username)
        {
            this.password = password;
        }

        public User(Guid id, Username username, Password password)
        {
            this.SetId(id);
            this.username = username;
            this.password = password;
        }

        public Guid Id { get; private set; }

        public string Username => username.Value;

        public string Password => password.Value;

        public bool ValidatePassword(Password passwordToValidate) => this.Password == passwordToValidate.Value;

        private void SetId(Guid id)
        {
            this.Id = id;
        }
    }
}
