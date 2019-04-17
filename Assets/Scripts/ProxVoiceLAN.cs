using Adrenak.UniMic;
using Adrenak.UniStream;
using Adrenak.UniVoice;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace SocialVR
{
    public class ProxVoiceLAN : NetworkBehaviour
    {
        public Mic Mic { get; private set; }
        public VolumeGate gate;

        /// <summary>
        /// Used to enable or disable the Mic
        /// </summary>
        public bool speaking;

        private AudioStreamerComponent streamer;

        // Must be constant for all clients
        /// <summary>
        /// The sampling frequency at which the Mic should operate
        /// </summary>
        const int k_MicFrequency = 16000;

        /// <summary>
        /// The Mic outputs audio data as segments of arbitrary length. Ensure that 1000 % k_MicSegLenMS == 0
        /// </summary>
        const int k_MicSegLenMS = 100;

        /// <summary>
        /// The number of audio channels used by the Mic
        /// </summary>
        [SyncVar]
        private int channels;

        Text speakingIndicator;

        private void Start()
        {
            if (!isLocalPlayer)
                return;

            // MIC SETUP
            Mic = Mic.Instance;
            Mic.StartRecording(k_MicFrequency, k_MicSegLenMS);

            gate = new VolumeGate(120, 1f, 5);

            // When the microphone is ready with a segment
            Mic.OnSampleReady += OnSampleReady;

            // Enable Mic
            speaking = true;

            // Find speakingIndicator
            speakingIndicator = GameObject.FindGameObjectWithTag("speakingIndicator").GetComponent<Text>();

            CmdSetChannels(Mic.Clip.channels);
        }

        private void Update()
        {
            if (!isLocalPlayer)
                return;

            if (Input.GetKeyDown(KeyCode.M))
                speaking = !speaking;

            if (speakingIndicator)
            {
                if (speaking)
                    speakingIndicator.text = "Speaking";
                else
                    speakingIndicator.text = "Muted";
            }
        }

        private void OnSampleReady(int index, float[] segment)
        {
            // Return checks
            if (!speaking) return;

            // NOISE REMOVAL
            // Very primitive way to reduce the audio input noise
            // by averaging audio samples with a radius (radius value suggested: 2)
            int radius = 2;
            for (int i = radius; i < segment.Length - radius; i++)
            {
                float temp = 0;
                for (int j = i - radius; j < i + radius; j++)
                    temp += segment[j];
                segment[i] = temp / (2 * radius + 1);
            }

            if (!gate.Evaluate(segment)) return;

            // If Speaking is on, create a payload byte array and send
            CmdSendAudioSegment(
                new UniStreamWriter()
                .WriteInt(index)
                .WriteFloatArray(segment)
                .Bytes
            );
        }

        [Command]
        private void CmdSetChannels(int channels)
        {
            this.channels = channels;
        }

        [Command]
        private void CmdSendAudioSegment(byte[] payload)
        {
            var reader = new UniStreamReader(payload);
            var index = reader.ReadInt();
            var segment = reader.ReadFloatArray();

            // If the streamer is getting filled, request for a packet skip
            if (streamer)
                streamer.Stream(index, segment);
            else if (channels > 0)
            {
                var segLen = k_MicFrequency / 1000 * k_MicSegLenMS;
                var segCap = 1000 / k_MicSegLenMS;

                // Create an AudioBuffer using the Mic values
                AudioBuffer buffer = new AudioBuffer(
                    k_MicFrequency,
                    channels,
                    segLen,
                    segCap
                );

                AudioSource source = GetComponent<AudioSource>();

                // Use the buffer to create a streamer
                streamer = AudioStreamerComponent.New(gameObject, buffer, source);

                streamer.Stream(index, segment);
            }
            
            RpcSendAudioSegment(payload);
        }

        [ClientRpc]
        private void RpcSendAudioSegment(byte[] payload)
        {
            if (isLocalPlayer)
                return;

            var reader = new UniStreamReader(payload);
            var index = reader.ReadInt();
            var segment = reader.ReadFloatArray();

            // If the streamer is getting filled, request for a packet skip
            if (streamer)
                streamer.Stream(index, segment);
            else if (channels > 0)
            {
                var segLen = k_MicFrequency / 1000 * k_MicSegLenMS;
                var segCap = 1000 / k_MicSegLenMS;

                // Create an AudioBuffer using the Mic values
                AudioBuffer buffer = new AudioBuffer(
                    k_MicFrequency,
                    channels,
                    segLen,
                    segCap
                );

                AudioSource source = GetComponent<AudioSource>();

                // Use the buffer to create a streamer
                streamer = AudioStreamerComponent.New(gameObject, buffer, source);

                streamer.Stream(index, segment);
            }
        }
    }
}