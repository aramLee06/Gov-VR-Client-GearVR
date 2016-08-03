//////////////////////////////////////////////////////////////////////////////
// bl_PlayerAnimations.cs
//
// - was ordered to encourage TPS player animations using legacy animations,
//  and heat look controller from Unity technologies.
//
//                           Lovatto Studio
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using System.Collections;

public class bl_PlayerAnimator : MonoBehaviour
{

    public PlayerState m_PlayerState = PlayerState.Stand;
    [Separator("Animator")]
    public Animator Anim;
    public float SpeedDampTime = 0.27f;
    [Space(5)]
    [Separator("Heat Look")]
    public Transform rootNode;
    public Transform mTarget = null;
    public BendingSegment[] segments;
    public NonAffectedJoints[] nonAffectedJoints;
    public Vector3 headLookVector = Vector3.forward;
    public Vector3 headUpVector = Vector3.up;
    public Vector3 target = Vector3.zero;
    public float effect = 1;
    public bool overrideAnimation = false;
    [Space(5)]
    public bl_PlayerMovement PlayerController;

    [HideInInspector] public bool m_Update = true;
    [HideInInspector] public bool grounded = true;
    [HideInInspector] public Vector3 velocity = Vector3.zero;
    [HideInInspector] public Vector3 DirectionalSpeed = Vector3.zero;
    [HideInInspector] public float m_PlayerSpeed;
    [HideInInspector] public float lastYRotation;

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        SetupSegments();
    }
    /// <summary>
    /// 
    /// </summary>
    void SetupSegments()
    {
        if (rootNode == null)
        {
            rootNode = transform;
        }

        // Setup segments
        foreach (BendingSegment segment in segments)
        {
            Quaternion parentRot = segment.firstTransform.parent.rotation;
            Quaternion parentRotInv = Quaternion.Inverse(parentRot);
            segment.referenceLookDir =
                parentRotInv * rootNode.rotation * headLookVector.normalized;
            segment.referenceUpDir =
                parentRotInv * rootNode.rotation * headUpVector.normalized;
            segment.angleH = 0;
            segment.angleV = 0;
            segment.dirUp = segment.referenceUpDir;

            segment.chainLength = 1;
            Transform t = segment.lastTransform;
            while (t != segment.firstTransform && t != t.root)
            {
                segment.chainLength++;
                t = t.parent;
            }

            segment.origRotations = new Quaternion[segment.chainLength];
            t = segment.lastTransform;
            for (int i = segment.chainLength - 1; i >= 0; i--)
            {
                segment.origRotations[i] = t.localRotation;
                t = t.parent;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (!m_Update)
            return;

        ControllerInfo();
        Animate();       
    }
    /// <summary>
    /// Get current player information for send to animator
    /// </summary>
    void ControllerInfo()
    {
        DirectionalSpeed = rootNode.InverseTransformDirection(velocity);
        DirectionalSpeed.y = 0;
        m_PlayerSpeed = velocity.magnitude;
    }

    /// <summary>
    /// 
    /// </summary>
    void Animate()
    {
        Anim.SetFloat("Speed",m_PlayerSpeed,SpeedDampTime,Time.deltaTime);
        Anim.SetFloat("Z", DirectionalSpeed.z);
        float x = DirectionalSpeed.x / PlayerController.walkSpeed;
        Anim.SetFloat("X", x);

        if (grounded)
        {
            Anim.SetBool("Jump", false);
        }
        else
        {
             Anim.SetBool("Jump", true);
        }
        if (grounded && m_PlayerState == PlayerState.Crouch)
        {
            Anim.SetBool("Crouch", true);
        }
        else
        {
            Anim.SetBool("Crouch", false);
        }
      
    }
    /// <summary>
    /// 
    /// </summary>
void LateUpdate()
{
    if (!m_Update)
        return;
    if (Time.timeScale == 0)
        return;


    if (mTarget != null)
    {
        target = mTarget.position;
    }
    // Remember initial directions of joints that should not be affected
   Vector3[] jointDirections = new Vector3[nonAffectedJoints.Length];
    for (int i = 0; i < nonAffectedJoints.Length; i++)
    {
        foreach (Transform child in nonAffectedJoints[i].joint)
        {
            jointDirections[i] = child.position - nonAffectedJoints[i].joint.position;
            break;
        }
    }

    // Handle each segment
    foreach (BendingSegment segment in segments)
    {
        Transform t = segment.lastTransform;
        if (overrideAnimation)
        {
            for (int i = segment.chainLength - 1; i >= 0; i--)
            {
                t.localRotation = segment.origRotations[i];
                t = t.parent;
            }
        }

        Quaternion parentRot = segment.firstTransform.parent.rotation;
        Quaternion parentRotInv = Quaternion.Inverse(parentRot);

        // Desired look direction in world space
        Vector3 lookDirWorld = (target - segment.lastTransform.position).normalized;

        // Desired look directions in neck parent space
        Vector3 lookDirGoal = (parentRotInv * lookDirWorld);

        // Get the horizontal and vertical rotation angle to look at the target
        float hAngle = bl_UtilityHelper.AngleAroundAxis(
            segment.referenceLookDir, lookDirGoal, segment.referenceUpDir
        );

        Vector3 rightOfTarget = Vector3.Cross(segment.referenceUpDir, lookDirGoal);

        Vector3 lookDirGoalinHPlane =
            lookDirGoal - Vector3.Project(lookDirGoal, segment.referenceUpDir);

        float vAngle = bl_UtilityHelper.AngleAroundAxis(
            lookDirGoalinHPlane, lookDirGoal, rightOfTarget
        );

        // Handle threshold angle difference, bending multiplier,
        // and max angle difference here
        float hAngleThr = Mathf.Max(
            0, Mathf.Abs(hAngle) - segment.thresholdAngleDifference
        ) * Mathf.Sign(hAngle);

        float vAngleThr = Mathf.Max(
            0, Mathf.Abs(vAngle) - segment.thresholdAngleDifference
        ) * Mathf.Sign(vAngle);

        hAngle = Mathf.Max(
            Mathf.Abs(hAngleThr) * Mathf.Abs(segment.bendingMultiplier),
            Mathf.Abs(hAngle) - segment.maxAngleDifference
        ) * Mathf.Sign(hAngle) * Mathf.Sign(segment.bendingMultiplier);

        vAngle = Mathf.Max(
            Mathf.Abs(vAngleThr) * Mathf.Abs(segment.bendingMultiplier),
            Mathf.Abs(vAngle) - segment.maxAngleDifference
        ) * Mathf.Sign(vAngle) * Mathf.Sign(segment.bendingMultiplier);

        // Handle max bending angle here
        if (!segment.OnlyVertical)
        {
            hAngle = Mathf.Clamp(hAngle, -segment.maxBendingAngle, segment.maxBendingAngle);
        }
        else
        {
            hAngle = Mathf.Clamp(hAngle, -1, 1);
        }
        vAngle = Mathf.Clamp(vAngle, -segment.maxBendingAngle, segment.maxBendingAngle);

        Vector3 referenceRightDir =
            Vector3.Cross(segment.referenceUpDir, segment.referenceLookDir);

        // Lerp angles
        if (!segment.OnlyVertical)
        {
            segment.angleH = Mathf.Lerp(
                segment.angleH, hAngle, Time.deltaTime * segment.responsiveness
            );
        }
        else
        { //Use this when find the transform only move in vertical.
            // segment.angleH = 0.0f;
            segment.angleH = Mathf.Lerp(
                segment.angleH, (hAngle / 2.5f), Time.deltaTime * segment.responsiveness
            );
        }
        segment.angleV = Mathf.Lerp(
            segment.angleV, vAngle, Time.deltaTime * segment.responsiveness
        );

        // Get direction
        lookDirGoal = Quaternion.AngleAxis(segment.angleH, segment.referenceUpDir)
            * Quaternion.AngleAxis(segment.angleV, referenceRightDir)
            * segment.referenceLookDir;

        // Make look and up perpendicular
        Vector3 upDirGoal = segment.referenceUpDir;
        Vector3.OrthoNormalize(ref lookDirGoal, ref upDirGoal);

        // Interpolated look and up directions in neck parent space
        Vector3 lookDir = lookDirGoal;
        segment.dirUp = Vector3.Slerp(segment.dirUp, upDirGoal, Time.deltaTime * 5);
        Vector3.OrthoNormalize(ref lookDir, ref segment.dirUp);

        // Look rotation in world space
        Quaternion lookRot = (
            (parentRot * Quaternion.LookRotation(lookDir, segment.dirUp))
            * Quaternion.Inverse(
                parentRot * Quaternion.LookRotation(
                    segment.referenceLookDir, segment.referenceUpDir
                )
            )
        );

        // Distribute rotation over all joints in segment
        Quaternion dividedRotation =
            Quaternion.Slerp(Quaternion.identity, lookRot, effect / segment.chainLength);
        t = segment.lastTransform;
        for (int i = 0; i < segment.chainLength; i++)
        {
            t.rotation = dividedRotation * t.rotation;
            t = t.parent;
        }
    }

    // Handle non affected joints
    for (int i = 0; i < nonAffectedJoints.Length; i++)
    {
        Vector3 newJointDirection = Vector3.zero;

        foreach (Transform child in nonAffectedJoints[i].joint)
        {
            newJointDirection = child.position - nonAffectedJoints[i].joint.position;
            break;
        }

        Vector3 combinedJointDirection = Vector3.Slerp(
            jointDirections[i], newJointDirection, nonAffectedJoints[i].effect
        );

        nonAffectedJoints[i].joint.rotation = Quaternion.FromToRotation(
            newJointDirection, combinedJointDirection
        ) * nonAffectedJoints[i].joint.rotation;
    }
    
    //for rotation
    lastYRotation = transform.rotation.eulerAngles.y;
}

    [System.Serializable]
    public class BendingSegment
    {
        public Transform firstTransform;
        public Transform lastTransform;
        public float thresholdAngleDifference = 0;
        public float bendingMultiplier = 0.6f;
        public float maxAngleDifference = 30;
        public float maxBendingAngle = 80;
        public float responsiveness = 5;
        public bool OnlyVertical = false;
        internal float angleH;
        internal float angleV;
        internal Vector3 dirUp;
        internal Vector3 referenceLookDir;
        internal Vector3 referenceUpDir;
        internal int chainLength;
        internal Quaternion[] origRotations;
    }
    [System.Serializable]
    public class NonAffectedJoints
    {
        public Transform joint;
        public float effect = 0;
    }
}