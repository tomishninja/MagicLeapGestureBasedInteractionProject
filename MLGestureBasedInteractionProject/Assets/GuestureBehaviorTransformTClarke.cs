using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class GuestureBehaviorTransformTClarke : MonoBehaviour {

    //
    public bool trackLeftHand = true;

    //
    public bool trackRightHand = false;

    Vector3 previousLeftHandTransformCenter = new Vector3(0,0,0);

    Vector3 previousRightHandTransformCenter = new Vector3(0, 0, 0);

    // Update is called once per frame
    void Update () {
        if (MLHands.IsStarted)
        {
            // the confidence of each hand will be set to zero by default if it isn't being tracked
            float confidenceLeftFist = trackLeftHand ? GetKeyPoseConfidenceFist(MLHands.Left) : 0.0f;
            float confidenceRightFist = trackRightHand ? GetKeyPoseConfidenceFist(MLHands.Right) : 0.0f;

            if(trackLeftHand && trackRightHand &&
                GetKeyPoseConfidenceFist(MLHands.Left) > 0.0f && 
                GetKeyPoseConfidenceFist(MLHands.Right) > 0.0f
                )
            {
                // Change the scale
            }
            else if (confidenceLeftFist > 0.0f)
            {
                this.ChangePosition(false);
                /*if (!previousLeftHandTransformCenter.Equals(new Vector3()))
                {
                    
                    
                    // left and right?
                    float newXValue = previousLeftHandTransformCenter.x - MLHands.Left.Center.x;
                    // up and down?
                    float newYValue = previousLeftHandTransformCenter.y - MLHands.Left.Center.y;
                    // backwards and forward?
                    float newZValue = previousLeftHandTransformCenter.z - MLHands.Left.Center.z;

                    // calculate the new x value

                    //TODO some maths to rotate this properly
                    transform.position = new Vector3(
                            transform.position.x + newXValue,
                            transform.position.y + newYValue,
                            transform.position.z + newZValue
                        );
                
                }*/

                //Update and set the new position
                previousLeftHandTransformCenter = MLHands.Left.Center;
            }

            if (confidenceRightFist > 0.0f)
            {
                // Move it as well just based on the right fist
                this.ChangePosition(true);
                previousRightHandTransformCenter = MLHands.Left.Center;
            }
        }
    }

    private void ChangePosition(bool isRightHand)
    {
        // set the variables
        Vector3 previousHandInfoCenter;
        MLHand currentHand;
        if (isRightHand)
        {
            previousHandInfoCenter = previousRightHandTransformCenter;
            currentHand = MLHands.Right;
        }
        else
        {
            previousHandInfoCenter = previousLeftHandTransformCenter;
            currentHand = MLHands.Left;
        }

        // perform the equasion //

        // if this is the first frame do nothing just save data
        // or else begin moving the object
        if (!previousLeftHandTransformCenter.Equals(new Vector3()))
        {

            // left and right?
            float newXValue = previousHandInfoCenter.x - currentHand.Center.x;
            // up and down?
            float newYValue = previousHandInfoCenter.y - currentHand.Center.y;
            // backwards and forward?
            float newZValue = previousHandInfoCenter.z - currentHand.Center.z;

            // calculate the new x value

            //TODO some maths to rotate this properly
            transform.position = new Vector3(
                    transform.position.x + newXValue,
                    transform.position.y + newYValue,
                    transform.position.z + newZValue
                );
        }

        //Update and set the new position
        /*
        if (isRightHand)
        {
            previousRightHandTransformCenter = MLHands.Right.Center;
        }
        else
        {
            previousLeftHandTransformCenter = MLHands.Left.Center;
        }
        */
    }



    /// <summary>
    /// Get the confidence value for the hand being tracked.
    /// </summary>
    /// <remarks>
    /// Copied from Key Pose Visualiser class
    /// </remarks>
    /// <param name="hand">Hand to check the confidence value on. </param>
    /// <returns></returns>
    private float GetKeyPoseConfidenceFist(MLHand hand)
    {
        if (hand != null)
        {
            if (hand.KeyPose == MLHandKeyPose.Fist)
            {
                return hand.KeyPoseConfidence;
            }
        }
        return 0.0f;
    }

}
