using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshBaker : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;

    public Mesh bakedMesh;

    public Vector3 colliderRotation = new Vector3(-90, 0, 0); //To combat blender.



    private MeshCollider meshCollider;

    private GameObject colliderContainer;



    private void Start()
    {

        bakedMesh = new Mesh();

        colliderContainer = new GameObject("Collider Container");

        colliderContainer.transform.SetParent(transform);

        colliderContainer.transform.localRotation = Quaternion.Euler(colliderRotation);

        meshCollider = colliderContainer.AddComponent<MeshCollider>();

        meshCollider.sharedMesh = bakedMesh;

    }


    [ContextMenu("Initiate Bake")]
    private void InitiateBake()
    {

        bakedMesh = new Mesh();

        colliderContainer = new GameObject("Collider Container");

        colliderContainer.transform.SetParent(transform);

        colliderContainer.transform.localRotation = Quaternion.Euler(colliderRotation);

        meshCollider = colliderContainer.AddComponent<MeshCollider>();

        meshCollider.sharedMesh = bakedMesh;

        BakeMesh();
    }

    private void BakeMesh()
    {
        skinnedMeshRenderer.BakeMesh(bakedMesh);
        GameObject newObject = new GameObject();
        newObject.name = skinnedMeshRenderer.gameObject.name;
        MeshFilter newFilter = newObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
        newFilter.mesh = bakedMesh;
        newObject.AddComponent<MeshRenderer>();
        ObjExporter.MeshToFile(newFilter,"testtttt");
    }


    private void FixedUpdate()
    {

       

    }
}
