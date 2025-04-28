using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using SimpleJSON;
using Newtonsoft.Json;
using UnityEditor;

public class DbManager
{
    private static DbManager instance;

    public static DbManager GetInstance()
    {
        if (instance == null) instance = new DbManager();
        
        return instance;
    }

    private DbManager()
    {
        
    }

    #region Define data
    
    private List<ItemDrop> _lstItemDrop = new List<ItemDrop>();

    #endregion
    
    public void LoadAllDb()
    {
        LoadDbItemDrop(ResourceHelper.LoadDbTextContent("db_item_drop"));
    }
    
    #region Item Drop

    public List<ItemDrop> GetLstItemDrop()
    {
        return _lstItemDrop;
    }

    [CanBeNull]
    public ItemDrop GetItemDrop(int itemKey)
    {
        return _lstItemDrop.Find(c => c.ID == itemKey);
    }

    [CanBeNull]
    public ItemDrop GetItemDropMaxRank()
    {
        return _lstItemDrop.LastOrDefault();
    }

    private void LoadDbItemDrop(string json)
    {
        _lstItemDrop = new List<ItemDrop>();
        JSONArray jsonArr = JSONArray.Parse(json).AsArray;
        for (int i = 0; i < jsonArr.Count; i++)
        {
            JSONClass jObj = jsonArr[i].AsObject;
            ItemDrop itemDrop = new ItemDrop(jObj["id"].AsInt, jObj["rate"].AsInt, jObj["rank"].AsInt, jObj["scale"].AsFloat);
            _lstItemDrop.Add(itemDrop);
        }
    }

    #endregion
}