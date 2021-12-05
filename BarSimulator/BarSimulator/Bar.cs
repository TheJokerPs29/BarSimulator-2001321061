﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BarSimulator
{
    class Bar
    {
        List<Student> students = new List<Student>();
        Semaphore semaphore = new Semaphore(10, 10);

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