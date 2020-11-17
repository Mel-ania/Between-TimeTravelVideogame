﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform player = null;
    [SerializeField] private Transform end = null;
    private float min;
    private float max;

    private void Awake()
    {
        min = 0f;
        max = end.position.x - 7.5f;
    }

    private void LateUpdate()
    {
        Vector3 temp = transform.position;
        temp.x = player.position.x;
        if(temp.x > min && temp.x < max)
        {
            transform.position = temp;
        }
    }
}