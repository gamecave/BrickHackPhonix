using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string _id;
    public string Id { get => _id; set => _id = value; }
    public int score = 0;
    public string displayName;
}
