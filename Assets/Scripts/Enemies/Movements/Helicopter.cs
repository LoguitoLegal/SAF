using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public bool _sentidoDireita = true;
    public bool invert = false;
    private int timesInBorder = 0;
    private bool haveBeenSeen = false;
    public Renderer detectPointRender;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (detectPointRender.isVisible)
        {
            haveBeenSeen = true;
        }

        if (haveBeenSeen)
        {
            //Look at player
            transform.LookAt(player.transform);

            Move();

            ///Keep with camera

            //if (transform.position.x >= -0.639 && transform.position.x <= 0.639)
            //{

            //}

            if (transform.position.x >= 1.65f || transform.position.x <= -1.65f)
            {
                Destroy(gameObject);
            }
            if (_sentidoDireita && transform.position.x >= 0.56f && timesInBorder == 0)
            {
                invert = true;
            }
            else if (!_sentidoDireita && transform.position.x <= -0.56f && timesInBorder == 0)
            {
                invert = true;
            }
        }

    }

    private void LateUpdate()
    {
        if (haveBeenSeen)
        {
            transform.position += Vector3.forward * 0.24f * Time.deltaTime;
        }
    }

    void Move()
    {
        if (invert)
        {
            if (_sentidoDireita)
            {
                transform.position += -Vector3.right * Time.deltaTime * speed;
            }
            else
            {
                transform.position += Vector3.right * Time.deltaTime * speed;
            }
        }
        else
        {
            if (_sentidoDireita)
            {
                transform.position += Vector3.right * Time.deltaTime * speed;
            }
            else
            {
                transform.position += -Vector3.right * Time.deltaTime * speed;
            }
        }


        //if (!_sentidoDireita && transform.position.x <= -0.56f && timesInBorder == 0)
        //{
        //    timesInBorder++;
        //    speed = speed * -1;
        //}
    }
}
