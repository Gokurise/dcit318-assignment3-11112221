using System;
using System.Collections.Generic;

// Generic Repository
public class Repository<T>
{
    private List<T> items = new List<T>();

    public void Add(T item)
    {
        items.Add(item);
    }

    public IEnumerable<T> GetAll()
    {
        return items;
    }
}

// Patient Class
public class Patient
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Patient(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

// Prescription Class
public class Prescription
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string Medicine { get; set; }

    public Prescription(int id, int patientId, string medicine)
    {
        Id = id;
        PatientId = patientId;
        Medicine = medicine;
    }
}

// Main Healthcare Application
public class HealthSystemApp
{
    private Repository<Patient> patientRepo = new Repository<Patient>();
    private Repository<Prescription> prescriptionRepo = new Repository<Prescription>();
    private Dictionary<int, List<Prescription>> prescriptionMap = new Dictionary<int, List<Prescription>>();

    public void SeedData()
    {
        // Adding Patients
        patientRepo.Add(new Patient(1, "Jack Grelish"));
        patientRepo.Add(new Patient(2, "Sarah Connor"));

        // Adding Prescriptions
        prescriptionRepo.Add(new Prescription(1, 1, "Paracetamol"));
        prescriptionRepo.Add(new Prescription(2, 1, "Sabultanol"));
        prescriptionRepo.Add(new Prescription(3, 2, "Trisilicate"));
    }

    public void BuildPrescriptionMap()
    {
        foreach (var patient in patientRepo.GetAll())
        {
            prescriptionMap[patient.Id] = new List<Prescription>();
        }

        foreach (var prescription in prescriptionRepo.GetAll())
        {
            if (prescriptionMap.ContainsKey(prescription.PatientId))
            {
                prescriptionMap[prescription.PatientId].Add(prescription);
            }
        }
    }

    public void PrintPatientPrescriptions()
    {
        foreach (var patient in patientRepo.GetAll())
        {
            Console.WriteLine($"Patient: {patient.Name}");
            if (prescriptionMap.ContainsKey(patient.Id))
            {
                foreach (var prescription in prescriptionMap[patient.Id])
                {
                    Console.WriteLine($"  - {prescription.Medicine}");
                }
            }
            else
            {
                Console.WriteLine("  No prescriptions found.");
            }
        }
    }

    public void Run()
    {
        SeedData();
        BuildPrescriptionMap();
        PrintPatientPrescriptions();
    }
}

class Program
{
    static void Main()
    {
        var app = new HealthSystemApp();
        app.Run();
    }
}
