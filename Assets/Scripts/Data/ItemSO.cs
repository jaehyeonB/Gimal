using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item", fileName = "NewItem")]
public class ItemSO : ScriptableObject
{
    [Header("이속 증가율")]
    public int IncSpeed = 5;
}
