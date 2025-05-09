using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Diagnostics;

public class Teacher
{
    public string Email { get; }
    public string Name { get; }
    public int Age { get; }
    public string[] Students { get; }

    public Teacher(string email, string name, int age, string[] students)
    {
        Email = email;
        Name = name;
        Age = age;
        Students = students;
    }
}

public class Main
{
    public Teacher CreateTeacher(string email, string name, int age, string[] students)
    {
        List<string> errors = new();

        if (!Regex.IsMatch(email, @"^[a-zA-Z]+@[a-zA-Z]+\.[a-zA-Z]{2,}$"))
            errors.Add($"Неверный email: {email}");

        if (!Regex.IsMatch(name, @"^[А-Яа-яЁё\s]+$"))
            errors.Add($"Неверное имя: {name}");

        if (age <= 0 || age > 120)
            errors.Add($"Некорректный возраст: {age}");

        if (errors.Count > 0)
        {
            LogErrors(errors, email, name, age);
            return null;
        }

        return new Teacher(email, name, age, students);
    }

    private void LogErrors(List<string> errors, string email, string name, int age)
    {
        string path = "errors.txt";
        using StreamWriter writer = new(path, append: true);
        writer.WriteLine($"[{DateTime.Now}] Ошибки при создании Teacher:");
        writer.WriteLine($"Введённые данные: email='{email}', name='{name}', age={age}");
        foreach (var error in errors)
            writer.WriteLine($" - {error}");
        writer.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        Creator creator = new();
        string[] students = { "Иванов", "Петров" };

        Teacher teacher = creator.CreateTeacher("ivano.юортv@mail.ru", "Иван Иванов", 35, students);

        if (teacher != null)
            Console.WriteLine("Teacher успешно создан.");
        else
            Console.WriteLine(@"Ошибка при создании Teacher. Смотрите файл 'errors.txt'. Лежит по пути...\bin\Debug\net9.0");
    }
}
