﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolStateBehaviour : StateBehaviour {
    protected Animator characterAnimator;
    protected NavMeshAgent navMeshAgent;
    protected bool isWalkingTo = false;

    [SerializeField]
    protected Transform[] wayPoints;
    protected int wayPointIndex=-1;

    private void Start()
    {
        characterAnimator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    override public void onEnter(StateMachineManager machine)
    {
        walkTo(getNextPoint());
    }

    protected void walkTo(Vector3 target)
    {
        navMeshAgent.SetDestination(target);
        navMeshAgent.isStopped = false;
        isWalkingTo = true;
        characterAnimator.SetBool("Walk", true);
    }

    protected Vector3 getNextPoint()
    {
        //Verifica se está no final da lista. Se está, reinicia o index
        if (++wayPointIndex == wayPoints.Length)
            wayPointIndex = 0;
        return wayPoints[wayPointIndex].position;
    }

    // Update is called once per frame
    void Update () {
        //Ao finalizar um path, já dispara o próximo. 
        if (isWalkingTo && navMeshAgent.remainingDistance < 1)
            navMeshAgent.SetDestination(getNextPoint());
    }

    public override void onExit()
    {
        navMeshAgent.isStopped = true;
        characterAnimator.SetBool("Walk", false);
    }
}
