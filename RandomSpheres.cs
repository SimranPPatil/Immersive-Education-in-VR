using UnityEngine;
using System.Collections;

public class RandomSpheres : MonoBehaviour {

    public GameObject KDsphere;
    public GameObject KDsphereRed;
    public GameObject boundingCuboid;

    [Range(0, 100)]
    public int how_many;

    // Use this for initialization
    void Start () {
        boundingCuboid.GetComponent<KDTree>().enabled = false;
        InitializeRandom();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void InitializeRandom()
    {
        float xstart, xend, ystart, yend, zstart, zend;
        Vector3 scale = boundingCuboid.transform.localScale / 2f;
        xstart = boundingCuboid.transform.position.x - scale.x;
        xend = boundingCuboid.transform.position.x + scale.x;
        ystart = boundingCuboid.transform.position.y - scale.y;
        yend = boundingCuboid.transform.position.y + scale.y;
        zstart = boundingCuboid.transform.position.z - scale.z;
        zend = boundingCuboid.transform.position.z + scale.z;

        for (int i = 0; i < how_many; i++)
        {
            GameObject inst = Instantiate(KDsphere) as GameObject;
            inst.transform.position = new Vector3(Random.Range(xstart, xend),
                                                  Random.Range(ystart, yend),
                                                  Random.Range(zstart, zend));
            KDTree.AllKDSpheres.Add(inst);
        }

        KDTree.redNode = Instantiate(KDsphereRed) as GameObject;
        KDTree.redNode.transform.position =
            new Vector3(Random.Range(xstart, xend),
                        Random.Range(ystart, yend),
                        Random.Range(zstart, zend));

        boundingCuboid.GetComponent<KDTree>().enabled = true;
        boundingCuboid.GetComponent<KDTree>().boundingBox = boundingCuboid;
        //KDTree tree = new KDTree();
        //KDTree tree = gameObject.AddComponent<KDTree>();
        //KDNode root = tree.BuildTree(KDTree.AllKDSpheres, 0);
    }
}
