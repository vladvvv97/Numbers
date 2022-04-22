using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class NumberCube : MonoBehaviour
{     
    private SpriteRenderer _sr;
    private Rigidbody2D _rb2d;
    private TextMeshPro _tmpro;

    private float _initialMousePositionX;

    public bool ReadyToMerge { get => _readyToMerge;  private set => _readyToMerge = value; }
    private bool _readyToMerge = false;
    public int Value { get => _value; private set => _value = value; }    
    private int _value;

    protected virtual void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _tmpro = GetComponentInChildren<TextMeshPro>();
    }
    protected virtual void Start()
    {
        _rb2d.sleepMode = RigidbodySleepMode2D.NeverSleep;
        _rb2d.gravityScale = 0;
        _rb2d.velocity = new Vector2(0, -GameManager.Instance.Speed);

        Value = GameManager.Instance.CubeValueTypeChance[Random.Range(0, GameManager.Instance.CubeValueTypeChance.Length)];

        _tmpro.text = Value.ToString();
        if (Value == 10)
        {
            _tmpro.enableAutoSizing = false;
            _tmpro.fontSize = 6f;
        }

        switch (PlayerPrefs.GetInt("CubeIndex"))
        {
            case 0:
                _sr.sprite = SkinsManager.Instance.Cubes1[Value - 1];
                break;
            case 1:
                _sr.sprite = SkinsManager.Instance.Cubes2[Value - 1];
                break;
            case 2:
                _sr.sprite = SkinsManager.Instance.Cubes3[Value - 1];
                break;
            case 3:
                _sr.sprite = SkinsManager.Instance.Cubes4[Value - 1];
                break;
        }
    }

    protected virtual void Update()
    {
        if (_rb2d.velocity.y < -0.01f)
        {
            ReadyToMerge = false;          
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        NumberCube OtherCube = collision.gameObject.GetComponent<NumberCube>();

        
        if(GameManager.Instance.AllCubesReadyToMerge && GameManager.Instance._timer >= GameManager.Instance.WaitForReadyToMerge)
        {           
            if (OtherCube && OtherCube.Value == this.Value)
            {
                
                if (!GameManager.Instance.CubesToDestroy.Contains(this))
                {
                    GameManager.Instance.CubesToDestroy.Add(this);
                }

                Instantiate(GameManager.Instance.VFX, this.transform.position, Quaternion.identity, null);
                Destroy(this.gameObject);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.Instance.CurrentCube)
        {
            AudioManager.Instance.Sounds.PlaySound(AudioManager.eAudioNames.Collision);
            Vibration.Vibrate(new long[] { 0, 25, 600, 25 });
            GameManager.Instance.SetSpeedEqualDropSpeed();
            GameManager.Instance.CurrentCube.transform.DetachChildren();
            Destroy(GameManager.Instance.CurrentCube);
        }

        InvokeRepeating(nameof(SetReadyToMergeToTrue), GameManager.Instance.WaitForReadyToMerge, GameManager.Instance.WaitForReadyToMerge);
        _rb2d.gravityScale = 1;       
    }
    private void SetReadyToMergeToTrue()
    {
        ReadyToMerge = true;        
    }

}