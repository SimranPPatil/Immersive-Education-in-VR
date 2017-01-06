using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class KDNode
{
    public KDNode leftNode;
    public KDNode rightNode;
    public GameObject sphereObj;
    public int splitDim; // x = 0; y = 1; z = 2; 

    public GameObject planeObj;

    //constructor
    public KDNode(GameObject sphere, int axis, float emission, GameObject plane, Vector3 mins, Vector3 maxs)
    {
        leftNode = null;
        rightNode = null;
        sphereObj = sphere;
        splitDim = axis;

        Color emColor = sphereObj.GetComponent<Renderer>().material.GetColor("_EmissionColor");
        emColor.b = emission;
        sphereObj.GetComponent<Renderer>().material.SetColor("_EmissionColor", emColor);

        createPlane(plane, mins, maxs);
    }

    public KDNode(GameObject sphere)
    {
        sphereObj = sphere;
    }

    public void setMaterial(Material mat)
    {
        planeObj.GetComponent<Renderer>().material = mat;
    }

    public void setEmission(float emission)
    {
        Color emColor = sphereObj.GetComponent<Renderer>().material.GetColor("_EmissionColor");
        emColor.b = emission;
        sphereObj.GetComponent<Renderer>().material.SetColor("_EmissionColor", emColor);
    }

    public void createPlane(GameObject plane, Vector3 mins, Vector3 maxs)
    {
        planeObj = plane;
        float xpos, ypos, zpos;
        float xscale, yscale, zscale;

        if (splitDim == 0)
        {
            xpos = sphereObj.transform.position.x;
            ypos = (mins.y + maxs.y) / 2f;
            zpos = (mins.z + maxs.z) / 2f;

            xscale = 0.01f;
            yscale = maxs.y - mins.y;
            zscale = maxs.z - mins.z;
        }
        else if (splitDim == 1)
        {
            xpos = (mins.x + maxs.x) / 2f;
            ypos = sphereObj.transform.position.y;
            zpos = (mins.z + maxs.z) / 2f;

            xscale = maxs.x - mins.x;
            yscale = 0.01f;
            zscale = maxs.z - mins.z;
        }
        else /* if (splitDim == 2) */
        {
            xpos = (mins.x + maxs.x) / 2f;
            ypos = (mins.y + maxs.y) / 2f;
            zpos = sphereObj.transform.position.z;

            xscale = maxs.x - mins.x;
            yscale = maxs.y - mins.y;
            zscale = 0.01f;
        }

        planeObj.transform.position = new Vector3(xpos, ypos, zpos);
        planeObj.transform.localScale = new Vector3(xscale, yscale, zscale);
        planeObj.GetComponent<MeshRenderer>().enabled = false;
    }
}

public class KDPlane : IComparable<KDPlane>
{
    public KDNode node;
    public int dim;

    public KDPlane(KDNode n, int d)
    {
        node = n;
        dim = d;
    }

    public int CompareTo(KDPlane other)
    {
        if (dim < other.dim)
            return -1;
        else if (dim > other.dim)
            return 1;
        else
            return 0;
    }
}

public class KDTree : MonoBehaviour
{
    public static List<GameObject> AllKDSpheres = new List<GameObject>();
    static List<KDPlane> createdPlanes = new List<KDPlane>();

    public Material nnMat;

    public GameObject planeObj;
    public GameObject boundingBox;
    public Material planeXmat;
    public Material planeYmat;
    public Material planeZmat;

    public KDNode root;

    public AudioSource introAudio;
    public AudioSource descAudio;
    public AudioSource explainAudio;

    // public Canvas textCanvas;
    // public GameObject nodeTextPrefab;

    public static GameObject redNode;
    public KDNode query;
    KDNode nn;

    float glowTime;

    private bool hasTree = false;

    int displayIdx = -1;

    void Start()
    {
        nn = null;
        glowTime = 0;

        descAudio.Stop();
        explainAudio.Stop();
        introAudio.Play();
    }

