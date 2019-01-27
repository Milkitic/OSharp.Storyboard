﻿using System.Collections.Generic;

namespace OSharp.Storyboard.MGLib.Osu.Model.Osb.Commands
{
    public class RotateCommand : NormalizeCommand<Vector2<double>>
    {
        public RotateCommand(EasingType easing, Vector2<int> time,
            IEnumerable<Vector2<double>> param) : base(CommandType.Scale, easing, time, param)
        {
        }
    }
}
