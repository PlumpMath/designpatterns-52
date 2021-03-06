﻿using System;
using System.Collections.Generic;

namespace DesignPattern.Structural
{
  public interface IEmployee
  {
    string Name { get; set; }
    string Department { get; set; }
    string Designation { get; set; }

    bool AddReportees(IEmployee employee);
    bool RemoveReportees(IEmployee employee);
    void Display(int level);
  }

  public class Branch : IEmployee
  {
    private readonly IList<IEmployee> _reportees = new List<IEmployee>();

    public Branch(string name, string designation, string department)
    {
      Name = name;
      Designation = designation;
      Department = department;
    }

    public string Name { get; set; }

    public string Department { get; set; }

    public string Designation { get; set; }

    public bool AddReportees(IEmployee employee)
    {
      _reportees.Add(employee);
      return true;
    }

    public bool RemoveReportees(IEmployee employee)
    {
      return _reportees.Remove(employee);
    }

    public void Display(int level)
    {
      var empDisp = new string('-', level) + Name + " (" + Designation + ") [Dept: " + Designation + "]";
      Console.WriteLine(empDisp);

      level += 3;
      foreach (var emp in _reportees)
        emp.Display(level);
    }
  }

  public class Leaf : IEmployee
  {
    public Leaf(string name, string designation, string department)
    {
      Name = name;
      Designation = designation;
      Department = department;
    }

    public string Name { get; set; }

    public string Department { get; set; }

    public string Designation { get; set; }

    public bool AddReportees(IEmployee employee)
    {
      throw new NotImplementedException("Invalid Leaf Operation: AddReportees().");
    }

    public bool RemoveReportees(IEmployee employee)
    {
      throw new NotImplementedException("Invalid Leaf Operation: RemoveReportees().");
    }

    public void Display(int level)
    {
      var empDisp = new string('-', level) + Name + " (" + Designation + ") [Dept: " + Department + "]";
      Console.WriteLine(empDisp);
    }
  }

  public class CompositeProgram
  {
    public static void RunComposite()
    {
      IEmployee director = new Branch("John", "Director", "GSE");
      IEmployee manager = new Branch("Steve", "Manager", "GSE");
      IEmployee srTechSpec1 = new Branch("David", "Sr. Technical Specialist", "GSE");
      IEmployee srTechSpec2 = new Branch("Bill", "Sr. Technical Specialist", "GSE");
      IEmployee srSoftEng = new Leaf("Ted", "Sr. Software Engineer", "GSE");
      IEmployee softEng = new Leaf("Tim", "Software Engineer", "GSE");

      srTechSpec1.AddReportees(srSoftEng);
      srTechSpec2.AddReportees(softEng);

      manager.AddReportees(srTechSpec1);
      manager.AddReportees(srTechSpec2);

      director.AddReportees(manager);
      director.Display(1);
    }
  }
}