using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reader : MonoBehaviour
{
    
    // Reads JSON File
    public TextAsset itemsJSON;
    public TextAsset playerJSON;
    public ItemList myItemList = new ItemList();
    public PlayerRead myPlayer = new PlayerRead();

    [System.Serializable]
    public class Item
    {
        public int ID;
        public string image;
    }

    [System.Serializable]
    public class PlayerRead
    {
        public int score_player;
        public int day;
        public bool double_coin;
        public bool free_time;
        public bool win;
    }

    [System.Serializable]
    public class ItemList
    {
        public Item[] box;
        public Item[] cake;
        public Item[] gauntlet;
    }

    void Awake()
    {
        try{myItemList = JsonUtility.FromJson<ItemList>(itemsJSON.text);}catch{}
        try{myPlayer = JsonUtility.FromJson<PlayerRead>(playerJSON.text);}catch{}
    }
}