using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemButton : MonoBehaviour
{
    public OwnedItemsData.OwnedItem OwnedItem
    {
        get { return _ownedItem; }
        set
        {
            _ownedItem = value;
            
            // アイテムが割り当てられたかどうかでアイテム画像や所持個数の表示を切り替える
            var isEmpty = null == _ownedItem;
            image.gameObject.SetActive(!isEmpty);
            number.gameObject.SetActive(!isEmpty);
            _button.interactable = !isEmpty;            
            if (!isEmpty)
            {
                image.sprite = itemSprites.First(x => x.itemType == _ownedItem.Type).sprite;
                number.text = "" + _ownedItem.Number;
            }
        }
    }
    
    [SerializeField] private ItemTypeSpriteMap[] itemSprites; // 各アイテム用の画像を指定するフィールド
    [SerializeField] private Image image;
    [SerializeField] private Text number;

    private Button _button;
    private OwnedItemsData.OwnedItem _ownedItem;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        // TODO ボタンを押した時の処理はここに書く
    }
    
    /// <summary>
    /// アイテムの種類とSpriteをインスペクタで紐付けられるようにするためのクラス
    /// </summary>
    [Serializable]
    public class ItemTypeSpriteMap
    {
        public Item.ItemType itemType;
        public Sprite sprite;
    }
}