using Mediapipe;
using System.Collections.Generic;
using UnityEngine;

public class PoseTrackingGraph : DemoGraph {
  [SerializeField] private bool useGPU = true;

  private const string poseLandmarksStream = "pose_landmarks_smoothed";
  private OutputStreamPoller<NormalizedLandmarkList> poseLandmarksStreamPoller;
  private NormalizedLandmarkListPacket poseLandmarksPacket;

  private const string poseDetectionStream = "pose_detection";
  private OutputStreamPoller<Detection> poseDetectionStreamPoller;
  private DetectionPacket poseDetectionPacket;

  private const string poseLandmarksPresenceStream = "pose_landmarks_smoothed_presence";
  private OutputStreamPoller<bool> poseLandmarksPresenceStreamPoller;
  private BoolPacket poseLandmarksPresencePacket;

  private const string poseDetectionPresenceStream = "pose_detection_presence";
  private OutputStreamPoller<bool> poseDetectionPresenceStreamPoller;
  private BoolPacket poseDetectionPresencePacket;

  private GameObject annotation;

  void Awake() {
    annotation = GameObject.Find("PoseTrackingAnnotation");
  }

  public override Status StartRun(SidePacket sidePacket) {
    poseLandmarksStreamPoller = graph.AddOutputStreamPoller<NormalizedLandmarkList>(poseLandmarksStream).ConsumeValue();
    poseLandmarksPacket = new NormalizedLandmarkListPacket();

    poseDetectionStreamPoller = graph.AddOutputStreamPoller<Detection>(poseDetectionStream).ConsumeValue();
    poseDetectionPacket = new DetectionPacket();

    poseLandmarksPresenceStreamPoller = graph.AddOutputStreamPoller<bool>(poseLandmarksPresenceStream).ConsumeValue();
    poseLandmarksPresencePacket = new BoolPacket();

    poseDetectionPresenceStreamPoller = graph.AddOutputStreamPoller<bool>(poseDetectionPresenceStream).ConsumeValue();
    poseDetectionPresencePacket = new BoolPacket();

    return graph.StartRun(sidePacket);
  }

  public override void RenderOutput(WebCamScreenController screenController, Color32[] pixelData) {
    var poseTrackingValue = FetchNextPoseTrackingValue();
    RenderAnnotation(screenController, poseTrackingValue);

    var texture = screenController.GetScreen();
    texture.SetPixels32(pixelData);

    texture.Apply();
  }

  private PoseTrackingValue FetchNextPoseTrackingValue() {
    if (!FetchNextPoseLandmarksPresence()) {
      return new PoseTrackingValue();
    }

    var poseLandmarks = FetchNextPoseLandmarks();

    if (!FetchNextPoseDetectionPresence()) {
      return new PoseTrackingValue(poseLandmarks);
    }

    var poseDetection = FetchNextPoseDetection();

    return new PoseTrackingValue(poseLandmarks, poseDetection);
  }

  private NormalizedLandmarkList FetchNextPoseLandmarks() {
    if (!poseLandmarksStreamPoller.Next(poseLandmarksPacket)) {
      Debug.LogWarning($"Failed to fetch next packet from {poseLandmarksStream}");
      return null;
    }

    return poseLandmarksPacket.GetValue();
  }

  private Detection FetchNextPoseDetection() {
    if (!poseDetectionStreamPoller.Next(poseDetectionPacket)) {
      Debug.LogWarning($"Failed to fetch next packet from {poseDetectionStream}");
      return null;
    }

    return poseDetectionPacket.GetValue();
  }

  private bool FetchNextPoseLandmarksPresence() {
    if (!poseLandmarksPresenceStreamPoller.Next(poseLandmarksPresencePacket)) {
      Debug.LogWarning($"Failed to fetch next packet from {poseLandmarksPresenceStream}");
      return false;
    }

    return poseLandmarksPresencePacket.GetValue();
  }

  private bool FetchNextPoseDetectionPresence() {
    if (!poseDetectionPresenceStreamPoller.Next(poseDetectionPresencePacket)) {
      Debug.LogWarning($"Failed to fetch next packet from {poseDetectionPresenceStream}");
      return false;
    }

    return poseDetectionPresencePacket.GetValue();
  }

  private void RenderAnnotation(WebCamScreenController screenController, PoseTrackingValue value) {
    // NOTE: input image is flipped
    annotation.GetComponent<PoseTrackingAnnotationController>().Draw(screenController.transform, value.PoseLandmarkList, value.PoseDetection, true);
  }

  public override bool shouldUseGPU() {
    return useGPU;
  }
}