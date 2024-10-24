using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectDropItem : MonoBehaviour
{
    const int COIN_EXCHANGE_RATE = 100;
    const int COIN_DIF = 10;
    const int DIAMOND_EXCHANGE_RATE = 40;
    const int DIAMOND_DIF = 5;

    private UserData data;

    private void Start()
    {
        data = UserDataManager.Ins.userData;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollideWithDropItem(collision);
    }

    private void CollideWithDropItem(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(S_Constant.TAG_DROP_ITEM_COIN))
        {
            data.coin += Random.Range(COIN_EXCHANGE_RATE - COIN_DIF, COIN_EXCHANGE_RATE + COIN_DIF);
            GamePlayManager.Ins.CoinUpdated();
        }

        if (collision.gameObject.CompareTag(S_Constant.TAG_DROP_ITEM_DIAMOND))
        {
            data.diamond += Random.Range(DIAMOND_EXCHANGE_RATE - DIAMOND_DIF, DIAMOND_EXCHANGE_RATE + DIAMOND_DIF);
            GamePlayManager.Ins.DiamondUpdated();
        }
    }
}
