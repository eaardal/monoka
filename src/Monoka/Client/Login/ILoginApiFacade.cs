using System.Threading.Tasks;

namespace Monoka.Client.Login
{
    public interface ILoginApiFacade
    {
        Task<HandshakeResult> Handshake(string playerName);
    }
}