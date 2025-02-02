using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using UnityEngine.UI;
using System;

public class Shop : MonoBehaviour
{

    Player player_data;
    Player player;
    Reader area_item;
    public TMP_Text Coin;

    public Image Buycoin_Image;
    public TMP_Text Buycoin_Text;
    public Image Buyfree_Image;
    public TMP_Text Buyfree_Text;
    public Image Buywin_Image;
    public TMP_Text Buywin_Text;

    void start()
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

        Coin.text = player.score_player + " Coin";
        if(player.double_coin == true)
        {Buycoin_Text.text = "already buy";
        Buycoin_Image.sprite = Resources.Load<Sprite>("sell");}
        Check(player.double_coin, Buycoin_Text, Buycoin_Image);
        Check(player.free_time, Buyfree_Text, Buyfree_Image);
        Check(player.win, Buywin_Text, Buywin_Image);
    }

    public void Check(bool sell , TMP_Text a, Image b)
    {
       if(sell == true)
        {a.text = "already buy";
        b.sprite = Resources.Load<Sprite>("sell");} 
    }
    public void Exit()
    {
        string strOutPut = JsonUtility.ToJson(player);
        File.WriteAllText(Application.dataPath + "/Data/save.json", strOutPut);
        SceneManager.LoadSceneAsync(1);
    }

    public void Buy_DoubleCoin()
    {
        if(player.score_player == 25)
        {
        player.double_coin = true;
        Check(player.double_coin, Buycoin_Text, Buycoin_Image);
        }
    }

    public void Buy_FreeTime()
    {
        if(player.score_player == 50)
        {
        Check(player.free_time, Buyfree_Text, Buyfree_Image);
        player.free_time = true;
        }
    }

    public void Buy_Win()
    {
        if(player.score_player == 999)
        {
        Check(player.win, Buywin_Text, Buywin_Image);
        player.win = true;
        }
    }
}
