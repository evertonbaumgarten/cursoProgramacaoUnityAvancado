﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPostBehaviour : MonoBehaviour {

    public bool directionInvertion;
    private int direction;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            Vector3 direction = other.transform.position - transform.position;
            Debug.Log(direction);
            if(direction.z>0 && directionInvertion || direction.z < 0 && !directionInvertion)
                PinFootBallManager.instance.onGoal(gameObject);
        }
        
    }
}
