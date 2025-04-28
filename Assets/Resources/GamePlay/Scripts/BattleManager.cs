using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleManager : MonoBehaviour
{
    #region Define

    [SerializeField] private ItemSpawner spawner;
    
    #endregion

    #region Properties
    
    private Camera _mainCamera;
    private DbManager _dbManager;
    
    #endregion

    #region Core MonoBehavior

    private void Awake()
    {
        _dbManager = DbManager.GetInstance();
        _dbManager.LoadAllDb();
        _mainCamera = Camera.main;
        SpawnItemDrop();
    }


    private bool _isFalling = false;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isFalling)
        {
            Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            ItemDropPrefabs currItemDrop = spawner.GetCurItemDrop();
            currItemDrop.transform.position = new Vector3(mousePosition.x, currItemDrop.transform.position.y, 1);;
            _isFalling = true;
            currItemDrop.SetBodyType(RigidbodyType2D.Dynamic);
            currItemDrop.OnDropFinished = OnItemDropFinished;
        }
    }
    
    private void OnItemDropFinished()
    {
        _isFalling = false;
        spawner.SetCurItemDrop(null);
        SpawnItemDrop();
    }

    #endregion

    #region Public Method

    #endregion

    #region Private Method

    private void SpawnItemDrop()
    {
        // Viewport điểm (0,1) và (1,1)
        Vector3 leftTop = _mainCamera.ViewportToWorldPoint(new Vector3(0, 1, _mainCamera.nearClipPlane + 1f));
        Vector3 rightTop = _mainCamera.ViewportToWorldPoint(new Vector3(1, 1, _mainCamera.nearClipPlane + 1f));

        // Random vị trí x trong khoảng từ leftTop.x đến rightTop.x
        float randomX = Random.Range(leftTop.x, rightTop.x);
        float y = leftTop.y - 0.5f;

        ItemDrop itemDrop = RandomItem();
        Vector3 spawnPos = new Vector3(randomX, y, 0f);
        spawner.SpawnItem(spawnPos, itemDrop);
    }

    private ItemDrop RandomItem()
    {
        List<ItemDrop> lstItem = _dbManager.GetLstItemDrop();
        ItemDrop itemDrop = null;
        while (itemDrop == null)
        {
            foreach (var item in lstItem)
            {
                float roll = Random.Range(0f, 100f);
                if (roll <= item.Rate) itemDrop = _dbManager.GetItemDrop(item.ID);
            }
        }

        return itemDrop;
    }
    
    #endregion

    #region Network

    #endregion
}
