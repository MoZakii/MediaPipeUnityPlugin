using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
using Edge = System.Tuple<int ,int>;

namespace Mediapipe {
  public class FullBodyPoseLandmarkListAnnotationController : LandmarkListAnnotationController {
    protected static readonly IList<Edge> _Connections = new List<Edge> {
      // Right Arm
      new Edge(11, 13),
      new Edge(13, 15),
      // Left Arm
      new Edge(12, 14),
      new Edge(14, 16),
      // Torso
      new Edge(11, 12),
      new Edge(12, 24),
      new Edge(24, 23),
      new Edge(23, 11),
      // Right Leg
      new Edge(23, 25),
      new Edge(25, 27),
      new Edge(27, 29),
      new Edge(29, 31),
      new Edge(31, 27),
      // Left Leg
      new Edge(24, 26),
      new Edge(26, 28),
      new Edge(28, 30),
      new Edge(30, 32),
      new Edge(32, 28),
    };

    protected override IList<Edge> Connections {
      get { return _Connections; }
    }

    protected override int NodeSize {
      get { return 33; }
        }
      /*  private List<GameObject> Nodes;
        private bool isEmpty(NormalizedLandmarkList landmarkList)
        {
            return landmarkList.Landmark.All(landmark => {
                return landmark.X == 0 && landmark.Y == 0 && landmark.Z == 0;
            });
        }
       
        public new void Draw(Transform screenTransform, NormalizedLandmarkList landmarkList, bool isFlipped = false)
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

              
            }

            for (var i = 0; i < EdgeSize; i++)
            {
                var connection = Connections[i];
                
                //var a = Nodes[connection.Item1];
                //var b = Nodes[connection.Item2];
                var c = Nodes[12];
                var d = Nodes[14];
                

                float distance =
                Vector3.Distance(c.transform.position, d.transform.position); //Change Scale
                GameObject RightArm = GameObject.Find("/unitychan/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm");
                

                RightArm.transform.localScale = new Vector3(1, 1, 1);
                //         RightArm.transform.localScale = new Vector3(InitialScale.x, distance / 2f, InitialScale.z); //2 for object

                Vector3 middlePoint = (c.transform.position + d.transform.position) / 2f; //Change Position

                RightArm.transform.position = middlePoint;

                Vector3 rotationDirection = (c.transform.position - d.transform.position); //Change Rotation

                */
            
    }
}
