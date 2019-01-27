﻿using System;
using System.Globalization;

namespace OSharp.Storyboard.Events
{
    public sealed class Parameter : Event
    {
        public override EventType EventType => EventType.Parameter;

        public override int ParamLength => 1;
        public override bool IsStatic => true;

        public ParameterType Type
        {
            get => (ParameterType)(int)Start[0];
            set
            {
                Start[0] = (int)value;
                End[0] = (int)value;
            }
        }

        protected override string Script => Type.ToShortString();

        public Parameter(EasingType easing, float startTime, float endTime, ParameterType type) :
            base(easing, startTime, endTime, new float[] { (int)type }, new float[] { (int)type })
        {
            Easing = EasingType.Linear;
        }

        public override string ToString()
        {
            return string.Join(",",
                EventType.ToShortString(),
                (int)Easing,
                Math.Round(StartTime).ToString(CultureInfo.InvariantCulture),
                StartTime.Equals(EndTime) ? "" : Math.Round(EndTime).ToString(CultureInfo.InvariantCulture),
                Script);
        }
    }
}
