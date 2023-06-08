using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Sensor;

public class JointStateSub : MonoBehaviour
{
    public ArticulationBody[] articulationBodies;
    public string topicName = "/joint_states";
    public int jointLength = 7;
    private ROSConnection ros;

    // Start is called before the first frame update
    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.Subscribe<JointStateMsg>(topicName, cb);
    }

    // Update is called once per frame
    void cb(JointStateMsg msg)
    {
        for (int i = 0; i < jointLength; i++)
        {
            ArticulationDrive aDrive = articulationBodies[i].xDrive;
            aDrive.target = Mathf.Rad2Deg * (float)msg.position[i];
            articulationBodies[i].xDrive = aDrive;
        }
    }
}
