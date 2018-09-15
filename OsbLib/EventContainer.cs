﻿using Milkitic.OsbLib.Enums;
using Milkitic.OsbLib.Models;
using Milkitic.OsbLib.Models.EventType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Milkitic.OsbLib
{
    public abstract class EventContainer
    {
        public abstract List<Event> EventList { get; set; }

        public abstract float MaxTime { get; }
        public abstract float MinTime { get; }
        public abstract float MaxStartTime { get; }
        public abstract float MinEndTime { get; }
        public virtual void Adjust(float offsetX, float offsetY, int offsetTiming)
        {
            var events = EventList.GroupBy(k => k.EventType);
            foreach (var kv in events)
            {
                foreach (var e in kv)
                {
                    switch (e.EventType)
                    {
                        case EventEnum.Move:
                            ((Move)e).Adjust(offsetX, offsetY);
                            break;
                        case EventEnum.MoveX:
                            ((MoveX)e).Adjust(offsetX);
                            break;
                        case EventEnum.MoveY:
                            ((MoveY)e).Adjust(offsetY);
                            break;
                    }

                    e.AdjustTime(offsetTiming);
                }
            }
        }

        public virtual void AddEvent(EventEnum e, EasingType easing, float startTime, float endTime, float[] start, float[] end,
            bool sequential = true)
        {
            Event newE;
            switch (e)
            {
                case EventEnum.Fade:
                    newE = new Fade(easing, startTime, endTime, start[0], end[0]);
                    break;
                case EventEnum.Move:
                    newE = new Move(easing, startTime, endTime, start[0], start[1], end[0], end[1]);
                    break;
                case EventEnum.MoveX:
                    newE = new MoveX(easing, startTime, endTime, start[0], end[0]);
                    break;
                case EventEnum.MoveY:
                    newE = new MoveY(easing, startTime, endTime, start[0], end[0]);
                    break;
                case EventEnum.Scale:
                    newE = new Scale(easing, startTime, endTime, start[0], end[0]);
                    break;
                case EventEnum.Vector:
                    newE = new Vector(easing, startTime, endTime, start[0], start[1], end[0], end[1]);
                    break;
                case EventEnum.Rotate:
                    newE = new Rotate(easing, startTime, endTime, start[0], end[0]);
                    break;
                case EventEnum.Color:
                    newE = new Color(easing, startTime, endTime, start[0], start[1], start[2], end[0], end[1], start[2]);
                    break;
                case EventEnum.Parameter:
                    newE = new Parameter(easing, startTime, endTime, (ParameterEnum)(int)start[0]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(e), e, null);
            }

            if (sequential && EventList.Count > 0)
            {
                int compared = -1;
                if (startTime >= MaxStartTime)
                    EventList.Add(newE);
                else
                {
                    compared = EventList.Count;
                    for (var i = EventList.Count - 1; i >= 0; i--)
                    {
                        Event v = EventList[i];
                        if (startTime < v.StartTime)
                            compared = i;
                        else
                            break;
                    }
                }

                if (compared != -1)
                    EventList.Insert(compared, newE);
            }
            else
                EventList.Add(newE);
        }
    }
}
