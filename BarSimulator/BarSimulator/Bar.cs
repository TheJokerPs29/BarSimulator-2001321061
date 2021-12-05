using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BarSimulator
{
    class Bar
    {
        List<Student> students = new List<Student>();
        Semaphore semaphore = new Semaphore(10, 10);
        List<Drink> menu = new List<Drink>();
        public List<Drink> snapshotMenu { get; set; }


        public void SetMenu(List<Drink> menu)
        {
            this.menu = menu;
            this.snapshotMenu = menu.Select(x => new Drink(x.name, x.price, x.quantity)).ToList();
        }

        public List<Drink> GiveMenu()
        {
            return menu;
        }

        public Drink GiveDrink(string name, int count=1)
        {
            Drink selectedDrink = this.menu.First(x => x.name == name);

            try
            {
                selectedDrink.Give(count);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }


            return selectedDrink;
        }

        public void Enter(Student student)
        {
            semaphore.WaitOne();
            lock (students)
            {
                if (student.age < 18)
                {
                    Console.WriteLine($"Student: {student.Name} is under age");
                }
                else
                {
                    students.Add(student);
                }
            }
        }

        public void Leave(Student student)
        {
            lock (students)
            {
                students.Remove(student);
            }
            semaphore.Release();
        }

        public void Report()
        {
            foreach (var drink in this.menu)
            {
                Drink snapshotedDrink = this.snapshotMenu.First(x => x.name == drink.name);

                Console.WriteLine($"Drink {drink.name} sold {snapshotedDrink.quantity - drink.quantity} drinks in stock: {drink.quantity}");
            }
        }
    }   
}
    