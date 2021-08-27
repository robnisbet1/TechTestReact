namespace ThirdPartySDK
{
    using System;
    using System.Threading.Tasks;

    public sealed class EmailService
    {
        public Task Send(string to, string body)
        {
            if (!to.Contains("@"))
            {
                throw new InvalidOperationException("Not an email address");
            }

            return Task.CompletedTask;
        }
    }
}
