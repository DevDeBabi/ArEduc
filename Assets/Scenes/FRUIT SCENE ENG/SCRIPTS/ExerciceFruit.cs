using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciceFruit : MonoBehaviour
{
    public FruitExerciceManager fruitExerciceManager;

    private void OnMouseDown()
    {
        fruitExerciceManager.Response(gameObject);
    }
}
