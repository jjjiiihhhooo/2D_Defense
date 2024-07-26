using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{

}

[System.Serializable]
public class Data
{
    public Data(List<Item> _items, int _LV, int _SC, float _HP, float _ATK, float _DEF, float _EXP, float _AS, float _HR, float _curEXP)
    {
        Item_List = _items;
        LV = _LV;
        SC = _SC;
        HP = _HP;
        ATK = _ATK;
        DEF = _DEF;
        EXP = _EXP;
        AS = _AS;
        HR = _HR;
        curEXP = _curEXP;
    }

    public Data()
    {


    }

    public List<Item> Item_List;

    public int LV;
    public int SC; //Stage Count
    public float HP; //Max HP
    public float ATK;
    public float DEF;
    public float curEXP;
    public float EXP;
    public float AS; //Attack Speed
    public float HR; //HP Repear
}

public class DataManager : MonoBehaviour
{
    [SerializeField] private float gold;
    [SerializeField] private List<Item> item_List;

    public Data data;
    
    public float Gold { get => gold; }

    public void DefaultData()
    {
        item_List = new List<Item>();

        data = new Data(item_List, 1, 1, 100f, 1f, 0f, 100f, 2f, 0.1f, 0f);
    }

    public void SetData(Data _data)
    {
        data = _data;

    }

    public void SetData()
    {
        data = new Data();
    }

    public void PlusGold(float plus)
    {
        gold += plus;
    }
}
