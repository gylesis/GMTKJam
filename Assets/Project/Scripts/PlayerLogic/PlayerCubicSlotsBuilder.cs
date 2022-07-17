using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Project.Scripts
{
    public class PlayerCubicSlotsBuilder
    {
        private PlayerFacade _facade;
        private StickersVisualizer _visualizer;
        
        private List<PlayerCubicSlot> _slots = new List<PlayerCubicSlot>();
        private List<Sticker> _stickers = new List<Sticker>();
        

        [Inject]
        public PlayerCubicSlotsBuilder(StickersVisualizer visualizer, PlayerFacade facade)
        {
            _visualizer = visualizer;
            _facade = facade;
        }

        public void SetAllStickers(PlayerSlotUI[] uISlots)
        {
            ClearPrevious();

            GameObject slotRoot = new GameObject("slotRoot");
            slotRoot.transform.SetParent(_facade.Transform);
            slotRoot.transform.localPosition = Vector3.zero;

            foreach (var playerSlotUi in uISlots)
            {
                if (playerSlotUi.Sticker == null)
                {
                    Debug.Log("SLOT EMPTY");
                    continue;
                }

                GameObject tempSlot = new GameObject(playerSlotUi.CubeSide.ToString());
                var slot = tempSlot.AddComponent(typeof(PlayerCubicSlot)) as PlayerCubicSlot;
                slot.transform.SetParent(slotRoot.transform);
                slot.transform.localPosition = Vector3.zero;

                slot.SlotData = new PlayerCubicSlotData();


                

                slot.SlotData.MoveSide = playerSlotUi.Movement;
                if (playerSlotUi.Sticker != null)
                    slot.Sticker = playerSlotUi.Sticker;

                OrientationService.SetOrientationFromSide(playerSlotUi.CubeSide, slot.transform);

                var sticker = _visualizer.Create(playerSlotUi.Sticker, playerSlotUi.CubeSide, _facade.Transform);
                slot.SlotData.Forward = sticker.transform.GetChild(0).up;

                _stickers.Add(sticker);
                _slots.Add(slot);
            }

            _facade.SetPlayerSlots(_slots.ToArray());
        }

        public void ClearPrevious()
        {
            foreach (var slot in _slots)
            {
                Object.Destroy(slot.gameObject);
            }
            foreach (var sticker in _stickers)
            {
                Object.Destroy(sticker.gameObject);
            }

            _slots.Clear();
            _stickers.Clear();
        }
    }
}