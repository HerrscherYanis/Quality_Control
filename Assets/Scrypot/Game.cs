using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
using Unity.Collections;
using Unity.VisualScripting;
using TMPro;
using UnityEditor.Timeline.Actions;
using System.IO;
using UnityEngine.SceneManagement;
using System.Data.Common;


public class Game : MonoBehaviour
{
    Reader area_item;
    Player player_data;
    Player player;
    public Image view;
    public Image preview;
    public Image control;
    public TMP_Text remaining;
    public TMP_Text score_text;
    int objective;
    public Reader.Item[] items_list;
    Reader.Item Compare;
    int passing = 0;

    void Start()
    {
        area_item = FindObjectOfType<Reader>();
        player_data = FindObjectOfType<Player>();
        player = new Player();
        try{
        player.score_player = area_item.myPlayer.score_player;
        player.day = area_item.myPlayer.day;
        player.double_coin = area_item.myPlayer.double_coin;
        player.free_time = area_item.myPlayer.free_time;
        player.win = area_item.myPlayer.win;
        }catch{}
        player.day += 1;
        Item_SetUp(Select());
    }

    Reader.Item[] Select()
    {
        switch((int)UnityEngine.Random.Range(0.0f, 3.0f))
        {
        case 0:
        return area_item.myItemList.box;
        case 1:
        return area_item.myItemList.cake;
        case 2:
        return area_item.myItemList.gauntlet;
        }
        return null;
    }

    void Item_SetUp(Reader.Item[] select)
    {
        float free = 0.5f;
        if(player.free_time == true)
        {free = 1.5f;}
        objective = (int)UnityEngine.Random.Range(4.0f, 8.0f*player.day/free);
        items_list = new Reader.Item[objective];
        for(int x = 0; x < items_list.Length; x++)
        {
            items_list[x] = select[(int)UnityEngine.Random.Range(0.0f, select.Length)];
        }
        Compare = select[(int)UnityEngine.Random.Range(0.0f, select.Length)];
        set_Image(Compare.image, view);
    }

    void set_Image(string itemImage, Image print_Image)
    {
        try{
        print_Image.sprite = Resources.Load<Sprite>(itemImage);
        }catch{
        print_Image.sprite = Resources.Load<Sprite>("Error");}
    }
    void Update()
    {
        finality();
        try{
        set_Image(items_list[passing].image, control);
        if(passing == objective-1)
        {preview.gameObject.SetActive(false);}
        else
        {set_Image(items_list[passing+1].image, preview);}
        }catch{}
        remaining.text = (objective-passing).ToString() + " remaining";
        score_text.text = player.score_player + " Coin";
    }

    public void Trash() 
    {
        if(Compare.ID == items_list[passing].ID)
        {player.score_player += (int)(-3 * player.day);}
        passing += 1;
    }

    public void Accept() 
    {
        int gain = 1;
        if(player.double_coin == true)
        {gain = 2;}
        if(Compare.ID == items_list[passing].ID)
        {player.score_player += 1 * player.day * gain;} 
        else
        {player.score_player += -3 * player.day;}
        passing += 1;
    }

    void finality()
    {
        if(passing == objective)
        {
            control.gameObject.SetActive(false);
            if(player.score_player < 0)
            {
                File.Delete(Application.dataPath + "/Data/save.json");
                SceneManager.LoadSceneAsync(2);
            }
            else
            {
            string strOutPut = JsonUtility.ToJson(player);
            File.WriteAllText(Application.dataPath + "/Data/save.json", strOutPut);
            SceneManager.LoadSceneAsync(3);
            }
        }
    }
}
