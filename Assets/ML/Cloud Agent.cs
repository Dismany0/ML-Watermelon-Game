using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Integrations.Match3;

public class CloudAgent : Agent
{
    public CloudControl cloudControl;
    public FruitManager fruitManager;
    public GameManager gameManager;

    // public BufferSensor bufferSensor;
    public BufferSensorComponent bufferSensor;
    void Start()
    {
        cloudControl = GetComponentInChildren<CloudControl>();
        fruitManager = GetComponentInChildren<FruitManager>();
        gameManager = GetComponentInChildren<GameManager>();
        bufferSensor = GetComponentInChildren<BufferSensorComponent>();
    }
    public override void OnEpisodeBegin()
    {
        cloudControl.Restart();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        //Collect position, handindex, nextindex of cloud
        sensor.AddObservation(cloudControl.transform.position.x - cloudControl.startPos.x);
        sensor.AddObservation(cloudControl.moving);
        sensor.AddOneHotObservation(cloudControl.handIndex, 5);
        sensor.AddOneHotObservation(cloudControl.nextIndex, 5);

        Fruit[] fruits = fruitManager.GetComponentsInChildren<Fruit>();
        //Collect position, index, velocity of each fruit
        for (int i = 0; i < fruits.Length; i++){
            float[] temp = new float[15];
            temp[0] = fruits[i].transform.position.x - cloudControl.startPos.x;
            temp[1] = fruits[i].transform.position.y - cloudControl.startPos.y;
            temp[2] = fruits[i].rb.velocity.x;
            temp[3] = fruits[i].rb.velocity.y;
            temp[fruits[i].index + 4] = 1;
            bufferSensor.AppendObservation(temp);
        }
        
        base.CollectObservations(sensor);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //Possible actions should be move left, move right, drop

        var Moves = actions.ContinuousActions[0];
        var action = actions.DiscreteActions[0];
        if(action == 1){
            cloudControl.MoveTo(Moves);
        }
        

        base.OnActionReceived(actions);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        
    }
}
