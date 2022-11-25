namespace FacadeExample;

/// <summary>
/// This is a model class that represents an Employee
/// </summary>
public class Employee
{
    public Employee()
    {
        Id = Random.Shared.Next(1111, 999999);
    }
    public int Id { get; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

/// <summary>
/// This is a model class that represents an Employee
/// </summary>
public class Project
{
    public int EmployeeId { get; set; }
    public string? Department { get; set; }
}

/// <summary>
/// This is a model class that represents a Salary of an employee
/// </summary>
public class Salary
{
    public int EmployeeId { get; set; }
    public decimal Basic { get; set; }
    public decimal Incentives { get; set; }
}

/// <summary>
/// This is a Employee database service that have two method to Add and Update Employee details to the database
/// </summary>
public class EmployeeDbService
{
    public int AddEmployee(Employee employee)
    {
        Console.WriteLine($"[New Employee] - Id# [{employee.Id}] FirstName# {employee.FirstName}  LastName# {employee.LastName} added to the database");
        return employee.Id;
    }

    public void UpdateEmployeeDetails(Employee employee)
    {
        Console.WriteLine($"[Update Employee] - Id# [{employee.Id}] FirstName# {employee.FirstName}  LastName# {employee.LastName} added to the database");
    }
}

/// <summary>
/// This is the Project Database Service that Adds and Updates project details of an employee in the database
/// </summary>
public class ProjectDbService
{
    public void AddProjectDetails(int employeeId)
    {
        Project project = new Project();
        project.EmployeeId = employeeId;
        project.Department = "NO-DEPARTMENT";
        Console.WriteLine($"EmployeeId# [{employeeId}] allocated to [{project.Department}] department");
    }
    public void UpdateProjectDetails(Project project)
    {
        Console.WriteLine($"[Updated] EmployeeId# [{project.EmployeeId}] allocated to [{project.Department}] department");
    }
}

/// <summary>
/// This is the Salary Database Service, that Adds and Update Salary details of an employee in the database
/// </summary>
public class SalaryDbService
{
    public void AddSalaryDetails(int employeeId)
    {
        Salary salary = new Salary();
        salary.EmployeeId = employeeId;
        salary.Basic = 56543;
        salary.Incentives = 45656;
        Console.WriteLine($"Salary for EmployeeId# [{employeeId}] - Basic: {salary.Basic}  Incentives: {salary.Incentives} added to the database");
    }
    public void UpdateSalaryDetails(Salary salary)
    {
        Console.WriteLine($"Salary for EmployeeId# [{salary.EmployeeId}] - Basic: {salary.Basic}  Incentives: {salary.Incentives} updated to the database");
    }
}

/// <summary>
/// This is the Facade class that combines all the three database services and provides a
/// APIs AddNewEmployee and UpdateEmployee as per Client's requirement and hide the implementation details from the client
/// that it is calling all the related service to fulfill the client's requirement
/// </summary>
public class EmployeeFacade
{
    private EmployeeDbService employeeDbService;
    private ProjectDbService projectDbService;
    private SalaryDbService salaryDbService;

    public EmployeeFacade()
    {
        employeeDbService = new EmployeeDbService();
        projectDbService = new ProjectDbService();
        salaryDbService = new SalaryDbService();
    }

    public int AddNewEmployee(Employee employee)
    {
        var employeeId = employeeDbService.AddEmployee(employee);
        salaryDbService.AddSalaryDetails(employeeId);
        projectDbService.AddProjectDetails(employeeId);
        return employeeId;
    }

    public void UpdateEmployee(Employee employee, Salary salary, Project project)
    {
        employeeDbService.UpdateEmployeeDetails(employee);
        salaryDbService.UpdateSalaryDetails(salary);
        projectDbService.UpdateProjectDetails(project);
    }
}

/// <summary>
/// Here Client works only with the EmployeeFacade
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        var facade = new EmployeeFacade();
        var employee = new Employee { FirstName = "John", LastName = "Doe" };
        
        facade.AddNewEmployee(employee);
        Console.WriteLine();
        employee.FirstName = "Jonny";
        var updatedSalary = new Salary { EmployeeId = employee.Id, Basic = 999999, Incentives = 22222 };
        var updatedProject = new Project { EmployeeId = employee.Id, Department = "Finance" };
        facade.UpdateEmployee(employee, updatedSalary, updatedProject);
    }
}