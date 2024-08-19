using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CountingData
{
    int _mapCounting;
    int _enemyCounting;
    int _restCounting;
    int _storeCounting;
    int _bossCounting;
    int _mysteryCounting;
    public int MapCounting { get { return _mapCounting; } set { _mapCounting = value; } }
    public int EnemyCounting { get { return _enemyCounting; } set { _enemyCounting = value; _mysteryCounting = value; } }
    public int RestCounting { get { return _restCounting; } set { _restCounting = value; } }
    public int StoreCounting { get { return _storeCounting; } set { _storeCounting = value; } }
    public int BossCounting { get { return _bossCounting; } set { _bossCounting = value; } }
    public int MysteryCounting { get { return _mysteryCounting; } set { _mysteryCounting = value; } }
    public void Init()
    {
        _mapCounting = 0;
        _enemyCounting = 0;
        _restCounting = 0;
        _storeCounting = 0;
        _bossCounting = 0;  
        _mysteryCounting = 0;
    }
    public void MapAddCounting()
    {
        MapCounting++;
    }
    public void EnemyAddCounting()
    {
        EnemyCounting++;
    }
    public void RestAddCounting()
    {
        RestCounting++;
    }
    public void StoreAddCounting()
    {
        StoreCounting++;
    }
    public void BossAddCounting()
    {
        BossCounting++;
    }
    public void MysteryAddCounting() 
    {
        MysteryCounting++;
    }
}
