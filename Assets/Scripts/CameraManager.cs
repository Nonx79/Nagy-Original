using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraManager : MonoBehaviour
{
    public  float speedCamera = 5;

    //map
    TileMap map;

    //Restrictions
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;

    public PhotonView view;

    private void Start()
    {
        map = GameObject.FindObjectOfType<TileMap>();
        /*
        up.transform.position = new Vector2(0, map.mapSizeY + 6);
        down.transform.position = new Vector2(0, -6);
        left.transform.position = new Vector2(-6, 0);
        right.transform.position = new Vector2(map.mapSizeX + 6, 0);
        */
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
       //if (view.IsMine)
       //{
            float moveH = Input.GetAxis("Horizontal");
            float moveV = Input.GetAxis("Vertical");

            Vector3 mvmtD = new Vector3(moveH, moveV, 0);

            transform.position = transform.position + mvmtD * speedCamera * Time.deltaTime;
       //}
       //else if (!view.IsMine)
       //{
       //     this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
       //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Restrictions")
        {
            Debug.Log("ola");
            speedCamera = .00000000000000000000000000000000000001f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Restrictions")
        {
            speedCamera = 5;
        }
    }
}
