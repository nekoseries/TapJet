using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldandSlingshot : MonoBehaviour
{
    public GameObject parent;
    public bool usingMouse;

    [Range(1,10)] public float multiplier;
    
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
    }


    void Stop()
    {
        if (canDrag)
        {
            parent.transform.position = gameObject.transform.position;
            gameObject.transform.parent = parent.transform;
        }
        else
        {
            desireVelocity = Vector2.zero;
            doAction = true;
        }
    }

    void Drag(Vector2 position)
    {
        gameObject.transform.position = (Vector2) Camera.main.ScreenToWorldPoint(position);
    }

    void DoAction()
    {
        
        if (doAction && !canDrag)
        {
            rigidbody.velocity = desireVelocity;
            canDrag = true;
        } else if (canDrag)
        {
            desireVelocity = parent.transform.position - gameObject.transform.position;
            gameObject.transform.parent = null;
            rigidbody.velocity = desireVelocity * multiplier;
            canDrag = false;
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
            if (canDrag)
            {
                Drag(Input.mousePosition);
            }
            else
            {
                if (firstPosition != (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition))
                {
                    Debug.Log("Meong");
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
                        } else doAction = false;
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
    
    
}
