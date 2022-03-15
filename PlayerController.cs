using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _movespeed;
    private int _x, _z;
    public bool CanControll { get; set; }
    void Update()
    {
        if(!CanControll)
        {
            return;
        }

        _x = 0;
        _z = 0;

        if (Input.GetKey(KeyCode.A))
        {
            _x--;
        }
        if (Input.GetKey(KeyCode.W))
        {
            _z++;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _z--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _x++;
        }
            transform.position += new Vector3(_x, 0, _z) * _movespeed * Time.deltaTime;
            GameUIManager.Instance.ControllArrows(_x, _z);
    }
}
