using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.XR;
//using Unity.XR.CoreUtils;
//using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : MonoBehaviourPunCallbacks
{
    public string PlayerName;
    private int PlayerNum = 0;

    public Transform head;
    public Transform leftHand;
    public Transform rightHand;
    //private PhotonView photonView;

    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    private Transform headRig;
    private Transform leftHandRig;
    private Transform rightHandRig;

    public Material[] Player_mat = new Material[2];

    // Start is called before the first frame update
    void Start()
    {
        //PlayerNum = PhotonNetwork.CountOfPlayersInRooms;

        //if (PlayerNum >=1)
        //{
        //    print((PlayerNum + 1) + " Player in in this room");

        //    PlayerName = (PlayerNum + 1) + "번 플레이어";
        //    print("PlayerName : " + PlayerName);
        //    if (PlayerName == "2번 플레이어")
        //    {
        //        this.transform.Find("Head").transform.Find("Head_Sphere").GetComponent<MeshRenderer>().material = Player_mat[1];
        //        this.transform.position = new Vector3(0, 0, 1);
        //        this.transform.rotation = Quaternion.Euler(0, 180, 0);
        //    }
        //}

        //PlayerNum = PhotonNetwork.CountOfPlayersInRooms;
        //PlayerName = PhotonNetwork.PlayerList[PlayerNum].ActorNumber+ "번 플레이어";
        ////PlayerNum = PhotonNetwork.PlayerList[0].ActorNumber;
        //print((PlayerNum+1) + " Player is in this room");
        //if (PlayerNum == 1)
        //{

        //    this.transform.Find("Head").transform.Find("Head_Sphere").GetComponent<MeshRenderer>().material = Player_mat[1];
        //    this.transform.position = new Vector3(0, 0, 1);
        //    this.transform.rotation = Quaternion.Euler(0, 180, 0);
        //}

        //photonView = GetComponenet<photonView>();
        BNG.VREmulator rig = FindObjectOfType<BNG.VREmulator>();
        //XR Origin/Camera Offset/
        headRig = rig.transform.Find("PlayerController/CameraRig/TrackingSpace/CenterEyeAnchor");
        leftHandRig = rig.transform.Find("PlayerController/CameraRig/TrackingSpace/LeftHandAnchor/LeftControllerAnchor/LeftController");
        rightHandRig = rig.transform.Find("PlayerController/CameraRig/TrackingSpace/RightHandAnchor/RightControllerAnchor/RightController");

        if (!photonView.IsMine)
        {
            //PlayerName = "Opponent";
            this.transform.position = new Vector3(0, 0, 1);
            this.transform.rotation = Quaternion.Euler(0, 180, 0);

        }
        else
        {
            //this.transform.Find("Head_Sphere").GetComponent<MeshRenderer>().material = Player_mat[1];
            //PlayerName = "Mine";
            foreach(var item in GetComponentsInChildren<Renderer>()){
                //item.enabled = false;
            }
        }
        //Debug.Log("Player Name : "+PlayerName);
        //Debug.Log("rig : "+rig);
        //Debug.Log("headRig : "+headRig);
        //Debug.Log("leftHandRig : "+leftHandRig);
        //Debug.Log("rightHandRig : "+rightHandRig);
        // Debug.Log(andr);

        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (photonView.IsMine)
        {
            //Debug.Log("MetWorkPlayer is mine ");
            //Debug.Log("headRig: "+ headRig.transform.position);
            //Debug.Log("leftHandRig: " + leftHandRig.transform.position);
            //Debug.Log("rightHandRig: " + rightHandRig.transform.position);

            // head.gameObject.SetActive(false);
            // leftHand.gameObject.SetActive(false);
            // rightHand.gameObject.SetActive(false);

            MapPosition(head, headRig);
            MapPosition(leftHand, leftHandRig);
            MapPosition(rightHand, rightHandRig);

            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), leftHandAnimator);
            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), rightHandAnimator);


            //Debug.Log("headRig : "+headRig.transform.position);
            //Debug.Log("leftHandRig : "+leftHandRig.transform.position);
            //Debug.Log("rightHandRig : "+rightHandRig.transform.position);
        }
    } 
    
     void UpdateHandAnimation(InputDevice targetDevice, Animator handAnimator)
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Flex", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Flex", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Pinch", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Pinch", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float PoseValue))
        {
            handAnimator.SetFloat("Pose", PoseValue);
        }
        else
        {
            handAnimator.SetFloat("Pose", 0);
        }
    }


     void MapPosition(Transform target, Transform rigTransform)
    {
        //InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
        //InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);

        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }
}
