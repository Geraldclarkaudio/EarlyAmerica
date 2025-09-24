using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Guard : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private GameObject[] _waypoints;
    [SerializeField]
    private int _targetWaypoint;
    [SerializeField]
    private bool reversing;

    private void Start()
    {
        _targetWaypoint = 0;
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _waypoints[_targetWaypoint].transform.position, _speed * Time.deltaTime);

        if(transform.position == _waypoints[_targetWaypoint].transform.position)
        {
            if (!reversing)
            {
                _targetWaypoint++;
                transform.eulerAngles = new Vector3(0, 180, 0);
                if(_targetWaypoint >= _waypoints.Length -1)
                {
                    reversing = true;
                }
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                _targetWaypoint--;
                if(_targetWaypoint < 0)
                {
                    _targetWaypoint = 0;
                    reversing = false;
                }
            }
        }
    }
}
