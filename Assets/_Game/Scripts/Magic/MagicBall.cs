using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : Magic
{
    [SerializeField] private GameObject ball;
    [SerializeField] private Transform ballTransform;
    [SerializeField] private Animator ballAnim;

    [SerializeField] private float speed = 4f;

    private float timer = 2.5f;

    private void Update()
    {
        Fly();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollideWithPlayer(collision);
    }

    private void CollideWithPlayer(Collider2D col)
    {
        if (!col.CompareTag(Constant.TAG_PLAYER)) return;

        ballAnim.SetTrigger(Constant.TRIGGER_DESPAWN);
    }

    private void Fly()
    {
        timer -= Time.deltaTime;

        if (timer < 0f) ballAnim.SetTrigger(Constant.TRIGGER_DESPAWN);

        float distance = speed * Time.deltaTime;

        ballTransform.position += ballTransform.right * distance;
    }

    public override void Spawn(Vector3 pos)
    {
        timer = 2.5f;

        ballTransform.position = pos;
        ball.SetActive(true);

        ballAnim.SetTrigger(Constant.TRIGGER_FLY);
    }

    public override void Despawn()
    {
        ball.SetActive(false);
        ballTransform.localPosition = Vector3.zero;
    }
}
