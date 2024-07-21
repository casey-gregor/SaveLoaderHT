using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoaderProject
{
    public class GameRepository : IGameRepository
    {
        private const string repoName = "GAME_REPOSITORY";
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

        public void SaveState()
        {
            var gameStateJson = JsonConvert.SerializeObject(gameState);
            PlayerPrefs.SetString(repoName, gameStateJson);
        }

        public void LoadState()
        {
            if (PlayerPrefs.HasKey(repoName))
            {
                string savedState = PlayerPrefs.GetString(repoName);
                this.gameState = JsonConvert.DeserializeObject<Dictionary<string, string>>(savedState);
            }
        }
    }
}