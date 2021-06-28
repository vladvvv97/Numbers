using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberCube : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private BoxCollider2D[] bc2d;
    private bool _readyToMerge = false;

    protected virtual void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    protected virtual void Start()
    {
        rb2d.sleepMode = RigidbodySleepMode2D.NeverSleep;
        rb2d.velocity = new Vector3(0, -GameManager.Instance.Speed, 0);

    }

    protected virtual void Update()
    {
        if (rb2d.velocity.y == 0)
        {
            Invoke(nameof(ReadyToMerge),GameManager.Instance.WaitForTriggerEnterCheckTime);
        }

    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<NumberCube>() && _readyToMerge == true)
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void ReadyToMerge()
    {
        _readyToMerge = true;
    }

}