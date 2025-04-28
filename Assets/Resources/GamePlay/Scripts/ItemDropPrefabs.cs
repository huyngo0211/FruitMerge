using System;
using System.Collections;
using System.Collections.Generic;
using BunSamaCSCore;
using UnityEngine;

public class ItemDropPrefabs : MonoBehaviour, IObjectPoolable
{
    #region Define
    
    #endregion

    #region Properties

    private SpriteRenderer _sprite;
    private GameObjectPool _poolMachine;
    private Rigidbody2D _rigidbody;
    private ItemDrop _itemDrop;

    public System.Action OnDropFinished;
    
    #endregion

    #region Core MonoBehavior

    private void Awake()
    {
        _sprite = this.GetComponent<SpriteRenderer>();
        _rigidbody = this.GetComponent<Rigidbody2D>();
    }

    #endregion

    #region Public Method

    public void SetBodyType(RigidbodyType2D type)
    {
        _rigidbody.bodyType = type;
    }
    
    public ItemDrop GetItemDropData()
    {
        return _itemDrop;
    }
    
    public void OnObjectSpawn(object data, GameObjectPool poolMachine)
    {
        _poolMachine = poolMachine;
        if (data is ItemDrop itemDrop)
            SetData(itemDrop);
    }
    
    #endregion

    #region Private Method

    private void SetData(ItemDrop itemDrop)
    {
        _itemDrop = itemDrop;
        _sprite.sprite = ResourceHelper.LoadSprite("_Common/ItemDrop/" + itemDrop.ID);
        _sprite.transform.localScale = new Vector3(itemDrop.Scale, itemDrop.Scale, itemDrop.Scale);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ItemDropPrefabs itemDrop = other.gameObject.GetComponent<ItemDropPrefabs>();
        if (itemDrop != null && itemDrop.GetItemDropData()!.Rank == this._itemDrop.Rank)
        {
            if (itemDrop.GetItemDropData().Rank < DbManager.GetInstance().GetItemDropMaxRank()!.Rank)
            {
                Vector3 mergePosition = (this.transform.position + itemDrop.transform.position) / 2;

                _poolMachine.ReturnObjectToPool(this.gameObject);
                _poolMachine.ReturnObjectToPool(itemDrop.gameObject);

                GameObject newFruit = _poolMachine.GetPooledObject(DbManager.GetInstance().GetItemDrop(_itemDrop.ID + 1));
                if (newFruit != null) newFruit.transform.position = mergePosition;
            }
            SetBodyType(RigidbodyType2D.Dynamic);
        }
        OnDropFinished?.Invoke();
        OnDropFinished = null;
    }

    #endregion

    #region Network

    #endregion
}
