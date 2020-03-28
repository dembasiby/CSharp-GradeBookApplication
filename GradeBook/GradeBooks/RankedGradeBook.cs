using System.Collections.Generic;
using System;
using System.Linq;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count() < 5)
                throw new InvalidOperationException();

            var sortedGrades = Students.OrderByDescending(s => s.AverageGrade)
                                       .Select(s => s.AverageGrade);

            var twentyPercent = sortedGrades.Count() / 4;

            if (sortedGrades.Take(twentyPercent).Contains(averageGrade))
                return 'A';
            else if (sortedGrades.Skip(twentyPercent).Take(twentyPercent)
                                 .Contains(averageGrade))
                return 'B';
            else if (sortedGrades.Skip(twentyPercent * 2).Take(twentyPercent)
                                 .Contains(averageGrade))
                return 'C';
            else if (sortedGrades.Skip(twentyPercent * 3).Take(twentyPercent)
                                 .Contains(averageGrade))
                return 'D';

            return 'F';
        }
        public override void CalculateStatistics()
        {
            if (Students.Count() < 5)
            {
                Console.WriteLine($"Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count() < 5)
            {
                Console.WriteLine($"Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStudentStatistics(name);
        }
    }
}