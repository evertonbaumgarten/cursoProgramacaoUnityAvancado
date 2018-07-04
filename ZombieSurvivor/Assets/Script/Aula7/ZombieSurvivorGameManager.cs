﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSurvivorGameManager : MonoBehaviour, HitEventHandler
{

    public static ZombieSurvivorGameManager instance;
    [SerializeField]
    protected SoldierStateMachineManager playerStateMachine;
    [SerializeField]
    private GameObject sparkPrefab;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }

    private void Start()
    {
        //Cadastra-se como ouvinte do evento de colisão
        (playerStateMachine.getState(SoldierStateMachineManager.SHOOT_STATE) as ShootingStateBehaviour).hitHandler = this;
        (playerStateMachine.getState(SoldierStateMachineManager.THROW_STATE) as ShootingStateBehaviour).hitHandler = this;
    }

    public void OnAgentHited(WeaponDataObject weapon, GameObject agentHited, Vector3 hitPoint)
    {
        // O teste de munição não deve ser mais feito aqui no Manager, pois cada arma possui sua especificidade

        if (agentHited.CompareTag("Hitable"))
        {
            GameObject spark = Instantiate(weapon.particle);
            spark.transform.position = hitPoint;
            spark.transform.LookAt(agentHited.transform);
            Destroy(spark, spark.GetComponent<ParticleSystem>().main.duration);

            if (agentHited.GetComponent<Animator>())
                agentHited.GetComponent<Animator>().SetTrigger("Hit");
            if (agentHited.GetComponent<AgentData>())
            {
                agentHited.GetComponent<AgentData>().life -= weapon.damage;
                if (agentHited.GetComponent<AgentData>().life <= 0)
                {
                    agentHited.GetComponent<Animator>().SetTrigger("Dead");
                    Destroy(agentHited, 5);
                }
            }

        }
    }
}
