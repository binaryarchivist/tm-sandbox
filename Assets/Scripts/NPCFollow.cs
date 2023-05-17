using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCFollow : MonoBehaviour
{
    public Player Player;
    public float TargetDistance;
    public float AllowedDistance = 5;

    public GameObject NPC;
    public float FollowSpeed;
    public RaycastHit Shot;

    public int prob = 50;
    
    void Update()
    {
        transform.LookAt(Player.transform);

        if (Physics.Raycast(
                transform.position,
                transform.TransformDirection(Vector3.forward),
                out Shot
            ))
        {
            TargetDistance = Shot.distance;

            if (TargetDistance >= AllowedDistance)
            {
                FollowSpeed = 0.02f;
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    Player.transform.position,
                    FollowSpeed
                );
            }
            else
            {
                FollowSpeed = 0;
            }
        }

        TakePlayerObject();
    }

    private bool _shouldTakeObject()
    {
        float r = Random.Range(0, prob) * Time.fixedDeltaTime;

        return r == 0;
    }

    public void TakePlayerObject()
    {
        KitchenObject kitchenObject = Player.GetKitchenObject();
        
        if (kitchenObject && _shouldTakeObject() && TargetDistance <= AllowedDistance)
        {
            kitchenObject.DestroySelf();
            Player.ClearKitchenObject();
        }
    }
}
