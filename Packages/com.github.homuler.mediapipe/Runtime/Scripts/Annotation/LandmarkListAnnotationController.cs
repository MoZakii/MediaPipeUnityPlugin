using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Edge = System.Tuple<int ,int>;

namespace Mediapipe {
  public abstract class LandmarkListAnnotationController : AnnotationController {

        private Vector3 InitialScale;
       
        
    [SerializeField] protected GameObject nodePrefab = null;
    [SerializeField] protected GameObject edgePrefab = null;
    [SerializeField] protected float NodeScale = 0.5f;

    private List<GameObject> Nodes;
    private List<GameObject> Edges;

    void Awake() {
      Nodes = new List<GameObject>(NodeSize);
      for (var i = 0; i < NodeSize; i++) {
        Nodes.Add(Instantiate(nodePrefab));
      }

      Edges = new List<GameObject>(EdgeSize);
      for (var i = 0; i < EdgeSize; i++) {
        Edges.Add(Instantiate(edgePrefab));
      }
    }

    void OnDestroy() {
      foreach (var landmark in Nodes) {
        Destroy(landmark);
      }

      foreach (var line in Edges) {
        Destroy(line);
      }
    }

    public override void Clear() {
      foreach (var landmark in Nodes) {
        landmark.GetComponent<NodeAnnotationController>().Clear();
      }

      foreach (var line in Edges) {
        line.GetComponent<EdgeAnnotationController>().Clear();
      }
    }

