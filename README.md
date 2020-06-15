# SOLID (C#)

## Single Responsibility Principle (SRP) 
Each module has one and only one reason to change. If any class has more than one responsibility then it has more than one reason to change. Classes having more than one responsibility should be broken into smaller classes.

### Code

```
public class Employee 
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public decimal Salary { get; set; }

        public void Add(Employee employee)
        {
            /// Implementation to add Employee to database
        }
    }
```

### Problem (SRP Violation)
We can see in code that `Employee` class is dealing with two responsibilities, one is business logic and the other is data access logic for the entity.

### SRP Compliant

```
public class Employee 
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public decimal Salary { get; set; }
    
    }

 public class EmployeeRepository
    {        
        public void Add(Employee employee)
        {
            /// Implementation to add Employee to database
        }
    }

```
Now, responsibilities are divided into different classes.

## Open Closed Principle (OCP)
Software entities(classes, modules, functions) are open for the extension which means we can extend the entity fir new behavior and features to satisfy new requirements while closed for modification.  This principle can be useful when the library is written which has many clients.

### Code

```
   public class Package
    {
        public void AssignPackagePlan(Customer customer)
        {
            if (customer.PurchasedPlan == "Free")
            {
                // Implement logic and privileges for free account
            }            
            else if (customer.PurchasedPlan == "Gold")
            {
                // Implement logic and privileges for Gold account
            }
        }
    }
```

### Problem (OCP Violation)
We can see in the above code that if we introduce a new package for example `Platinium` then we have to modify the source code which is not encouraged in OCP.

### OCP Compliant

```
public interface  IPackagePlan
    {
        void Plan();
    }

    public class FreePlan : IPackagePlan
    {
        public void Plan()
        {
            throw new NotImplementedException();
        }
    }
public class GoldPlan : IPackagePlan
    {
        public void Plan()
        {
            throw new NotImplementedException();
        }
    }
```

Now, if we add new packages, it won't affect the existing package classes.

## Liskov Substitution Principle (LSP)
The collection of guidelines to create inheritance hierarchies which make sure that the client can use any class or subclass without compromising expected behavior.

### Code

```
public class Employee 
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public decimal Salary { get; set; }

        public void Add(Employee employee)
        {
            /// Implementation to add Employee to database
        }

       
    }

     public class Customer :Employee
    {
       
        public string PurchasedPlan { get; set; }
        

    }
```

### Problem (LSP Violation)
The above simple example is easy to understand, `Salary` does not belong to `Customer`. LSP state that child class should not break the type of definition or behavior.

### LSP Compliant

```
    public class Person
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }

      public class Employee:Person
    {
        public decimal Salary { get; set; }
    }

     public class Customer:Person
    {
        public string PurchasedPlan { get; set; }
    }
```
By adding `Person` class, we can see the correct inherited hierarchy.

## Interface Segregation Principle (ISP)
ISP states that the interfaces should be smaller so classes cannot be forced to use interface which has irrelevant methods.

### Code

```
public interface IBase
    {
        void Add(Employee employee);

        void Plan();

    }

        public class Employee :IBase
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public decimal Salary { get; set; }

        public void Add(Employee employee)
        {
            /// Implementation to add Employee to database
        }

        public void Plan()
        {
            throw new NotImplementedException();
        }
    }
```

### Problem (ISP Violation)
The problem in the above code is, we are forced to implement `Plan` method.

### ISP Compliant

```
public interface  IPackagePlan
    {
        void Plan();
    }

public interface IEmployeeRepository
    {
        void Add(Employee employee);

    }

public class EmployeeRepository : IEmployeeRepository
    {
        public EmployeeRepository()
        {
            // Constructor to initilate connection string, etc
        }

        public void Add(Employee employee)
        {
            /// Implementation to add Employee to database
        }
    }
```
We have split `IBase` interface into two interfaces. Thus, classes are not implementing methods that are not part of business logic.

## Dependency Inversion Principle (DIP)
DIP focus on writing loosely coupled code because it is easy to maintain when the application grows bigger. This can be achieved by depending on abstraction rather than implementation.
### Code

```
    public class Person
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }

  public class Employee:Person
    {
        public decimal Salary { get; set; }
    }

        public class EmployeeRepository
    {
        public EmployeeRepository()
        {
            // Constructor to initilate connection string, etc
        }

        public void Add(Employee employee)
        {
            /// Implementation to add Employee to database
        }
    }
public class EmployeeService
    {
        private EmployeeRepository _employeeRepository = new EmployeeRepository();
        

        public void Add(Employee employee)
        {
            _employeeRepository.Add(employee);
        }
    }

```

### Problem (DIP Violation)
We can see in the above code that `EmployeeService` class depends on `EmployeeRepository` and `EmployeeService` has initiated `EmployeeRepository` as well. DIP states that classes should depend on abstraction not implementatoin.

### DIP Compliant

```
    public class Person
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }

  public class Employee:Person
    {
        public decimal Salary { get; set; }
    }

   public interface IEmployeeRepository
    {
        void Add(Employee employee);

    }
        public class EmployeeRepository : IEmployeeRepository
    {
        public EmployeeRepository()
        {
            // Constructor to initilate connection string, etc
        }

        public void Add(Employee employee)
        {
            /// Implementation to add Employee to database
        }
    }

    public class EmployeeService
    {
        private IEmployeeRepository _employeeRepository { get; }
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
                

        public void Add(Employee employee)
        {
            _employeeRepository.Add(employee);
        }
    }
```
We can see now that EmployeeService depends on abstraction instead of implementation.
