using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberCube : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rb2d;

    public virtual void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    public virtual void Start()
    {
        rb2d.velocity = new Vector3(0,-speed,0);
    }

    public virtual void Update()
    {
        if (transform.position.y < -GameManager.CamHeight + transform.localScale.y / 2)
        {
            rb2d.velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, -GameManager.CamHeight + transform.localScale.y / 2, transform.position.z); 
        }

    }

}
