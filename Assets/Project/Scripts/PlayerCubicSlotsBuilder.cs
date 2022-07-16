using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class PlayerCubicSlotsBuilder
    {
        [Inject]
        private PlayerFacade _facade;
        private StickersVisualizer _visualizer;

        [Inject]
        public PlayerCubicSlotsBuilder(StickersVisualizer visualizer)
        {
            _visualizer = visualizer;
        }

        public void SetAllStickers(PlayerSlotUI[] uISlots)
        {
            List<PlayerCubicSlot> slots = new List<PlayerCubicSlot>();

            GameObject slotRoot = new GameObject("slotRoot");
            slotRoot.transform.SetParent(_facade.Transform);
            slotRoot.transform.localPosition = Vector3.zero;
            foreach (var playerSlotUi in uISlots)
            {
                GameObject tempSlot = new GameObject(playerSlotUi.CubeSide.ToString());
                var slot = tempSlot.AddComponent(typeof(PlayerCubicSlot)) as PlayerCubicSlot;
                slot.transform.SetParent(slotRoot.transform);
                slot.transform.localPosition = Vector3.zero;

                slot.SlotData = new PlayerCubicSlotData();
                slot.SlotData.MoveSide = playerSlotUi.Movement;
                if (playerSlotUi.Sticker != null)
                    slot.Sticker = playerSlotUi.Sticker;

                OrientationService.SetOrientationFromSide(playerSlotUi.CubeSide, slot.transform);
                _visualizer.Create(playerSlotUi.Sticker, playerSlotUi.CubeSide, _facade.Transform);

                slots.Add(slot);
            }

            _facade.SetPlayerSlots(slots.ToArray());
        }
    }
}