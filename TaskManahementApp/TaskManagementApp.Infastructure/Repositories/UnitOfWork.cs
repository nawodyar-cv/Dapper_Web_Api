using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaanagementApp.Application.Interfaces;

namespace TaskManagementApp.Infastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ITaskRepository taskRepository)
        {
            Tasks = taskRepository;
        }

        public ITaskRepository Tasks { get; }

        public ITaskRepository Task => throw new NotImplementedException();
    }
}
