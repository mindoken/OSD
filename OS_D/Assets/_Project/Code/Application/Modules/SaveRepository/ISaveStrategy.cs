using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace App
{
    public interface ISaveStrategy
    {
        UniTask<Dictionary<string, string>> LoadRepository(string KEY);
        UniTask SaveRepository(Dictionary<string, string> gameState, string KEY);
        UniTask DeleteRepository(string KEY);
    }
}