        /// <summary>
        ///   Renders landmarks on a screen.
        ///   It is assumed that the screen is vertical to terrain and not inverted.
        /// </summary>
        /// <param name="isFlipped">
        ///   if true, x axis is oriented from right to left (top-right point is (0, 0) and bottom-left is (1, 1))
        /// </param>
        /// <remarks>
        ///   In <paramref name="landmarkList" />, y-axis is oriented from top to bottom.
        /// </remarks>
        public void Draw(Transform screenTransform, NormalizedLandmarkList landmarkList, bool isFlipped = false)
        {
            if (isEmpty(landmarkList))
            {
                Clear();
                return;
            }

            for (var i = 0; i < NodeSize; i++) 
            {
                var landmark = landmarkList.Landmark[i];
                var node = Nodes[i];

                node.GetComponent<NodeAnnotationController>().Draw(screenTransform, landmark, isFlipped, NodeScale);
            }

            for (var i = 0; i < EdgeSize; i++)
            {
                var connection = Connections[i];
                var edge = Edges[i];

                var a = Nodes[connection.Item1];
                var b = Nodes[connection.Item2];
                
           
                /*  var c = Nodes[5];
                  var d = Nodes[8];
                  var e = Nodes[0];
                  var e1 = Nodes[1];
                  var e2 = Nodes[2];
                  var e3 = Nodes[3];
                  var e4 = Nodes[4];
                  var e17 = Nodes[17];
                  var e18 = Nodes[18];
                  var e19 = Nodes[19];
                  var e20 = Nodes[20];
                  var e16 = Nodes[16];
                  var e9 = Nodes[9];
                  var e10 = Nodes[10];
                  var e11 = Nodes[11];
                  var e12 = Nodes[12];
                  var e6 = Nodes[6];
                  var e7 = Nodes[7];
                  var e13 = Nodes[13];
                  var e14 = Nodes[14];
                  var e15 = Nodes[15];

                  edge.GetComponent<EdgeAnnotationController>().Draw(screenTransform, a, b);

                  float distance =
                   Vector3.Distance(a.transform.position, b.transform.position); //Change Scale

                  GameObject RightArm = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm");
                  GameObject Right5 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Character1_RightHandIndex1");
                  GameObject Right6 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Character1_RightHandIndex1/Character1_RightHandIndex2");
                  GameObject Right7 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Character1_RightHandIndex1/Character1_RightHandIndex2/Character1_RightHandIndex3");
                  GameObject Right8 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Character1_RightHandIndex1/Character1_RightHandIndex2/Character1_RightHandIndex3/Character1_RightHandIndex4");
                  GameObject Right1 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Character1_RightHandThumb1/");
                  GameObject Right2 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Character1_RightHandThumb1/Character1_RightHandThumb2");
                  GameObject Right3 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Character1_RightHandThumb1/Character1_RightHandThumb2/Character1_RightHandThumb3");
                  GameObject Right4 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Character1_RightHandThumb1/Character1_RightHandThumb2/Character1_RightHandThumb3/Character1_RightHandThumb4");
                  GameObject Right0 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand");
                  GameObject Right17 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Character1_RightHandPinky1");
                  GameObject Right18 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Character1_RightHandPinky1/Character1_RightHandPinky2");
                  GameObject Right19 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Character1_RightHandPinky1/Character1_RightHandPinky2/Character1_RightHandPinky3");
                  GameObject Right20 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Character1_RightHandPinky1/Character1_RightHandPinky2/Character1_RightHandPinky3/Character1_RightHandPinky4");
                  GameObject Right13 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Character1_RightHandRing1");
                  GameObject Right14 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Character1_RightHandRing1/Character1_RightHandRing2");
                  GameObject Right15 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Character1_RightHandRing1/Character1_RightHandRing2/Character1_RightHandRing3");
                  GameObject Right16 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Character1_RightHandRing1/Character1_RightHandRing2/Character1_RightHandRing3/Character1_RightHandRing4");
                  GameObject Right9 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Character1_RightHandMiddle1/");
                  GameObject Right10 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Character1_RightHandMiddle1/Character1_RightHandMiddle2");
                  GameObject Right11 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Character1_RightHandMiddle1/Character1_RightHandMiddle2/Character1_RightHandMiddle3");
                  GameObject Right12 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Character1_RightHandMiddle1/Character1_RightHandMiddle2/Character1_RightHandMiddle3/Character1_RightHandMiddle4");

                  //Pose Landmarks Access below
                  GameObject Pose12 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder");
                  GameObject Pose11 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_LeftShoulder");
                  GameObject Pose24 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_RightUpLeg");
                  GameObject Pose23 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_LeftUpLeg");
                  GameObject Pose14 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm");
                  GameObject Pose13 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_LeftShoulder/Character1_LeftArm/Character1_LeftForeArm");
                  GameObject Pose26 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_RightUpLeg/Character1_RightLeg");
                  GameObject Pose25 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_LeftUpLeg/Character1_LeftLeg");
                  GameObject Pose28 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_RightUpLeg/Character1_RightLeg/Character1_RightFoot");
                  GameObject Pose32 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_RightUpLeg/Character1_RightLeg/Character1_RightFoot/Character1_RightToeBase");
                  GameObject Pose27 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_LeftUpLeg/Character1_LeftLeg/Character1_LeftFoot");
                  GameObject Pose31 = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_LeftUpLeg/Character1_LeftLeg/Character1_LeftFoot/Character1_LeftToeBase");

                  //  GameObject xyz = GameObject.Find("/unitychan").GetComponent<LandmarkMappingtothemodel>().Leftarm;

                  Right5.transform.localScale = new Vector3(1, 1, 1);
                  //         RightArm.transform.localScale = new Vector3(InitialScale.x, distance / 2f, InitialScale.z); //2 for object

                 // Vector3 middlePoint = (a.transform.position + b.transform.position) / 2f; //Change Position 

                  Right5.transform.position = c.transform.position;

                  //Vector3 rotationDirection = (a.transform.position - b.transform.position); //Change Rotation

                  //RightArm.transform.up = rotationDirection; 

                  Right8.transform.localScale = new Vector3(1, 1, 1);
                 // Vector3 middlePoint8 = (a.transform.position + b.transform.position) / 2f; //Change Position
                  Right8.transform.position = d.transform.position;
                  // Vector3 rotationDirection8 = (a.transform.position - b.transform.position);
                  Right0.transform.position = e.transform.position;
                  Right1.transform.position = e1.transform.position;
                  Right2.transform.position = e2.transform.position; 
                  Right3.transform.position = e3.transform.position;
                  Right4.transform.position = e4.transform.position;
                  Right16.transform.position = e16.transform.position;
                  Right9.transform.position = e9.transform.position;
                  Right10.transform.position = e10.transform.position;
                  Right11.transform.position = e11.transform.position;
                  Right12.transform.position = e12.transform.position;
                  Right6.transform.position = e6.transform.position;
                  Right7.transform.position = e7.transform.position;
                  Right13.transform.position = e13.transform.position;
                  Right14.transform.position = e14.transform.position;
                  Right15.transform.position = e15.transform.position;
                  Right17.transform.position = e17.transform.position;
                  Right18.transform.position = e18.transform.position;
                  Right19.transform.position = e19.transform.position;
                  Right20.transform.position = e20.transform.position;
              } */
            }
        }

    protected abstract IList<Edge> Connections { get; }

    protected abstract int NodeSize { get; }

    protected int EdgeSize {
      get { return Connections.Count; }
    }

    private bool isEmpty(NormalizedLandmarkList landmarkList) {
      return landmarkList.Landmark.All(landmark => {
        return landmark.X == 0 && landmark.Y == 0 && landmark.Z == 0;
      });
    }
  }
}
