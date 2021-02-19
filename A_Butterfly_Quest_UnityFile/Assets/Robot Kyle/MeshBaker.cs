using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshBaker : MonoBehaviour
{
    public  SkinnedMeshRenderer skinnedMeshRenderer;
    GameObject ObjectToBake;

    private Mesh bakedMesh;

    public Vector3 rotationOffset = new Vector3(-90, 0, 0); //To combat blender.



    private MeshCollider meshCollider;

    private GameObject colliderContainer;

    private void Reset()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        ObjectToBake = this.gameObject;
    }

    private void Start()
    {

        bakedMesh = new Mesh();

        colliderContainer = new GameObject("Collider Container");

        colliderContainer.transform.SetParent(transform);

        colliderContainer.transform.localRotation = Quaternion.Euler(rotationOffset);

        meshCollider = colliderContainer.AddComponent<MeshCollider>();

        meshCollider.sharedMesh = bakedMesh;

    }


    [ContextMenu("Initiate Bake")]
    private void InitiateBake()
    {

        bakedMesh = new Mesh();

        colliderContainer = new GameObject("Collider Container");

        colliderContainer.transform.SetParent(ObjectToBake.transform);

        colliderContainer.transform.localRotation = Quaternion.Euler(rotationOffset);

        meshCollider = colliderContainer.AddComponent<MeshCollider>();

        meshCollider.sharedMesh = bakedMesh;

        BakeMesh();
    }

    private void BakeMesh()
    {
        skinnedMeshRenderer.BakeMesh(bakedMesh);
        GameObject newObject = new GameObject();
        newObject.transform.position = transform.position;
        newObject.name = skinnedMeshRenderer.gameObject.name;
        MeshFilter newFilter = newObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
        newFilter.mesh = bakedMesh;
        newFilter.mesh.name = ObjectToBake.name + "_export";
        MeshRenderer newRenderer = newObject.AddComponent<MeshRenderer>();
        newRenderer.material = skinnedMeshRenderer.sharedMaterial;
        ObjExporter.MeshToFile(newFilter,newObject.name + "_exported");
    }



}
