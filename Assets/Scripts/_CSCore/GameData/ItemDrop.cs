using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop
{
    public int ID { get; private set; }
    public int Rate { get; private set; }
    public int Rank { get; private set; }
    public float Scale { get; private set; }

    public ItemDrop(int id, int rate, int rank, float scale)
    {
        this.ID = id;
        this.Rate = rate;
        this.Rank = rank;
        this.Scale = scale;
    }
}
