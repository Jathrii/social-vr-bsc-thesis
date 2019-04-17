using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SocialVR
{
    public class LazyUI : MonoBehaviour
    {
        private bool done = true;
        private void Start()
        {
            Transform VRCamTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().transform;

            Debug.Log(VRCamTransform.forward.x + " " + VRCamTransform.forward.y + " " + VRCamTransform.forward.z);
            Debug.Log(transform.forward.x + " " + transform.forward.y + " " + transform.forward.z);
        }

        // Update is called once per frame
        void Update()
        {
            Transform VRCamTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().transform;


            if (Vector3.Angle(VRCamTransform.forward, transform.forward) > 30.0f && done)
            {
                done = false;
                float angle = VRCamTransform.eulerAngles.y - transform.eulerAngles.y;
                StartCoroutine(RotateObject(FlowController.startPosition.position, Vector3.up, angle, 0.5f));
            }

            /*if (done)
            {
                Debug.Log(VRCamTransform.forward.x + " " + VRCamTransform.forward.y + " " + VRCamTransform.forward.z);
                Debug.Log(transform.forward.x + " " + transform.forward.y + " " + transform.forward.z);

                done = false;
            }*/
        }

        private IEnumerator RotateObject(Vector3 point, Vector3 axis, float rotateAmount, float rotateTime)
        {
            var rotation = Quaternion.AngleAxis(rotateAmount, axis);

            Vector3 startPos = transform.position - point;
            Vector3 endPos = rotation * startPos;
            Quaternion startRot = transform.rotation;
            float step = 0.0f; //non-smoothed
            float smoothStep = 0.0f; //smoothed
            float rate = 1.0f / rotateTime; //amount to increase non-smooth step by

            while (step < 1.0)
            { // until we're done
                step += Time.deltaTime * rate; //increase the step
                smoothStep = Mathf.SmoothStep(0.0f, 1.0f, step);
                transform.position = point + Vector3.Slerp(startPos, endPos, smoothStep);
                transform.rotation = startRot * Quaternion.Slerp(Quaternion.identity,
                                                                rotation, smoothStep);
                yield return null;
            }
            //finish any left-over
            if (step > 1.0)
            {
                transform.position = point + endPos;
                transform.rotation = startRot * rotation;
            }

            done = true;

            yield return null;
        }
    }
}