    void Update()
    {
        if (displayIdx == createdPlanes.Count && Input.GetButtonDown("A"))
        {
            nn = FindNearestNeighbor();
            nn.sphereObj.GetComponent<Renderer>().material = nnMat;

            introAudio.Stop();
            descAudio.Stop();
            explainAudio.Play();
            
            displayIdx++;
        }

        if (displayIdx >= 0 && Input.GetButtonDown("A") && displayIdx < createdPlanes.Count)
        {
            createdPlanes[displayIdx].node.planeObj.GetComponent<MeshRenderer>().enabled = true;

            //GameObject nodeText = Instantiate(nodeTextPrefab) as GameObject;
            //nodeText.transform.SetParent(textCanvas.transform, false);

            //nodeText.GetComponentInChildren<Text>().text = "Hello";

            if (displayIdx == 0)
            {
                introAudio.Stop();
                explainAudio.Stop();
                descAudio.Play();
            }

            displayIdx++;
        }

        if (!hasTree && Input.GetButtonDown("A"))
        {
            hasTree = true;
            Vector3 scale = boundingBox.transform.localScale / 2f;
            Vector3 mins = new Vector3(boundingBox.transform.position.x - scale.x,
                                       boundingBox.transform.position.y - scale.y,
                                       boundingBox.transform.position.z - scale.z);
            Vector3 maxs = new Vector3(boundingBox.transform.position.x + scale.x,
                                       boundingBox.transform.position.y + scale.y,
                                       boundingBox.transform.position.z + scale.z);
            root = BuildTree(AllKDSpheres, 0, 3f, mins, maxs);
            query = new KDNode(redNode);

            createdPlanes.Sort();
            displayIdx++;
        }
    }

    //find the nearest neighbor
    public KDNode FindNearestNeighbor()
    {
        return nnHelp(ref root, query, 0);
    }

    private KDNode nnHelp(ref KDNode curNode, KDNode target, int dim)
    {
        int axis = dim % 3;
        // Debug.Log("Axis: " + axis + "| Pos: " + curNode.sphereObj.transform.position);

        if (curNode.leftNode == null && curNode.rightNode == null)
            return curNode;

        KDNode potential, next;

        if (smallerDimVal(target, curNode, axis))
            potential = nnHelp(ref curNode.leftNode, target, dim + 1);
        else
            potential = nnHelp(ref curNode.rightNode, target, dim + 1);

        if (distNodes(curNode, target) < distNodes(potential, target))
        {
            potential = curNode;

            if (smallerDimVal(target, curNode, axis))
                next = nnHelp(ref curNode.rightNode, target, dim + 1);
            else
                next = nnHelp(ref curNode.leftNode, target, dim + 1);

            if (distNodes(next, target) < distNodes(potential, target))
                potential = next;
        }

        return potential;
    }

    private bool smallerDimVal(KDNode first, KDNode second, int dim)
    {
        if (dim == 0)
            return first.sphereObj.transform.position.x < second.sphereObj.transform.position.x;
        else if (dim == 1)
            return first.sphereObj.transform.position.y < second.sphereObj.transform.position.y;
        else // dim == 2
            return first.sphereObj.transform.position.z < second.sphereObj.transform.position.z;
    }

    private float distNodes(KDNode first, KDNode second)
    {
        return Vector3.Distance(first.sphereObj.transform.position, second.sphereObj.transform.position);
    }

