﻿using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Ваши сохранения
        public int maxLevel = 1;
        public int coins = 0;
        public List<string> idCoinsList = new List<string>();
        public bool[] isBought = new bool[15];
        public int curSprite = 0;
    }
}
