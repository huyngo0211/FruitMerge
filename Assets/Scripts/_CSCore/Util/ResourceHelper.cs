using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHelper : MonoBehaviour
{
    #region Load DB

    public static byte[] LoadDbBinContent(string fileDb)
    {
        return LoadDbBinContentByLanguage(fileDb);
    }

    private static byte[] LoadDbBinContentByLanguage(string fileDb)
    {
        string pathDbFile = "_Db/" + fileDb;
        
        TextAsset textAsset = Resources.Load<TextAsset>(pathDbFile);

        return textAsset?.bytes;
    }
    
    public static string LoadDbTextContent(string fileDb)
    {
        return LoadDbTextContentByLanguage(fileDb);
    }
    
    private static string LoadDbTextContentByLanguage(string fileDb)
    {
        string textDb = "";
        string pathDbFile = "_Db/" + fileDb;
        //
        if (string.IsNullOrEmpty(textDb))
        {
            TextAsset textAsset = Resources.Load<TextAsset>(pathDbFile);
            //
            if (textAsset != null)
                return textAsset.text;
            else
                return "";
        }
        else
            return textDb;
    }


    #endregion
    
    public static Sprite LoadSprite(string pathSprite, string pathDefault = "_Common/Images/Icon/icon_unknown")
    {
        Sprite sprite = null;
        
        sprite = Resources.Load<Sprite>(pathSprite);

        if (sprite == null)
        {
            sprite = Resources.Load<Sprite>(pathDefault);
            Debug.LogError($"Error loading Sprite: {pathSprite}");
        }

        return sprite;
    }
    
}