    public void mergeSort(ref List<GameObject> KDSpheres, int dim, int left, int right)
    {
        if (left < right)
        {
            //find the mid point
            int mid = (left + (right - 1)) / 2;
            //merge the left array
            mergeSort(ref KDSpheres, dim, left, mid);
            //merge the right array
            mergeSort(ref KDSpheres, dim, mid + 1, right);

            int i = 0, j = 0, k = 0;
            int lenLeft = mid - left + 1;
            int lenRight = right - mid;

            List<GameObject> LeftHalf = new List<GameObject>();
            List<GameObject> RightHalf = new List<GameObject>();
            //create the left array
            for (i = 0; i < lenLeft; i++)
            {
                LeftHalf.Add(KDSpheres[left + i]);
            }
            //create the right array
            for (j = 0; j < lenRight; j++)
            {
                RightHalf.Add(KDSpheres[mid + 1 + j]);
            }

            i = 0;
            j = 0;
            k = left;
            while (i < lenLeft && j < lenRight)
            {
                float leftValue;
                float rightValue;

                if (dim == 0) //x-axis
                {
                    leftValue = LeftHalf[i].transform.position.x;
                    rightValue = RightHalf[j].transform.position.x;
                }
                else if (dim == 1) //y-axis
                {
                    leftValue = LeftHalf[i].transform.position.y;
                    rightValue = RightHalf[j].transform.position.y;
                }
                else // if (dim == 2) //z-axis
                {
                    leftValue = LeftHalf[i].transform.position.z;
                    rightValue = RightHalf[j].transform.position.z;
                }

                if (leftValue <= rightValue)
                {
                    KDSpheres[k] = LeftHalf[i];
                    i++;
                }
                else
                {
                    KDSpheres[k] = RightHalf[j];
                    j++;
                }
                k++;
            }

            while (i < lenLeft)
            {
                KDSpheres[k] = LeftHalf[i];
                i++;
                k++;
            }

            while (j < lenRight)
            {
                KDSpheres[k] = RightHalf[j];
                j++;
                k++;
            }
        }
    }

    public int findMedian(ref List<GameObject> KDSpheres, int dim)
    {
        //sort the array and then return the median object
        mergeSort(ref KDSpheres, dim, 0, KDSpheres.Count - 1);
        return (KDSpheres.Count) / 2;
    }

    /*
    Build the KD tree using a list of spheres
    and return the root of the tree.
    */
    public KDNode BuildTree(List<GameObject> KDSpheres, int dim, float emission, Vector3 mins, Vector3 maxs)
    {
        //find the axis of the current node
        int axis = dim % 3;

        //if all spheres are in the tree then return
        if (KDSpheres.Count == 0)
        {
            return null;
        }

        //find the median based on the axis
        int sphereIndex = findMedian(ref KDSpheres, axis);
        GameObject plane = Instantiate(planeObj) as GameObject;
        KDNode node = new KDNode(KDSpheres[sphereIndex], axis, emission, plane, mins, maxs);
        createdPlanes.Add(new KDPlane(node, dim));

        List<GameObject> LeftHalf = new List<GameObject>();
        List<GameObject> RightHalf = new List<GameObject>();
        //create the left array
        int i;
        for (i = 0; i < sphereIndex; i++)
        {
            LeftHalf.Add(KDSpheres[i]);
        }
        //create the right array
        for (i = sphereIndex + 1; i < KDSpheres.Count; i++)
        {
            RightHalf.Add(KDSpheres[i]);
        }

        Vector3 rightmins = mins;
        Vector3 leftmaxs = maxs;

        if (node.splitDim == 0)
        {
            leftmaxs.x = node.sphereObj.transform.position.x;
            rightmins.x = node.sphereObj.transform.position.x;
            node.setMaterial(planeXmat);
        }
        else if (node.splitDim == 1)
        {
            leftmaxs.y = node.sphereObj.transform.position.y;
            rightmins.y = node.sphereObj.transform.position.y;
            node.setMaterial(planeYmat);
        }
        else
        {
            leftmaxs.z = node.sphereObj.transform.position.z;
            rightmins.z = node.sphereObj.transform.position.z;
            node.setMaterial(planeZmat);
        }

        node.leftNode = BuildTree(LeftHalf, dim + 1, emission/* - 0.4f*/, mins, leftmaxs);
        node.rightNode = BuildTree(RightHalf, dim + 1, emission/* - 0.4f*/, rightmins, maxs);

        //Debug.Log("Axis: " + axis);
        //Debug.Log("L: " + node.leftNode);
        //Debug.Log("R: " + node.rightNode);

        return node;
    }
}
