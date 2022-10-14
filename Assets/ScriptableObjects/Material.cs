using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Material", menuName = "ScriptableObjects")]

public class Material : ScriptableObject
{
    public GameObject gameObject;
    public Sprite sprite;
    public string name;
    public int quantity;

}
