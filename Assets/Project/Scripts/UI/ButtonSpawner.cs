using System.Collections;
using System.Collections.Generic;
using Project.Scripts;
using Project.Scripts.UI;
using UnityEngine;
using Zenject;

public class ButtonSpawner : MonoBehaviour
{
    
    [SerializeField] private LevelButton _buttonPrefab;
    [SerializeField] private Transform _buttonRoot;

    [Inject]
    void Init(LevelsContainer levelsContainer, DiContainer container)
    {
        for (int id = 0; id < levelsContainer.Count; id++)
        {
            var button = container.InstantiatePrefab(_buttonPrefab, _buttonRoot).GetComponent<LevelButton>();
            button.Init(id + 1);
        }
    }
}