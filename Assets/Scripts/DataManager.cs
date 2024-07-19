using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private float gold;

    public float Gold { get => gold; }




    public void PlusGold(float plus)
    {
        gold += plus;
    }

}
