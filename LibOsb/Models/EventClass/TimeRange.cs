﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibOsb.Models.EventClass
{
    // 差个排序
    public class TimeRange
    {
        public List<(double startTime, double endTime)> TimingList { get; set; } = new List<(double, double)>();

        public double FirstStartTime => TimingList[0].startTime;
        public double FirstEndTime => TimingList[0].endTime;
        public double LastStartTime => TimingList[TimingList.Count - 1].startTime;
        public double LastEndTime => TimingList[TimingList.Count - 1].endTime;
        public int Count => TimingList.Count;

        public void Add(double startTime, double endTime) =>
            TimingList.Add((startTime, endTime));

        public bool InRange(int time)
        {
            foreach (var item in TimingList)
                if (time >= item.startTime && time <= item.endTime)
                    return true;
            return false;
        }

        public bool InRange(out bool isLast, params int[] time)
        {
            for (int i = 0; i < TimingList.Count; i++)
            {
                if (time.All(t => t >= TimingList[i].startTime && t <= TimingList[i].endTime))
                {
                    isLast = i == TimingList.Count - 1;
                    return true;
                }
            }
            isLast = false;
            return false;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var (startTime, endTime) in TimingList)
                sb.AppendLine(startTime + "," + endTime);
            return sb.ToString();
        }
    }
}
