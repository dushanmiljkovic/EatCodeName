using System.Collections.Generic;

namespace EatCode.SignalR.Services
{
    public interface IChatHistoryService
    {
        bool AddToHistory(string msg);
        List<string> GetChatHistory();
        List<string> GetChatHistory(int n);
    }
}