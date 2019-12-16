using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OwnedItemsData
{
    /// <summary>
    /// PlayerPrefs保存先キー
    /// </summary>
    private const string PlayerPrefsKey = "OWNED_ITEMS_DATA";
    
    /// <summary>
    /// インスタンスを返します。
    /// </summary>
    public static OwnedItemsData Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = PlayerPrefs.HasKey(PlayerPrefsKey) 
                    ? JsonUtility.FromJson<OwnedItemsData>(PlayerPrefs.GetString(PlayerPrefsKey))
                    : new OwnedItemsData();
            }

            return _instance;
        }
    }

    private static OwnedItemsData _instance;

    /// <summary>
    /// 所持アイテム一覧を取得します。
    /// </summary>
    public OwnedItem[] OwnedItems
    {
        get { return ownedItems.ToArray(); }
    }
    
    /// <summary>
    /// どのアイテムを何個所持しているかのリスト
    /// </summary>
    [SerializeField] private List<OwnedItem> ownedItems = new List<OwnedItem>();

    /// <summary>
    /// コンストラクタ
    /// シングルトンでは外部からnewできないようコンストラクタをprivateにする
    /// </summary>
    private OwnedItemsData()
    {
    }

    /// <summary>
    /// JSON化してPlayerPrefsに保存します。
    /// </summary>
    public void Save()
    {
        var jsonString = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(PlayerPrefsKey, jsonString);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// アイテムを追加します。
    /// </summary>
    /// <param name="type"></param>
    /// <param name="number"></param>
    public void Add(Item.ItemType type, int number = 1)
    {
        var item = GetItem(type);
        if (null == item)
        {
            item = new OwnedItem(type);
            ownedItems.Add(item);
        }
        item.Add(number);
    }

    /// <summary>
    /// アイテムを消費します。
    /// </summary>
    /// <param name="type"></param>
    /// <param name="number"></param>
    /// <exception cref="Exception"></exception>
    public void Use(Item.ItemType type, int number = 1)
    {
        var item = GetItem(type);
        if (null == item || item.Number < number)
        {
            throw new Exception("アイテムが足りません");
        }
        item.Use(number);        
    }

    /// <summary>
    /// 対象の種類のアイテムデータを取得します。
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public OwnedItem GetItem(Item.ItemType type)
    {
        return ownedItems.FirstOrDefault(x => x.Type == type);
    }

    /// <summary>
    /// アイテムの所持数管理用モデル
    /// </summary>
    [Serializable]
    public class OwnedItem
    {
        /// <summary>
        /// アイテムの種類を返します。
        /// </summary>
        public Item.ItemType Type
        {
            get { return type; }
        }

        public int Number
        {
            get { return number; }
        } 

        /// <summary>
        /// アイテムの種類
        /// </summary>
        [SerializeField] private Item.ItemType type;

        /// <summary>
        /// 所持個数
        /// </summary>
        [SerializeField] private int number;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="type"></param>
        public OwnedItem(Item.ItemType type)
        {
            this.type = type;
        }

        public void Add(int number = 1)
        {
            this.number += number;
        }

        public void Use(int number = 1)
        {
            this.number -= number;
        }
    }
}