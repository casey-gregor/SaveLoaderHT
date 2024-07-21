using System;
using System.Collections;
using UnityEngine;

namespace SaveLoaderProject
{
    public abstract class SaveLoader<TData>: ISaveLoader
    {
        public void LoadGame(IGameRepository gameRepository)
        {
            if(gameRepository.TryGetData(out TData data))
            {
                SetupData(data);
            }
        }


        public void SaveGame(IGameRepository gameRepository)
        {
            TData data = ConvertToData();
            gameRepository.SetData(data);
            
        }

        protected abstract TData ConvertToData();
        protected abstract void SetupData(TData data);
      
    }
}