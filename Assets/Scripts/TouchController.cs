using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    [SerializeField] private BacklightLines backlightLines;
    [SerializeField] private float deadZone = 0.5f;

    private Vector2 _startTapPosition;
    private Vector2 _endTapPosition;
    private Vector2 _deltaPosition;

    private float x0;
    private float x1;
    private float x2;

    private float _x0;
    private float _x1;
    private float _x2;

    private bool _isMobile;
    private bool _alreadyTouch = false;
    private bool _isAlreadyPositionSet = false;

    private bool _isSwiping;
    private int _swipeCounter = 1;    

    public bool IsSwiping { get => _isSwiping; set => _isSwiping = value; }
    public int SwipeCounter { get => _swipeCounter; private set { if (value > 5) _swipeCounter = 0; else if (value < 0) _swipeCounter = 0; else _swipeCounter = value; } }

    void Start()
    {
        _isMobile = Application.isMobilePlatform;
    }

    private void Update()
    {
        if (!_isMobile)
        {
            if (Input.GetMouseButtonDown(0) && _alreadyTouch == false)
            {               
                _startTapPosition = GameManager.Instance.MousePosition;               
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _endTapPosition = GameManager.Instance.MousePosition;
                _deltaPosition = _endTapPosition - _startTapPosition;
                if (_deltaPosition.magnitude > deadZone)
                {
                    IsSwiping = true;
                    Swipe();
                }
                else
                {
                    ResetSwipe();//IsSwiping = false;
                    _alreadyTouch = true;
                    SwipeCounter = 1;
                    backlightLines.EnableBacklights();
                    Vibration.Vibrate(25);
                }
            }
        }
        else
        {
            if (Input.touchCount > 0 && _alreadyTouch == false)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _startTapPosition = Input.GetTouch(0).position;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    _endTapPosition = Input.GetTouch(0).position;
                    _deltaPosition = _endTapPosition - _startTapPosition;

                    if (_deltaPosition.magnitude > deadZone)
                    {
                        IsSwiping = true;
                        Swipe();
                    }
                    else
                    {
                        ResetSwipe();//IsSwiping = false;
                        _alreadyTouch = true;
                        SwipeCounter = 1;
                        backlightLines.EnableBacklights();
                        Vibration.Vibrate(25);
                    }
                }
            }
        }

        if (GameManager.Instance.AllCubesReadyToMerge == true)
        {
            _alreadyTouch = false;
            ResetSwipe();
            _isAlreadyPositionSet = false;
        }
    }
    public void Swipe()
    {
        GameObject currentCube = GameManager.Instance.CurrentCube; 
        
        if (currentCube && currentCube.gameObject.GetComponentInChildren<TwoNumberCube>())
        {
            AudioManager.Instance.Sounds.PlaySound(AudioManager.eAudioNames.Swipe);
            Vibration.Vibrate(35);
            AchievementConditionManager.Instance.InvokeOnSwipeAction();

            NumberCube[] cubes = currentCube.GetComponentsInChildren<NumberCube>();
            float x1 = cubes[cubes.GetLowerBound(0)].transform.position.x;
            float x2 = cubes[cubes.GetUpperBound(0)].transform.position.x;
            float temp = cubes[cubes.GetLowerBound(0)].transform.position.x;

            cubes[cubes.GetLowerBound(0)].transform.position = new Vector2(x2, cubes[cubes.GetLowerBound(0)].transform.position.y);
            cubes[cubes.GetUpperBound(0)].transform.position = new Vector2(temp, cubes[cubes.GetUpperBound(0)].transform.position.y);
        }
        else if (currentCube && currentCube.gameObject.GetComponentInChildren<ThreeNumberCube>())
        {
            AudioManager.Instance.Sounds.PlaySound(AudioManager.eAudioNames.Swipe);
            Vibration.Vibrate(35);
            AchievementConditionManager.Instance.InvokeOnSwipeAction();

            NumberCube[] cubes = currentCube.GetComponentsInChildren<NumberCube>();

            if (cubes[0].Value == cubes[1].Value || cubes[0].Value == cubes[2].Value || cubes[1].Value == cubes[2].Value)
            {
                _x0 = cubes[0].transform.position.x;
                _x1 = cubes[1].transform.position.x;
                _x2 = cubes[2].transform.position.x;

                cubes[0].transform.position = new Vector2(_x1, cubes[0].transform.position.y);
                cubes[1].transform.position = new Vector2(_x2, cubes[1].transform.position.y);
                cubes[2].transform.position = new Vector2(_x0, cubes[2].transform.position.y);
            }
            else
            {

                if (_isAlreadyPositionSet == false)
                {
                    x0 = cubes[0].transform.position.x;
                    x1 = cubes[1].transform.position.x;
                    x2 = cubes[2].transform.position.x;

                    _isAlreadyPositionSet = true;
                }
                switch (_swipeCounter)
                {
                    case 0:
                        cubes[0].transform.position = new Vector2(x0, cubes[0].transform.position.y);
                        cubes[1].transform.position = new Vector2(x1, cubes[1].transform.position.y);
                        cubes[2].transform.position = new Vector2(x2, cubes[2].transform.position.y);
                        SwipeCounter++;
                        break;
                    case 1:
                        cubes[0].transform.position = new Vector2(x0, cubes[0].transform.position.y);
                        cubes[1].transform.position = new Vector2(x2, cubes[1].transform.position.y);
                        cubes[2].transform.position = new Vector2(x1, cubes[2].transform.position.y);
                        SwipeCounter++;
                        break;
                    case 2:
                        cubes[0].transform.position = new Vector2(x1, cubes[0].transform.position.y);
                        cubes[1].transform.position = new Vector2(x0, cubes[1].transform.position.y);
                        cubes[2].transform.position = new Vector2(x2, cubes[2].transform.position.y);
                        SwipeCounter++;
                        break;
                    case 3:
                        cubes[0].transform.position = new Vector2(x1, cubes[0].transform.position.y);
                        cubes[1].transform.position = new Vector2(x2, cubes[1].transform.position.y);
                        cubes[2].transform.position = new Vector2(x0, cubes[2].transform.position.y);
                        SwipeCounter++;
                        break;
                    case 4:
                        cubes[0].transform.position = new Vector2(x2, cubes[0].transform.position.y);
                        cubes[1].transform.position = new Vector2(x0, cubes[1].transform.position.y);
                        cubes[2].transform.position = new Vector2(x1, cubes[2].transform.position.y);
                        SwipeCounter++;
                        break;
                    case 5:
                        cubes[0].transform.position = new Vector2(x2, cubes[0].transform.position.y);
                        cubes[1].transform.position = new Vector2(x1, cubes[1].transform.position.y);
                        cubes[2].transform.position = new Vector2(x0, cubes[2].transform.position.y);
                        SwipeCounter++;
                        break;

                    default:
                        break;
                }

            }            
        }
        else
        {
            return;
        }
    }
    public void ResetSwipe()
    {
        IsSwiping = false;
        SwipeCounter = 1;
        _isAlreadyPositionSet = false;
        //_startTapPosition = Vector2.zero;
        //_endTapPosition = Vector2.zero;
        //_deltaPosition = Vector2.zero;
    }
    
}
