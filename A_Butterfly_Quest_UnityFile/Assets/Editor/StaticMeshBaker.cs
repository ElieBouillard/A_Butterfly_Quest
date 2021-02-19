using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StaticMeshBaker : MonoBehaviour
{
    static SkinnedMeshRenderer skinnedMeshRenderer;
    static GameObject ObjectToBake;

    private static Mesh bakedMesh;

    static Vector3 rotationOffset = new Vector3(-90, 0, 0); //new rotation

    private MeshCollider meshCollider;

    private static GameObject colliderContainer;

    private void Reset()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        ObjectToBake = this.gameObject;
    }

    //private void Start()
    //{

    //    bakedMesh = new Mesh();

    //    colliderContainer = new GameObject("Collider Container");

    //    colliderContainer.transform.SetParent(transform);

    //    colliderContainer.transform.localRotation = Quaternion.Euler(rotationOffset);

    //    meshCollider = colliderContainer.AddComponent<MeshCollider>();

    //    meshCollider.sharedMesh = bakedMesh;

    //}


    [MenuItem("Mesh Baker/Bake skinned Mesh")]
    public static void StaticBakeMesh()
    {
        //GameObject objToBake = Selection.activeGameObject;
        ObjectToBake = Selection.activeGameObject;
        skinnedMeshRenderer = ObjectToBake.GetComponent<SkinnedMeshRenderer>();
        InitiateBake();
    }


    private static void InitiateBake()
    {

        bakedMesh = new Mesh();

        colliderContainer = new GameObject("Collider Container");

        colliderContainer.transform.SetParent(ObjectToBake.transform);

        colliderContainer.transform.localRotation = Quaternion.Euler(rotationOffset);

        //meshCollider = colliderContainer.AddComponent<MeshCollider>();

        //meshCollider.sharedMesh = bakedMesh;

        BakeMesh();
    }

    private static void BakeMesh()
    {
        skinnedMeshRenderer.BakeMesh(bakedMesh);
        GameObject newObject = new GameObject();
        newObject.transform.position = ObjectToBake.transform.position;
        newObject.name = skinnedMeshRenderer.gameObject.name;
        MeshFilter newFilter = newObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
        newFilter.mesh = bakedMesh;
        newFilter.mesh.name = ObjectToBake.name + "_export";
        MeshRenderer newRenderer = newObject.AddComponent<MeshRenderer>();
        newRenderer.material = skinnedMeshRenderer.sharedMaterial;
        ObjExporter.MeshToFile(newFilter,newObject.name + "_exported");

        Selection.activeObject = newObject;
    }





}
