using System.Threading.Tasks;
using Monoka.Client.Login.Results;

namespace Monoka.Client.Login
{
    public interface ILoginFacade
    {
        Task<HandshakeResult> Handshake(string playerName);
    }
}