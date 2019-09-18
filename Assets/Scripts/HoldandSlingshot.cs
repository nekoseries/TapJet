using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldandSlingshot : MonoBehaviour
{
    public GameObject anchor;
    private Vector2 anchorFirstPlace;
    
    public GameObject firstPlace;
    public bool usingMouse;

    [Range(1, 10), SerializeField] private float radius = 1;
    [Range(1, 15), SerializeField] private float momentumTime;
    private float timeElapse;
    [Range(1,10)] public float multiplierSpeed = 3;
    
    private Rigidbody2D rigidbody;
    private Vector2 desireVelocity;
    private bool canDrag;
    private bool doAction;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (usingMouse) MouseInput();
        else TouchInput();
        VelocityHandler();
        BoundingPlayer();
    }


    void Stop()
    {
        if (canDrag)
        {
            anchor.transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
            anchorFirstPlace = anchor.transform.position;
            firstPlace.SetActive(true);
            firstPlace.transform.position = gameObject.transform.position;
            gameObject.transform.parent = anchor.transform;
            GameVariables.dragable = true;
        }
        else
        {
            desireVelocity = Vector2.zero;
            doAction = true;
            
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }

    void Drag(Vector2 position)
    {
        gameObject.transform.position = TranslateInsideCircle((Vector2) Camera.main.ScreenToWorldPoint(position), firstPlace.transform.position, radius);
    }

    void DoAction()
    {
        
        if (doAction && !canDrag)
        {
            rigidbody.velocity = desireVelocity;
            canDrag = true;
        } else if (canDrag)
        {
            desireVelocity = firstPlace.transform.position - gameObject.transform.position;
            gameObject.transform.parent = null;
            rigidbody.velocity = (desireVelocity * multiplierSpeed);
            /*GameVariables.yVelocity = 0;*/
            canDrag = false;
            GameVariables.dragable = false;
            firstPlace.SetActive(false);
            
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }


    private Vector2 firstPosition;
    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Stop();
            firstPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            if (GameVariables.dragable)
            {
                Drag(Input.mousePosition);
            }
            else
            {
                if (firstPosition != (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition))
                {
                    GetComponent<SpriteRenderer>().color = Color.white;
                    
                    doAction = false;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            DoAction();
        }
    }
  
    void TouchInput()
    {
        if (Input.touchCount>0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.fingerId == 0)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        Stop();
                        break;
                    
                    case TouchPhase.Moved:
                        if (canDrag)
                        {
                            Drag(touch.position);
                        }
                        else
                        {
                            GetComponent<SpriteRenderer>().color = Color.white;
                            
                            doAction = false;
                        }
                        break;
                    
                    case TouchPhase.Ended:
                        DoAction();
                        break;
                    
                    /*case TouchPhase.Canceled:
                        Cancel();
                        break;*/
                    
                }
            }
        }
    }
    
    Vector2 TranslateInsideCircle(Vector2 _position, Vector2 _center, float _radius)
    {
        float distance = Vector3.Distance(_position, _center);
        if (distance >= _radius) //To keep in the inside circle
        {
            Vector2 fromOriginToObject = _position - _center;
            fromOriginToObject *= _radius / distance; 
            return _center + fromOriginToObject;
        }
        return _position;
    }

    void VelocityHandler()
    {
        if (rigidbody.velocity == Vector2.zero)
        {
            timeElapse = momentumTime;
        }
        else
        {
            timeElapse -= Time.deltaTime;
            /*if (desireVelocity.y > 0 && gameObject.transform.position.y > 0)
            {
                GameVariables.yVelocity = rigidbody.velocity.y;
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
                gameObject.transform.position = new Vector2(gameObject.transform.position.x , 0);
            }*/

            rigidbody.velocity *= (timeElapse / momentumTime);

            /*if (gameObject.transform.position.y == 0)
            {
                GameVariables.yVelocity *= (timeElapse/momentumTime);
            }*/

        }
    }

    void BoundingPlayer()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -2.7f, 2.7f), transform.position.y);
    }
    
}
