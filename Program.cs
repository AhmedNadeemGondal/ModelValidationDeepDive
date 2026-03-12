using ModelValidationDeepDive;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// This is top level endpoint routing
// It abstracts away app.UseRouting() and app.UseEndpoints()
// It's less boilerplate but one needs to be aware of this.
app.MapPost("/employees", (Employee employee) => // the employee class has annotations for data validation rules.
{
    EmployeesRepository.AddEmployee(employee);
    return "Employee is added successfully.";
})
    .WithParameterValidation(); // Needs minimalApi.Extensions NuGet Package

app.Run();
public static class EmployeesRepository
{
    private static List<Employee> employees = new List<Employee>
{
    new Employee(1, "John Doe", "Engineer", 60000),
    new Employee(2, "Jane Smith", "Manager", 75000),
    new Employee(3, "Sam Brown", "Technician", 50000)
};

    public static List<Employee> GetEmployees() => employees;

    public static Employee? GetEmployeeById(int id)
    {
        return employees.FirstOrDefault(x => x.Id == id);
    }

    public static void AddEmployee(Employee? employee)
    {
        if (employee is not null)
        {
            employees.Add(employee);
        }
    }

    public static bool UpdateEmployee(Employee? employee)
    {
        if (employee is not null)
        {
            var emp = employees.FirstOrDefault(x => x.Id == employee.Id);
            if (emp is not null)
            {
                emp.Name = employee.Name;
                emp.Position = employee.Position;
                emp.Salary = employee.Salary;

                return true;
            }
        }

        return false;
    }

    public static bool DeleteEmployee(int id)
    {
        var employee = employees.FirstOrDefault(x => x.Id == id);
        if (employee is not null)
        {
            employees.Remove(employee);
            return true;
        }

        return false;
    }
}

public class Employee
{
    public int Id { get; set; }

    [Required] // This annotation will check the request body
               // and provide a valid reponse telling the user why validation failed.
    public string Name { get; set; }
    public string Position { get; set; }

    [Required]
    [Range(50000, 200000)] // This annotation will check the request body
                           // and provide a valid reponse telling the user why validation failed.
    [Employee_EnsureSalary]
    public double Salary { get; set; }

    public Employee(int id, string name, string position, double salary)
    {
        Id = id;
        Name = name;
        Position = position;
        Salary = salary;
    }
}
