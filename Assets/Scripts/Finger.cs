using UnityEngine;

namespace SocialVR
{
    public class Finger
    {
        private float sensorMin;
        private float sensorMax;
        private AnimationCurve curve;
        private string animationState;

        public Finger(float sensorMin, float sensorMax, string animationState)
        {
            this.sensorMin = sensorMin;
            this.sensorMax = sensorMax;
            this.curve = AnimationCurve.Linear(sensorMin, 0, sensorMax, 0.9999999f);
            this.animationState = animationState;
        }

        public float evaluate(float sensorValue)
        {
            if (sensorValue < sensorMin)
                sensorValue = sensorMin;
            else if (sensorValue > sensorMax)
                sensorValue = sensorMax;

            return curve.Evaluate(sensorValue);
        }

        public string getAnimationState()
        {
            return animationState;
        }
    }
}