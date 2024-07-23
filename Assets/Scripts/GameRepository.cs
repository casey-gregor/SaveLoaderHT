using Newtonsoft.Json;
using System.Collections.Generic;

namespace SaveLoaderProject
{
    public class GameRepository : IGameRepository
    {
        private Dictionary<string, string> gameState = new Dictionary<string, string>();
      

        public void SetData<T>(T data)
        {
            var jsonData = JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            string stateKey = typeof(T).ToString();
            if(this.gameState.ContainsKey(stateKey))
            {
                this.gameState[stateKey] = jsonData;
            }
            else
            {
                this.gameState.Add(stateKey, jsonData);
            }
        }

        public bool TryGetData<T>(out T data)
        {
            
            string stateKey = typeof(T).ToString();
            if (this.gameState.ContainsKey(stateKey))
            {
                data = JsonConvert.DeserializeObject<T>(this.gameState[stateKey]);
                return true;
            }
            else
            {    
                data = default;
                return false;
            }
        }

        public string SaveState()
        {
            var gameStateJson = JsonConvert.SerializeObject(gameState);

            return gameStateJson;
        }

        public void LoadState(string data)
        {
            this.gameState = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
        }
    }
}