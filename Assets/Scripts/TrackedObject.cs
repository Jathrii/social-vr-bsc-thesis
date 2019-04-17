using UnityEngine;
using Valve.VR;

namespace SocialVR
{
    public class TrackedObject : MonoBehaviour
    {
        public enum EIndex
        {
            None = -1,
            Hmd = (int)OpenVR.k_unTrackedDeviceIndex_Hmd,
            Device1,
            Device2,
            Device3,
            Device4,
            Device5,
            Device6,
            Device7,
            Device8,
            Device9,
            Device10,
            Device11,
            Device12,
            Device13,
            Device14,
            Device15
        }

        public enum Hand
        {
            Left = 0,
            Right = 1
        }

        public Hand hand;

        private EIndex index;

        private int[] trackerIndices;

        [Tooltip("If not set, relative to parent")]
        public Transform origin;

        public bool isValid { get; private set; }

        private void OnNewPoses(TrackedDevicePose_t[] poses)
        {
            if (index == EIndex.None)
                return;

            var i = (int)index;

            isValid = false;
            if (poses.Length <= i)
                return;

            if (!poses[i].bDeviceIsConnected)
                return;

            if (!poses[i].bPoseIsValid)
                return;

            isValid = true;

            var pose = new SteamVR_Utils.RigidTransform(poses[i].mDeviceToAbsoluteTracking);

            if (origin != null)
            {
                transform.position = origin.transform.TransformPoint(pose.pos);
                transform.rotation = origin.rotation * pose.rot;
            }
            else
            {
                transform.localPosition = pose.pos;
                transform.localRotation = pose.rot;
            }
        }

        SteamVR_Events.Action newPosesAction;

        TrackedObject()
        {
            newPosesAction = SteamVR_Events.NewPosesAction(OnNewPoses);
        }

        private void Awake()
        {
            OnEnable();
        }

        void OnEnable()
        {
            var render = SteamVR_Render.instance;
            if (render == null)
            {
                enabled = false;
                return;
            }

            newPosesAction.enabled = true;
        }

        void OnDisable()
        {
            newPosesAction.enabled = false;
            isValid = false;
        }

        public void SetDeviceIndex(int index)
        {
            if (System.Enum.IsDefined(typeof(EIndex), index))
                this.index = (EIndex)index;
        }

        private void Start()
        {
            if (!SteamVR.active)
                return;

            var error = ETrackedPropertyError.TrackedProp_Success;

            trackerIndices = new int[2];
            bool found = false;

            for (int i = 0; i < 7; i++)
            {
                var result = new System.Text.StringBuilder((int)64);
                OpenVR.System.GetStringTrackedDeviceProperty((uint)i, ETrackedDeviceProperty.Prop_RenderModelName_String, result, 64, ref error);
                Debug.Log(i + ": " + result.ToString());

                if (result.ToString().Contains("tracker"))
                {
                    if (found)
                        trackerIndices[1] = i;
                    else
                    {
                        trackerIndices[0] = i;
                        found = true;
                    }
                }
            }

            if (hand == Hand.Left)
                SetDeviceIndex(trackerIndices[0]);
            else
                SetDeviceIndex(trackerIndices[1]);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                int temp = trackerIndices[0];
                trackerIndices[0] = trackerIndices[1];
                trackerIndices[1] = temp;

                if (hand == Hand.Left)
                    SetDeviceIndex(trackerIndices[0]);
                else
                    SetDeviceIndex(trackerIndices[1]);
            }
        }
    }
}