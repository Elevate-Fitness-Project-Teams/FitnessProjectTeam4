using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace AuthService.Common.Email;

public class SmtpEmailSender(IOptions<SmtpOptions> opts) : IEmailSender
{
    private readonly SmtpOptions _opts = opts.Value;

    public async Task SendAsync(string to, string subject, string body, CancellationToken ct)
    {
        var msg = new MimeMessage();
        msg.From.Add(MailboxAddress.Parse(_opts.From));
        msg.To.Add(MailboxAddress.Parse(to));
        msg.Subject = subject;
        msg.Body = new TextPart("plain") { Text = body };

        using var client = new SmtpClient();
        var socketOptions = _opts.UseStartTls
            ? SecureSocketOptions.StartTls
            : SecureSocketOptions.SslOnConnect;

        await client.ConnectAsync(_opts.Host, _opts.Port, socketOptions, ct);
        await client.AuthenticateAsync(_opts.User, _opts.Pass, ct);
        await client.SendAsync(msg, ct);
        await client.DisconnectAsync(true, ct);
    }
}
