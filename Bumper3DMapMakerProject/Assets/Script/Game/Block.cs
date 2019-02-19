using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Block
{
    public int objectNumber; // 0: NotGameOver, 1: GameOver, 2: ColorChangeItem
    public string shape;
    public string tag;
    public float pX;
    public float pY;
    public float pZ;
    public float sX;
    public float sY;
    public float sZ;
}