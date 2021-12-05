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


        public void SetMenu(List<Drink> menu)
        {
            this.menu = menu;
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
                students.Add(student);
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
    }
}
