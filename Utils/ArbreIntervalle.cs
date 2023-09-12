using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class TimeInterval
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public class IntervalNode
    {
        public TimeInterval Interval { get; set; }
        public DateTime MaxEnd { get; set; }
        public IntervalNode Left { get; set; }
        public IntervalNode Right { get; set; }
    }

    public class IntervalTree
    {
        public IntervalNode Root { get; set; }

        public void Insert(TimeInterval interval)
        {
            Root = Insert(Root, interval);
        }

        private IntervalNode Insert(IntervalNode node, TimeInterval interval)
        {
            if (node == null)
            {
                return new IntervalNode { Interval = interval, MaxEnd = interval.End };
            }

            if (interval.Start < node.Interval.Start)
            {
                node.Left = Insert(node.Left, interval);
            }
            else
            {
                node.Right = Insert(node.Right, interval);
            }

            if (node.MaxEnd < interval.End)
            {
                node.MaxEnd = interval.End;
            }

            return node;
        }

        public List<TimeInterval> FindOverlappingIntervals(TimeInterval searchInterval)
        {
            var overlappingIntervals = new List<TimeInterval>();
            FindOverlappingIntervals(Root, searchInterval, overlappingIntervals);
            return overlappingIntervals;
        }

        private void FindOverlappingIntervals(IntervalNode node, TimeInterval searchInterval, List<TimeInterval> overlappingIntervals)
        {
            if (node == null) return;

            if (DoIntervalsOverlap(searchInterval, node.Interval))
            {
                overlappingIntervals.Add(node.Interval);
            }

            if (node.Left != null && node.Left.MaxEnd >= searchInterval.Start)
            {
                FindOverlappingIntervals(node.Left, searchInterval, overlappingIntervals);
            }

            FindOverlappingIntervals(node.Right, searchInterval, overlappingIntervals);
        }

        private bool DoIntervalsOverlap(TimeInterval interval1, TimeInterval interval2)
        {
            return interval1.Start < interval2.End && interval1.End > interval2.Start;
        }
    }

    public class CommonAvailabilityFinder
    {
        public static List<TimeInterval> FindCommonAvailability(List<List<TimeInterval>> availabilities)
        {
            var intervalTree = new IntervalTree();
            var commonAvailability = new List<TimeInterval>();

            if (availabilities.Count == 0) return commonAvailability;

            // Insert les disponibilités de la première personne de la liste dans l'arbre
            foreach (var interval in availabilities[0])
            {
                intervalTree.Insert(interval);
            }

            // Trouve les disponibilités qui se superposent pour chaque utilisateur ensuite (List<TimeInterval> == un utilisateur)
            for (int i = 1; i < availabilities.Count; i++)
            {
                var newTree = new IntervalTree();
                foreach (var interval in availabilities[i])
                {
                    var overlappingIntervals = intervalTree.FindOverlappingIntervals(interval);
                    foreach (var overlap in overlappingIntervals)
                    {
                        var commonInterval = new TimeInterval
                        {
                            Start = Max(overlap.Start, interval.Start),
                            End = Min(overlap.End, interval.End)
                        };
                        newTree.Insert(commonInterval);
                    }
                }
                intervalTree = newTree;
            }
            TraverseInOrder(intervalTree.Root, commonAvailability);

            return commonAvailability;
        }

        private static void TraverseInOrder(IntervalNode node, List<TimeInterval> commonAvailability)
        {
            if (node == null) return;

            TraverseInOrder(node.Left, commonAvailability);
            commonAvailability.Add(node.Interval);
            TraverseInOrder(node.Right, commonAvailability);
        }

        private static DateTime Max(DateTime a, DateTime b)
        {
            return a > b ? a : b;
        }

        private static DateTime Min(DateTime a, DateTime b)
        {
            return a < b ? a : b;
        }
    }

}
