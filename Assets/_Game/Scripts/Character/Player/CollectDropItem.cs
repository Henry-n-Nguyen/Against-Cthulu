using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using HuySpace;

public class CollectDropItem : MonoBehaviour
{
    const int COIN_EXCHANGE_RATE = 45;
    const int COIN_DIF = 10;
    const int DIAMOND_EXCHANGE_RATE = 12;
    const int DIAMOND_DIF = 2;

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
            AudioManager.Ins.PlaySFX(SFX.Collect_Drop);

            int coin = Random.Range(COIN_EXCHANGE_RATE - COIN_DIF, COIN_EXCHANGE_RATE + COIN_DIF);
            data.Coin += coin;
            GamePlayManager.Ins.CoinUpdated(coin);
        }

        if (collision.gameObject.CompareTag(S_Constant.TAG_DROP_ITEM_DIAMOND))
        {
            AudioManager.Ins.PlaySFX(SFX.Collect_Drop);

            int diamond = Random.Range(DIAMOND_EXCHANGE_RATE - DIAMOND_DIF, DIAMOND_EXCHANGE_RATE + DIAMOND_DIF);
            data.Diamond += diamond;
            GamePlayManager.Ins.DiamondUpdated(diamond);
        }
    }
}
