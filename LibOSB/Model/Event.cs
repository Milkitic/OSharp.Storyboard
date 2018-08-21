﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibOsb.Model.Constants;

namespace LibOsb
{
    /// <summary>
    /// Parent class of all actions. Should not be instantiated directly.
    /// </summary>
    public abstract class Event
    {
        public int StartTime { get; internal set; }
        public int EndTime { get; internal set; }
        public EasingType Easing { get; protected set; }
        public string Type { get; protected set; }
        public string ScriptParams { get; protected set; }
        // 扩展属性
        public abstract bool IsStatic { get; }

        internal abstract void BuildParams();

        public override string ToString()
        {
            var endTime = StartTime == EndTime ? "" : EndTime.ToString();
            return string.Join(",", Type, (int)Enum.Parse(typeof(EasingType), Easing.ToString()), StartTime, endTime, ScriptParams);
        }

        internal void _AdjustTime(int time)
        {
            StartTime += time;
            EndTime += time;
        }
    }
}