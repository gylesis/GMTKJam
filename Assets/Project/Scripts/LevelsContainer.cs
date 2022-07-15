﻿using UnityEngine;

namespace Project.Scripts
{
    [CreateAssetMenu(menuName = "Level/LevelsContainer", fileName = "LevelsContainer", order = 0)]
    public class LevelsContainer : ScriptableObject
    {
        [SerializeField] private Level[] _levels;

        public Level GetLevel(int id)
        {
            return _levels[id - 1];
        }
    }
}