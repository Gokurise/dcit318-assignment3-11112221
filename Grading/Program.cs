using System;
using System.Collections.Generic;
using System.IO;

// Represents a student
public class StudentRecord
{
    public string StudentName { get; }
    public int StudentScore { get; }

    public StudentRecord(string studentName, int studentScore)
    {
        StudentName = studentName;
        StudentScore = studentScore;
    }

    public string GetGrade()
    {
        if (StudentScore >= 90) return "A";
        if (StudentScore >= 80) return "B";
        if (StudentScore >= 70) return "C";
        if (StudentScore >= 60) return "D";
        return "F";
    }
}

// Handles grading logic
public class GradingProcessor
{
    public List<StudentRecord> LoadStudentsFromFile(string filePath)
    {
        var enrolledStudents = new List<StudentRecord>();

        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split(',');
            enrolledStudents.Add(new StudentRecord(parts[0].Trim(), int.Parse(parts[1])));
        }

        return enrolledStudents;
    }

    public void SaveReportToFile(List<StudentRecord> students, string filePath)
    {
        using (var writer = new StreamWriter(filePath))
        {
            foreach (var student in students)
            {
                writer.WriteLine($"{student.StudentName} - Score: {student.StudentScore}, Grade: {student.GetGrade()}");
            }
        }
    }
}

class Program
{
    static void Main()
    {
        var gradingProcessor = new GradingProcessor();
        string inputFile = "students.txt";
        string outputFile = "report.txt";

        if (!File.Exists(inputFile))
        {
            File.WriteAllLines(inputFile, new[]
            {
                "Paul,85",
                "John,72",
                "Peter,91"
            });
        }

        var enrolledStudents = gradingProcessor.LoadStudentsFromFile(inputFile);
        gradingProcessor.SaveReportToFile(enrolledStudents, outputFile);

        Console.WriteLine("Grading report generated:");
        Console.WriteLine(File.ReadAllText(outputFile));
    }
}

