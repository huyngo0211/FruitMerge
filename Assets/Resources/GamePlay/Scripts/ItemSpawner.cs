using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    #region Define

    [SerializeField] private GameObjectPool pool;
    
    #endregion

    #region Properties

    private ItemDropPrefabs _currItemDrop;
    
    #endregion

    #region Core MonoBehavior
    

    #endregion

    #region Public Method

    public ItemDropPrefabs GetCurItemDrop()
    {
        return _currItemDrop;
    }
    
    public void SetCurItemDrop(ItemDropPrefabs item)
    {
        _currItemDrop = item;
    }
    
    public void SpawnItem(Vector3 pos, ItemDrop itemData)
    {
        GameObject itemObj = pool.GetPooledObject(itemData);
        if (itemObj != null)
        {
            itemObj.transform.position = pos;
            _currItemDrop = itemObj.GetComponent<ItemDropPrefabs>();
            _currItemDrop.SetBodyType(RigidbodyType2D.Kinematic);
        }
    }
    
    #endregion

    #region Private Method

    #endregion

    #region Network

    #endregion
}
