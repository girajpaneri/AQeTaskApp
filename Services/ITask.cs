using DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ITask
    {
        IEnumerable<TaskDto> GetAllTasks();
        TaskDto GetTaskById(int id);
        void AddTask(TaskDto taskDto);
        void UpdateTask(TaskDto taskDto);
        void DeleteTask(int id);

    }
}
